using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSyncData.Resp
{
  public class DataClient
  {
    public int? TotalRecord { get; set; }
    public List<ListDatumClient>? ListData { get; set; }
  }

  public class ListDatumClient : IServerRecord
  {
    public Guid? Id { get; set; }
    public int? ClientType { get; set; }
    public string? Name { get; set; }
    public string? OfficeAddress { get; set; }
    public string? PhoneForOfficeAddress { get; set; }
    public string? AgentAddress { get; set; }
    public string? PhoneForAgentAddress { get; set; }
    public string? Description { get; set; }
    public bool? IsDefaultDelivery { get; set; }
    public int? CreateFrom { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDelete { get; set; }
  }

  public class ClientAPI
  {
    public string? Status { get; set; }
    public DataClient? Data { get; set; }
  }
}
