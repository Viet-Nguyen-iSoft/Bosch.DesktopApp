using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Communication.JsonPayload
{
  public class JsonConfigTcpSerial
  {
    [Browsable(false)]
    [DisplayName("Mã kết nối")]
    public Guid Id { get; set; }

    [DisplayName("Tên thiết bị")]
    public string? NameDevice { get; set; }


    [DisplayName("COM")]
    public string? COM { get; set; }

    [DisplayName("BaudRate")]
    public int BaudRate { get; set; } = 9600;

    [DisplayName("Data Bits")]
    public int DataBits { get; set; } = 8;

    [DisplayName("Stop Bits")]
    [JsonConverter(typeof(StringEnumConverter))]
    public StopBits StopBits { get; set; } = StopBits.One;

    [DisplayName("Parity")]
    [JsonConverter(typeof(StringEnumConverter))]
    public Parity Parity { get; set; } = Parity.None;


    [DisplayName("Tự động kết nối")]
    public bool AutoConnect { get; set; } = false;


    [DisplayName("Gửi lệnh lấy data")]
    public bool Request { get; set; } = false;


    [DisplayName("Thời gian Gửi lệnh")]
    public int TimeRequest { get; set; } = 200;
  }
}
