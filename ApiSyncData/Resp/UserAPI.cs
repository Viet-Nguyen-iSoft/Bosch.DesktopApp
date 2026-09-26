using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApiSyncData.Resp
{
  public class UserAPI
  {
    public string? Status { get; set; }
    public DataUser? Data { get; set; }
  }
  public class DataUser
  {
    public int TotalRecord { get; set; }
    public List<ListDatumUser>? ListData { get; set; }
  }

  public class ListDatumUser : IServerRecord
  {
    public Guid? Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDelete { get; set; }
    public string? DisplayName { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? EmployeeCode { get; set; }
    public string? IdCardCode { get; set; }

    [JsonProperty("Permission")]
    public List<string>? Permission { get; set; }
  }

  

}
