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
using static LTP.Truck.EnumData;

namespace LTP.Truck.MasterData
{
  public partial class PopupWarehouse : Form
  {
    public event Action<Warehouse>? OnSendSuccess;

    private WarehouseService _warehouseService { get; set; }
    private Warehouse _warehouseUpdate { get; set; }
    private EnumTypePopup _enumTypePopup = EnumTypePopup.Add;
    public PopupWarehouse()
    {
      InitializeComponent();

      this.Load += PopupAddClient_Load;
      this.btnConfirm.Click += BtnConfirm_Click;
      this.btnClose.Click += BtnClose_Click;
    }

    public PopupWarehouse(Warehouse warehouse) : this()
    {
      _warehouseUpdate = warehouse;
      _enumTypePopup = EnumTypePopup.Update;
      btnConfirm.Text = "Cập nhật";

      LoadDataUpdate(warehouse);
    }

    private void PopupAddClient_Load(object? sender, EventArgs e)
    {
      _warehouseService = new WarehouseService();
    }

    private void LoadDataUpdate(Warehouse warehouse)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadDataUpdate(warehouse);
        }));
        return;
      }

      txtName.Texts = warehouse?.Name ?? string.Empty;
      txtDescription.Texts = warehouse?.Description ?? string.Empty;
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

        if (_enumTypePopup == EnumTypePopup.Add)
        {
          Warehouse warehouse = new Warehouse();
          warehouse.Name = txtName.Texts.Trim();
          warehouse.Description = txtDescription.Texts.Trim();
          warehouse.CreatedAt = DateTime.UtcNow;
          var rs = await _warehouseService.AddOrUpdateAsync(warehouse);

          using var popupMsg = new PopupConfirm("Thêm thành công.",
              EnumTypeMsg.MessageAutoClose, EnumImageMsg.Information);
          popupMsg.ShowDialog(this);

          OnSendSuccess?.Invoke(rs);
          this.Close();
        }
        else if (_enumTypePopup == EnumTypePopup.Update)
        {
          _warehouseUpdate.Name = txtName.Texts.Trim();
          _warehouseUpdate.Description = txtDescription.Texts.Trim();
          _warehouseUpdate.UpdatedAt = DateTime.UtcNow;
          var rs = await _warehouseService.AddOrUpdateAsync(_warehouseUpdate);

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
