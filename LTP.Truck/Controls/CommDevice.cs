using HelperManager;
using iSoft.Communication.Communication;
using iSoft.Communication.Interface;
using iSoft.Communication.JsonPayload;
using iSoft.Communication.Mode;
using iSoft.Database.Models;
using System.Net.NetworkInformation;
using static HelperManager.EnumData;
using static iSoft.Communication.EnumCommunication;

namespace LTP.Truck.Controls
{
  public partial class AppCore
  {
    public event EventHandler<DataWeightInterface>? OnSendDataWeight;
    public event EventHandler<CommunicationStatusChangedEventArgs>? OnSendStatusWeight;
    public event EventHandler<EnumStatusConnectTcp>? OnSendStatusServer;
    private const string ScaleId = "SCALE_01";

    private readonly ICommunicationService _communication =
        new CommunicationService();
    private bool _communicationEventsSubscribed;

    public void ConnectWeight(Connection? connection = null)
    {
      SubscribeCommunicationEvents();

      _communication.RemoveConnection(ScaleId);
      if (connection != null)
        _connection = connection;

      if (_connection == null)
      {
        SendWeightConnectionStatus(false);
        return;
      }

      try
      {
        IConfigJson? config = _connection.EnumCommunicationType switch
        {
          EnumCommunicationType.TcpClient => CreateTcpWeightConfig(_connection),
          EnumCommunicationType.SerialPort => CreateSerialWeightConfig(_connection),
          _ => null
        };

        if (config == null)
        {
          SendWeightConnectionStatus(false);
          return;
        }

        _communication.AddConnection(
          config,
          machineId: null,
          device: eDevice.Weight);
      }
      catch (Exception ex)
      {
        _communication.RemoveConnection(ScaleId);
        SendWeightConnectionStatus(false);
        LogHelper.LogErrorToFileLog(ex, _folderFileLog);
      }
    }

    public void DisconnectWeight()
    {
      _communication.RemoveConnection(ScaleId);
      _connection = null;
      SendWeightConnectionStatus(false);
    }

    private void SubscribeCommunicationEvents()
    {
      if (_communicationEventsSubscribed)
        return;

      _communication.DataWeightInterface += Communication_DataReceived;
      _communication.ConnectionStatusChanged += Communication_StatusChanged;
      _communicationEventsSubscribed = true;
    }

    private static ConfigTcpClient? CreateTcpWeightConfig(Connection connection)
    {
      var configData = JsonHelper.FromJson<JsonConfigTcpClient>(
        connection.JsonStrConfig ?? string.Empty);
      if (configData == null)
        return null;

      return new ConfigTcpClient
      {
        Code = ScaleId,
        NameDevice = connection.Name ?? "Cân TCP",
        Host = configData.Host,
        Port = configData.Port,
        eModeCommunication = EnumModeCommunication.SICS,
        AutoConnect = configData.AutoConnect,
        TimeoutMs = configData.TimeoutMs,
        Request = configData.Request,
        TimeRequest = configData.TimeRequest
      };
    }

    private static ConfigSerialPort? CreateSerialWeightConfig(Connection connection)
    {
      var configData = JsonHelper.FromJson<JsonConfigTcpSerial>(
        connection.JsonStrConfig ?? string.Empty);
      if (configData == null || string.IsNullOrWhiteSpace(configData.COM))
        return null;

      return new ConfigSerialPort
      {
        Code = ScaleId,
        NameDevice = connection.Name ?? "Cân Serial",
        PortName = configData.COM,
        BaudRate = configData.BaudRate,
        DataBits = configData.DataBits,
        StopBits = configData.StopBits,
        Parity = configData.Parity,
        AutoConnect = configData.AutoConnect,
        Request = configData.Request,
        TimeRequest = configData.TimeRequest
      };
    }

    private void SendWeightConnectionStatus(bool isConnected)
    {
      Communication_StatusChanged(
        this,
        new CommunicationStatusChangedEventArgs(ScaleId, isConnected));
    }

    private void Communication_DataReceived(
       object? sender,
        DataWeightInterface dataWeightInterface)
    {
      OnSendDataWeight?.Invoke(sender, dataWeightInterface);
    }

    private void Communication_StatusChanged(
        object? sender,
        CommunicationStatusChangedEventArgs e)
    {
      OnSendStatusWeight?.Invoke(sender, e);
    }



    #region Check kết nối Server
    public System.Timers.Timer _timerCheckConnectServer = new System.Timers.Timer();
    public void CheckConnectServer()
    {
      _timerCheckConnectServer.Interval = 2000;
      _timerCheckConnectServer.Elapsed += TimerCheckConnectServer_Elapsed;
      _timerCheckConnectServer.Start();
    }

    private void TimerCheckConnectServer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
      try
      {
        _timerCheckConnectServer.Stop();

        EnumStatusConnectTcp enumStatusConnectCurrent = EnumStatusConnectTcp.Disconnect;
        if (_appConfig != null)
        {
          var rsPing = CanPingServer(_appConfig?.IpServer??string.Empty, _appConfig?.PortServer ?? 8000, _appConfig?.TimeoutConnectServer ?? 500);
          if (rsPing)
          {
            enumStatusConnectCurrent = EnumStatusConnectTcp.Connect;
          }
          else
          {
            enumStatusConnectCurrent = EnumStatusConnectTcp.Disconnect;
          }
        }

        OnSendStatusServer?.Invoke(sender, enumStatusConnectCurrent);
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
      finally
      {
        _timerCheckConnectServer.Start();
      }
    }
    #endregion
    public bool CanPingServer(string ip, int port, int timout)
    {
      try
      {
        using var ping = new Ping();
        int pingTimeout = Math.Clamp(timout, 100, 1000);
        PingReply reply = ping.Send(ip, pingTimeout);
        return reply.Status == IPStatus.Success;
      }
      catch
      {
        return false;
      }
    }
  }
}
