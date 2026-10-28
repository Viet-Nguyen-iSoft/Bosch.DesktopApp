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

      if (!AppCore.Ins.IsServerConnected)
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
              var clientUpsertRequest = JsonHelper.FromJson<ClientUpsertRequest>(apiJob.Json ?? string.Empty);
              if (clientUpsertRequest != null)
              {
                var rs = await api.UpsertClientAsync(clientUpsertRequest);
              }  
              break;
            case EnumTypeAPI.MD_WareHouse:
              var warehouseUpsert = JsonHelper.FromJson<WarehouseUpsertRequest>(apiJob.Json ?? string.Empty);
              if (warehouseUpsert != null)
              {
                var rs = await api.UpsertWarehouseAsync(warehouseUpsert);
              }
              break;
            case EnumTypeAPI.MD_TypeGoods:
              var typeGoodsUpsertRequest = JsonHelper.FromJson<ProductUpsertRequest>(apiJob.Json ?? string.Empty);
              if (typeGoodsUpsertRequest != null)
              {
                // Tương thích job Loại hàng cũ chưa có TypeWeight.
                typeGoodsUpsertRequest.TypeWeight = 1;
                await api.UpsertProductAsync(typeGoodsUpsertRequest);
              }
              break;
            case EnumTypeAPI.MD_Tare:
              var categoryTareUpsertRequest = JsonHelper.FromJson<CategoryTareUpsertRequest>(apiJob.Json ?? string.Empty);
              if (categoryTareUpsertRequest != null)
              {
                await api.UpsertCategoryTareAsync(categoryTareUpsertRequest);
              }
              break;
            case EnumTypeAPI.MD_ProductGroup:
              var productGroupUpsertRequest = JsonHelper.FromJson<ProductGroupUpsertRequest>(apiJob.Json ?? string.Empty);
              if (productGroupUpsertRequest != null)
              {
                await api.UpsertProductGroupAsync(productGroupUpsertRequest);
              }
              break;
            case EnumTypeAPI.MD_Product:
              var productUpsertRequest = JsonHelper.FromJson<ProductUpsertRequest>(apiJob.Json ?? string.Empty);
              if (productUpsertRequest != null)
              {
                // Tương thích job Chất thải cũ chưa có TypeWeight.
                if (productUpsertRequest.TypeWeight == 0)
                  productUpsertRequest.TypeWeight = 2;
                await api.UpsertProductAsync(productUpsertRequest);
              }
              break;
            case EnumTypeAPI.MD_Delivery:
              var deliveryUpsertRequest = JsonHelper.FromJson<ClientUpsertRequest>(apiJob.Json ?? string.Empty);
              if (deliveryUpsertRequest != null)
              {
                // Tương thích cả job Delivery cũ chưa có ClientType trong JSON.
                deliveryUpsertRequest.ClientType = 2;
                await api.UpsertClientAsync(deliveryUpsertRequest);
              }
              break;
            case EnumTypeAPI.MD_User:
              var userUpsertRequest = JsonHelper.FromJson<UserUpsertRequest>(apiJob.Json ?? string.Empty);
              if (userUpsertRequest != null)
              {
                // Bỏ qua cả các job ISOFT cũ đã nằm trong hàng đợi.
                if (!PasswordPolicy.IsBuiltInAccount(userUpsertRequest.Username))
                  await api.UpsertUserAsync(userUpsertRequest);
              }
              break;
            case EnumTypeAPI.MD_Document:
              var documentUpsertRequest = JsonHelper.FromJson<DocumentUpsertRequest>(
                apiJob.Json ?? string.Empty);
              if (documentUpsertRequest != null)
              {
                await api.UpsertDocumentAsync(documentUpsertRequest, cancellationToken)
                  .ConfigureAwait(false);
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
          HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
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
          HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
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
