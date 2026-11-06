using ApiSyncData.Record;
using iSoft.Database.DbContexts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static iSoft.Database.EnumData;
using RecordModel = iSoft.Database.Models.Record;

namespace ApiSyncData
{
  public static class LocalDataSyncService
  {
    private static readonly SemaphoreSlim SyncLock = new(1, 1);

    public static int LastSynchronizedCount { get; private set; }
    
    /// <summary>
    /// Đẩy Record local chưa đồng bộ lên API RecordTruck sau mỗi 5 giây.
    /// Xe tải và cân hàng/phế phẩm dùng chung payload, phân biệt bằng RecordType.
    /// Chiều tải dữ liệu từ các trạm khác vẫn chỉ áp dụng cho xe tải.
    /// Bản ghi lỗi sẽ giữ SyncFlag = false để được thử lại ở chu kỳ tiếp theo.
    /// </summary>
    public static Task RunEvery5SecondsAsync(
      CancellationToken cancellationToken = default,
      Action<Exception>? onError = null)
    {
      var api = new ApiService();
      return PeriodicRunner.RunEvery5SecondsAsync(
        async token =>
        {
          LastSynchronizedCount = await SyncOnceAsync(api, token).ConfigureAwait(false);
        },
        cancellationToken,
        onError ?? (ex =>
        {
          System.Diagnostics.Trace.TraceError(ex.ToString());
          HelperManager.LogHelper.LogErrorToFileLog(
            ex,
            Path.Combine(AppContext.BaseDirectory, "Logs"));
        }));
    }

    /// <summary>
    /// Chạy ngay một lượt đồng bộ và trả về số bản ghi được đánh dấu thành công.
    /// </summary>
    public static async Task<int> SyncOnceAsync(
      ApiService api,
      CancellationToken cancellationToken = default)
    {
      ArgumentNullException.ThrowIfNull(api);

      if (!PeriodicRunner.CanCallApi())
      {
        LastSynchronizedCount = 0;
        return 0;
      }

      await SyncLock.WaitAsync(cancellationToken).ConfigureAwait(false);
      try
      {
        var errors = new List<Exception>();
        var synchronizedCount = await SyncRecordsAsync(
          api, errors, cancellationToken).ConfigureAwait(false);

        if (errors.Count > 0)
          throw new AggregateException("Một hoặc nhiều bản ghi local đồng bộ thất bại.", errors);

        return synchronizedCount;
      }
      finally
      {
        SyncLock.Release();
      }
    }

    private static async Task<int> SyncRecordsAsync(
      ApiService api,
      List<Exception> errors,
      CancellationToken cancellationToken)
    {
      var pendingRecords = await LoadPendingRecordsAsync(cancellationToken)
        .ConfigureAwait(false);
      var synchronizedCount = 0;

      foreach (var record in pendingRecords)
      {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
          await SyncRecordAsync(api, record, cancellationToken)
            .ConfigureAwait(false);

          if (await MarkAsSynchronizedAsync(
            record.Id,
            record.UpdatedAt,
            cancellationToken).ConfigureAwait(false))
          {
            synchronizedCount++;
          }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
          throw;
        }
        catch (Exception ex)
        {
          errors.Add(new InvalidOperationException(
            $"Không thể đồng bộ Record local Id {record.Id}, " +
            $"TypeData={record.TypeData}.", ex));
        }
      }

      return synchronizedCount;
    }

    private static async Task<List<RecordModel>> LoadPendingRecordsAsync(
      CancellationToken cancellationToken)
    {
      await using var db = new MySqlDbContext();
      return await db.Set<RecordModel>()
        .Where(record => !record.SyncFlag &&
          (record.TypeData == EnumTypeDataLocation.Truck ||
           record.TypeData == EnumTypeDataLocation.Good))
        .OrderBy(record => record.CreatedAt)
        .ThenBy(record => record.Id)
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false);
    }

    private static async Task SyncRecordAsync(
      ApiService api,
      RecordModel record,
      CancellationToken cancellationToken)
    {
      if (record.TypeData is not (
          EnumTypeDataLocation.Truck or EnumTypeDataLocation.Good))
      {
        throw new InvalidOperationException(
          $"Không hỗ trợ đồng bộ Record {record.Id} với " +
          $"TypeData={record.TypeData}.");
      }

      var payload = MapRecord(record);
      await api.SyncRecordTruckFromLocal(
        JsonConvert.SerializeObject(new[] { payload }),
        cancellationToken).ConfigureAwait(false);
    }

    private static RecordSync MapRecord(RecordModel record)
    {
      bool isTruck = record.TypeData == EnumTypeDataLocation.Truck;

      return new RecordSync
      {
        Id = record.Id,
        RecordType = record.TypeData,
        NoLabelAuto = record.NoLabelAuto,
        NoLabelManual = record.NoLabelManual,
        NetTime01 = record.NetTime01,
        // Với cân phế phẩm, Tare là giá trị bì thực tế của phiếu.
        Tare01 = isTruck ? record.TareTime01 : record.Tare,
        NetTime02 = record.NetTime02,
        Tare02 = record.TareTime02,
        NumberTare = record.NumberTare,
        EnumTypeDataTruck = isTruck == true ? record.EnumTypeDataTruck : EnumTypeDataTruck.DoneTime02,
        NameDriver = record.NameDriver,
        IdCard = record.IdCard,
        LicensePlate = record.LicensePlate,
        Type = record.Type,
        Document = record.Note,
        ReasonDelete = record.ReasonDelete,
        UserId = record.UserId,
        ClientId = record.ClientId,
        CategoryTareId = record.CategoryTareId,
        ProductionId = record.ProductionId,
        WarehouseId = record.WarehouseId,
        StationId = record.StationId,
        CreatedAt = record.CreatedAt,
        UpdatedAt = record.UpdatedAt,
        DeletedFlag = record.DeletedFlag,
        WeighInAt = record.WeighInAt,
        WeighOutAt = record.WeighOutAt,
      };
    }

    private static async Task<bool> MarkAsSynchronizedAsync(
      Guid localId,
      DateTime? expectedUpdatedAt,
      CancellationToken cancellationToken)
    {
      await using var db = new MySqlDbContext();
      var current = await db.Set<RecordModel>()
        .SingleOrDefaultAsync(record => record.Id == localId, cancellationToken)
        .ConfigureAwait(false);

      if (current == null || current.SyncFlag)
          return false;

      // Nếu dữ liệu thay đổi trong khi request đang chạy, giữ trạng thái chưa đồng bộ.
      if (current.UpdatedAt != expectedUpdatedAt)
        return false;

      current.SyncFlag = true;
      await db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
      return true;
    }

  }
}
