using iSoft.Communication.Interface;
using iSoft.Communication.JsonPayload;
using iSoft.Communication.Mode;
using System.Globalization;
using static iSoft.Communication.EnumCommunication;

namespace iSoft.Communication.Communication;

/// <summary>
/// Single facade used by WinForms for TCP, Serial and USB HID connections.
/// </summary>
public sealed class CommunicationService : ICommunicationService
{
  private readonly Dictionary<string, IScaleConnection> _connections =
    new(StringComparer.OrdinalIgnoreCase);
  private readonly object _syncRoot = new();
  private bool _disposed;


  public event EventHandler<CommunicationStatusChangedEventArgs>? ConnectionStatusChanged;
  public event EventHandler<DataWeightInterface>? DataWeightInterface;

  public IReadOnlyCollection<IScaleConnection> Connections
  {
    get
    {
      lock (_syncRoot)
        return _connections.Values.ToArray();
    }
  }

  public IScaleConnection AddConnection(
    IConfigJson config,
    Guid? machineId = null,
    eDevice device = eDevice.None)
  {
    ObjectDisposedException.ThrowIf(_disposed, this);
    IScaleConnection connection = CommunicationFactory.Create(config, machineId, device);

    lock (_syncRoot)
    {
      if (_connections.ContainsKey(connection.Id))
        throw new InvalidOperationException($"Connection '{connection.Id}' already exists.");

      connection.DataReceived += Connection_DataReceived;
      connection.ConnectionStatusChanged += Connection_ConnectionStatusChanged;
      _connections.Add(connection.Id, connection);
    }

    connection.Start();
    return connection;
  }

  public IScaleConnection? GetConnection(string id)
  {
    ArgumentException.ThrowIfNullOrWhiteSpace(id);
    lock (_syncRoot)
      return _connections.GetValueOrDefault(id);
  }

  public bool RemoveConnection(string id)
  {
    IScaleConnection? connection;
    lock (_syncRoot)
    {
      if (!_connections.Remove(id, out connection))
        return false;
    }

    StopAndUnsubscribe(connection);
    return true;
  }

  public void Connect(string id)
  {
    IScaleConnection connection = GetRequiredConnection(id);
    connection.Connect();
    if (connection.IsConnected && connection.IsRequestGetData)
      connection.RequestGetData();
  }

  public void ConnectAll()
  {
    foreach (IScaleConnection connection in Connections)
      Connect(connection.Id);
  }

  public void Disconnect(string id)
  {
    IScaleConnection connection = GetRequiredConnection(id);
    connection.Disconnect();
  }

  public void DisconnectAll()
  {
    foreach (IScaleConnection connection in Connections)
      connection.Disconnect();
  }

  public void SendData(string id, string data) => GetRequiredConnection(id).SendData(data);
  public void Tare(string id) => GetRequiredConnection(id).Tare();
  public void PresetTare(string id, double tareWeight, string unit = "kg")
  {
    if (double.IsNaN(tareWeight) ||
        double.IsInfinity(tareWeight) ||
        tareWeight < 0)
    {
      throw new ArgumentOutOfRangeException(
        nameof(tareWeight),
        "Giá trị tare phải là số lớn hơn hoặc bằng 0.");
    }

    if (string.IsNullOrWhiteSpace(unit) || unit.Any(character => !char.IsLetter(character)))
      throw new ArgumentException("Đơn vị cân không hợp lệ.", nameof(unit));

    string value = tareWeight.ToString("0.################", CultureInfo.InvariantCulture);
    GetRequiredConnection(id).SendData($"TA {value} {unit.Trim()}\r\n");
  }
  public void Zero(string id) => GetRequiredConnection(id).Zero();

  public void Dispose()
  {
    if (_disposed)
      return;

    _disposed = true;
    foreach (IScaleConnection connection in Connections)
      StopAndUnsubscribe(connection);

    lock (_syncRoot)
      _connections.Clear();
  }

  private IScaleConnection GetRequiredConnection(string id) =>
    GetConnection(id) ?? throw new KeyNotFoundException($"Connection '{id}' was not found.");

  private void Connection_DataReceived(object? sender, DataWeightInterface data) =>
    DataWeightInterface?.Invoke(sender, data);

  private void Connection_ConnectionStatusChanged(object? sender, bool isConnected)
  {
    string id = sender is IScaleConnection connection ? connection.Id : string.Empty;
    ConnectionStatusChanged?.Invoke(
      this,
      new CommunicationStatusChangedEventArgs(id, isConnected));
  }

  private void StopAndUnsubscribe(IScaleConnection connection)
  {
    connection.DataReceived -= Connection_DataReceived;
    connection.ConnectionStatusChanged -= Connection_ConnectionStatusChanged;
    connection.Stop();
    connection.Disconnect();
  }
}
