using iSoft.Communication.Interface;
using iSoft.Communication.Preprocessing;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iSoft.Communication.EnumCommunication;
using static System.Net.Mime.MediaTypeNames;

namespace iSoft.Communication.Serial
{
  public class SerialConnection : ConnectionBase
  {
    private SerialPort _serialPort;
    public MessageDataInput _messageDataInput { get; set; } = new MessageDataInput();
    private System.Timers.Timer _timerSendStatusConnect = new System.Timers.Timer();
    private string[] _ports = new string[10];
    private EnumValueWeightType _eValueWeightType = EnumValueWeightType.Net;
    public SerialConnection(string id,Guid? machineId, EnumModeCommunication eModeCommunication, eDevice eTypeInput, string nameDevice,
                                      string portName, int baudRate, Parity parity, 
                                      int dataBits, StopBits stopBits, int timeout = 5000, bool autoConnect = true,
                                      bool requestGetData = false, int intervalRequestGetData=200)
                            : base(id, machineId, eModeCommunication, eTypeInput, nameDevice, timeout, autoConnect, requestGetData, intervalRequestGetData)
    {
      _serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
      _serialPort.DataReceived += SerialPort_DataReceived;

      NameDevice = nameDevice;
      _messageDataInput.Source = id;
      _messageDataInput.NameDevice = nameDevice;
      _messageDataInput.eModeCommunication = eModeCommunication;
      _messageDataInput.eValueWeightType = EnumValueWeightType.Net;

      _timerSendStatusConnect.Interval = 1000;
      _timerSendStatusConnect.Elapsed += TimerSendStatusConnect_Elapsed;
      _timerSendStatusConnect.Start();
    }

    private void TimerSendStatusConnect_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
      try
      {
        _timerSendStatusConnect.Stop();
        if (_serialPort!=null)
        {
          OnConnectionStatusChanged(_serialPort.IsOpen);
        }
        else
        {
          OnConnectionStatusChanged(false);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        _timerSendStatusConnect.Start();
      }
    }


    public override void Connect()
    {
      try
      {
        if (!_serialPort.IsOpen)
        {
          _ports = SerialPort.GetPortNames();
          if (_ports.Contains(_serialPort.PortName))
          {
            _serialPort.Open();
          }
        }

        OnConnectionStatusChanged(_serialPort.IsOpen);
      }
      catch (Exception ex)
      {
        OnConnectionStatusChanged(false);
        throw ex;
      }
    }

    public override void Disconnect()
    {
      try
      {
        if (_serialPort.IsOpen)
        {
          _serialPort.Close();
          OnConnectionStatusChanged(false);
        }
      }
      catch (Exception ex)
      {
        OnConnectionStatusChanged(false);
        throw ex;
      }
    }

    public override void SendData(string data)
    {
      try
      {
        if (_serialPort != null && _serialPort.IsOpen)
        {
          _serialPort.WriteLine(data);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }



    private readonly object _lock = new object();
    private byte[] _latestData = Array.Empty<byte>();
    private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      try
      {
        //string recievedStringData = _serialPort.ReadLine();
        string recievedStringData = _serialPort?.ReadTo("\r");
        string data = _serialPort.ReadExisting();
        byte[] bytes = new byte[16];

        if (!string.IsNullOrEmpty(recievedStringData))
        {
          _messageDataInput.MachineId = this.MachineId;
          _messageDataInput.DataAsString = recievedStringData;
          _messageDataInput.DataAsBytes = Encoding.UTF8.GetBytes(recievedStringData);
          _messageDataInput.SourceDateTime = DateTime.Now;
          OnDataReceived(_messageDataInput, EnumModeCommunication.SCOD);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public override void Tare()
    {
      SendData("T\r\n");
    }

    public override void Zero()
    {
      SendData("Z\r\n");
    }
  }
}
