using iSoft.Communication.Mode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static iSoft.Communication.EnumCommunication;

namespace iSoft.Communication.Interface
{
  public interface IScaleConnection
  {
    string Id { get; }
    bool IsConnected { get; }
    bool IsRequestGetData { get; set; }
    bool AutoConnect { get; set; }
    int Timeout { get; set; }
    EnumModeCommunication EModeCommunication { get; set; }
    eDevice ETypeInput { get; set; }
    ActiveWeighingStatus ActiveWeighingStatus { get; set; }



    event EventHandler<DataWeightInterface> DataReceived;
    event EventHandler<bool> ConnectionStatusChanged;
    event EventHandler<ActiveWeighingStatus> OnActiveWeighingStatusChangeEvent;

    void Connect();
    void Disconnect();
    void Reconnect();
    void Start();
    void Stop();
    void RequestGetData();
    void SendData(string data);

    void Tare();
    void Zero();
  }
}
