namespace ApiSyncData.Req
{
  public sealed class DeliveryUpsertRequest
  {
    public Guid? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? OfficeAddress { get; set; }
    public string? PhoneForOfficeAddress { get; set; }
    public string? AgentAddress { get; set; }
    public string? PhoneForAgentAddress { get; set; }
    public string? Description { get; set; }
    public bool DeletedFlag { get; set; }
  }
}
