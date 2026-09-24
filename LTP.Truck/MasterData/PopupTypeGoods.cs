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
        if (string.IsNullOrWhiteSpace(txtCode.Texts))
        {
          using var popupMsgAlarm = new PopupConfirm("Vui lòng nhập mã !",
            EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsgAlarm.ShowDialog(this);
          txtCode.Focus();
          return;
        }

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
          string typeGoodsCode = txtCode.Texts.Trim();
          var typeGoodsList = await _typeGoodsService.GetAllAsync(IsContainDelete: true);
          bool isDuplicateCode = typeGoodsList.Any(typeGoods =>
            !typeGoods.DeletedFlag &&
            string.Equals(typeGoods.Code?.Trim(), typeGoodsCode,
              StringComparison.CurrentCultureIgnoreCase));

          if (isDuplicateCode)
          {
            using var popupMsgAlarm = new PopupConfirm("Mã loại hàng đã tồn tại !",
              EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
            popupMsgAlarm.ShowDialog(this);
            txtCode.Focus();
            return;
          }

          TypeGoods typeGoods = new TypeGoods();
          typeGoods.Code = typeGoodsCode;
          typeGoods.Name = txtName.Texts.Trim();
          typeGoods.Description = txtDescription.Texts.Trim();
          typeGoods.CreatedAt = DateTime.UtcNow;
          var rs = await _typeGoodsService.AddOrUpdateAsync(typeGoods);

          this.Close();
          OnSendSuccess?.Invoke(rs);
        }
        else if (_enumTypePopup == EnumTypePopup.Update)
        {
          _typeGoodsUpdate.Code = txtCode.Texts.Trim();
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
