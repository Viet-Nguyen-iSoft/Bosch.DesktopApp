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
    private readonly ApiService _apiService = new();
    private readonly SemaphoreSlim _processingLock = new(1, 1);

    public async Task CheckCreatedJobsAsync(
      CancellationToken cancellationToken = default)
    {
      cancellationToken.ThrowIfCancellationRequested();

      if (!AppCore.Ins.IsServerConnected)
        return;

      await _processingLock.WaitAsync(cancellationToken).ConfigureAwait(false);
      try
      {
        var apiJobs = await _apiJobsService
          .GetCreatedAsync(cancellationToken)
          .ConfigureAwait(false);

        foreach (var apiJob in apiJobs)
        {
          cancellationToken.ThrowIfCancellationRequested();

          try
          {
            switch (apiJob.EnumTypeAPI)
            {
            case EnumTypeAPI.Plate:
              var licensePlate = DeserializeRequired<LicensePlateUpsertRequest>(apiJob);
              await _apiService.UpsertLicensePlateAsync(
                licensePlate,
                cancellationToken: cancellationToken).ConfigureAwait(false);
              break;
            case EnumTypeAPI.MD_Client:
              var clientUpsertRequest = DeserializeRequired<ClientUpsertRequest>(apiJob);
              await _apiService.UpsertClientAsync(
                clientUpsertRequest,
                cancellationToken: cancellationToken).ConfigureAwait(false);
              break;
            case EnumTypeAPI.MD_WareHouse:
              var warehouseUpsert = DeserializeRequired<WarehouseUpsertRequest>(apiJob);
              await _apiService.UpsertWarehouseAsync(
                warehouseUpsert,
                cancellationToken: cancellationToken).ConfigureAwait(false);
              break;
            case EnumTypeAPI.MD_TypeGoods:
              var typeGoodsUpsertRequest = DeserializeRequired<ProductUpsertRequest>(apiJob);
              // Tương thích job Loại hàng cũ chưa có TypeWeight.
              typeGoodsUpsertRequest.TypeWeight = 1;
              if (typeGoodsUpsertRequest.WasteType != 0)
                await _apiService.UpsertProductAsync(
                  typeGoodsUpsertRequest,
                  cancellationToken: cancellationToken).ConfigureAwait(false);
              break;
            case EnumTypeAPI.MD_Tare:
              var categoryTareUpsertRequest = DeserializeRequired<CategoryTareUpsertRequest>(apiJob);
              await _apiService.UpsertCategoryTareAsync(
                categoryTareUpsertRequest,
                cancellationToken: cancellationToken).ConfigureAwait(false);
              break;
            case EnumTypeAPI.MD_ProductGroup:
              var productGroupUpsertRequest = DeserializeRequired<ProductGroupUpsertRequest>(apiJob);
              await _apiService.UpsertProductGroupAsync(
                productGroupUpsertRequest,
                cancellationToken: cancellationToken).ConfigureAwait(false);
              break;
            case EnumTypeAPI.MD_Product:
              var productUpsertRequest = DeserializeRequired<ProductUpsertRequest>(apiJob);
              // Tương thích job Chất thải cũ chưa có TypeWeight.
              if (productUpsertRequest.TypeWeight == 0)
                productUpsertRequest.TypeWeight = 2;
              if (productUpsertRequest.WasteType != 0)
              {
                await _apiService.UpsertProductAsync(
                  productUpsertRequest,
                  cancellationToken: cancellationToken).ConfigureAwait(false);
              }
              break;
            case EnumTypeAPI.MD_Delivery:
              var deliveryUpsertRequest = DeserializeRequired<ClientUpsertRequest>(apiJob);
              // Tương thích cả job Delivery cũ chưa có ClientType trong JSON.
              deliveryUpsertRequest.ClientType = 2;
              await _apiService.UpsertClientAsync(
                deliveryUpsertRequest,
                cancellationToken: cancellationToken).ConfigureAwait(false);
              break;
            case EnumTypeAPI.MD_User:
              var userUpsertRequest = DeserializeRequired<UserUpsertRequest>(apiJob);
              // Bỏ qua cả các job ISOFT cũ đã nằm trong hàng đợi.
              if (!PasswordPolicy.IsBuiltInAccount(userUpsertRequest.Username))
              {
                await _apiService.UpsertUserAsync(
                  userUpsertRequest,
                  cancellationToken: cancellationToken).ConfigureAwait(false);
              }
              break;
            case EnumTypeAPI.MD_Document:
              var documentUpsertRequest = DeserializeRequired<DocumentUpsertRequest>(apiJob);
              await _apiService.UpsertDocumentAsync(documentUpsertRequest, cancellationToken)
                .ConfigureAwait(false);
              break;
            case EnumTypeAPI.MD_Template:
              var templateUpsertRequest = DeserializeRequired<TemplateUpsertRequest>(apiJob);
              await _apiService.UpsertTemplateAsync(
                templateUpsertRequest,
                cancellationToken: cancellationToken).ConfigureAwait(false);
              break;
            default:
              throw new NotSupportedException(
                $"Không hỗ trợ EnumTypeAPI '{apiJob.EnumTypeAPI}'.");
            }

            apiJob.EnumStatusAPI = EnumStatusAPI.Success;
            apiJob.UpdatedAt = DateTime.UtcNow;
            if ((apiJob.Retry ?? 0) > 0)
            {
              apiJob.Description =
                $"Đồng bộ {apiJob.EnumTypeAPI} thành công sau " +
                $"{apiJob.Retry} lần thử lại.";
            }
            await _apiJobsService.AddOrUpdateAsync(apiJob).ConfigureAwait(false);
          }
          catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
          {
            throw;
          }
          catch (Exception ex)
          {
            int retry = (apiJob.Retry ?? 0) + 1;
            var contextualException = new InvalidOperationException(
              $"ApiJob thất bại. JobId={apiJob.Id}, " +
              $"Type={apiJob.EnumTypeAPI}, Retry={retry}.",
              ex);
            LogHelper.LogErrorToFileLog(
              contextualException,
              AppCore.Ins._folderFileLog);
            apiJob.EnumStatusAPI = EnumStatusAPI.Fail;
            apiJob.Retry = retry;
            apiJob.Description =
              $"[{DateTime.UtcNow:O}] {apiJob.EnumTypeAPI}: " +
              ex.GetBaseException().Message;
            await _apiJobsService.AddOrUpdateAsync(apiJob).ConfigureAwait(false);
          }
        }
      }
      finally
      {
        _processingLock.Release();
      }
    }

    private static T DeserializeRequired<T>(ApiJobs apiJob)
    {
      if (string.IsNullOrWhiteSpace(apiJob.Json))
      {
        throw new InvalidDataException(
          $"ApiJob {apiJob.Id} không có JSON payload.");
      }

      try
      {
        return JsonHelper.FromJson<T>(apiJob.Json)
          ?? throw new InvalidDataException(
            $"ApiJob {apiJob.Id} không deserialize được thành {typeof(T).Name}.");
      }
      catch (Exception ex) when (ex is not InvalidDataException)
      {
        throw new InvalidDataException(
          $"ApiJob {apiJob.Id} có JSON không hợp lệ cho {typeof(T).Name}.",
          ex);
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
