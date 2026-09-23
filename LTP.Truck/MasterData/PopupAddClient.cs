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
  public partial class PopupAddClient : Form
  {
    public event Action<Client>? OnSendSuccess;

    private ClientService _clientService { get; set; }
    private Client _clientUpdate { get; set; }
    private EnumTypePopup _enumTypePopup = EnumTypePopup.Add;
    public PopupAddClient()
    {
      InitializeComponent();
      this.Load += PopupAddClient_Load;
      this.btnConfirm.Click += BtnConfirm_Click;
      this.btnClose.Click += BtnClose_Click;
    }

    public PopupAddClient(Client client) : this()
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

          using var popupMsg = new PopupConfirm("Thêm thành công.",
              EnumTypeMsg.MessageAutoClose, EnumImageMsg.Information);
          popupMsg.ShowDialog(this);

          OnSendSuccess?.Invoke(rs);
          this.Close();
        }  
        else if (_enumTypePopup == EnumTypePopup.Update)
        {
          _clientUpdate.Name = txtName.Texts.Trim();
          _clientUpdate.Description = txtDescription.Texts.Trim();
          _clientUpdate.CreatedAt = DateTime.UtcNow;
          var rs = await _clientService.AddOrUpdateAsync(_clientUpdate);

          using var popupMsg = new PopupConfirm("Cập nhật thành công.",
              EnumTypeMsg.MessageAutoClose, EnumImageMsg.Information);
          popupMsg.ShowDialog(this);

          OnSendSuccess?.Invoke(rs);
          this.Close();
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
