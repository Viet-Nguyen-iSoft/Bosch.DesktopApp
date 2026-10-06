using Common;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using System.Globalization;
using static Common.EnumData;
using static LTP.Truck.EnumData;

namespace LTP.Truck.MasterData
{
  public partial class PopupTareForTruck : Form
  {
    public event Action<TareTruck>? OnSendSuccess;

    private readonly TareTruckService _tareTruckService = new();
    private readonly TareTruck? _tareTruckUpdate;
    private readonly EnumTypePopup _enumTypePopup = EnumTypePopup.Add;

    public PopupTareForTruck()
    {
      InitializeComponent();
      btnConfirm.Click += BtnConfirm_Click;
      btnClose.Click += BtnClose_Click;
      txtValueTare.KeyPress += TxtValueTare_KeyPress;
      txtValueTare._TextChanged += TxtValueTare_TextChanged;
    }

    public PopupTareForTruck(TareTruck tareTruck) : this()
    {
      _tareTruckUpdate = tareTruck;
      _enumTypePopup = EnumTypePopup.Update;
      btnConfirm.Text = "Cập nhật";
      txtName.Texts = tareTruck.Name ?? string.Empty;
      txtValueTare.Texts = tareTruck.Value?.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
      txtDescription.Texts = tareTruck.Description ?? string.Empty;
    }

    private void BtnClose_Click(object? sender, EventArgs e) => Close();

    private async void BtnConfirm_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      try
      {
        if (string.IsNullOrWhiteSpace(txtName.Texts))
        {
          using var popup = new PopupConfirm("Vui lòng nhập tên !",
            EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popup.ShowDialog(this);
          txtName.Focus();
          return;
        }

        if (!TryGetPositiveTareValue(out double tareValue))
        {
          using var popup = new PopupConfirm("Khối lượng bì phải là số lớn hơn 0 !",
            EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popup.ShowDialog(this);
          txtValueTare.Focus();
          return;
        }

        TareTruck tareTruck;
        if (_enumTypePopup == EnumTypePopup.Add)
        {
          tareTruck = new TareTruck
          {
            Name = txtName.Texts.Trim(),
            Value = tareValue,
            Description = txtDescription.Texts.Trim(),
            CreatedAt = DateTime.UtcNow,
          };
        }
        else
        {
          tareTruck = _tareTruckUpdate!;
          tareTruck.Name = txtName.Texts.Trim();
          tareTruck.Value = tareValue;
          tareTruck.Description = txtDescription.Texts.Trim();
          tareTruck.UpdatedAt = DateTime.UtcNow;
        }

        var result = await _tareTruckService.AddOrUpdateAsync(tareTruck);
        Close();
        OnSendSuccess?.Invoke(result);
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        using var popup = new PopupConfirm("Lưu dữ liệu thất bại !",
          EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popup.ShowDialog(this);
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
