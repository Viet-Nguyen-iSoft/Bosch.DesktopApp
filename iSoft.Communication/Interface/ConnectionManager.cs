using iSoft.Communication.Mode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Communication.Interface
{
  public class ConnectionManager
  {
    public List<IScaleConnection> _scales = new List<IScaleConnection>();
    public event EventHandler<DataWeightInterface>? OnDataReceived;
    public event EventHandler<bool>? OnConnectionStatusChanged;

    public IEnumerable<IScaleConnection> GetAll() => _scales;
    public void AddScale(IScaleConnection scale)
    {
      scale.DataReceived += Scale_DataReceived;
      scale.ConnectionStatusChanged += Scale_ConnectionStatusChanged;
      scale.Start();
      _scales.Add(scale);
    }

    private void Scale_DataReceived(object? sender, DataWeightInterface e)
    {
      OnDataReceived?.Invoke(sender, e);
    }


    public void Connect()
    {
      foreach (IScaleConnection sc in _scales)
      {
        sc.Connect();
        sc.Start();
        sc.RequestGetData();
      }  
    }

    public void Disconnect()
    {
      try
      {
        foreach (IScaleConnection sc in _scales)
        {
          sc.Disconnect();
          sc.Stop();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public void AutoReconnect()
    {
      foreach (IScaleConnection sc in _scales)
      {
        
      }
    }

    public void RemoveScale(string id)
    {
      try
      {
        var scale = _scales.FirstOrDefault(s => s.Id == id);
        if (scale != null)
        {
          scale.Stop();
          scale.Disconnect();
          _scales.Remove(scale);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    

    private void Scale_ConnectionStatusChanged(object? sender, bool isConnected)
    {
      OnConnectionStatusChanged?.Invoke(sender, isConnected);
    }

    
  }
}
