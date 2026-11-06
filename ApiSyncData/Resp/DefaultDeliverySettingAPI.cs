namespace ApiSyncData.Resp
{
  public sealed class DefaultDeliverySettingAPI
  {
    public string? Status { get; set; }
    public DefaultDeliverySettingData? Data { get; set; }
  }

  public sealed class DefaultDeliverySettingData
  {
    public DefaultDeliverySettingClient? Client { get; set; }
  }

  public sealed class DefaultDeliverySettingClient
  {
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? OfficeAddress { get; set; }
    public string? PhoneForOfficeAddress { get; set; }
    public string? AgentAddress { get; set; }
    public string? PhoneForAgentAddress { get; set; }
  }
}
