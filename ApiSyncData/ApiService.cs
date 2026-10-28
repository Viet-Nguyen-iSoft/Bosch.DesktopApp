using ApiSyncData.Req;
using ApiSyncData.Resp;
using iSoft.Database.Models;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;
using WebDriverBiDi.Network;

namespace ApiSyncData
{
  public class ApiService
  {
    public async Task<string> UpsertUserAsync(
      UserUpsertRequest user,
      string lang = "vi",
      CancellationToken cancellationToken = default)
    {
      try
      {
        ArgumentNullException.ThrowIfNull(user);

        if (user.Id == Guid.Empty)
          throw new ArgumentException("User Id không được là Guid.Empty.", nameof(user));

        string baseAPI = Environment.GetEnvironmentVariable("URL_API_AUTH")
          ?? throw new InvalidOperationException("Environment variable URL_API is not configured.");
        string apiKey = Environment.GetEnvironmentVariable("API_KEY")
          ?? throw new InvalidOperationException("Environment variable API_KEY is not configured.");
        string apiUrl = $"{baseAPI.TrimEnd('/')}/v1/User/upsert-multi-lang";

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey.Trim());

        using var formData = new MultipartFormDataContent();
        if (user.Id.HasValue)
          formData.Add(new StringContent(user.Id.Value.ToString()), "Id");

        formData.Add(
          new StringContent(JsonConvert.SerializeObject(user.TranslateFormDatas)),
          "TranslateFormDatas");
        AddOptionalFormField(formData, "Id", user.Id.ToString());
        AddOptionalFormField(formData, "Username", user.Username);
        AddOptionalFormField(formData, "Password", user.Password);
        AddOptionalFormField(formData, "EmployeeCode", user.EmployeeCode);
        AddOptionalFormField(formData, "IdCardCode", "");
        formData.Add(new StringContent(user.EnableFlag.ToString().ToLowerInvariant()), "EnableFlag");
        formData.Add(new StringContent(user.SyncFlag.ToString().ToLowerInvariant()), "SyncFlag");
        formData.Add(new StringContent(user.DeletedFlag.ToString().ToLowerInvariant()), "DeletedFlag");
        formData.Add(new StringContent(lang), "lang");

        using var response = await httpClient.PostAsync(apiUrl, formData, cancellationToken)
          .ConfigureAwait(false);
        string responseContent = await response.Content.ReadAsStringAsync(cancellationToken)
          .ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
          throw new HttpRequestException(
            $"UpsertUser. URL: {apiUrl}. HTTP {(int)response.StatusCode} " +
            $"({response.ReasonPhrase}). Response: {responseContent}");
        }

        return responseContent;
      }
      catch (Exception ex)
      {
        throw;
      }
    }

    private static void AddOptionalFormField(
      MultipartFormDataContent formData,
      string fieldName,
      string? value)
    {
      if (!string.IsNullOrWhiteSpace(value))
        formData.Add(new StringContent(value.Trim()), fieldName);
      else
        formData.Add(new StringContent(fieldName), "DeleteFields");
    }

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

    public async Task<DeliveryAPI> Delivery(
      bool isContainDelete = false,
      CancellationToken cancellationToken = default)
    {
      string baseAPI = Environment.GetEnvironmentVariable("URL_API")
        ?? throw new InvalidOperationException("Environment variable URL_API is not configured.");
      string apiKey = Environment.GetEnvironmentVariable("API_KEY")
        ?? throw new InvalidOperationException("Environment variable API_KEY is not configured.");
      string apiUrl =
        $"{baseAPI.TrimEnd('/')}/v1/ClientGoods/get-list-simplify" +
        $"?IsDeleted={isContainDelete.ToString().ToLowerInvariant()}";

      using var httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey.Trim());

      using var response = await httpClient.GetAsync(apiUrl, cancellationToken)
        .ConfigureAwait(false);
      string responseContent = await response.Content
        .ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

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

    public async Task<string> UpsertCategoryTareAsync(
      Req.CategoryTareUpsertRequest categoryTare,
      string lang = "vi",
      CancellationToken cancellationToken = default)
    {
      ArgumentNullException.ThrowIfNull(categoryTare);
      ArgumentException.ThrowIfNullOrWhiteSpace(categoryTare.Name);
      ArgumentException.ThrowIfNullOrWhiteSpace(lang);

      if (!categoryTare.Id.HasValue || categoryTare.Id == Guid.Empty)
        throw new ArgumentException("CategoryTare Id là bắt buộc và không được là Guid.Empty.", nameof(categoryTare));
      if (categoryTare.TypeTare is not 1 and not 2)
        throw new ArgumentOutOfRangeException(
          nameof(categoryTare), "TypeTare phải bằng 1 (Truck) hoặc 2 (Good).");
      if (!categoryTare.WeightTare.HasValue || categoryTare.WeightTare.Value < 0)
        throw new ArgumentOutOfRangeException(
          nameof(categoryTare), "WeightTare phải có giá trị lớn hơn hoặc bằng 0.");

      string baseAPI = Environment.GetEnvironmentVariable("URL_API")
        ?? throw new InvalidOperationException("Environment variable URL_API is not configured.");
      string apiKey = Environment.GetEnvironmentVariable("API_KEY")
        ?? throw new InvalidOperationException("Environment variable API_KEY is not configured.");
      string apiUrl = $"{baseAPI.TrimEnd('/')}/v1/CategoryTare/upsert-multi-lang";

      using var httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey.Trim());

      using var formData = new MultipartFormDataContent
      {
        { new StringContent(categoryTare.SerialCode?.Trim() ?? string.Empty), "SerialCode" },
        { new StringContent(categoryTare.Name.Trim()), "Name" },
        { new StringContent(categoryTare.Description?.Trim() ?? string.Empty), "Description" },
        {
          new StringContent(categoryTare.WeightTare.Value.ToString(CultureInfo.InvariantCulture)),
          "WeightTare"
        },
        { new StringContent(categoryTare.Id.Value.ToString()), "Id" },
        {
          new StringContent(categoryTare.TypeTare.ToString(CultureInfo.InvariantCulture)),
          "TypeTare"
        },
        {
          new StringContent((categoryTare.DeletedFlag ?? false).ToString().ToLowerInvariant()),
          "DeletedFlag"
        },
        { new StringContent(lang.Trim()), "lang" }
      };

      using var response = await httpClient.PostAsync(apiUrl, formData, cancellationToken)
        .ConfigureAwait(false);
      string responseContent = await response.Content.ReadAsStringAsync(cancellationToken)
        .ConfigureAwait(false);

      if (!response.IsSuccessStatusCode)
      {
        throw new HttpRequestException(
          $"UpsertCategoryTare. URL: {apiUrl}. " +
          $"HTTP {(int)response.StatusCode} ({response.ReasonPhrase}). " +
          $"Response: {responseContent}");
      }

      return responseContent;
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
        if (client.ClientType is not (1 or 2))
        {
          throw new ArgumentOutOfRangeException(
            nameof(client),
            client.ClientType,
            "ClientType chỉ nhận giá trị 1 (Khách hàng) hoặc 2 (Khách hàng giao).");
        }

        var additionalFields = new Dictionary<string, string>
        {
          ["ClientType"] = client.ClientType.ToString(),
          ["OfficeAddress"] = client.OfficeAddress?.Trim() ?? string.Empty,
          ["PhoneForOfficeAddress"] =
            client.PhoneForOfficeAddress?.Trim() ?? string.Empty,
          ["AgentAddress"] = client.AgentAddress?.Trim() ?? string.Empty,
          ["PhoneForAgentAddress"] =
            client.PhoneForAgentAddress?.Trim() ?? string.Empty,
          ["UpdatedAt"] = (client.UpdatedAt ?? DateTime.UtcNow).ToString("O"),
        };

        return PostUpsertMultiLangAsync(
          "Client",
          client.Id,
          client.Name,
          string.Empty,
          client.Description,
          client.DeletedFlag,
          lang,
          cancellationToken,
          additionalFields,
          includeEmptyDescription: true);
      }
      catch (Exception)
      {
        throw;
      }
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

    public async Task<CategoryTareAPI> CategoryTare(
      bool isContainDelete = true,
      CancellationToken cancellationToken = default)
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

        using var response = await httpClient.GetAsync(apiUrl, cancellationToken);

        var responseContent =
            await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
          throw new HttpRequestException(
              $"CategoryTare. " +
              $"URL: {apiUrl}. " +
              $"HTTP {(int)response.StatusCode} " +
              $"({response.ReasonPhrase}). " +
              $"Response: {responseContent}");
        }

        return JsonConvert.DeserializeObject<CategoryTareAPI>(responseContent)
          ?? throw new InvalidOperationException(
            "CategoryTare API trả về dữ liệu không hợp lệ.");
      }
      catch (Exception)
      {
        throw;
      }
    }

    public async Task<ClientAPI> Client(
      bool isContainDelete = false,
      CancellationToken cancellationToken = default)
    {
      string baseAPI = Environment.GetEnvironmentVariable("URL_API")
        ?? throw new InvalidOperationException(
          "Environment variable URL_API is not configured.");
      string apiKey = Environment.GetEnvironmentVariable("API_KEY")
        ?? throw new InvalidOperationException(
          "Environment variable API_KEY is not configured.");

      string apiUrl =
        $"{baseAPI.TrimEnd('/')}/v1/Client/get-list-simplify" +
        $"?IsDeleted={isContainDelete.ToString().ToLowerInvariant()}";

      using var httpClient = new HttpClient();

      // Giống cấu hình Authorization trong Postman:
      // API Key, Key = X-API-KEY, Add to = Header
      httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey.Trim());

      using var response = await httpClient.GetAsync(apiUrl, cancellationToken)
        .ConfigureAwait(false);

      string responseContent = await response.Content
        .ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

      if (!response.IsSuccessStatusCode)
      {
        throw new HttpRequestException(
          $"Client. URL: {apiUrl}. HTTP {(int)response.StatusCode} " +
          $"({response.ReasonPhrase}). Response: {responseContent}");
      }

      return JsonConvert.DeserializeObject<ClientAPI>(responseContent)
        ?? throw new InvalidOperationException(
          "Client API trả về dữ liệu không hợp lệ.");
    }

    public async Task<UserAPI> Users()
    {
      try
      {
        string baseAPI = Environment.GetEnvironmentVariable("URL_API");
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

    public async Task<string> RemoveRole(
      Guid userId,
      IEnumerable<string> phanQuyenCodes,
      CancellationToken cancellationToken = default)
    {
      if (userId == Guid.Empty)
        throw new ArgumentException("UserId không được là Guid.Empty.", nameof(userId));
      ArgumentNullException.ThrowIfNull(phanQuyenCodes);

      var codes = phanQuyenCodes
        .Where(code => !string.IsNullOrWhiteSpace(code))
        .Select(code => code.Trim())
        .Distinct(StringComparer.Ordinal)
        .ToList();
      if (codes.Count == 0)
        throw new ArgumentException("Phải có ít nhất một mã quyền.", nameof(phanQuyenCodes));

      string baseApi = Environment.GetEnvironmentVariable("URL_API")
        ?? throw new InvalidOperationException("Environment variable URL_API is not configured.");
      string apiKey = Environment.GetEnvironmentVariable("API_KEY")
        ?? throw new InvalidOperationException("Environment variable API_KEY is not configured.");
      string apiUrl = $"{baseApi.TrimEnd('/')}/v1/PhanQuyen/remove-codes";
      string body = JsonConvert.SerializeObject(new
      {
        UserId = userId,
        PhanQuyenCodes = codes,
      });

      using var httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey.Trim());
      using var requestContent = new StringContent(
        body,
        System.Text.Encoding.UTF8,
        "application/json");
      using var response = await httpClient.PostAsync(
        apiUrl,
        requestContent,
        cancellationToken).ConfigureAwait(false);
      string responseContent = await response.Content
        .ReadAsStringAsync(cancellationToken)
        .ConfigureAwait(false);

      if (!response.IsSuccessStatusCode)
      {
        throw new HttpRequestException(
          $"RemoveRole. URL: {apiUrl}. HTTP {(int)response.StatusCode} " +
          $"({response.ReasonPhrase}). Response: {responseContent}");
      }

      return responseContent;
    }

    public async Task<string> AssignRole(
      Guid userId,
      IEnumerable<string> phanQuyenCodes,
      CancellationToken cancellationToken = default)
    {
      if (userId == Guid.Empty)
        throw new ArgumentException("UserId không được là Guid.Empty.", nameof(userId));
      ArgumentNullException.ThrowIfNull(phanQuyenCodes);

      var codes = phanQuyenCodes
        .Where(code => !string.IsNullOrWhiteSpace(code))
        .Select(code => code.Trim())
        .Distinct(StringComparer.Ordinal)
        .ToList();
      if (codes.Count == 0)
        throw new ArgumentException("Phải có ít nhất một mã quyền.", nameof(phanQuyenCodes));

      string baseApi = Environment.GetEnvironmentVariable("URL_API")
        ?? throw new InvalidOperationException("Environment variable URL_API is not configured.");
      string apiKey = Environment.GetEnvironmentVariable("API_KEY")
        ?? throw new InvalidOperationException("Environment variable API_KEY is not configured.");
      string apiUrl = $"{baseApi.TrimEnd('/')}/v1/PhanQuyen/assign-codes";
      string body = JsonConvert.SerializeObject(new
      {
        UserId = userId,
        PhanQuyenCodes = codes,
      });

      using var httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey.Trim());
      using var requestContent = new StringContent(
        body,
        System.Text.Encoding.UTF8,
        "application/json");
      using var response = await httpClient.PostAsync(
        apiUrl,
        requestContent,
        cancellationToken).ConfigureAwait(false);
      string responseContent = await response.Content
        .ReadAsStringAsync(cancellationToken)
        .ConfigureAwait(false);

      if (!response.IsSuccessStatusCode)
      {
        throw new HttpRequestException(
          $"RemoveRole. URL: {apiUrl}. HTTP {(int)response.StatusCode} " +
          $"({response.ReasonPhrase}). Response: {responseContent}");
      }

      return responseContent;
    }

    public async Task<RecordTruckAPI> RecordTruckFromServer(
      Guid? stationId,
      int numberLastDay)
    {
      if (numberLastDay <= 0)
        throw new ArgumentOutOfRangeException(
          nameof(numberLastDay),
          "NumberLastDay phải lớn hơn 0.");

      string baseAPI = Environment.GetEnvironmentVariable("URL_API")
        ?? throw new InvalidOperationException("Environment variable URL_API is not configured.");
      string apiKey = Environment.GetEnvironmentVariable("API_KEY")
        ?? throw new InvalidOperationException("Environment variable API_KEY is not configured.");

      var dateTo = DateTime.UtcNow;
      var dateFrom = dateTo.AddDays(-numberLastDay);
      var dateFromQuery = Uri.EscapeDataString(
        dateFrom.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture));
      var dateToQuery = Uri.EscapeDataString(
        dateTo.ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture));
      var apiUrl =
        $"{baseAPI.TrimEnd('/')}/v1/RecordTruck/get-list-simplify" +
        $"?ExcludeStationId={stationId:D}" +
        $"&dateFrom={dateFromQuery}" +
        $"&dateTo={dateToQuery}" +
        $"&isDeleted=true";
      using var httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey.Trim());

      using var response = await httpClient.GetAsync(apiUrl).ConfigureAwait(false);
      var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

      if (!response.IsSuccessStatusCode)
      {
        throw new HttpRequestException(
          $"RecordTruckFromServer. " +
          $"URL: {apiUrl}. " +
          $"HTTP {(int)response.StatusCode} " +
          $"({response.ReasonPhrase}). " +
          $"Response: {responseContent}");
      }

      return JsonConvert.DeserializeObject<RecordTruckAPI>(responseContent)
        ?? throw new InvalidOperationException(
          "RecordTruckFromServer API trả về dữ liệu không hợp lệ.");
    }

    public async Task<DocumentAPI> Documents(
      DateTime dateFrom,
      DateTime dateTo,
      bool isContainDelete = true,
      CancellationToken cancellationToken = default)
    {
      try
      {
        if (dateFrom.Date > dateTo.Date)
        {
          throw new ArgumentException(
            "dateFrom phải nhỏ hơn hoặc bằng dateTo.", nameof(dateFrom));
        }

        string baseAPI = Environment.GetEnvironmentVariable("URL_API")
          ?? throw new InvalidOperationException("Environment variable URL_API is not configured.");
        string apiKey = Environment.GetEnvironmentVariable("API_KEY")
          ?? throw new InvalidOperationException("Environment variable API_KEY is not configured.");
        string dateFromQuery = Uri.EscapeDataString(
          dateFrom.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
        string dateToQuery = Uri.EscapeDataString(
          dateTo.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
        string apiUrl =
          $"{baseAPI.TrimEnd('/')}/v1/Document/get-list-simplify" +
          $"?IsDeleted={isContainDelete.ToString().ToLowerInvariant()}" +
          $"&dateFrom={dateFromQuery}" +
          $"&dateTo={dateToQuery}";

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey.Trim());

        using var response = await httpClient.GetAsync(apiUrl, cancellationToken)
          .ConfigureAwait(false);
        string responseContent = await response.Content
          .ReadAsStringAsync(cancellationToken)
          .ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
          throw new HttpRequestException(
            $"Documents. URL: {apiUrl}. HTTP {(int)response.StatusCode} " +
            $"({response.ReasonPhrase}). Response: {responseContent}");
        }

        return JsonConvert.DeserializeObject<DocumentAPI>(responseContent)
          ?? throw new InvalidOperationException(
            "Document API trả về dữ liệu không hợp lệ.");
      }
      catch (Exception ex)
      {
        throw new Exception("Lỗi API");
      }
    }

    public async Task<string> UpsertDocumentAsync(
      DocumentUpsertRequest document,
      CancellationToken cancellationToken = default)
    {
      ArgumentNullException.ThrowIfNull(document);
      ArgumentException.ThrowIfNullOrWhiteSpace(document.Name);

      if (document.Id == Guid.Empty)
        throw new ArgumentException("Document Id không được là Guid.Empty.", nameof(document));

      string baseAPI = Environment.GetEnvironmentVariable("URL_API")
        ?? throw new InvalidOperationException("Environment variable URL_API is not configured.");
      string apiKey = Environment.GetEnvironmentVariable("API_KEY")
        ?? throw new InvalidOperationException("Environment variable API_KEY is not configured.");
      string apiUrl = $"{baseAPI.TrimEnd('/')}/v1/Document/upsert-multi-lang";

      using var httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Add("X-API-KEY", apiKey.Trim());

      using var formData = new MultipartFormDataContent
      {
        { new StringContent(FormatUtcTimestamp(document.DocumentDate)), "DocumentDate" },
        { new StringContent(document.Name.Trim()), "Name" },
        { new StringContent(document.Description?.Trim() ?? string.Empty), "Description" },
        { new StringContent(FormatUtcTimestamp(document.UpdatedAt)), "UpdatedAt" },
        { new StringContent(document.DeletedFlag.ToString().ToLowerInvariant()), "DeletedFlag" },
      };
      if (document.Id.HasValue)
        formData.Add(new StringContent(document.Id.Value.ToString()), "Id");

      using var response = await httpClient.PostAsync(
        apiUrl, formData, cancellationToken).ConfigureAwait(false);
      string responseContent = await response.Content
        .ReadAsStringAsync(cancellationToken)
        .ConfigureAwait(false);

      if (!response.IsSuccessStatusCode)
      {
        throw new HttpRequestException(
          $"UpsertDocument. URL: {apiUrl}. HTTP {(int)response.StatusCode} " +
          $"({response.ReasonPhrase}). Response: {responseContent}");
      }

      return responseContent;
    }

    private static string FormatUtcTimestamp(DateTime value)
    {
      var utcValue = value.Kind switch
      {
        DateTimeKind.Utc => value,
        DateTimeKind.Local => value.ToUniversalTime(),
        _ => DateTime.SpecifyKind(value, DateTimeKind.Utc)
      };
      return utcValue.ToString(
        "yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture);
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
      string nameField = "Name",
      bool includeEmptyDescription = false)
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

        if (includeEmptyDescription)
          formData.Add(new StringContent(description?.Trim() ?? string.Empty), "Description");
        else if (!string.IsNullOrWhiteSpace(description))
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

    //public async Task<string> UploadReportTruckPdf(Guid? recordTruck, string pdfFilePath)
    //{
    //  try
    //  {
    //    if (!recordTruck.HasValue || recordTruck.Value == Guid.Empty)
    //      throw new ArgumentException(
    //          "recordTruck không hợp lệ.",
    //          nameof(recordTruck));

    //    if (string.IsNullOrWhiteSpace(pdfFilePath))
    //      throw new ArgumentException(
    //          "Đường dẫn file PDF không được để trống.",
    //          nameof(pdfFilePath));

    //    if (!File.Exists(pdfFilePath))
    //      throw new FileNotFoundException(
    //          "Không tìm thấy file PDF.",
    //          pdfFilePath);

    //    if (!string.Equals(
    //            Path.GetExtension(pdfFilePath),
    //            ".pdf",
    //            StringComparison.OrdinalIgnoreCase))
    //    {
    //      throw new ArgumentException(
    //          "File tải lên phải có định dạng PDF.",
    //          nameof(pdfFilePath));
    //    }

    //    string baseAPI = Environment.GetEnvironmentVariable("URL_API")
    //      ?? throw new InvalidOperationException("Environment variable URL_API is not configured.");
    //    string apiKey = Environment.GetEnvironmentVariable("API_KEY")
    //      ?? throw new InvalidOperationException("Environment variable API_KEY is not configured.");
    //    string apiUrl =
    //      $"{baseAPI.TrimEnd('/')}/v1/RecordTruck/upload-pdf";

    //    using var httpClient = new HttpClient();
    //    using var formData = new MultipartFormDataContent();
    //    using var fileStream = File.OpenRead(pdfFilePath);
    //    using var fileContent = new StreamContent(fileStream);

    //    // Giống cấu hình Authorization trong Postman:
    //    // API Key, Key = X-API-KEY, Add to = Header
    //    httpClient.DefaultRequestHeaders.Add(
    //        "X-API-KEY",
    //        apiKey);

    //    fileContent.Headers.ContentType =
    //        new MediaTypeHeaderValue("application/pdf");

    //    formData.Add(
    //        fileContent,
    //        "pdfFile",
    //        Path.GetFileName(pdfFilePath));

    //    formData.Add(
    //        new StringContent(recordTruck.Value.ToString()),
    //        "recordTruckId");

    //    using var response = await httpClient.PostAsync(
    //        apiUrl,
    //        formData);

    //    var responseContent =
    //        await response.Content.ReadAsStringAsync();

    //    if (!response.IsSuccessStatusCode)
    //    {
    //      throw new HttpRequestException(
    //          $"Upload PDF thất bại. " +
    //          $"URL: {apiUrl}. " +
    //          $"HTTP {(int)response.StatusCode} " +
    //          $"({response.ReasonPhrase}). " +
    //          $"Response: {responseContent}");
    //    }

    //    return responseContent;
    //  }
    //  catch (Exception ex)
    //  {
    //    throw;
    //  }
    //}

  }
}
