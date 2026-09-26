using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSyncData.Req
{
  public class UserUpsertRequest
  {
    public Guid? Id { get; set; }
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
