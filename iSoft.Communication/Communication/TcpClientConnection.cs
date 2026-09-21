using iSoft.Communication.Interface;
using SuperSimpleTcp;
using System.Net.NetworkInformation;
using System.Text;
using static iSoft.Communication.EnumCommunication;
using static System.Net.Mime.MediaTypeNames;

namespace iSoft.Communication.Communication
{
  public class TcpClientConnection : ConnectionBase
  {
    string _ServerIp;
    int _ServerPort;
    bool _Ssl;
    string ConnectionId;
    public string Name;
    SimpleTcpClient? _Client;
    public bool Connected => _Client != null && _Client.IsConnected;
    public event EventHandler<ConnectionEventArgs> OnConnectionEventRaise;
    public event EventHandler<DataReceivedEventArgs> OnDataReceive;
    public MessageDataInput _messageDataInput { get; set; } = new MessageDataInput();
    public TcpClientConnection(string id, Guid? machineId, EnumModeCommunication eModeCommunication, eDevice eDevice, string nameDevice,
                              string host, int port, bool requestGetData, int intervalRequestGetData, bool ssl = false,
                              int timeout = 5000, bool autoConnect = true
                              ) : base(id, machineId, eModeCommunication, eDevice, nameDevice, timeout, autoConnect, requestGetData, intervalRequestGetData)
    {
      Init(host, port, nameDevice, ssl);

      this.EModeCommunication = eModeCommunication;
      this.ConnectionId = id;
    }

    public void Init(string host, int port, string Name, bool _ssl = false)
    {
      _ServerIp = host;
      _ServerPort = port;
      _Ssl = _ssl;
      this.Name = Name;
      _Client = new SimpleTcpClient(_ServerIp, _ServerPort);

      _Client.Events.Connected += ConnectedHandler;
      _Client.Events.Disconnected += Disconnected;
      _Client.Events.DataReceived += Client_DataReceived;
      _Client.Events.DataSent += DataSent;
      _Client.Keepalive.EnableTcpKeepAlives = true;
      _Client.Settings.MutuallyAuthenticate = false;
      _Client.Settings.AcceptInvalidCertificates = true;
      _Client.Settings.ConnectTimeoutMs = 300;
      _Client.Settings.NoDelay = true;
    }
    public override void Connect()
    {
      if (!CanPingServer())
      {
        OnConnectionStatusChanged(false);
        return;
      }

      try
      {
        if (_Client is null)
          Init(_ServerIp, _ServerPort, Name, _Ssl);

        if (!_Client.IsConnected)
          _Client.Connect();
        IsConnected = _Client.IsConnected;
      }
      catch (Exception)
      {
        OnConnectionStatusChanged(false);
      }
    }

    private bool CanPingServer()
    {
      try
      {
        using var ping = new Ping();
        int pingTimeout = Math.Clamp(Timeout, 100, 1000);
        PingReply reply = ping.Send(_ServerIp, pingTimeout);
        return reply.Status == IPStatus.Success;
      }
      catch
      {
        return false;
      }
    }

    public override void Disconnect()
    {
      if (_Client!=null)
      {
        _Client.Disconnect();
        _Client.Dispose();
        _Client = null;
      }  
      OnConnectionStatusChanged(false);
    }

    public override async void SendData(string data)
    {
      byte[] byteArray = Encoding.ASCII.GetBytes(data);
      await SendAsync(byteArray);
    }



    private void ConnectedHandler(object? sender, ConnectionEventArgs e)
    {
      Console.WriteLine("*** Server " + e.IpPort + " connected");
      OnConnectionStatusChanged(true);
      OnConnectionEventRaise?.Invoke(sender, e);
    }

    public void Disconnected(object? sender, ConnectionEventArgs e)
    {
      Console.WriteLine("*** Server " + e.IpPort + " disconnected");
      OnConnectionStatusChanged(false);
      OnConnectionEventRaise?.Invoke(sender, e);
    }


    private StringBuilder _receiveBuffer = new StringBuilder();
    private void Client_DataReceived(object? sender, DataReceivedEventArgs e)
    {
      string chunk = Encoding.UTF8.GetString(e.Data);
      _receiveBuffer.Append(chunk);

      string buffer = _receiveBuffer.ToString();

      int index;
      while ((index = buffer.IndexOf("\n")) >= 0)
      {
        try
        {
          string fullMessage = buffer.Substring(0, index).Trim();
          buffer = buffer.Substring(index + 1);
          byte[] dataByte = System.Text.Encoding.UTF8.GetBytes(fullMessage);

          _messageDataInput.Source = this.ConnectionId;
          _messageDataInput.MachineId = this.MachineId;
          _messageDataInput.DataAsString = fullMessage;
          _messageDataInput.DataAsBytes = dataByte;
          _messageDataInput.SourceDateTime = DateTime.Now;
          _messageDataInput.eValueWeightType = EnumValueWeightType.Tare;
          _messageDataInput.eModeCommunication = this.EModeCommunication;

          OnDataReceived(_messageDataInput, EnumModeCommunication.SICS);
        }
        catch (Exception)
        {
          throw;
        }
        
      }

      _receiveBuffer.Clear();
      _receiveBuffer.Append(buffer);
    }



    




    //public void DataReceived(object? sender, DataReceivedEventArgs e)
    //{
    //  string msg = Encoding.UTF8.GetString(e.Data);
    //  byte[] responseBuffer = e.Data.ToArray();

    //  if (responseBuffer == null) return;
    //  if (responseBuffer.Count() <= 0) return;

    //  if (this.EModeCommunication == eModeCommunication.SICS)
    //  {
    //    string response = Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length);
    //    byte[] dataByte = System.Text.Encoding.UTF8.GetBytes(response);

    //    string[] parts = response.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

    //    if (parts.Length >= 2)
    //    {
    //      if (parts[1] == "I")
    //      {
    //        Disconnect();
    //        Connect();
    //      }
    //      else
    //      {
    //        if (parts[0]=="TA")
    //        {
    //          _messageDataInput.DataAsString = response;
    //          _messageDataInput.DataAsBytes = dataByte;
    //          _messageDataInput.SourceDateTime = DateTime.Now;
    //          _messageDataInput.eValueWeightType = eValueWeightType.Tare;
    //          _messageDataInput.eModeCommunication = this.EModeCommunication;
    //          OnDataReceived(_messageDataInput);
    //        }
    //        else if (parts[0] == "S")
    //        {
    //          _messageDataInput.DataAsString = response;
    //          _messageDataInput.DataAsBytes = dataByte;
    //          _messageDataInput.SourceDateTime = DateTime.Now;
    //          _messageDataInput.eValueWeightType = eValueWeightType.Net;
    //          _messageDataInput.eModeCommunication = this.EModeCommunication;
    //          OnDataReceived(_messageDataInput);
    //        }
    //      }
    //    }
    //  }
    //  else if (this.EModeCommunication == eModeCommunication.SCOD)
    //  {
    //    string response = Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length);
    //    _messageDataInput.eModeCommunication = this.EModeCommunication;
    //    _messageDataInput.DataAsString = response;
    //    _messageDataInput.DataAsBytes = responseBuffer;
    //    _messageDataInput.SourceDateTime = DateTime.Now;

    //    OnDataReceived(_messageDataInput);
    //  }
    //  else if (this.EModeCommunication == eModeCommunication.OHAUS)
    //  {
    //    string response = Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length);
    //    _messageDataInput.eModeCommunication = this.EModeCommunication;
    //    _messageDataInput.DataAsString = response;
    //    _messageDataInput.DataAsBytes = responseBuffer;
    //    _messageDataInput.SourceDateTime = DateTime.Now;

    //    OnDataReceived(_messageDataInput);
    //  }
    //}

    private static void DataSent(object? sender, DataSentEventArgs e)
    {
      //Console.WriteLine("[" + e.IpPort + "] sent " + e.BytesSent + " bytes");
    }

    public async Task SendAsync(byte[] data)
    {
      try
      {
        if (_Client == null || !_Client.IsConnected) throw new Exception("Socket Server Can Not Be Connect");
        await _Client.SendAsync(data);
      }
      catch (Exception)
      {
        //TODO
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
