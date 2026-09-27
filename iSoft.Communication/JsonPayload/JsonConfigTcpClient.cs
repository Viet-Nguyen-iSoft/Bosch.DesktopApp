using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iSoft.Communication.EnumCommunication;
using Newtonsoft.Json;

namespace iSoft.Communication.JsonPayload
{
  public class JsonConfigTcpClient
  {
    [DisplayName("Chuẩn giải mã")]
    [JsonConverter(typeof(StringEnumConverter))]
    public EnumModeCommunication eModeCommunication { get; set; } =
      EnumModeCommunication.SICS;

    [Browsable(false)]
    [DisplayName("Mã kết nối")]
    public Guid Id { get; set; }

    [DisplayName("Tên thiết bị")]
    public string? NameDevice { get; set; }


    [DisplayName("Host")]
    public string? Host { get; set; } = "127.0.0.1";

    [DisplayName("Port")]
    public int Port { get; set; } = 4305;


    [DisplayName("Tự động kết nối")]
    public bool AutoConnect { get; set; } = false;

    [DisplayName("Thời gian Timeout (ms)")]
    public int? TimeoutMs { get; set; } = 1000;

    [DisplayName("Gửi lệnh lấy data")]
    public bool Request { get; set; } = false;
    [DisplayName("Thời gian Gửi lệnh")]
    public int TimeRequest { get; set; } = 200;
  }
}
