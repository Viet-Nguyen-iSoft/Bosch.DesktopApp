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
    public eModeCommunication EModeCommunication { get; set; }
    public ActiveWeighingStatus ActiveWeighingStatus { get; set; }
    public eDevice ETypeInput { get; set; }
    public eValueWeightType EValueWeightType { get; set; } = eValueWeightType.Net;

    public bool IsRequestGetData { get; set; } = false;
    public int IntervalRequestGetData { get; set; } = 200;

    public MessageDataOutput MessageDataOutput { get; set; } = new MessageDataOutput();


    public event EventHandler<MessageDataOutput> DataReceived;
    public event EventHandler<bool> ConnectionStatusChanged;
    public event EventHandler<ActiveWeighingStatus> OnActiveWeighingStatusChangeEvent;

    private System.Timers.Timer TimerAutoConnect = new System.Timers.Timer();
    private System.Timers.Timer TimeRequestGetData = new System.Timers.Timer();

    private StandardContinuousOutputData _standardContinuousOutputData;
    public ConnectionBase(string id, Guid? machineId, eModeCommunication eModeCommunication, eDevice eTypeInput, string nameDevice, int timeout = 5000, bool autoConnect = true, bool requestGetData = false, int intervalRequestGetData = 200)
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
        if (EValueWeightType == eValueWeightType.Net)
        {
          SendData("SI\r\n");
          EValueWeightType = eValueWeightType.Tare;
        }
        else if (EValueWeightType == eValueWeightType.Tare)
        {
          SendData("TA\r\n");
          EValueWeightType = eValueWeightType.Net;
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

    protected virtual void OnDataReceived(MessageDataInput messageDataInput)
    {
      try
      {
        var a = SicsOutputData.Decode(messageDataInput.DataAsString);



        var result = Processing(messageDataInput);
        if (result!=null)
        {
          if (result.eValueWeight == eValueWeightType.Tare)
          {
            this.MessageDataOutput.Source = result.Source;
            this.MessageDataOutput.MachineId = messageDataInput.MachineId;
            this.MessageDataOutput.NameDevice = result.NameDevice;
            this.MessageDataOutput.Tare = result.Tare;
            this.MessageDataOutput.eValueWeight = result.eValueWeight;
            this.MessageDataOutput.SourceDateTime = result.SourceDateTime;
            this.MessageDataOutput.DataAsBytes = result.DataAsBytes;
            this.MessageDataOutput.DataAsString = result.DataAsString;
          }
          else if (result.eValueWeight == eValueWeightType.Net)
          {
            this.MessageDataOutput.Source = result.Source;
            this.MessageDataOutput.MachineId = messageDataInput.MachineId;
            this.MessageDataOutput.NameDevice = result.NameDevice;
            this.MessageDataOutput.Net = result.Net;
            this.MessageDataOutput.eValueWeight = result.eValueWeight;
            this.MessageDataOutput.unitOfWeight = result.unitOfWeight;
            this.MessageDataOutput.SourceDateTime = result.SourceDateTime;
            this.MessageDataOutput.DataAsBytes = result.DataAsBytes;
            this.MessageDataOutput.DataAsString = result.DataAsString;
          }
          else
          {
            this.MessageDataOutput.Source = result.Source;
            this.MessageDataOutput.MachineId = messageDataInput.MachineId;
            this.MessageDataOutput.NameDevice = result.NameDevice;
            this.MessageDataOutput.Net = result.Net;
            this.MessageDataOutput.Tare = result.Tare;
            this.MessageDataOutput.eValueWeight = result.eValueWeight;
            this.MessageDataOutput.unitOfWeight = result.unitOfWeight;
            this.MessageDataOutput.SourceDateTime = result.SourceDateTime;
            this.MessageDataOutput.DataAsBytes = result.DataAsBytes;
            this.MessageDataOutput.DataAsString = result.DataAsString;
          }  
        }  

        DataReceived?.Invoke(this, this.MessageDataOutput);
      }
      catch (Exception ex)
      {
        //TODO
      }
    }

    //public static MessageDataOutput? DecodeSICS(string message)
    //{
    //  try
    //  {
    //    if (string.IsNullOrEmpty(message)) return null;

    //    MessageDataOutput sicsOutputData = new MessageDataOutput();
    //    string[] parts = message.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

    //    if (parts.Length < 4)
    //    {
    //      sicsOutputData.Net = 0.0;
    //      sicsOutputData.ActiveWeighingStatus = ActiveWeighingStatus.Default;
    //      sicsOutputData.EnumValueWeight = eValueWeightType.Net;
    //      sicsOutputData.UnitOfWeight = UnitOfWeight.None;
    //      sicsOutputData.DataAsString = message;
    //      sicsOutputData.SourceDateTime = DateTime.Now;
    //      return sicsOutputData;
    //    }

    //    string key = parts[0].Replace("\r", "").Replace("\n", "");
    //    if (key == "S")
    //    {
    //      sicsOutputData.EnumValueWeight = eValueWeightType.Net;
    //      if (parts[1] == "S")
    //      {
    //        sicsOutputData.Net = double.Parse(parts[2]);
    //        sicsOutputData.ActiveWeighingStatus = ActiveWeighingStatus.Stable;
    //        if (parts[3].Trim() == "kg")
    //        {
    //          sicsOutputData.UnitOfWeight = UnitOfWeight.Kilograms;
    //        }
    //      }
    //      else if (parts[1] == "D")
    //      {
    //        sicsOutputData.Net = double.Parse(parts[2]);
    //        sicsOutputData.ActiveWeighingStatus = ActiveWeighingStatus.Motion;
    //        if (parts[3].Trim() == "kg")
    //        {
    //          sicsOutputData.UnitOfWeight = UnitOfWeight.Kilograms;
    //        }
    //      }
    //      else if (parts[1] == "+")
    //      {
    //        sicsOutputData.Net = 0.0;
    //        sicsOutputData.Tare = 0.0;
    //        sicsOutputData.ActiveWeighingStatus = ActiveWeighingStatus.Overload;
    //        sicsOutputData.UnitOfWeight = UnitOfWeight.None;
    //      }
    //      else if (parts[1] == "-")
    //      {
    //        sicsOutputData.Net = 0.0;
    //        sicsOutputData.Tare = 0.0;
    //        sicsOutputData.ActiveWeighingStatus = ActiveWeighingStatus.Underload;
    //        sicsOutputData.UnitOfWeight = UnitOfWeight.None;
    //      }
    //    }
    //    else if (key == "TA")
    //    {
    //      sicsOutputData.EnumValueWeight = eValueWeightType.Tare;
    //      sicsOutputData.Tare = double.Parse(parts[2]);
    //    }

    //    return sicsOutputData;
    //  }
    //  catch (Exception)
    //  {
    //    return null;
    //  }
    //}

    protected virtual void OnConnectionStatusChanged(bool isConnected)
    {
      IsConnected = isConnected;
      ConnectionStatusChanged?.Invoke(this, isConnected);
    }

    public MessageDataOutput? Processing(MessageDataInput messsage)
    {
      try
      {
        MessageDataOutput messageDataOutput = new MessageDataOutput();
        double _IndicatedWeight = 0;
        double _IndicatedTare = 0;

        messageDataOutput.unitOfWeight = UnitOfWeight.Kilograms;

        switch (messsage.eModeCommunication)
        {
          case eModeCommunication.None:
            messageDataOutput.DataAsBytes = messsage?.DataAsBytes ?? new byte[255];
            messageDataOutput.DataAsString = messsage?.DataAsString ?? string.Empty;
            break;
          case eModeCommunication.SICS:
            messageDataOutput.DataAsBytes = messsage?.DataAsBytes ?? new byte[255];
            messageDataOutput.DataAsString = messsage?.DataAsString ?? string.Empty;
            var sicsFormatData = SicsOutputData.Decode(messageDataOutput?.DataAsString ?? string.Empty);
            if (sicsFormatData != null)
            {
              if (sicsFormatData.EValueWeightType == eValueWeightType.All)
              {
                messageDataOutput.Net = sicsFormatData.IndicatedWeight ?? 0;
                messageDataOutput.Tare = sicsFormatData.TareWeight ?? 0;
                messageDataOutput.ActiveWeighingStatus = sicsFormatData.ActiveWeighingStatus;
                messageDataOutput.unitOfWeight = sicsFormatData.Unit;
              }
              else if (sicsFormatData.EValueWeightType == eValueWeightType.Net)
              {
                messageDataOutput.Net = sicsFormatData.IndicatedWeight ?? 0.0;
                messageDataOutput.ActiveWeighingStatus = sicsFormatData.ActiveWeighingStatus;
                messageDataOutput.unitOfWeight = sicsFormatData.Unit;
                messageDataOutput.eValueWeight = eValueWeightType.Net;
              }
              else if (sicsFormatData.EValueWeightType == eValueWeightType.Tare)
              {
                messageDataOutput.Tare = sicsFormatData.TareWeight ?? 0.0;
                //messageDataOutput.activeWeighingStatus = sicsFormatData.ActiveWeighingStatus;
                //messageDataOutput.unitOfWeight = sicsFormatData.Unit;
                messageDataOutput.eValueWeight = eValueWeightType.Tare;
              }
            }
            else
            {
              return null;
            }
            break;
          case eModeCommunication.SCOD:
            StandardContinuousOutputData newStandardContinuousOutputData = new StandardContinuousOutputData();
            var dataBytes = messsage.DataAsBytes;
            var len = dataBytes.Length;

            newStandardContinuousOutputData = StandardContinuousOutputData.Decode(dataBytes, false);
            if (newStandardContinuousOutputData != null)
            {
              _IndicatedWeight = (newStandardContinuousOutputData?.IndicatedWeight == null) ? 0 : (double)newStandardContinuousOutputData.IndicatedWeight;
              _IndicatedTare = (newStandardContinuousOutputData?.TareWeight == null) ? 0 : (double)newStandardContinuousOutputData.TareWeight;

              messageDataOutput.Net = _IndicatedWeight;
              messageDataOutput.Tare = _IndicatedTare;

              if (this.ActiveWeighingStatus != newStandardContinuousOutputData?.StatusB.ActiveWeighingStatus)
                OnActiveWeighingStatusChangeEvent?.Invoke(this, newStandardContinuousOutputData.StatusB.ActiveWeighingStatus);

              this._standardContinuousOutputData = newStandardContinuousOutputData;
              this.ActiveWeighingStatus = _standardContinuousOutputData.StatusB.ActiveWeighingStatus;

              messageDataOutput.unitOfWeight = _standardContinuousOutputData.Unit;
              messageDataOutput.ActiveWeighingStatus = this.ActiveWeighingStatus;
              messageDataOutput.DataAsBytes = messsage.DataAsBytes ?? new byte[0];
              messageDataOutput.DataAsString = messsage.DataAsString ?? string.Empty;
              messageDataOutput.eValueWeight = eValueWeightType.All;
            }
            break;
          case eModeCommunication.OHAUS:
          case eModeCommunication.Continuous:
            // TOTO: 
            //var continuousFomatData = ContinuousFomatData.Decode(dataReceived.DataAsString);

            //if (continuousFomatData.Status != this.ActiveWeighingStatus)
            //{
            //  this.ActiveWeighingStatus = continuousFomatData.Status;
            //  OnActiveWeighingStatusChangeEvent?.Invoke(this, this.ActiveWeighingStatus);
            //}


            //_unit = continuousFomatData.Unit;
            //_IndicatedWeight = continuousFomatData.Indicated;
            break;
          case eModeCommunication.Digi:
            // TOTO: 
            //var digiFormatData = DigiFormatData.Decode(dataReceived.DataAsString);

            //if (digiFormatData.Status != this.ActiveWeighingStatus)
            //{
            //  this.ActiveWeighingStatus = digiFormatData.Status;
            //  OnActiveWeighingStatusChangeEvent?.Invoke(this, this.ActiveWeighingStatus);
            //}


            //_unit = digiFormatData.Unit;
            //_IndicatedWeight = digiFormatData.Indicated;

            break;
          default:
            break;
        }

        //switch (_unit)
        //{
        //  case UnitOfWeight.Kilograms:
        //    this.Unit = WeighingUnit.Kilograms;
        //    break;
        //  case UnitOfWeight.Grams:
        //    this.Unit = WeighingUnit.Grams;
        //    break;
        //  case UnitOfWeight.Pounds:
        //    this.Unit = WeighingUnit.Pounds;
        //    break;
        //  case UnitOfWeight.Ounces:
        //    this.Unit = WeighingUnit.Ounces;
        //    break;
        //  default:
        //    this.Unit = WeighingUnit.Kilograms;
        //    break;
        //}
        //receiveStopwatch.Restart();
        //LastRecievedData = DateTime.Now;
        //this.CountRecievedData++;
        //LastIndicatedWeight = _IndicatedWeight;

        //RecievedInterval.Enqueue(receiveStopwatch.Elapsed);
        //if (RecievedInterval.Count > 100)
        //  RecievedInterval.Dequeue();

        messageDataOutput.Source = messsage.Source;
        messageDataOutput.NameDevice = messsage.NameDevice;
        messageDataOutput.SourceDateTime = messsage.SourceDateTime;
        return messageDataOutput;
      }
      catch (Exception ex)
      {
        //TODO
        string a = ex.StackTrace;
        return null;
      }
      
    }
  }
}
