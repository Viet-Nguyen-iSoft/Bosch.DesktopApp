using iSoft.Communication.JsonPayload;
using iSoft.Communication.Mode;
using static iSoft.Communication.EnumCommunication;

namespace iSoft.Communication.Interface;

/// <summary>
/// Common entry point for all communication types used by an application.
/// </summary>
public interface ICommunicationService : IDisposable
{
  event EventHandler<DataWeightInterface>? DataWeightInterface;
  event EventHandler<CommunicationStatusChangedEventArgs>? ConnectionStatusChanged;

  IReadOnlyCollection<IScaleConnection> Connections { get; }

  IScaleConnection AddConnection(
    IConfigJson config,
    Guid? machineId = null,
    eDevice device = eDevice.None);

  bool RemoveConnection(string id);
  IScaleConnection? GetConnection(string id);

  void Connect(string id);
  void ConnectAll();
  void Disconnect(string id);
  void DisconnectAll();
  void SendData(string id, string data);
  void Tare(string id);
  void PresetTare(string id, double tareWeight, string unit = "kg");
  void Zero(string id);
}

public sealed class CommunicationStatusChangedEventArgs : EventArgs
{
  public CommunicationStatusChangedEventArgs(string connectionId, bool isConnected)
  {
    ConnectionId = connectionId;
    IsConnected = isConnected;
  }

  public string ConnectionId { get; }
  public bool IsConnected { get; }
}
