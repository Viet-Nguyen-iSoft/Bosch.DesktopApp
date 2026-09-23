using ApiSyncData;
using ApiSyncData.Req;
using HelperManager;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using static HelperManager.EnumData;

namespace LTP.Truck.Services
{
  public sealed class ApiJobsBackgroundService
  {
    private readonly ApiJobsService _apiJobsService = new();

    public async Task CheckCreatedJobsAsync(
      CancellationToken cancellationToken = default)
    {
      cancellationToken.ThrowIfCancellationRequested();

      var appConfig = AppCore.Ins._appConfig;
      if (appConfig == null || string.IsNullOrWhiteSpace(appConfig.IpServer))
        return;

      var pingTimeout = Math.Clamp(appConfig.TimeoutConnectServer ?? 500, 100, 1000);
      if (!TcpHelper.IsPing(appConfig.IpServer.Trim(), pingTimeout))
        return;

      var apiJobs = await _apiJobsService
        .GetCreatedAsync(cancellationToken)
        .ConfigureAwait(false);

      foreach (var apiJob in apiJobs)
      {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
          var api = new ApiService();
          switch (apiJob.EnumTypeAPI)
          {
            case EnumTypeAPI.Plate:
              var licensePlate = JsonHelper.FromJson<LicensePlateUpsertRequest>(apiJob.Json ?? string.Empty);
              if (licensePlate != null)
              {
                await api.UpsertLicensePlateAsync(licensePlate);
              }  
              break;
            case EnumTypeAPI.MD_Client:
              var clientUpsertRequest = JsonHelper.FromJson<ClientUpsertRequest>(apiJob?.Json??string.Empty);
              if (clientUpsertRequest != null)
              {
                var rs = await api.UpsertClientAsync(clientUpsertRequest);
              }  
              break;
            case EnumTypeAPI.MD_WareHouse:
              var warehouseUpsert = JsonHelper.FromJson<WarehouseUpsertRequest>(apiJob?.Json ?? string.Empty);
              if (warehouseUpsert != null)
              {
                var rs = await api.UpsertWarehouseAsync(warehouseUpsert);
              }
              break;
          }

          apiJob.EnumStatusAPI = EnumStatusAPI.Success;
          apiJob.UpdatedAt = DateTime.UtcNow;
          await _apiJobsService.AddOrUpdateAsync(apiJob).ConfigureAwait(false);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
          throw;
        }
        catch (Exception ex)
        {
          apiJob.EnumStatusAPI = EnumStatusAPI.Fail;
          apiJob.Retry = (apiJob.Retry ?? 0) + 1;
          apiJob.Description = ex.Message;
          await _apiJobsService.AddOrUpdateAsync(apiJob).ConfigureAwait(false);
        }
      }
    }

    public async Task RunAsync(
      CancellationToken cancellationToken = default,
      Action<Exception>? onError = null,
      TimeSpan? interval = null)
    {
      var delay = interval ?? TimeSpan.FromSeconds(3);

      while (!cancellationToken.IsCancellationRequested)
      {
        try
        {
          await CheckCreatedJobsAsync(cancellationToken).ConfigureAwait(false);
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

        try
        {
          await Task.Delay(delay, cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
          return;
        }
      }
    }
  }
}
