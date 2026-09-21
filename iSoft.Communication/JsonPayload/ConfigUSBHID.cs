using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iSoft.Communication.EnumCommunication;

namespace iSoft.Communication.JsonPayload
{
  public class ConfigUSBHID : IConfigJson
  {
    [Browsable(false)]
    public long Id { get; set; } = 0;
    [DisplayName("Tên thiết bị")]
    public string? NameDevice { get; set; }
    [Browsable(false)]
    public bool AutoConnect { get; set; } = false;
    [DisplayName("Thời gian Timeout (ms)")]
    public int? TimeoutMs { get; set; } = 1000;

    [Browsable(false)]
    public eCommunicationType eCommunicationType { get; set; } = eCommunicationType.USBHID;


    [DisplayName("Chuẩn giải mã")]
    [JsonConverter(typeof(StringEnumConverter))]
    [Browsable(false)]
    public EnumModeCommunication eModeCommunication { get; set; } = EnumModeCommunication.Digi;



    [DisplayName("Mã kết nối")]
    [ReadOnly(true)]
    public string? Code { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");
  }
}
