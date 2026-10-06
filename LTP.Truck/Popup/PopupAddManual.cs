using Common;
using HelperManager;
using iSoft.Database.DTO;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using System.Globalization;
using static Common.EnumData;
using static iSoft.Database.EnumData;

namespace LTP.Truck.Popup
{
  public partial class PopupAddManual : Form
  {
    public event Action<RecordTruck>? OnSendSuccess;

    private readonly TareTruckService _tareTruckService = new();
    private Guid? _clientId;
    private Guid? _typeGoodsId;
    private Guid? _warehouseId;

    public PopupAddManual()
    {
      InitializeComponent();
      btnClose.Click += (_, _) => Close();
      btnConfirm.Click += BtnConfirm_Click;
      btnLoadClient.Click += BtnLoadClient_Click;
      btnLoadTypeGoods.Click += BtnLoadTypeGoods_Click;
      btnLoadWarehouse.Click += BtnLoadWarehouse_Click;
      cbbTareForTruck.SelectedIndexChanged += CbbTareForTruck_SelectedIndexChanged;
      txtValueWeight01.KeyPress += IntegerTextBox_KeyPress;
      txtValueWeight01._TextChanged += IntegerTextBox_TextChanged;
      txtValueTareForTruck.KeyPress += IntegerTextBox_KeyPress;
      txtValueTareForTruck._TextChanged += IntegerTextBox_TextChanged;
      Load += PopupAddManual_Load;
    }

    private async void PopupAddManual_Load(object? sender, EventArgs e)
    {
      try
      {
        var tareTrucks = await _tareTruckService.GetAllAsync(IsContainDelete: false);
        tareTrucks.Insert(0, new TareTruck { Name = "Không có", Value = 0 });
        cbbTareForTruck.DisplayMember = nameof(TareTruck.Name);
        cbbTareForTruck.ValueMember = nameof(TareTruck.Id);
        cbbTareForTruck.DataSource = tareTrucks;
        cbbTareForTruck.SelectedIndex = 0;
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        ShowWarning("Không thể tải danh sách bì xe !");
      }
    }

    private async void BtnLoadClient_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      try
      {
        var items = await AppCore.Ins._clientService.GetAllAsync(IsContainDelete: false);
        using var popup = new PopupLoadMD();
        popup.SetData(items);
        popup.OnSendData += (value, type) =>
        {
          if (type == EnumTypeData.Client && value is ClientDTO dto)
          {
            _clientId = dto.Client?.Id;
            txtClient.Texts = dto.Name ?? string.Empty;
          }
        };
        popup.ShowDialog(this);
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        ShowWarning("Không thể tải danh sách khách hàng !");
      }
    }

    private async void BtnLoadTypeGoods_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      try
      {
        var items = await AppCore.Ins._typeGoodsService.GetAllAsync(IsContainDelete: false);
        using var popup = new PopupLoadMD();
        popup.SetData(items);
        popup.OnSendData += (value, type) =>
        {
          if (type == EnumTypeData.TypeGoods && value is TypeGoodsDTO dto)
          {
            _typeGoodsId = dto.TypeGoods?.Id;
            txtTypeGoods.Texts = dto.Name ?? string.Empty;
          }
        };
        popup.ShowDialog(this);
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        ShowWarning("Không thể tải danh sách loại hàng !");
      }
    }

    private async void BtnLoadWarehouse_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      try
      {
        var items = await AppCore.Ins._warehouseService.GetAllAsync(IsContainDelete: false);
        using var popup = new PopupLoadMD();
        popup.SetData(items);
        popup.OnSendData += (value, type) =>
        {
          if (type == EnumTypeData.Warehouse && value is WareHouseDTO dto)
          {
            _warehouseId = dto.Warehouse?.Id;
            txtWareHouse.Texts = dto.Name ?? string.Empty;
          }
        };
        popup.ShowDialog(this);
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        ShowWarning("Không thể tải danh sách kho !");
      }
    }

    private void CbbTareForTruck_SelectedIndexChanged(object? sender, EventArgs e)
    {
      var value = (cbbTareForTruck.SelectedItem as TareTruck)?.Value ?? 0;
      txtValueTareForTruck.Texts = WeightFormatHelper.Format(Math.Round(value));
    }

    private void BtnConfirm_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);

      if (!TryGetNonNegativeInteger(txtValueWeight01.Texts, out double weight01) || weight01 <= 0)
      {
        ShowWarning("Khối lượng cân lần 1 phải là số nguyên lớn hơn 0 !");
        txtValueWeight01.Focus();
        return;
      }

      var validLicense = LicensePlateHelper.IsValidVietnamLicensePlate(txtLicensePlate.Texts);
      if (!validLicense.IsValid)
      {
        ShowWarning("Biển số xe không hợp lệ !");
        txtLicensePlate.Focus();
        return;
      }

      if (!_clientId.HasValue)
      {
        ShowWarning("Vui lòng chọn khách hàng !");
        return;
      }

      if (!_typeGoodsId.HasValue)
      {
        ShowWarning("Vui lòng chọn loại hàng !");
        return;
      }

      if (!TryGetNonNegativeInteger(txtValueTareForTruck.Texts, out double tare))
      {
        ShowWarning("Khối lượng bì phải là số nguyên lớn hơn hoặc bằng 0 !");
        txtValueTareForTruck.Focus();
        return;
      }

      var now = DateTime.UtcNow;
      var selectedTare = cbbTareForTruck.SelectedItem as TareTruck;
      var record = new RecordTruck
      {
        NetTime01 = weight01,
        EnumTypeDataTruck = EnumTypeDataTruck.DoneTime01,
        NoLabelManual = txtNoLabel.Texts.Trim(),
        NameDriver = txtNameDriver.Texts.Trim(),
        LicensePlate = validLicense.Plate,
        IdCard = txtIdCard.Texts.Trim(),
        Note = txtDocument.Text.Trim(),
        ClientId = _clientId,
        TypeGoodsId = _typeGoodsId,
        WarehouseId = _warehouseId,
        Tare = tare,
        NameTareForTruck = selectedTare?.Name == "Không có" ? null : selectedTare?.Name,
        Type = cbbType.SelectedItem?.ToString(),
        StationId = AppCore.Ins._station?.Id,
        UserId = AppCore.Ins._userCurrent?.Id,
        CreatedAt = now,
        UpdatedAt = now,
        WeighInAt = now,
      };

      OnSendSuccess?.Invoke(record);
      Close();
    }

    private static void IntegerTextBox_KeyPress(object? sender, KeyPressEventArgs e)
    {
      e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
    }

    private void IntegerTextBox_TextChanged(object? sender, EventArgs e)
    {
      if (sender is not TextBox innerTextBox)
        return;

      string sanitized = new(innerTextBox.Text.Where(char.IsDigit).ToArray());
      if (!string.Equals(innerTextBox.Text, sanitized, StringComparison.Ordinal))
        innerTextBox.Text = sanitized;
    }

    private static bool TryGetNonNegativeInteger(string value, out double result)
    {
      return double.TryParse(value.Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out result) &&
        double.IsFinite(result) && result >= 0;
    }

    private void ShowWarning(string message)
    {
      using var popup = new PopupConfirm(message,
        EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
      popup.ShowDialog(this);
    }
  }
}
