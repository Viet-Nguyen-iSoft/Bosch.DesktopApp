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
using static Common.EnumData;
using static LTP.Truck.EnumData;

namespace LTP.Truck.MasterData
{
  public partial class PopupTypeGoods : Form
  {
    public event Action<TypeGoods>? OnSendSuccess;

    private TypeGoodsService _typeGoodsService { get; set; }
    private TypeGoods _typeGoodsUpdate { get; set; }
    private EnumTypePopup _enumTypePopup = EnumTypePopup.Add;
    public PopupTypeGoods()
    {
      InitializeComponent();
      this.Load += PopupAddClient_Load;
      this.btnConfirm.Click += BtnConfirm_Click;
      this.btnClose.Click += BtnClose_Click;
    }

    public PopupTypeGoods(TypeGoods typeGoods) : this()
    {
      _typeGoodsUpdate = typeGoods;
      _enumTypePopup = EnumTypePopup.Update;
      btnConfirm.Text = "Cập nhật";

      LoadDataUpdate(typeGoods);
    }

    private void PopupAddClient_Load(object? sender, EventArgs e)
    {
      _typeGoodsService = new TypeGoodsService();
    }

    private void LoadDataUpdate(TypeGoods typeGoods)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadDataUpdate(typeGoods);
        }));
        return;
      }

      txtCode.Texts = typeGoods?.Code ?? string.Empty;
      txtName.Texts = typeGoods?.Name ?? string.Empty;
      txtDescription.Texts = typeGoods?.Description ?? string.Empty;
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

        if (_enumTypePopup == EnumTypePopup.Add)
        {
          TypeGoods typeGoods = new TypeGoods();
          typeGoods.Code = txtCode.Texts.Trim();
          typeGoods.Name = txtName.Texts.Trim();
          typeGoods.Description = txtDescription.Texts.Trim();
          typeGoods.CreatedAt = DateTime.UtcNow;
          var rs = await _typeGoodsService.AddOrUpdateAsync(typeGoods);

          this.Close();
          OnSendSuccess?.Invoke(rs);
        }
        else if (_enumTypePopup == EnumTypePopup.Update)
        {
          _typeGoodsUpdate.Name = txtName.Texts.Trim();
          _typeGoodsUpdate.Description = txtDescription.Texts.Trim();
          _typeGoodsUpdate.UpdatedAt = DateTime.UtcNow;
          var rs = await _typeGoodsService.AddOrUpdateAsync(_typeGoodsUpdate);

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
