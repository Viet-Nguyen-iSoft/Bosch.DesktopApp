namespace ApiSyncData
{
  public static class PeriodicRunner
  {
    public static event EventHandler<MasterDataChangedEventArgs>? EntityChanged;

    // Dữ liệu của lần đồng bộ thành công gần nhất; null trước lần đầu thành công.
    public static Resp.StationAPI? Stations { get; private set; }
    public static Resp.WarehouseAPI? Warehouses { get; private set; }
    public static Resp.TypeGoodsAPI? TypeGoods { get; private set; }
    public static Resp.ProductGroupAPI? ProductGroups { get; private set; }
    public static Resp.ProductAPI? Products { get; private set; }
    public static Resp.CategoryTareAPI? CategoryTares { get; private set; }
    public static Resp.ClientAPI? Clients { get; private set; }
    public static Resp.UserAPI? Users { get; private set; }

    /// <summary>
    /// Chạy các API mặc định sau mỗi 5 giây. Giữ Task và hủy token khi cần dừng.
    /// Lỗi được báo qua onError (mặc định ghi Trace), rồi thử lại ở lượt tiếp theo.
    /// Chỉ gọi một lần khi khởi động để tránh tạo nhiều vòng lặp.
    /// </summary>
    public static Task RunEvery5SecondsAsync(
      CancellationToken cancellationToken = default,
      Action<Exception>? onError = null)
    {
      var api = new ApiService();
      return RunEvery5SecondsAsync(
        token => RunDefaultFunctionsAsync(api, token),
        cancellationToken,
        onError ?? (ex => System.Diagnostics.Trace.TraceError(ex.ToString())));
    }

    // Thêm hoặc thay đổi các hàm chạy định kỳ tại đây.
    private static async Task RunDefaultFunctionsAsync(
      ApiService api, CancellationToken cancellationToken)
    {
      try
      {
        cancellationToken.ThrowIfCancellationRequested();

        var stations = LoadAndSyncAsync(api.Station(), MasterDataSyncService.SyncStationsAsync,
          value => Stations = value, cancellationToken);
        var warehouses = LoadAndSyncAsync(api.Warehouse(), MasterDataSyncService.SyncWarehousesAsync,
          value => Warehouses = value, cancellationToken);
        var typeGoods = LoadAndSyncAsync(api.TypeGoods(), MasterDataSyncService.SyncTypeGoodsAsync,
          value => TypeGoods = value, cancellationToken);
        var productGroups = LoadAndSyncAsync(api.ProductGroup(), MasterDataSyncService.SyncProductGroupsAsync,
          value => ProductGroups = value, cancellationToken);
        var products = SyncProductsAfterGroupsAsync(api.Product(), productGroups, cancellationToken);
        var categoryTares = LoadAndSyncAsync(api.CategoryTare(), MasterDataSyncService.SyncCategoryTaresAsync,
          value => CategoryTares = value, cancellationToken);

        var clients = LoadAndSyncAsync(api.Client(), MasterDataSyncService.SyncClientsAsync,
          value => Clients = value, cancellationToken);


        await Task.WhenAll(stations, warehouses, typeGoods, productGroups,
          products, categoryTares, clients).ConfigureAwait(false);
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
