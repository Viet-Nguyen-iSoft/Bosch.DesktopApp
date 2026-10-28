namespace ApiSyncData.Req
{
  public sealed class ClientUpsertRequest
  {
    public Guid? Id { get; set; }
    public int ClientType { get; set; } = 1;
    public string Name { get; set; } = string.Empty;
    public string? OfficeAddress { get; set; }
    public string? PhoneForOfficeAddress { get; set; }
    public string? AgentAddress { get; set; }
    public string? PhoneForAgentAddress { get; set; }
    public string? Description { get; set; }
    public bool IsDefaultDelivery { get; set; }
    public bool DeletedFlag { get; set; }
  }
}
