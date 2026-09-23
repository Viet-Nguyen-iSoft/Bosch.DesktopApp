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
using static Common.EnumData;

namespace LTP.Truck.MasterData
{
  public partial class PopupAddClient : Form
  {
    public event Action<Client>? OnSendAddSuccess;
    public PopupAddClient()
    {
      InitializeComponent();
      this.Load += PopupAddClient_Load;
      this.btnConfirm.Click += BtnConfirm_Click;
      this.btnClose.Click += BtnClose_Click;
    }

    private ClientService _clientService { get; set; }

    private void PopupAddClient_Load(object? sender, EventArgs e)
    {
      _clientService = new ClientService();
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

        Client client = new Client();
        client.Name = txtName.Texts.Trim();
        client.Description = txtDescription.Texts.Trim();
        client.CreatedAt = DateTime.UtcNow;
        var rs = await _clientService.AddAsync(client);

        using var popupMsg = new PopupConfirm("Thêm thành công.",
            EnumTypeMsg.MessageAutoClose, EnumImageMsg.Information);
        popupMsg.ShowDialog(this);

        OnSendAddSuccess?.Invoke(rs);
        this.Close();
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
