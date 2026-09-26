using ApiSyncData.Req;
using ApiSyncData.Resp;
using iSoft.Database.Models;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;

namespace ApiSyncData
{
  public class ApiService
  {
    public async Task<StationAPI> Station(bool isContainDelete = false)
    {
      try
      {
        string baseAPI = Environment.GetEnvironmentVariable("URL_API");
        string apiKey = Environment.GetEnvironmentVariable("API_KEY");

        var apiUrl =
          $"{baseAPI.TrimEnd('/')}/v1/Station/get-list-simplify?IsDeleted={isContainDelete}";

        using var httpClient = new HttpClient();

        // Giống cấu hình Authorization trong Postman:
        // API Key, Key = X-API-KEY, Add to = Header
        httpClient.DefaultRequestHeaders.Add(
            "X-API-KEY",
            apiKey.Trim());

        using var response = await httpClient.GetAsync(apiUrl);

        var responseContent =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
          throw new HttpRequestException(
              $"Station. " +
              $"URL: {apiUrl}. " +
              $"HTTP {(int)response.StatusCode} " +
              $"({response.ReasonPhrase}). " +
              $"Response: {responseContent}");
        }

        return JsonConvert.DeserializeObject<StationAPI>(responseContent);
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<WarehouseAPI> Warehouse(bool isContainDelete = false)
    {
      try
      {
        string baseAPI = Environment.GetEnvironmentVariable("URL_API");
        string apiKey = Environment.GetEnvironmentVariable("API_KEY");

        var apiUrl =
          $"{baseAPI.TrimEnd('/')}/v1/Warehouse/get-list-simplify?IsDeleted={isContainDelete}";

        using var httpClient = new HttpClient();

        // Giống cấu hình Authorization trong Postman:
        // API Key, Key = X-API-KEY, Add to = Header
        httpClient.DefaultRequestHeaders.Add(
            "X-API-KEY",
            apiKey.Trim());

        using var response = await httpClient.GetAsync(apiUrl);

        var responseContent =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
          throw new HttpRequestException(
              $"Warehouse. " +
              $"URL: {apiUrl}. " +
              $"HTTP {(int)response.StatusCode} " +
              $"({response.ReasonPhrase}). " +
              $"Response: {responseContent}");
        }

        return JsonConvert.DeserializeObject<WarehouseAPI>(responseContent);
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<DeliveryAPI> Delivery(bool isContainDelete = false)
    {
      string baseAPI = Environment.GetEnvironmentVariable("URL_API")
        ?? throw new InvalidOperationException("Environment variable URL_API is not configured.");
      string apiKey = Environment.GetEnvironmentVariable("API_KEY")
        ?? throw new InvalidOperationException("Environment variable API_KEY is not configured.");
      string apiUrl =
        $"{baseAPI.TrimEnd('/')}/v1/ClientGoods/get-list-simplify?IsDeleted={isContainDelete}";

      using var httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey.Trim());

      using var response = await httpClient.GetAsync(apiUrl).ConfigureAwait(false);
      string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

      if (!response.IsSuccessStatusCode)
      {
        throw new HttpRequestException(
          $"Delivery. URL: {apiUrl}. HTTP {(int)response.StatusCode} " +
          $"({response.ReasonPhrase}). Response: {responseContent}");
      }

      return JsonConvert.DeserializeObject<DeliveryAPI>(responseContent)
        ?? throw new InvalidOperationException("Delivery API trả về dữ liệu không hợp lệ.");
    }

    public async Task<string> UpsertWarehouseAsync(
      Req.WarehouseUpsertRequest warehouse,
      string lang = "vi",
      CancellationToken cancellationToken = default)
    {
      ArgumentNullException.ThrowIfNull(warehouse);
      ArgumentException.ThrowIfNullOrWhiteSpace(warehouse.Name);
      ArgumentException.ThrowIfNullOrWhiteSpace(lang);

      if (warehouse.Id == Guid.Empty)
        throw new ArgumentException("Warehouse Id không được là Guid.Empty.", nameof(warehouse));

      string baseAPI = Environment.GetEnvironmentVariable("URL_API")
        ?? throw new InvalidOperationException("Environment variable URL_API is not configured.");
      string apiKey = Environment.GetEnvironmentVariable("API_KEY")
        ?? throw new InvalidOperationException("Environment variable API_KEY is not configured.");
      string apiUrl =
        $"{baseAPI.TrimEnd('/')}/v1/Warehouse/upsert-multi-lang?lang={Uri.EscapeDataString(lang.Trim())}";

      using var httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey.Trim());

      using var formData = new MultipartFormDataContent();
      if (warehouse.Id.HasValue)
        formData.Add(new StringContent(warehouse.Id.Value.ToString()), "Id");

      formData.Add(new StringContent(warehouse.Name.Trim()), "Name");

      //if (!string.IsNullOrWhiteSpace(warehouse.SerialCode))
      //  formData.Add(new StringContent(warehouse.SerialCode.Trim()), "SerialCode");

      if (!string.IsNullOrWhiteSpace(warehouse.Description))
        formData.Add(new StringContent(warehouse.Description.Trim()), "Description");
      else
        formData.Add(new StringContent("Description"), "DeleteFields");

      formData.Add(
        new StringContent(warehouse.DeletedFlag.ToString().ToLowerInvariant()),
        "DeletedFlag");

      using var response = await httpClient.PostAsync(
        apiUrl,
        formData,
        cancellationToken).ConfigureAwait(false);
      string responseContent = await response.Content
        .ReadAsStringAsync(cancellationToken)
        .ConfigureAwait(false);

      if (!response.IsSuccessStatusCode)
      {
        throw new HttpRequestException(
          $"UpsertWarehouse. " +
          $"URL: {apiUrl}. " +
          $"HTTP {(int)response.StatusCode} " +
          $"({response.ReasonPhrase}). " +
          $"Response: {responseContent}");
      }

      return responseContent;
    }

    public Task<string> UpsertCategoryTareAsync(
      Req.CategoryTareUpsertRequest categoryTare,
      string lang = "vi",
      CancellationToken cancellationToken = default)
    {
      ArgumentNullException.ThrowIfNull(categoryTare);

      Dictionary<string, string>? additionalFields = null;
      if (categoryTare.WeightTare.HasValue)
      {
        additionalFields = new Dictionary<string, string>
        {
          ["WeightTare"] = categoryTare.WeightTare.Value.ToString(CultureInfo.InvariantCulture)
        };
      }

      return PostUpsertMultiLangAsync(
        "CategoryTare",
        categoryTare.Id,
        categoryTare.Name,
        categoryTare.SerialCode,
        categoryTare.Description,
        categoryTare.DeletedFlag,
        lang,
        cancellationToken,
        additionalFields);
    }

    public Task<string> UpsertTypeGoodsAsync(
      Req.TypeGoodsUpsertRequest typeGoods,
      string lang = "vi",
      CancellationToken cancellationToken = default)
    {
      ArgumentNullException.ThrowIfNull(typeGoods);

      return PostUpsertMultiLangAsync(
        "TypeGoods",
        typeGoods.Id,
        typeGoods.Name,
        typeGoods.SerialCode,
        typeGoods.Description,
        typeGoods.DeletedFlag,
        lang,
        cancellationToken);
    }

    public Task<string> UpsertProductGroupAsync(
      Req.ProductGroupUpsertRequest productGroup,
      string lang = "vi",
      CancellationToken cancellationToken = default)
    {
      ArgumentNullException.ThrowIfNull(productGroup);

      return PostUpsertMultiLangAsync(
        "ProductGroup",
        productGroup.Id,
        productGroup.Name,
        productGroup.SerialCode,
        productGroup.Description,
        productGroup.DeletedFlag,
        lang,
        cancellationToken);
    }

    public Task<string> UpsertProductAsync(
      Req.ProductUpsertRequest product,
      string lang = "vi",
      CancellationToken cancellationToken = default)
    {
      ArgumentNullException.ThrowIfNull(product);

      var additionalFields = new Dictionary<string, string>
      {
        ["WasteType"] = product.WasteType.ToString(),
      };

      if (product.ProductGroupId.HasValue)
        additionalFields["ProductGroupId"] = product.ProductGroupId.Value.ToString();

      return PostUpsertMultiLangAsync(
        "ProductFood",
        product.Id,
        product.Name,
        product.SerialCode,
        product.Description,
        product.DeletedFlag,
        lang,
        cancellationToken,
        additionalFields);
    }

    public Task<string> UpsertClientAsync(
      Req.ClientUpsertRequest client,
      string lang = "vi",
      CancellationToken cancellationToken = default)
    {
      try
      {
        ArgumentNullException.ThrowIfNull(client);
        Dictionary<string, string>? additionalFields = null;

        return PostUpsertMultiLangAsync(
          "Client",
          client.Id,
          client.Name,
          string.Empty,
          client.Description,
          client.DeletedFlag,
          lang,
          cancellationToken,
          additionalFields);
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<string> UpsertDeliveryAsync(
      Req.DeliveryUpsertRequest delivery,
      string lang = "vi",
      CancellationToken cancellationToken = default)
    {
      ArgumentNullException.ThrowIfNull(delivery);
      ArgumentException.ThrowIfNullOrWhiteSpace(delivery.Name);
      ArgumentException.ThrowIfNullOrWhiteSpace(lang);

      if (delivery.Id == Guid.Empty)
        throw new ArgumentException("Delivery Id không được là Guid.Empty.", nameof(delivery));

      string baseAPI = Environment.GetEnvironmentVariable("URL_API")
        ?? throw new InvalidOperationException("Environment variable URL_API is not configured.");
      string apiKey = Environment.GetEnvironmentVariable("API_KEY")
        ?? throw new InvalidOperationException("Environment variable API_KEY is not configured.");
      string apiUrl =
        $"{baseAPI.TrimEnd('/')}/v1/ClientGoods/upsert-multi-lang?lang={Uri.EscapeDataString(lang.Trim())}";

      using var httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey.Trim());

      using var formData = new MultipartFormDataContent
      {
        { new StringContent(delivery.Id?.ToString() ?? string.Empty), "Id" },
        { new StringContent(delivery.Name.Trim()), "Name" },
        { new StringContent(delivery.OfficeAddress?.Trim() ?? string.Empty), "OfficeAddress" },
        { new StringContent(delivery.PhoneForOfficeAddress?.Trim() ?? string.Empty), "PhoneForOfficeAddress" },
        { new StringContent(delivery.AgentAddress?.Trim() ?? string.Empty), "AgentAddress" },
        { new StringContent(delivery.PhoneForAgentAddress?.Trim() ?? string.Empty), "PhoneForAgentAddress" },
        { new StringContent(delivery.Description?.Trim() ?? string.Empty), "Description" },
        { new StringContent(delivery.DeletedFlag.ToString().ToLowerInvariant()), "DeletedFlag" },
      };

      using var response = await httpClient.PostAsync(
        apiUrl,
        formData,
        cancellationToken).ConfigureAwait(false);
      string responseContent = await response.Content
        .ReadAsStringAsync(cancellationToken)
        .ConfigureAwait(false);

      if (!response.IsSuccessStatusCode)
      {
        throw new HttpRequestException(
          $"UpsertDelivery. URL: {apiUrl}. HTTP {(int)response.StatusCode} " +
          $"({response.ReasonPhrase}). Response: {responseContent}");
      }

      return responseContent;
    }

    public Task<string> UpsertLicensePlateAsync(
      LicensePlateUpsertRequest licensePlate,
      string lang = "vi",
      CancellationToken cancellationToken = default)
    {
      ArgumentNullException.ThrowIfNull(licensePlate);

      return PostUpsertMultiLangAsync(
        "LicensePlate",
        null,
        licensePlate.LicensePlateCode,
        null,
        licensePlate.Description,
        licensePlate.DeletedFlag,
        lang,
        cancellationToken,
        nameField: "LicensePlateCode");
    }

    public async Task<TypeGoodsAPI> TypeGoods(bool isContainDelete = false)
    {
      try
      {
        string baseAPI = Environment.GetEnvironmentVariable("URL_API");
        string apiKey = Environment.GetEnvironmentVariable("API_KEY");

        var apiUrl =
          $"{baseAPI.TrimEnd('/')}/v1/TypeGoods/get-list-simplify?IsDeleted={isContainDelete}";

        using var httpClient = new HttpClient();

        // Giống cấu hình Authorization trong Postman:
        // API Key, Key = X-API-KEY, Add to = Header
        httpClient.DefaultRequestHeaders.Add(
            "X-API-KEY",
            apiKey.Trim());

        using var response = await httpClient.GetAsync(apiUrl);

        var responseContent =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
          throw new HttpRequestException(
              $"TypeGoods. " +
              $"URL: {apiUrl}. " +
              $"HTTP {(int)response.StatusCode} " +
              $"({response.ReasonPhrase}). " +
              $"Response: {responseContent}");
        }

        return JsonConvert.DeserializeObject<TypeGoodsAPI>(responseContent);
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<ProductGroupAPI> ProductGroup(bool isContainDelete = false)
    {
      try
      {
        string baseAPI = Environment.GetEnvironmentVariable("URL_API");
        string apiKey = Environment.GetEnvironmentVariable("API_KEY");

        var apiUrl =
          $"{baseAPI.TrimEnd('/')}/v1/ProductGroup/get-list-simplify?IsDeleted={isContainDelete}";

        using var httpClient = new HttpClient();

        // Giống cấu hình Authorization trong Postman:
        // API Key, Key = X-API-KEY, Add to = Header
        httpClient.DefaultRequestHeaders.Add(
            "X-API-KEY",
            apiKey.Trim());

        using var response = await httpClient.GetAsync(apiUrl);

        var responseContent =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
          throw new HttpRequestException(
              $"ProductGroup. " +
              $"URL: {apiUrl}. " +
              $"HTTP {(int)response.StatusCode} " +
              $"({response.ReasonPhrase}). " +
              $"Response: {responseContent}");
        }

        return JsonConvert.DeserializeObject<ProductGroupAPI>(responseContent);
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<ProductAPI> Product(bool isContainDelete = false)
    {
      try
      {
        string baseAPI = Environment.GetEnvironmentVariable("URL_API");
        string apiKey = Environment.GetEnvironmentVariable("API_KEY");

        var apiUrl =
          $"{baseAPI.TrimEnd('/')}/v1/ProductFood/get-list-simplify?IsDeleted={isContainDelete}";

        using var httpClient = new HttpClient();

        // Giống cấu hình Authorization trong Postman:
        // API Key, Key = X-API-KEY, Add to = Header
        httpClient.DefaultRequestHeaders.Add(
            "X-API-KEY",
            apiKey.Trim());

        using var response = await httpClient.GetAsync(apiUrl);

        var responseContent =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
          throw new HttpRequestException(
              $"Product. " +
              $"URL: {apiUrl}. " +
              $"HTTP {(int)response.StatusCode} " +
              $"({response.ReasonPhrase}). " +
              $"Response: {responseContent}");
        }

        return JsonConvert.DeserializeObject<ProductAPI>(responseContent);
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<CategoryTareAPI> CategoryTare(bool isContainDelete = false)
    {
      try
      {
        string baseAPI = Environment.GetEnvironmentVariable("URL_API");
        string apiKey = Environment.GetEnvironmentVariable("API_KEY");

        var apiUrl =
          $"{baseAPI.TrimEnd('/')}/v1/CategoryTare/get-list-simplify?IsDeleted={isContainDelete}";

        using var httpClient = new HttpClient();

        // Giống cấu hình Authorization trong Postman:
        // API Key, Key = X-API-KEY, Add to = Header
        httpClient.DefaultRequestHeaders.Add(
            "X-API-KEY",
            apiKey.Trim());

        using var response = await httpClient.GetAsync(apiUrl);

        var responseContent =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
          throw new HttpRequestException(
              $"CategoryTare. " +
              $"URL: {apiUrl}. " +
              $"HTTP {(int)response.StatusCode} " +
              $"({response.ReasonPhrase}). " +
              $"Response: {responseContent}");
        }

        return JsonConvert.DeserializeObject<CategoryTareAPI>(responseContent);
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<ClientAPI> Client(bool isContainDelete = false)
    {
      try
      {
        string baseAPI = Environment.GetEnvironmentVariable("URL_API");
        string apiKey = Environment.GetEnvironmentVariable("API_KEY");

        var apiUrl =
          $"{baseAPI.TrimEnd('/')}/v1/Client/get-list-simplify?IsDeleted={isContainDelete}";

        using var httpClient = new HttpClient();

        // Giống cấu hình Authorization trong Postman:
        // API Key, Key = X-API-KEY, Add to = Header
        httpClient.DefaultRequestHeaders.Add(
            "X-API-KEY",
            apiKey.Trim());

        using var response = await httpClient.GetAsync(apiUrl);

        var responseContent =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
          throw new HttpRequestException(
              $"Client. " +
              $"URL: {apiUrl}. " +
              $"HTTP {(int)response.StatusCode} " +
              $"({response.ReasonPhrase}). " +
              $"Response: {responseContent}");
        }

        return JsonConvert.DeserializeObject<ClientAPI>(responseContent);
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<UserAPI> Users(bool isContainDelete = false)
    {
      try
      {
        string baseAPI = Environment.GetEnvironmentVariable("URL_API_AUTH");
        string apiKey = Environment.GetEnvironmentVariable("API_KEY");

        var apiUrl = $"{baseAPI.TrimEnd('/')}/v1/User/get-list-simplify";
        using var httpClient = new HttpClient();

        // Giống cấu hình Authorization trong Postman:
        // API Key, Key = X-API-KEY, Add to = Header
        httpClient.DefaultRequestHeaders.Add(
            "X-API-KEY",
            apiKey.Trim());

        using var response = await httpClient.GetAsync(apiUrl);

        var responseContent =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
          throw new HttpRequestException(
              $"User. " +
              $"URL: {apiUrl}. " +
              $"HTTP {(int)response.StatusCode} " +
              $"({response.ReasonPhrase}). " +
              $"Response: {responseContent}");
        }

        return JsonConvert.DeserializeObject<UserAPI>(responseContent);
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<UserAPI> RecordTruckFromServer(Guid stationId)
    {
      try
      {
        string baseAPI = Environment.GetEnvironmentVariable("URL_API_AUTH");
        string apiKey = Environment.GetEnvironmentVariable("API_KEY");

        var apiUrl = $"{baseAPI.TrimEnd('/')}/v1/RecordTruck/get-list-filter-multi-lang";
      http://100.101.160.94:7902/api/v1/RecordTruck/get-list-filter-multi-lang?page=1&pageSize=20&searchStr=&sortStr=&filterStr=&dateFrom=2026-03-25T06:17:00.000Z&dateTo=2026-09-25T06:17:59.999Z&isDeleted=true&lang=vi
        using var httpClient = new HttpClient();

        // Giống cấu hình Authorization trong Postman:
        // API Key, Key = X-API-KEY, Add to = Header
        httpClient.DefaultRequestHeaders.Add(
            "X-API-KEY",
            apiKey.Trim());

        using var response = await httpClient.GetAsync(apiUrl);

        var responseContent =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
          throw new HttpRequestException(
              $"RecordTruckFromServer. " +
              $"URL: {apiUrl}. " +
              $"HTTP {(int)response.StatusCode} " +
              $"({response.ReasonPhrase}). " +
              $"Response: {responseContent}");
        }

        return JsonConvert.DeserializeObject<UserAPI>(responseContent);
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<string> SyncRecordTruckFromLocal(
      string rawData,
      CancellationToken cancellationToken = default)
    {
      ArgumentException.ThrowIfNullOrWhiteSpace(rawData);

      string baseAPI = Environment.GetEnvironmentVariable("URL_API")
        ?? throw new InvalidOperationException("Environment variable URL_API is not configured.");
      string apiKey = Environment.GetEnvironmentVariable("API_KEY")
        ?? throw new InvalidOperationException("Environment variable API_KEY is not configured.");

      var apiUrl = $"{baseAPI.TrimEnd('/')}/v1/RecordTruck/sync-desktop";

      using var httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey.Trim());

      using var requestContent = new StringContent(
        rawData,
        System.Text.Encoding.UTF8,
        "application/json");
      using var response = await httpClient.PostAsync(
        apiUrl,
        requestContent,
        cancellationToken);

      var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

      if (!response.IsSuccessStatusCode)
      {
        throw new HttpRequestException(
          $"SyncRecordTruck. " +
          $"URL: {apiUrl}. " +
          $"HTTP {(int)response.StatusCode} " +
          $"({response.ReasonPhrase}). " +
          $"Response: {responseContent}");
      }

      return responseContent;
    }

    public async Task<string> SyncRecordWeightFromLocal(
      string rawData,
      CancellationToken cancellationToken = default)
    {
      ArgumentException.ThrowIfNullOrWhiteSpace(rawData);

      string baseAPI = Environment.GetEnvironmentVariable("URL_API")
        ?? throw new InvalidOperationException("Environment variable URL_API is not configured.");
      string apiKey = Environment.GetEnvironmentVariable("API_KEY")
        ?? throw new InvalidOperationException("Environment variable API_KEY is not configured.");

      var apiUrl = $"{baseAPI.TrimEnd('/')}/v1/RecordGoods/sync-desktop";

      using var httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey.Trim());

      using var requestContent = new StringContent(
        rawData,
        System.Text.Encoding.UTF8,
        "application/json");
      using var response = await httpClient.PostAsync(
        apiUrl,
        requestContent,
        cancellationToken);

      var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

      if (!response.IsSuccessStatusCode)
      {
        throw new HttpRequestException(
          $"SyncRecordWeight. " +
          $"URL: {apiUrl}. " +
          $"HTTP {(int)response.StatusCode} " +
          $"({response.ReasonPhrase}). " +
          $"Response: {responseContent}");
      }

      return responseContent;
    }

    private static async Task<string> PostUpsertMultiLangAsync(
      string resource,
      Guid? id,
      string? name,
      string? code,
      string? description,
      bool deletedFlag,
      string? lang,
      CancellationToken cancellationToken,
      IReadOnlyDictionary<string, string>? additionalFields = null,
      string nameField = "Name")
    {
      try
      {
        ArgumentException.ThrowIfNullOrWhiteSpace(resource);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(nameField);
        ArgumentException.ThrowIfNullOrWhiteSpace(lang);

        if (id == Guid.Empty)
          throw new ArgumentException($"{resource} Id không được là Guid.Empty.", nameof(id));

        string baseAPI = Environment.GetEnvironmentVariable("URL_API")
          ?? throw new InvalidOperationException("Environment variable URL_API is not configured.");
        string apiKey = Environment.GetEnvironmentVariable("API_KEY")
          ?? throw new InvalidOperationException("Environment variable API_KEY is not configured.");
        string apiUrl =
          $"{baseAPI.TrimEnd('/')}/v1/{resource}/upsert-multi-lang?lang={Uri.EscapeDataString(lang.Trim())}";

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey.Trim());

        using var formData = new MultipartFormDataContent();
        if (id.HasValue)
          formData.Add(new StringContent(id.Value.ToString()), "Id");

        formData.Add(new StringContent(name.Trim()), nameField);

        if (!string.IsNullOrWhiteSpace(code))
          formData.Add(new StringContent(code.Trim()), "SerialCode");

        if (!string.IsNullOrWhiteSpace(description))
          formData.Add(new StringContent(description.Trim()), "Description");
        else
          formData.Add(new StringContent("Description"), "DeleteFields");

        formData.Add(
          new StringContent(deletedFlag.ToString().ToLowerInvariant()),
          "DeletedFlag");

        if (additionalFields != null)
        {
          foreach (var field in additionalFields)
            formData.Add(new StringContent(field.Value), field.Key);
        }

        using var response = await httpClient.PostAsync(
          apiUrl,
          formData,
          cancellationToken).ConfigureAwait(false);
        string responseContent = await response.Content
          .ReadAsStringAsync(cancellationToken)
          .ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
          throw new HttpRequestException(
            $"Upsert{resource}. " +
            $"URL: {apiUrl}. " +
            $"HTTP {(int)response.StatusCode} " +
            $"({response.ReasonPhrase}). " +
            $"Response: {responseContent}");
        }

        return responseContent;
      }
      catch (Exception)
      {
        throw;
      }
    }

    public static async Task<string> PostUpsertMultiLangLicensePlateAsync(
      string resource,
      string licensePlate,
      string description,
      string lang,
      CancellationToken cancellationToken,
      IReadOnlyDictionary<string, string>? additionalFields = null)
    {
      ArgumentException.ThrowIfNullOrWhiteSpace(resource);
      ArgumentException.ThrowIfNullOrWhiteSpace(licensePlate);

      string baseAPI = Environment.GetEnvironmentVariable("URL_API")
        ?? throw new InvalidOperationException("Environment variable URL_API is not configured.");
      string apiKey = Environment.GetEnvironmentVariable("API_KEY")
        ?? throw new InvalidOperationException("Environment variable API_KEY is not configured.");
      string apiUrl =
        $"{baseAPI.TrimEnd('/')}/v1/{resource}/upsert-multi-lang?lang={Uri.EscapeDataString(lang.Trim())}";

      using var httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey.Trim());

      using var formData = new MultipartFormDataContent();

      if (!string.IsNullOrWhiteSpace(licensePlate))
        formData.Add(new StringContent(licensePlate.Trim()), "LicensePlateCode");

        if (!string.IsNullOrWhiteSpace(description))
          formData.Add(new StringContent(description.Trim()), "Description");
        else
          formData.Add(new StringContent("Description"), "DeleteFields");

      if (additionalFields != null)
      {
        foreach (var field in additionalFields)
          formData.Add(new StringContent(field.Value), field.Key);
      }

      using var response = await httpClient.PostAsync(
        apiUrl,
        formData,
        cancellationToken).ConfigureAwait(false);
      string responseContent = await response.Content
        .ReadAsStringAsync(cancellationToken)
        .ConfigureAwait(false);

      if (!response.IsSuccessStatusCode)
      {
        throw new HttpRequestException(
          $"Upsert{resource}. " +
          $"URL: {apiUrl}. " +
          $"HTTP {(int)response.StatusCode} " +
          $"({response.ReasonPhrase}). " +
          $"Response: {responseContent}");
      }

      return responseContent;
    }

    public async Task<string> UploadReportTruckPdf(Guid? recordTruck, string pdfFilePath)
    {
      try
      {
        if (!recordTruck.HasValue || recordTruck.Value == Guid.Empty)
          throw new ArgumentException(
              "recordTruck không hợp lệ.",
              nameof(recordTruck));

        if (string.IsNullOrWhiteSpace(pdfFilePath))
          throw new ArgumentException(
              "Đường dẫn file PDF không được để trống.",
              nameof(pdfFilePath));

        if (!File.Exists(pdfFilePath))
          throw new FileNotFoundException(
              "Không tìm thấy file PDF.",
              pdfFilePath);

        if (!string.Equals(
                Path.GetExtension(pdfFilePath),
                ".pdf",
                StringComparison.OrdinalIgnoreCase))
        {
          throw new ArgumentException(
              "File tải lên phải có định dạng PDF.",
              nameof(pdfFilePath));
        }

        string baseAPI = Environment.GetEnvironmentVariable("URL_API")
          ?? throw new InvalidOperationException("Environment variable URL_API is not configured.");
        string apiKey = Environment.GetEnvironmentVariable("API_KEY")
          ?? throw new InvalidOperationException("Environment variable API_KEY is not configured.");
        string apiUrl =
          $"{baseAPI.TrimEnd('/')}/v1/RecordTruck/upload-pdf";

        using var httpClient = new HttpClient();
        using var formData = new MultipartFormDataContent();
        using var fileStream = File.OpenRead(pdfFilePath);
        using var fileContent = new StreamContent(fileStream);

        // Giống cấu hình Authorization trong Postman:
        // API Key, Key = X-API-KEY, Add to = Header
        httpClient.DefaultRequestHeaders.Add(
            "X-API-KEY",
            apiKey);

        fileContent.Headers.ContentType =
            new MediaTypeHeaderValue("application/pdf");

        formData.Add(
            fileContent,
            "pdfFile",
            Path.GetFileName(pdfFilePath));

        formData.Add(
            new StringContent(recordTruck.Value.ToString()),
            "recordTruckId");

        using var response = await httpClient.PostAsync(
            apiUrl,
            formData);

        var responseContent =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
          throw new HttpRequestException(
              $"Upload PDF thất bại. " +
              $"URL: {apiUrl}. " +
              $"HTTP {(int)response.StatusCode} " +
              $"({response.ReasonPhrase}). " +
              $"Response: {responseContent}");
        }

        return responseContent;
      }
      catch (Exception ex)
      {
        throw;
      }
    }

  }
}
