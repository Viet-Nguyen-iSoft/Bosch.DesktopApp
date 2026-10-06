using Common;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Common.EnumData;
using static LTP.Truck.EnumData;

namespace LTP.Truck.MasterData
{
  public partial class PopupTypeTare : Form
  {
    public event Action<CategoryTare>? OnSendSuccess;
    public event Action<TareTruck>? OnSendTareTruckSuccess;

    private CategoryTareService _categoryTareService { get; set; }
    private TareTruckService _tareTruckService { get; set; }
    private CategoryTare _categoryTareUpdate { get; set; }
    private TareTruck _tareTruckUpdate { get; set; }
    private bool _isTareForTruck;
    private EnumTypePopup _enumTypePopup = EnumTypePopup.Add;
    public PopupTypeTare()
    {
      InitializeComponent();
      this.Load += PopupAddClient_Load;
      this.btnConfirm.Click += BtnConfirm_Click;
      this.btnClose.Click += BtnClose_Click;
      txtValueTare.KeyPress += TxtValueTare_KeyPress;
      txtValueTare._TextChanged += TxtValueTare_TextChanged;
    }

    public PopupTypeTare(CategoryTare categoryTare) : this()
    {
      _categoryTareUpdate = categoryTare;
      _enumTypePopup = EnumTypePopup.Update;
      btnConfirm.Text = "Cập nhật";

      LoadDataUpdate(categoryTare);
    }

    public PopupTypeTare(bool isTareForTruck) : this()
    {
      _isTareForTruck = isTareForTruck;
      if (isTareForTruck)
        lbTitle.Text = "Tare thùng xe tải";
    }

    public PopupTypeTare(TareTruck tareTruck) : this(true)
    {
      _tareTruckUpdate = tareTruck;
      _enumTypePopup = EnumTypePopup.Update;
      btnConfirm.Text = "Cập nhật";
      LoadDataUpdate(tareTruck);
    }

    private void PopupAddClient_Load(object? sender, EventArgs e)
    {
      _categoryTareService = new CategoryTareService();
      _tareTruckService = new TareTruckService();
    }

    private void LoadDataUpdate(TareTruck tareTruck)
    {
      txtCode.Texts = tareTruck.Code ?? string.Empty;
      txtName.Texts = tareTruck.Name ?? string.Empty;
      txtValueTare.Texts = tareTruck.Value?.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
      txtDescription.Texts = tareTruck.Description ?? string.Empty;
    }

    private void LoadDataUpdate(CategoryTare categoryTare)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          LoadDataUpdate(categoryTare);
        }));
        return;
      }

      txtCode.Texts = categoryTare?.Code ?? string.Empty;
      txtName.Texts = categoryTare?.Name ?? string.Empty;
      txtValueTare.Texts = categoryTare?.Value?.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
      txtDescription.Texts = categoryTare?.Description ?? string.Empty;
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

        if (string.IsNullOrWhiteSpace(txtValueTare.Texts))
        {
          using var popupMsgAlarm = new PopupConfirm("Vui lòng nhập giá trị Tare !",
            EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsgAlarm.ShowDialog(this);
          txtValueTare.Focus();
          return;
        }

        if (!TryGetPositiveTareValue(out double tareValue))
        {
          using var popupMsgAlarm = new PopupConfirm("Khối lượng bì phải là số lớn hơn 0 !",
            EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsgAlarm.ShowDialog(this);
          txtValueTare.Focus();
          return;
        }

        if (_enumTypePopup == EnumTypePopup.Add)
        {
          string categoryTareCode = txtCode.Texts.Trim();
          if (_isTareForTruck)
          {
            var tareTrucks = await _tareTruckService.GetAllAsync(IsContainDelete: true);
            bool isDuplicateTareTruckCode = tareTrucks.Any(tareTruck =>
              !tareTruck.DeletedFlag &&
              string.Equals(tareTruck.Code?.Trim(), categoryTareCode,
                StringComparison.CurrentCultureIgnoreCase));

            if (isDuplicateTareTruckCode)
            {
              using var popupMsgAlarm = new PopupConfirm("Mã Tare thùng xe tải đã tồn tại !",
                EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
              popupMsgAlarm.ShowDialog(this);
              txtCode.Focus();
              return;
            }

            var tareTruck = new TareTruck
            {
              Code = categoryTareCode,
              Name = txtName.Texts.Trim(),
              Value = tareValue,
              Description = txtDescription.Texts.Trim(),
              CreatedAt = DateTime.UtcNow,
            };
            var tareTruckResult = await _tareTruckService.AddOrUpdateAsync(tareTruck);
            Close();
            OnSendTareTruckSuccess?.Invoke(tareTruckResult);
            return;
          }

          var categoryTares = await _categoryTareService.GetAllAsync(IsContainDelete: true);
          bool isDuplicateCode = categoryTares.Any(categoryTare =>
            !categoryTare.DeletedFlag &&
            string.Equals(categoryTare.Code?.Trim(), categoryTareCode,
              StringComparison.CurrentCultureIgnoreCase));

          if (isDuplicateCode)
          {
            using var popupMsgAlarm = new PopupConfirm("Mã loại Tare đã tồn tại !",
              EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
            popupMsgAlarm.ShowDialog(this);
            txtCode.Focus();
            return;
          }

          CategoryTare  categoryTare = new CategoryTare();
          categoryTare.Code = categoryTareCode;
          categoryTare.Name = txtName.Texts.Trim();
          categoryTare.Value = tareValue;
          categoryTare.Description = txtDescription.Texts.Trim();
          categoryTare.CreatedAt = DateTime.UtcNow;
          var rs = await _categoryTareService.AddOrUpdateAsync(categoryTare);

          this.Close();
          OnSendSuccess?.Invoke(rs);
        }
        else if (_enumTypePopup == EnumTypePopup.Update)
        {
          if (_isTareForTruck)
          {
            _tareTruckUpdate.Code = txtCode.Texts.Trim();
            _tareTruckUpdate.Name = txtName.Texts.Trim();
            _tareTruckUpdate.Value = tareValue;
            _tareTruckUpdate.Description = txtDescription.Texts.Trim();
            _tareTruckUpdate.UpdatedAt = DateTime.UtcNow;
            var tareTruckResult = await _tareTruckService.AddOrUpdateAsync(_tareTruckUpdate);
            Close();
            OnSendTareTruckSuccess?.Invoke(tareTruckResult);
            return;
          }

          _categoryTareUpdate.Code = txtCode.Texts.Trim();
          _categoryTareUpdate.Name = txtName.Texts.Trim();
          _categoryTareUpdate.Value = tareValue;
          _categoryTareUpdate.Description = txtDescription.Texts.Trim();
          _categoryTareUpdate.UpdatedAt = DateTime.UtcNow;
          var rs = await _categoryTareService.AddOrUpdateAsync(_categoryTareUpdate);

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

    private void TxtValueTare_KeyPress(object? sender, KeyPressEventArgs e)
    {
      if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
        return;

      if ((e.KeyChar == '.' || e.KeyChar == ',') &&
          !txtValueTare.Texts.Contains('.') &&
          !txtValueTare.Texts.Contains(','))
        return;

      e.Handled = true;
    }

    private void TxtValueTare_TextChanged(object? sender, EventArgs e)
    {
      string value = txtValueTare.Texts;
      bool hasDecimalSeparator = false;
      string sanitizedValue = new(value.Where(character =>
      {
        if (char.IsDigit(character))
          return true;

        if ((character == '.' || character == ',') && !hasDecimalSeparator)
        {
          hasDecimalSeparator = true;
          return true;
        }

        return false;
      }).ToArray());

      if (!string.Equals(value, sanitizedValue, StringComparison.Ordinal))
        txtValueTare.Texts = sanitizedValue;
    }

    private bool TryGetPositiveTareValue(out double tareValue)
    {
      string normalizedValue = txtValueTare.Texts.Trim().Replace(',', '.');
      return double.TryParse(normalizedValue, NumberStyles.AllowDecimalPoint,
          CultureInfo.InvariantCulture, out tareValue) &&
        double.IsFinite(tareValue) &&
        tareValue > 0;
    }
  }
}
