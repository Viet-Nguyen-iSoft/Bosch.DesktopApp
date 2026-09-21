using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iSoft.Communication.EnumCommunication;

namespace iSoft.Communication.JsonPayload
{
  public interface IConfigJson
  {
    [Browsable(false)]
    [DisplayName("Mã kết nối")]
    long Id { get; set; }

    [DisplayName("Tên thiết bị")]
    string? NameDevice { get; set; }
    [DisplayName("Mã kết nối")]
    string? Code { get; set; } 

    [Browsable(false)]
    [DisplayName("Chuẩn giao tiếp")]
    public eCommunicationType eCommunicationType { get; set; }

    [DisplayName("Chuẩn giải mã")]
    [JsonConverter(typeof(StringEnumConverter))]
    [Browsable(false)]
    public EnumModeCommunication eModeCommunication { get; set; }

    [Browsable(false)]
    [DisplayName("Tự động kết nối")]
    bool AutoConnect { get; set; }

    [DisplayName("Thời gian Timeout")]
    int? TimeoutMs { get; set; }
  }
}
