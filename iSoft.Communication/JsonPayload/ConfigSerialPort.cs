using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static iSoft.Communication.EnumCommunication;
using JsonConverter = Newtonsoft.Json.JsonConverter;
using JsonConverterAttribute = Newtonsoft.Json.JsonConverterAttribute;

namespace iSoft.Communication.JsonPayload
{
  public class ConfigSerialPort : IConfigJson
  {
    [Browsable(false)]
    [DisplayName("Mã kết nối")]
    public long Id { get; set; } = 0;

    [DisplayName("Tên thiết bị")]
    public string? NameDevice { get; set; }

    [Browsable(false)]
    [DisplayName("Chuẩn kết nối")]
    [JsonConverter(typeof(StringEnumConverter))]
    public eCommunicationType eCommunicationType { get; set; } = eCommunicationType.SerialPort;

    [DisplayName("Chuẩn giải mã")]
    [JsonConverter(typeof(StringEnumConverter))]
    [Browsable(false)]
    public EnumModeCommunication eModeCommunication { get; set; } = EnumModeCommunication.Digi;

    [Browsable(false)]
    [DisplayName("Tự động kết nối")]
    public bool AutoConnect { get; set; } = false;

    [DisplayName("Thời gian Timeout (ms)")]
    public int? TimeoutMs { get; set; } = 1000;



    [DisplayName("Port")]
    public string? PortName { get; set; } = "COM1";


    [DisplayName("BaudRate")]
    public int BaudRate { get; set; } = 9600;


    [DisplayName("Parity")]
    [JsonConverter(typeof(StringEnumConverter))]
    public Parity Parity { get; set; } = Parity.None;


    [DisplayName("DataBits")]
    public int DataBits { get; set; } = 8;


    [DisplayName("StopBits")]
    [JsonConverter(typeof(StringEnumConverter))]
    public StopBits StopBits { get; set; } = StopBits.One;

    [DisplayName("Mã kết nối")]
    [ReadOnly(true)]
    public string? Code { get; set; } = DateTime.Now.ToString("yyyyMMddHHmmss");




    [DisplayName("Gửi lệnh lấy data")]
    public bool Request { get; set; } = false;
    [DisplayName("Thời gian Gửi lệnh")]
    public int TimeRequest { get; set; } = 200;
  }
}
