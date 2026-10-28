using Common;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using System.Globalization;
using static Common.EnumData;
using static iSoft.Database.EnumData;
using static LTP.Truck.EnumData;

namespace LTP.Truck.MasterData
{
  public partial class PopupTypeTare : Form
  {
    public event Action<Tare>? OnSendSuccess;

    private readonly TareService _tareService = new();
    private readonly Tare? _tareUpdate;
    private readonly EnumTypePopup _enumTypePopup = EnumTypePopup.Add;

    public PopupTypeTare()
    {
      InitializeComponent();
      btnConfirm.Click += BtnConfirm_Click;
      btnClose.Click += (_, _) => Close();
      txtValueTare.KeyPress += TxtValueTare_KeyPress;
      txtValueTare._TextChanged += TxtValueTare_TextChanged;
    }

    public PopupTypeTare(Tare tare) : this()
    {
      _tareUpdate = tare;
      _enumTypePopup = EnumTypePopup.Update;
      btnConfirm.Text = "Cập nhật";
      txtCode.Texts = tare.Code ?? string.Empty;
      txtName.Texts = tare.Name ?? string.Empty;
      txtValueTare.Texts = tare.Value?.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
      txtDescription.Texts = tare.Description ?? string.Empty;
    }

    private async void BtnConfirm_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      try
      {
        if (string.IsNullOrWhiteSpace(txtName.Texts))
        {
          ShowWarning("Vui lòng nhập tên !");
          txtName.Focus();
          return;
        }

        if (!TryGetNonNegativeTareValue(out double tareValue))
        {
          ShowWarning("Khối lượng bì phải là số lớn hơn hoặc bằng 0 !");
          txtValueTare.Focus();
          return;
        }

        string? code = string.IsNullOrWhiteSpace(txtCode.Texts)
          ? null
          : txtCode.Texts.Trim();
        var existingTares = await _tareService.GetAllAsync(
          EnumTypeDataLocation.Good, isContainDelete: true);
        bool duplicateCode = code != null && existingTares.Any(tare =>
          tare.Id != _tareUpdate?.Id &&
          !tare.DeletedFlag &&
          string.Equals(tare.Code?.Trim(), code,
            StringComparison.CurrentCultureIgnoreCase));
        if (duplicateCode)
        {
          ShowWarning("Mã loại Tare đã tồn tại !");
          txtCode.Focus();
          return;
        }

        var tareToSave = _tareUpdate ?? new Tare
        {
          CreatedAt = DateTime.UtcNow,
          EnableFlag = true,
        };
        tareToSave.Code = code;
        tareToSave.Name = txtName.Texts.Trim();
        tareToSave.Value = tareValue;
        tareToSave.Description = txtDescription.Texts.Trim();
        if (_enumTypePopup == EnumTypePopup.Update)
          tareToSave.UpdatedAt = DateTime.UtcNow;

        var result = await _tareService.AddOrUpdateAsync(
          tareToSave, EnumTypeDataLocation.Good);
        Close();
        OnSendSuccess?.Invoke(result);
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        ShowWarning(_enumTypePopup == EnumTypePopup.Add
          ? "Thêm thất bại !"
          : "Cập nhật thất bại !");
      }
    }

    private void ShowWarning(string message)
    {
      using var popup = new PopupConfirm(message,
        EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
      popup.ShowDialog(this);
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

    private bool TryGetNonNegativeTareValue(out double tareValue)
    {
      string normalizedValue = txtValueTare.Texts.Trim().Replace(',', '.');
      return double.TryParse(normalizedValue, NumberStyles.AllowDecimalPoint,
          CultureInfo.InvariantCulture, out tareValue) &&
        double.IsFinite(tareValue) && tareValue >= 0;
    }
  }
}
