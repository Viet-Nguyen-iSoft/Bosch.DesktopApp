using Newtonsoft.Json;

namespace ApiSyncData.Req
{
  public sealed class DefaultDeliverySettingUpdateRequest
  {
    [JsonProperty("client")]
    public DefaultDeliverySettingUpdateClient Client { get; set; } = new();
  }

  public sealed class DefaultDeliverySettingUpdateClient
  {
    [JsonProperty("id")]
    public Guid Id { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("officeAddress")]
    public string? OfficeAddress { get; set; }

    [JsonProperty("phoneForOfficeAddress")]
    public string? PhoneForOfficeAddress { get; set; }

    [JsonProperty("agentAddress")]
    public string? AgentAddress { get; set; }

    [JsonProperty("phoneForAgentAddress")]
    public string? PhoneForAgentAddress { get; set; }
  }
}
