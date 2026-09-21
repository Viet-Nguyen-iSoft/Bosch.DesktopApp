using ApiSyncData.Record;
using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ApiSyncData
{
  public static class LocalDataSyncService
  {
    private static readonly SemaphoreSlim SyncLock = new(1, 1);

    public static int LastSynchronizedCount { get; private set; }
    public static string? PathFolderSrc { get; private set; }

    /// <summary>
    /// Đồng bộ RecordTruck và RecordWeight local chưa được gửi lên server sau mỗi 5 giây.
    /// Bản ghi lỗi sẽ giữ SyncFlag = false để được thử lại ở chu kỳ tiếp theo.
    /// </summary>
    public static Task RunEvery5SecondsAsync(
      CancellationToken cancellationToken = default, string? pathFolderSrc = null,
      Action<Exception>? onError = null)
    {
      PathFolderSrc = pathFolderSrc;
      var api = new ApiService();
      return PeriodicRunner.RunEvery5SecondsAsync(
        async token =>
        {
          LastSynchronizedCount = await SyncOnceAsync(api, token).ConfigureAwait(false);
        },
        cancellationToken,
        onError ?? (ex => System.Diagnostics.Trace.TraceError(ex.ToString())));
    }

    /// <summary>
    /// Chạy ngay một lượt đồng bộ và trả về số bản ghi được đánh dấu thành công.
    /// </summary>
    public static async Task<int> SyncOnceAsync(
      ApiService api,
      CancellationToken cancellationToken = default)
    {
      ArgumentNullException.ThrowIfNull(api);

      await SyncLock.WaitAsync(cancellationToken).ConfigureAwait(false);
      try
      {
        var errors = new List<Exception>();
        var synchronizedCount = await SyncRecordTrucksAsync(
          api,
          errors,
          cancellationToken).ConfigureAwait(false);

        synchronizedCount += await SyncRecordWeightsAsync(
          api,
          errors,
          cancellationToken).ConfigureAwait(false);

        if (errors.Count > 0)
          throw new AggregateException("Một hoặc nhiều bản ghi local đồng bộ thất bại.", errors);

        return synchronizedCount;
      }
      finally
      {
        SyncLock.Release();
      }
    }

    private static async Task<int> SyncRecordTrucksAsync(
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
          var payload = Map(record);
          List<RecordTruckSync> recordTruckSyncs = new List<RecordTruckSync>();
          recordTruckSyncs.Add(payload);
          var rawData = JsonConvert.SerializeObject(recordTruckSyncs);
          await api.SyncRecordTruck(rawData, cancellationToken).ConfigureAwait(false);

          if (await MarkAsSynchronizedAsync(
            record.Id,
            payload.Id,
            record.UpdatedAt,
            cancellationToken).ConfigureAwait(false))
          {
            synchronizedCount++;
          }

          //Đồng bộ pdf
          string pathPdf = Path.Combine(PathFolderSrc + "Report", $"{record.Id.ToString().Replace("-", "").Replace(" ", "")}.pdf");
          await (new ApiService()).UploadReportTruckPdf(record.Id, pathPdf);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
          throw;
        }
        catch (Exception ex)
        {
          errors.Add(new InvalidOperationException(
            $"Không thể đồng bộ RecordTruck local Id {record.Id}.", ex));
        }
      }

      return synchronizedCount;
    }

    private static async Task<int> SyncRecordWeightsAsync(
      ApiService api,
      List<Exception> errors,
      CancellationToken cancellationToken)
    {
      var pendingRecords = await LoadPendingRecordWeightsAsync(cancellationToken)
        .ConfigureAwait(false);
      var synchronizedCount = 0;

      foreach (var record in pendingRecords)
      {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
          var payload = Map(record);
          List<RecordWeightSync> recordWeightSyncs = new List<RecordWeightSync>();
          recordWeightSyncs.Add(payload);
          var rawData = JsonConvert.SerializeObject(recordWeightSyncs);
          await api.SyncRecordWeight(rawData, cancellationToken).ConfigureAwait(false);

          if (await MarkRecordWeightAsSynchronizedAsync(
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
            $"Không thể đồng bộ RecordWeight local Id {record.Id}.", ex));
        }
      }

      return synchronizedCount;
    }

    private static async Task<List<RecordTruck>> LoadPendingRecordsAsync(
      CancellationToken cancellationToken)
    {
      await using var db = new MySqlDbContext();
      var records = await db.Set<RecordTruck>()
        .Where(record => !record.SyncFlag)
        .Include(record => record.Client)
        .Include(record => record.TypeGoods)
        .Include(record => record.Warehouse)
        .Include(record => record.Station)
        .OrderBy(record => record.CreatedAt)
        .ThenBy(record => record.Id)
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false);

      var hasNewSourceIds = false;
      foreach (var record in records.Where(record =>
        !record.IdSrc.HasValue || record.IdSrc == Guid.Empty))
      {
        record.IdSrc = Guid.NewGuid();
        hasNewSourceIds = true;
      }

      // Lưu GUID trước khi gọi API để các lần retry luôn dùng cùng một Id.
      if (hasNewSourceIds)
        await db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

      return records;
    }

    private static async Task<List<RecordWeight>> LoadPendingRecordWeightsAsync(
      CancellationToken cancellationToken)
    {
      await using var db = new MySqlDbContext();
      return await db.Set<RecordWeight>()
        .Where(record => !record.SyncFlag &&
          record.RecordTruckId.HasValue)
        .Include(record => record.Station)
        .Include(record => record.User)
        .Include(record => record.Product)
        .Include(record => record.CategoryTare)
        .Include(record => record.RecordTruck)
        .OrderBy(record => record.CreatedAt)
        .ThenBy(record => record.Id)
        .ToListAsync(cancellationToken)
        .ConfigureAwait(false);
    }

    private static RecordTruckSync Map(RecordTruck record)
    {
      return new RecordTruckSync
      {
        Id = record.Id,
        NoLabelAuto = record.NoLabelAuto,
        NoLabelManual = record.NoLabelManual,
        NetTime01 = record.NetTime01,
        NetTime02 = record.NetTime02,
        EnumTypeDataTruck = record.EnumTypeDataTruck,
        NameDriver = record.NameDriver,
        IdCard = record.IdCard,
        LicensePlate = record.LicensePlate,
        Document = record.Document,
        ReasonDelete = record.ReasonDelete,
        ClientId = GetSourceId(
          nameof(RecordTruck),
          record.Id,
          record.ClientId,
          record.Client,
          nameof(record.Client)),
        TypeGoodsId = GetSourceId(
          nameof(RecordTruck),
          record.Id,
          record.TypeGoodsId,
          record.TypeGoods,
          nameof(record.TypeGoods)),
        WarehouseId = GetSourceId(
          nameof(RecordTruck),
          record.Id,
          record.WarehouseId,
          record.Warehouse,
          nameof(record.Warehouse)),
        StationId = GetSourceId(
          nameof(RecordTruck),
          record.Id,
          record.StationId,
          record.Station,
          nameof(record.Warehouse)),
        CreatedAt = record.CreatedAt,
        UpdatedAt = record.UpdatedAt,
        DeletedFlag = record.DeletedFlag,
        WeighInAt = record.WeighInAt,
        WeighOutAt = record.WeighOutAt,
      };
    }

    private static RecordWeightSync Map(RecordWeight record)
    {
      return new RecordWeightSync
      {
        Id = record.Id,
        Net = record.Net,
        Tare = record.Tare,
        StationId = GetSourceId(
          nameof(RecordWeight),
          record.Id,
          record.StationId,
          record.Station,
          nameof(record.Station)),
        //EmployeeId = GetSourceId(
        //  nameof(RecordWeight),
        //  record.Id,
        //  record.EmployeeId,
        //  record.Employee,
        //  nameof(record.Employee)),
        ProductId = GetSourceId(
          nameof(RecordWeight),
          record.Id,
          record.ProductId,
          record.Product,
          nameof(record.Product)),
        CategoryTareId = GetSourceId(
          nameof(RecordWeight),
          record.Id,
          record.CategoryTareId,
          record.CategoryTare,
          nameof(record.CategoryTare)),
        RecordTruckId = record.RecordTruckId,
        CreatedAt = record.CreatedAt,
        UpdatedAt = record.UpdatedAt
      };
    }

    private static Guid? GetSourceId(
      string entityName,
      Guid localId,
      Guid? localForeignKey,
      BaseModel? relatedEntity,
      string relationName)
    {
      if (!localForeignKey.HasValue)
        return null;

      if (relatedEntity?.IdSrc is Guid sourceId && sourceId != Guid.Empty)
        return sourceId;

      throw new InvalidOperationException(
        $"{entityName} local Id {localId}: {relationName} chưa có IdSrc để đồng bộ.");
    }

    private static async Task<bool> MarkAsSynchronizedAsync(
      Guid localId,
      Guid sourceId,
      DateTime? expectedUpdatedAt,
      CancellationToken cancellationToken)
    {
      await using var db = new MySqlDbContext();
      var current = await db.Set<RecordTruck>()
        .SingleOrDefaultAsync(record => record.Id == localId, cancellationToken)
        .ConfigureAwait(false);

      //if (current == null || current.SyncFlag || current.IdSrc != sourceId)
      if (current == null || current.SyncFlag)
          return false;

      // Nếu dữ liệu thay đổi trong khi request đang chạy, giữ trạng thái chưa đồng bộ.
      if (current.UpdatedAt != expectedUpdatedAt)
        return false;

      current.SyncFlag = true;
      await db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
      return true;
    }

    private static async Task<bool> MarkRecordWeightAsSynchronizedAsync(
      Guid localId,
      DateTime? expectedUpdatedAt,
      CancellationToken cancellationToken)
    {
      await using var db = new MySqlDbContext();
      var current = await db.Set<RecordWeight>()
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
