using iSoft.Communication.Mode;
using static iSoft.Communication.EnumCommunication;

namespace iSoft.Communication.Interface
{
  public abstract class ConnectionBase : IScaleConnection
  {
    public string Id { get; protected set; }
    public Guid? MachineId { get; protected set; }
    public string NameDevice { get; protected set; }
    public bool IsConnected { get; protected set; }
    public bool AutoConnect { get; set; }
    public int Timeout { get; set; }
    public EnumModeCommunication EModeCommunication { get; set; }
    public ActiveWeighingStatus ActiveWeighingStatus { get; set; }
    public eDevice ETypeInput { get; set; }
    public EnumValueWeightType EValueWeightType { get; set; } = EnumValueWeightType.Net;

    public bool IsRequestGetData { get; set; } = false;
    public int IntervalRequestGetData { get; set; } = 200;

    public DataWeightInterface DataWeightInterface { get;set; } = new DataWeightInterface();
    public event EventHandler<DataWeightInterface> DataWeightReceived;
    public event EventHandler<bool> ConnectionStatusChanged;
    public event EventHandler<ActiveWeighingStatus> OnActiveWeighingStatusChangeEvent;

    private System.Timers.Timer TimerAutoConnect = new System.Timers.Timer();
    private System.Timers.Timer TimeRequestGetData = new System.Timers.Timer();

    private StandardContinuousOutputData _standardContinuousOutputData;
    public ConnectionBase(string id, Guid? machineId, EnumModeCommunication eModeCommunication, eDevice eTypeInput, string nameDevice, int timeout = 5000, bool autoConnect = true, bool requestGetData = false, int intervalRequestGetData = 200)
    {
      this.Id = id;
      this.MachineId = machineId;
      this.Timeout = timeout;
      this.AutoConnect = autoConnect;
      this.EModeCommunication = eModeCommunication;
      this.NameDevice = nameDevice;
      this.ETypeInput = eTypeInput;
      this.IsRequestGetData = requestGetData;
      this.IntervalRequestGetData = intervalRequestGetData;

      TimerAutoConnect.Elapsed += TimerAutoConnect_Elapsed;
      TimeRequestGetData.Elapsed += TimeRequestGetData_Elapsed;
    }

    event EventHandler<DataWeightInterface> IScaleConnection.DataReceived
    {
      add => DataWeightReceived += value;
      remove => DataWeightReceived -= value;
    }

    public abstract void Connect();
    public abstract void Disconnect();
    public abstract void SendData(string data);
    public abstract void Tare();
    public abstract void Zero();

    public virtual void Reconnect()
    {
      Disconnect();
      Connect();
      if (IsConnected && IsRequestGetData)
        RequestGetData();
    }

    public virtual void Start()
    {
      TimerAutoConnect.Interval = Timeout;

      // Luôn thử kết nối một lần khi khởi động. AutoConnect chỉ
      // quyết định việc kiểm tra và kết nối lại theo chu kỳ.
      Connect();
      if (IsConnected && IsRequestGetData)
        RequestGetData();

      if (AutoConnect)
        TimerAutoConnect.Start();
    }
    public virtual void Stop()
    {
      TimerAutoConnect.Stop();
      TimeRequestGetData.Stop();
    }

    private void TimerAutoConnect_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
      try
      {
        this.TimerAutoConnect.Stop();

        ConnectionStatusChanged?.Invoke(this, IsConnected);
        if (!IsConnected && this.AutoConnect)
        {
          Reconnect();
        }
      }
      catch (Exception)
      {
        //LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
      finally
      {
        if (AutoConnect)
          this.TimerAutoConnect.Start();
      }
    }

    public virtual void RequestGetData()
    {
      IsRequestGetData = true;
      if (IsRequestGetData)
      {
        TimeRequestGetData.Interval = IntervalRequestGetData;
        TimeRequestGetData.Start();
      }
    }

    private void TimeRequestGetData_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
      try
      {
        TimeRequestGetData.Stop();
        //if (EValueWeightType == eValueWeightType.All)
        //{
        //  SendData("SI\r\n");
        //  EValueWeightType = eValueWeightType.All;
        //}
        //if (EValueWeightType == eValueWeightType.All)
        //{
        //  SendData("SXI\r\n");
        //  EValueWeightType = eValueWeightType.All;
        //}
        if (EValueWeightType == EnumValueWeightType.Net)
        {
          SendData("SI\r\n");
          EValueWeightType = EnumValueWeightType.Tare;
        }
        else if (EValueWeightType == EnumValueWeightType.Tare)
        {
          SendData("TA\r\n");
          EValueWeightType = EnumValueWeightType.Net;
        }
      }
      catch (Exception)
      {
        throw;
      }
      finally
      {
        TimeRequestGetData.Start();
      }
    }

    protected virtual void OnDataReceived(MessageDataInput messageDataInput, EnumModeCommunication eModeCommunication)
    {
      try
      {
        if (eModeCommunication == EnumModeCommunication.SICS)
        {
          var data = SicsOutputData.Decode(messageDataInput?.DataAsString ?? string.Empty);
          if (data != null)
          {
            if (data.EValueWeightType == EnumValueWeightType.Net)
            {
              DataWeightInterface.IndicatedWeight = data.IndicatedWeight ?? 0.0;
            }
            else if (data.EValueWeightType == EnumValueWeightType.Tare)
            {
              DataWeightInterface.TareWeight = data.TareWeight ?? 0.0;
            }
            else
            {
              DataWeightInterface.IndicatedWeight = 0.0;
              DataWeightInterface.TareWeight = 0.0;
            }
            DataWeightInterface.Unit = data.Unit;
            DataWeightInterface.ActiveWeighingStatus = data.ActiveWeighingStatus;
            DataWeightReceived?.Invoke(this, DataWeightInterface);
          }
        }  
        else if (eModeCommunication == EnumModeCommunication.SCOD)
        {
          var data = StandardContinuousOutputData.Decode(messageDataInput?.DataAsBytes);
          if (data != null)
          {
            DataWeightInterface.IndicatedWeight = data?.IndicatedWeight ?? 0.0;
            DataWeightInterface.TareWeight = data?.TareWeight ?? 0.0;
            DataWeightInterface.Unit = data?.Unit ?? UnitOfWeight.None;
            DataWeightInterface.ActiveWeighingStatus = data?.StatusB.ActiveWeighingStatus?? ActiveWeighingStatus.Default;
            DataWeightReceived?.Invoke(this, DataWeightInterface);
          }
        }
        else if (eModeCommunication == EnumModeCommunication.Continuous)
        {
          
        }
      }
      catch (Exception)
      {
        throw;
      }
    }

    protected virtual void OnConnectionStatusChanged(bool isConnected)
    {
      IsConnected = isConnected;
      ConnectionStatusChanged?.Invoke(this, isConnected);
    }

  }
}
