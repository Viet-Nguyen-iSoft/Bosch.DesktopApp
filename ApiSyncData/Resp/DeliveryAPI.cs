namespace ApiSyncData.Resp
{
  public class DataDelivery
  {
    public int? TotalRecord { get; set; }
    public List<ListDatumDelivery>? ListData { get; set; }
  }

  public class ListDatumDelivery : IServerRecord
  {
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? OfficeAddress { get; set; }
    public string? PhoneForOfficeAddress { get; set; }
    public string? AgentAddress { get; set; }
    public string? PhoneForAgentAddress { get; set; }
    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDelete { get; set; }
  }

  public class DeliveryAPI
  {
    public string? Status { get; set; }
    public DataDelivery? Data { get; set; }
  }
}
