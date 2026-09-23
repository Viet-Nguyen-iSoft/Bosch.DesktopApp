using iSoft.Communication.Interface;
using iSoft.Communication.USB;
using KeysStrokerLib;
using KeysStrokerLib.Entities;
using KeysStrokerLib.Entities.CallbackObjects;
using KeysStrokerLib.Events;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iSoft.Communication.EnumCommunication;
using Keystroker = iSoft.Communication.USB.Keystroker;

namespace iSoft.Communication.Communication
{
  public class USBHIDConnection : ConnectionBase
  {
    private Keystroker _connection;
    private System.Timers.Timer TimerSendStatusConnect = new System.Timers.Timer();
    public MessageDataInput _messageDataInput { get; set; } = new MessageDataInput();
    public USBHIDConnection(string id, 
                              Guid? machineId,
                              EnumCommunication.EnumModeCommunication eModeCommunication, 
                              eDevice eTypeInput,
                              string nameDevice, 
                              int timeout = 5000, 
                              bool autoConnect = true) : base(id, machineId, eModeCommunication, eTypeInput, nameDevice, timeout, autoConnect)
    {
      _connection = new Keystroker();
      _connection.OnFlushKeysInputEvent += _connection_OnFlushKeysInputEvent;

      _messageDataInput.Source = id;
      _messageDataInput.eModeCommunication = eModeCommunication;
      _messageDataInput.eValueWeightType = EnumValueWeightType.Net;

      this.NameDevice = nameDevice;
      this.AutoConnect = autoConnect;

      TimerSendStatusConnect.Interval = 1000;
      TimerSendStatusConnect.Elapsed += TimerSendStatusConnect_Elapsed;
    }

    private void TimerSendStatusConnect_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
      try
      {
        TimerSendStatusConnect.Stop();
        if (_connection != null)
        {
          OnConnectionStatusChanged(_connection.IsRunning);
        }
        else
        {
          OnConnectionStatusChanged(false);
        }
      }
      catch (Exception ex)
      {
        //TODO
      }
      finally
      {
        TimerSendStatusConnect.Start();
      }
    }

    private void _connection_OnFlushKeysInputEvent(object? sender, KeyStrokerEventArgs e)
    {
      try
      {
        string recievedStringData = e?.DataAsString;
        if (!string.IsNullOrEmpty(recievedStringData))
        { 
          //_messageDataInput.DataAsString = recievedStringData;
          //_messageDataInput.MachineId = this.MachineId;
          //_messageDataInput.DataAsBytes = Encoding.UTF8.GetBytes(recievedStringData);
          //_messageDataInput.SourceDateTime = DateTime.Now;

          //OnDataReceived(_messageDataInput);
        }
      }
      catch (Exception ex)
      {
        //TODO
      }
    }


    public override void Connect()
    {
      _connection = new Keystroker();
      _connection.OnFlushKeysInputEvent += _connection_OnFlushKeysInputEvent;
      _connection.Start();
      TimerSendStatusConnect.Start();
    }

    public override void Disconnect()
    {
      try
      {
        if (_connection!=null)
        {
          if (_connection.IsRunning)
          {
            TimerSendStatusConnect.Stop();
            _connection.OnFlushKeysInputEvent -= _connection_OnFlushKeysInputEvent;
            _connection.Stop();
            _connection.Dispose();
            OnConnectionStatusChanged(false);
          }
        }
        else
        {
          OnConnectionStatusChanged(false);
        }  
      }
      catch
      {
        OnConnectionStatusChanged(false);
      }
    }

    public override void SendData(string data)
    {

    }

    public override void Tare()
    {
      throw new NotImplementedException();
    }

    public override void Zero()
    {
      throw new NotImplementedException();
    }
  }
}
