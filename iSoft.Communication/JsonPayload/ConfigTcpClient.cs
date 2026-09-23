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
  public class ConfigTcpClient : IConfigJson
  {
    [Browsable(false)]
    [DisplayName("Mã kết nối")]
    public long Id { get; set; } = 0;

    [DisplayName("Tên thiết bị")]
    public string? NameDevice { get; set; }

    [Browsable(false)]
    [DisplayName("Chuẩn kết nối")]
    [JsonConverter(typeof(StringEnumConverter))]
    public eCommunicationType eCommunicationType { get; set; } = eCommunicationType.TcpClient;

    [DisplayName("Chuẩn giải mã")]
    [JsonConverter(typeof(StringEnumConverter))]
    [Browsable(false)]
    public EnumModeCommunication eModeCommunication { get; set; } = EnumModeCommunication.Digi;

    [Browsable(false)]
    [DisplayName("Tự động kết nối")]
    public bool AutoConnect { get; set; } = false;

    [DisplayName("Thời gian Timeout (ms)")]
    public int? TimeoutMs { get; set; } = 1000;


    [DisplayName("Host")]
    public string? Host { get; set; } = "127.0.0.1";

    [DisplayName("Port")]
    public int Port { get; set; } = 4305;

    [DisplayName("Ssl")]
    public bool Ssl { get; set; } = false;

    [DisplayName("Gửi lệnh lấy data")]
    public bool Request { get; set; } = false;
    [DisplayName("Thời gian Gửi lệnh")]
    public int TimeRequest { get; set; } = 200;


    [DisplayName("Mã kết nối")]
    [ReadOnly(true)]
    public string? Code { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");
  }
}
