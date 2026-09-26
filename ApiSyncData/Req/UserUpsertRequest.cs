using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ApiSyncData.Req
{
  public class UserUpsertRequest
  {
    public Guid? Id { get; set; }
    public List<UserTranslateFormData> TranslateFormDatas { get; set; } = new();
    public string? DisplayName { get; set; }
    public string? FullName { get; set; }
    public string? Username { get; set; }
    public string? EmployeeCode { get; set; }
    public string? IdCardCode { get; set; }
    public bool EnableFlag { get; set; }
    public bool SyncFlag { get; set; }
  }

  public class UserTranslateFormData
  {
    [JsonProperty("Lang")]
    public string Lang { get; set; } = string.Empty;

    [JsonProperty("FullName")]
    public string FullName { get; set; } = string.Empty;
  }
}
