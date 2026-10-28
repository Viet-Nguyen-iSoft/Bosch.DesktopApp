using Common;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
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
using static iSoft.Database.EnumData;

namespace LTP.Truck.MasterData
{
  public partial class PopupClient : Form
  {
    public event Action<ClientDelivery>? OnSendSuccess;

    private ClientDeliveryService _clientDeliveryService { get; set; }
    private ClientDelivery _clientUpdate { get; set; }
    private EnumTypePopup _enumTypePopup = EnumTypePopup.Add;
    public PopupClient()
    {
      InitializeComponent();
      this.Load += PopupAddClient_Load;
      this.btnConfirm.Click += BtnConfirm_Click;
      this.btnClose.Click += BtnClose_Click;
    }

    public PopupClient(ClientDelivery client) : this()
    {
      _clientUpdate = client;
      _enumTypePopup = EnumTypePopup.Update;
      btnConfirm.Text = "Cập nhật";

      LoadDataUpdate(client);
    }

    private void PopupAddClient_Load(object? sender, EventArgs e)
    {
      _clientDeliveryService = new ClientDeliveryService();
    }

    private void LoadDataUpdate(ClientDelivery client)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadDataUpdate(client);
        }));
        return;
      }

      txtName.Texts = client?.Name ?? string.Empty;
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
        if (string.IsNullOrWhiteSpace(txtName.Texts))
        {
          using var popupMsgAlarm = new PopupConfirm("Vui lòng nhập tên !",
            EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsgAlarm.ShowDialog(this);
          txtName.Focus();
          return;
        }

        if (_enumTypePopup == EnumTypePopup.Add)
        {
          string clientName = txtName.Texts.Trim();
          var clients = await _clientDeliveryService.GetAllAsync(
            EnumTypeDataLocation.Truck, isContainDelete: true);
          bool isDuplicateName = clients.Any(client =>
            !client.DeletedFlag &&
            string.Equals(client.Name?.Trim(), clientName,
              StringComparison.CurrentCultureIgnoreCase));

          if (isDuplicateName)
          {
            using var popupMsgAlarm = new PopupConfirm("Tên khách hàng đã tồn tại !",
              EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
            popupMsgAlarm.ShowDialog(this);
            txtName.Focus();
            return;
          }

          ClientDelivery client = new ClientDelivery();
          client.Name = clientName;
          client.Description = txtDescription.Texts.Trim();
          client.EnumTypeData = EnumTypeDataLocation.Truck;
          client.CreatedAt = DateTime.UtcNow;
          var rs = await _clientDeliveryService.AddOrUpdateAsync(
            client, EnumTypeDataLocation.Truck);

          this.Close();
          OnSendSuccess?.Invoke(rs);
        }
        else if (_enumTypePopup == EnumTypePopup.Update)
        {
          _clientUpdate.Name = txtName.Texts.Trim();
          _clientUpdate.Description = txtDescription.Texts.Trim();
          _clientUpdate.UpdatedAt = DateTime.UtcNow;
          _clientUpdate.EnumTypeData = EnumTypeDataLocation.Truck;
          var rs = await _clientDeliveryService.AddOrUpdateAsync(
            _clientUpdate, EnumTypeDataLocation.Truck);

          this.Close();
          OnSendSuccess?.Invoke(rs);
        }

      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        //TODO

        using var popupMsgAlarm = new PopupConfirm("Thêm thất bại !",
            EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popupMsgAlarm.ShowDialog(this);
      }
    }
  }
}
