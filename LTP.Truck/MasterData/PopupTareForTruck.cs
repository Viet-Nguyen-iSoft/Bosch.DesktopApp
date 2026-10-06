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

        if (!TryGetNonNegativeIntegerTareValue(out double tareValue))
        {
          using var popup = new PopupConfirm("Khối lượng bì phải là số nguyên lớn hơn hoặc bằng 0 !",
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
      e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
    }

    private void TxtValueTare_TextChanged(object? sender, EventArgs e)
    {
      string value = txtValueTare.Texts;
      string sanitizedValue = new(value.Where(char.IsDigit).ToArray());

      if (!string.Equals(value, sanitizedValue, StringComparison.Ordinal))
        txtValueTare.Texts = sanitizedValue;
    }

    private bool TryGetNonNegativeIntegerTareValue(out double tareValue)
    {
      return double.TryParse(txtValueTare.Texts.Trim(), NumberStyles.None,
          CultureInfo.InvariantCulture, out tareValue) &&
        double.IsFinite(tareValue) && tareValue >= 0;
    }
  }
}
