using iSoft.Database.Models;
using iSoft.Database.Service;

namespace ApiSyncData
{
  public static class PeriodicRunner
  {
    public static event EventHandler<MasterDataChangedEventArgs>? EntityChanged;
    private static Func<bool> _serverAvailabilityCheck = static () => false;

    public static Guid? StationId { get; private set; }
    public static int NumberLastDay { get; set; } = 30;
    // Dữ liệu của lần đồng bộ thành công gần nhất; null trước lần đầu thành công.
    public static Resp.StationAPI? Stations { get; private set; }
    public static Resp.WarehouseAPI? Warehouses { get; private set; }
    public static Resp.ProductGroupAPI? ProductGroups { get; private set; }
    public static Resp.ProductAPI? Products { get; private set; }
    public static Resp.CategoryTareAPI? CategoryTares { get; private set; }
    public static Resp.ClientAPI? Clients { get; private set; }
    public static Resp.DeliveryAPI? Deliveries { get; private set; }
    public static Resp.UserAPI? Users { get; private set; }
    public static Resp.DocumentAPI? Documents { get; private set; }
    public static Resp.TemplateAPI? Templates { get; private set; }

    public static void SetServerAvailabilityCheck(Func<bool> serverAvailabilityCheck)
    {
      ArgumentNullException.ThrowIfNull(serverAvailabilityCheck);
      _serverAvailabilityCheck = serverAvailabilityCheck;
    }

    public static bool CanCallApi()
    {
      try
      {
        return _serverAvailabilityCheck();
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// Chạy các API mặc định sau mỗi 5 giây. Giữ Task và hủy token khi cần dừng.
    /// Lỗi được báo qua onError (mặc định ghi Trace), rồi thử lại ở lượt tiếp theo.
    /// Chỉ gọi một lần khi khởi động để tránh tạo nhiều vòng lặp.
    /// </summary>
    public static Task RunEvery5SecondsAsync(
      Guid? stationId,
      int numberLastDay,
      CancellationToken cancellationToken = default,
      Action<Exception>? onError = null)
    {
      StationId = stationId;
      NumberLastDay = numberLastDay;
      var api = new ApiService();
      return RunEvery5SecondsAsync(
        token => CanCallApi()
          ? RunDefaultFunctionsAsync(api, token)
          : Task.CompletedTask,
        cancellationToken,
        onError ?? (ex => System.Diagnostics.Trace.TraceError(ex.ToString())));
    }

    // Thêm hoặc thay đổi các hàm chạy định kỳ tại đây.
    private static async Task RunDefaultFunctionsAsync(
      ApiService api, CancellationToken cancellationToken)
    {
      cancellationToken.ThrowIfCancellationRequested();

      var stations = LoadAndSyncAsync(api.Station(), MasterDataSyncService.SyncStationsAsync,
        value => Stations = value, cancellationToken);
      var warehouses = LoadAndSyncAsync(api.Warehouse(), MasterDataSyncService.SyncWarehousesAsync,
        value => Warehouses = value, cancellationToken);
      var productGroups = LoadAndSyncAsync(api.ProductGroup(), MasterDataSyncService.SyncProductGroupsAsync,
        value => ProductGroups = value, cancellationToken);
      var products = SyncProductsAfterGroupsAsync(
        api.Product(isContainDelete: true), productGroups, cancellationToken);
      var categoryTares = LoadAndSyncAsync(
        api.CategoryTare(isContainDelete: true, cancellationToken),
        MasterDataSyncService.SyncCategoryTaresAsync,
        value => CategoryTares = value, cancellationToken);

      var clientDeliveries = SyncClientDeliveriesAsync(api, cancellationToken);

      var users = LoadAndSyncAsync(api.Users(), MasterDataSyncService.SyncUsersAsync,
        value => Users = value, cancellationToken);

      var templates = SyncTemplatesAsync(api, cancellationToken);

      var dtNow = DateTime.UtcNow.Date;
      var dateTo = dtNow.AddYears(10);
      var dateFrom = dtNow.AddYears(-10);
      var documents = LoadAndSyncAsync(
        api.Documents(dateFrom, dateTo, isContainDelete: true, cancellationToken),
        MasterDataSyncService.SyncDocumentsAsync,
        value => Documents = value,
        cancellationToken);

      await Task.WhenAll(stations, warehouses, productGroups,
        products, categoryTares, clientDeliveries, users, templates, documents)
        .ConfigureAwait(false);

      try
      {
        if (StationId != null)
        {
          var recordTrucks = await api.RecordTruckFromServer(StationId, NumberLastDay)
            .ConfigureAwait(false);
          var recordTruckChanges = await RecordTruckServerSyncService.SyncAsync(
            recordTrucks,
            cancellationToken).ConfigureAwait(false);
          if (recordTruckChanges != null)
            NotifyEntityChanged(recordTruckChanges);
        }
      }
      catch (Exception ex)
      {

      }
    }

    private static async Task LoadAndSyncAsync<T>(Task<T> request,
      Func<T, CancellationToken, Task<MasterDataChangedEventArgs?>> sync,
      Action<T> updateCache, CancellationToken token)
    {
      var response = await request.ConfigureAwait(false);
      token.ThrowIfCancellationRequested();
      var changes = await sync(response, token).ConfigureAwait(false);
      updateCache(response);
      if (changes != null)
        NotifyEntityChanged(changes);
    }

    private static void NotifyEntityChanged(MasterDataChangedEventArgs changes)
    {
      var subscribers = EntityChanged;
      if (subscribers == null)
        return;

      foreach (EventHandler<MasterDataChangedEventArgs> subscriber in subscribers.GetInvocationList())
      {
        try
        {
          subscriber(null, changes);
        }
        catch (Exception ex)
        {
          System.Diagnostics.Trace.TraceError(ex.ToString());
        }
      }
    }

    private static async Task SyncClientDeliveriesAsync(
      ApiService api,
      CancellationToken token)
    {
      var clients = await api.Client(isContainDelete: false, token)
        .ConfigureAwait(false);
      token.ThrowIfCancellationRequested();

      var clientChanges = await MasterDataSyncService.SyncClientsAsync(clients, token)
        .ConfigureAwait(false);
      Clients = clients;
      if (clientChanges != null)
        NotifyEntityChanged(clientChanges);

      bool hasUnifiedDeliveries = clients.Data?.ListData?.Any(row =>
        row.ClientType == (int)iSoft.Database.EnumData.EnumTypeDataLocation.Good) == true;
      if (hasUnifiedDeliveries)
      {
        Deliveries = null;
        return;
      }

      // Tương thích server cũ: Client chỉ trả loại 1, nhóm loại 2 vẫn lấy
      // từ endpoint ClientGoods như trước đây.
      var deliveries = await api.Delivery(isContainDelete: false, token)
        .ConfigureAwait(false);
      token.ThrowIfCancellationRequested();
      var deliveryChanges = await MasterDataSyncService
        .SyncDeliveriesAsync(deliveries, token).ConfigureAwait(false);
      Deliveries = deliveries;
      if (deliveryChanges != null)
        NotifyEntityChanged(deliveryChanges);
    }

    private static async Task SyncTemplatesAsync(
      ApiService api,
      CancellationToken token)
    {
      var response = await api.Templates(token).ConfigureAwait(false);
      token.ThrowIfCancellationRequested();

      var rows = response.Data?.ListData;
      ServerSnapshot.Validate(rows, response.Data?.TotalRecord);

      var templateService = new TemplateService();
      var locals = await templateService.GetAllAsync(isContainDelete: true)
        .ConfigureAwait(false);
      var localsById = locals.ToDictionary(template => template.Id);
      var rowsToDownload = rows!
        .Where(row => TemplateNeedsDownload(
          row,
          localsById.GetValueOrDefault(row.Id!.Value)))
        .ToList();

      string destinationFolder = Path.Combine(
        AppContext.BaseDirectory,
        "TemplateFile");
      var downloads = new List<(Resp.ListDatumTemplate Row, string Path, bool Exists)>();
      foreach (var row in rowsToDownload)
      {
        token.ThrowIfCancellationRequested();
        bool exists = localsById.ContainsKey(row.Id!.Value);
        string path = await api.DownloadTemplateAsync(
          row.Id.Value,
          row.Name,
          row.TemplateFile,
          destinationFolder,
          token).ConfigureAwait(false);
        downloads.Add((row, path, exists));
      }

      // Cập nhật đường dẫn cho bản ghi đã có trước khi ghi nhận snapshot mới.
      // Nếu bước này lỗi, UpdatedAt cũ vẫn còn để lượt sau tải lại.
      foreach (var download in downloads.Where(item => item.Exists))
      {
        await templateService.UpdatePathLocalAsync(
          download.Row.Id!.Value,
          download.Path).ConfigureAwait(false);
      }

      var changes = await MasterDataSyncService.SyncTemplatesAsync(response, token)
        .ConfigureAwait(false);

      // Bản ghi mới chỉ tồn tại sau khi snapshot đã được insert.
      foreach (var download in downloads.Where(item => !item.Exists))
      {
        await templateService.UpdatePathLocalAsync(
          download.Row.Id!.Value,
          download.Path).ConfigureAwait(false);
      }

      Templates = response;
      if (changes != null)
      {
        NotifyEntityChanged(changes);
      }
      else if (downloads.Count > 0)
      {
        var downloadedIds = downloads
          .Select(item => item.Row.Id!.Value)
          .Distinct()
          .ToList();
        NotifyEntityChanged(new MasterDataChangedEventArgs(
          typeof(Template),
          Array.Empty<Guid>(),
          downloadedIds,
          Array.Empty<Guid>(),
          downloadedIds.Count));
      }
    }

    private static bool TemplateNeedsDownload(
      Resp.ListDatumTemplate source,
      Template? local)
    {
      if (local == null || string.IsNullOrWhiteSpace(local.PathLocal) ||
          !File.Exists(local.PathLocal))
      {
        return true;
      }

      string expectedFileName = ApiService.ResolveDownloadFileName(
        source.Name,
        responseFileName: null,
        serverFileName: source.TemplateFile,
        id: source.Id!.Value);
      if (!string.Equals(
        Path.GetFileName(local.PathLocal),
        expectedFileName,
        StringComparison.OrdinalIgnoreCase))
      {
        return true;
      }

      if (!string.Equals(
        local.TemplateFile,
        source.TemplateFile,
        StringComparison.Ordinal))
      {
        return true;
      }

      if (local.UpdatedAt.HasValue != source.UpdatedAt.HasValue)
        return true;
      if (!local.UpdatedAt.HasValue)
        return false;

      DateTime localUpdatedAt = ToUtc(local.UpdatedAt.Value);
      DateTime serverUpdatedAt = ToUtc(source.UpdatedAt!.Value);
      return Math.Abs((localUpdatedAt - serverUpdatedAt).TotalMilliseconds) >= 1;
    }

    private static DateTime ToUtc(DateTime value) => value.Kind switch
    {
      DateTimeKind.Utc => value,
      DateTimeKind.Local => value.ToUniversalTime(),
      _ => DateTime.SpecifyKind(value, DateTimeKind.Utc),
    };

    private static async Task SyncProductsAfterGroupsAsync(
      Task<Resp.ProductAPI> request, Task groupsSync, CancellationToken token)
    {
      // Tải song song, nhưng chỉ ghi Product khi nhóm đã lưu thành công.
      await Task.WhenAll(request, groupsSync).ConfigureAwait(false);
      await LoadAndSyncAsync(request, MasterDataSyncService.SyncProductsAsync,
        value => Products = value, token).ConfigureAwait(false);
    }

    public static async Task RunEvery5SecondsAsync(
      Func<CancellationToken, Task> callback,
      CancellationToken cancellationToken,
      Action<Exception>? onError = null)
    {
      ArgumentNullException.ThrowIfNull(callback);

      using var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
      try
      {
        while (await timer.WaitForNextTickAsync(cancellationToken).ConfigureAwait(false))
        {
          cancellationToken.ThrowIfCancellationRequested();
          try
          {
            await callback(cancellationToken).ConfigureAwait(false);
          }
          catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
          {
            return;
          }
          catch (Exception ex)
          {
            if (onError == null)
              throw;

            onError(ex);
          }
        }
      }
      catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
      {
        // Hủy chủ động kết thúc vòng lặp bình thường.
      }
    }
  }
}
