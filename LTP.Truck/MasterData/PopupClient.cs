using Common;
using iSoft.Database.Models;
using iSoft.Database.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestConnectPrinter;
using static Common.EnumData;
using static LTP.Truck.EnumData;

namespace LTP.Truck.MasterData
{
  public partial class PopupClient : Form
  {
    public event Action<Client>? OnSendSuccess;

    private ClientService _clientService { get; set; }
    private Client _clientUpdate { get; set; }
    private EnumTypePopup _enumTypePopup = EnumTypePopup.Add;
    public PopupClient()
    {
      InitializeComponent();
      this.Load += PopupAddClient_Load;
      this.btnConfirm.Click += BtnConfirm_Click;
      this.btnClose.Click += BtnClose_Click;
    }

    public PopupClient(Client client) : this()
    {
      _clientUpdate = client;
      _enumTypePopup = EnumTypePopup.Update;
      btnConfirm.Text = "Cập nhật";

      LoadDataUpdate(client);
    }

    private void PopupAddClient_Load(object? sender, EventArgs e)
    {
      _clientService = new ClientService();
    }

    private void LoadDataUpdate(Client client)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadDataUpdate(client);
        }));
        return;
      }

     txtName.Texts = client?.Name??string.Empty;
     txtDescription.Texts = client?.Description ?? string.Empty;
    }


    private void BtnClose_Click(object? sender, EventArgs e)
    {
      this.Close();
    }

    private async void BtnConfirm_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      try
      {
        if (string.IsNullOrEmpty(txtName.Texts))
        {
          using var popupMsgAlarm = new PopupConfirm("Vui lòng nhập tên !",
            EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsgAlarm.ShowDialog(this);
          return;
        }

        if (_enumTypePopup == EnumTypePopup.Add )
        {
          Client client = new Client();
          client.Name = txtName.Texts.Trim();
          client.Description = txtDescription.Texts.Trim();
          client.CreatedAt = DateTime.UtcNow;
          var rs = await _clientService.AddOrUpdateAsync(client);

          this.Close();
          OnSendSuccess?.Invoke(rs);
        }  
        else if (_enumTypePopup == EnumTypePopup.Update)
        {
          _clientUpdate.Name = txtName.Texts.Trim();
          _clientUpdate.Description = txtDescription.Texts.Trim();
          _clientUpdate.UpdatedAt = DateTime.UtcNow;
          var rs = await _clientService.AddOrUpdateAsync(_clientUpdate);

          this.Close();
          OnSendSuccess?.Invoke(rs);
        }

      }
      catch (Exception)
      {
        //TODO

        using var popupMsgAlarm = new PopupConfirm("Thêm thất bại !",
            EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popupMsgAlarm.ShowDialog(this);
      }
    }
  }
}
