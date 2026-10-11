using Common;
using HelperManager;
using iSoft.Database.DTO;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using LTP.Truck.Custom;
using LTP.Truck.Popup;
using System.Globalization;
using static Common.EnumData;
using static iSoft.Database.EnumData;

namespace LTP.Truck.Forms
{
  public partial class PopupAddRecordTruckInGoods : Form
  {
    public event Action<RecordTruck>? OnSendSuccess;

    private readonly TareTruckService _tareTruckService = new();
    private Guid? _clientId;
    private Guid? _typeGoodsId;
    private Guid? _warehouseId;

    public PopupAddRecordTruckInGoods()
    {
      InitializeComponent();
      btnClose.Click += (_, _) => Close();
      btnConfirm.Click += BtnConfirm_Click;
      btnLoadClient.Click += BtnLoadClient_Click;
      btnLoadTypeGoods.Click += BtnLoadTypeGoods_Click;
      btnLoadWarehouse.Click += BtnLoadWarehouse_Click;
      cbbTareForTruck.Format += CbbTareForTruck_Format;
      cbbTareForTruck.SelectedIndexChanged += (_, _) => UpdateTareForTruckValue();
      numericUpDownNumberTare.ValueChanged += (_, _) => UpdateTareForTruckValue();
      RegisterIntegerTextBox(txtValueNet);
      RegisterIntegerTextBox(txtValueGross);
      RegisterIntegerTextBox(txtValueTareForTruck);
      Load += PopupAddRecordTruckInGoods_Load;
    }

    private void RegisterIntegerTextBox(RJTextBox textBox)
    {
      textBox.KeyPress += IntegerTextBox_KeyPress;
      textBox._TextChanged += IntegerTextBox_TextChanged;
    }

    private async void PopupAddRecordTruckInGoods_Load(object? sender, EventArgs e)
    {
      try
      {
        var items = await _tareTruckService.GetAllAsync(IsContainDelete: false);
        items = items.OrderBy(item => item.Name ?? string.Empty).ToList();
        items.Insert(0, new TareTruck { Id = Guid.Empty, Name = "Không có", Value = 0 });
        cbbTareForTruck.DisplayMember = nameof(TareTruck.Name);
        cbbTareForTruck.ValueMember = nameof(TareTruck.Id);
        cbbTareForTruck.DataSource = items;
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

    private void CbbTareForTruck_Format(object? sender, ListControlConvertEventArgs e)
    {
      if (e.ListItem is TareTruck tareTruck)
        e.Value = $"{tareTruck.Name} - {WeightFormatHelper.Format(tareTruck.Value ?? 0)} Kg";
    }

    private void UpdateTareForTruckValue()
    {
      var selectedTare = cbbTareForTruck.SelectedItem as TareTruck;
      var value = selectedTare?.Id == Guid.Empty
        ? 0
        : (selectedTare?.Value ?? 0) * (double)numericUpDownNumberTare.Value;
      txtValueTareForTruck.Texts = WeightFormatHelper.Format(value);
    }

    private void BtnConfirm_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);

      if (!TryGetPositiveInteger(txtValueNet.Texts, out double netWeight))
      {
        ShowWarning("Giá trị cân Net phải là số nguyên lớn hơn 0 !");
        txtValueNet.Focus();
        return;
      }
      if (!TryGetPositiveInteger(txtValueGross.Texts, out double grossWeight))
      {
        ShowWarning("Giá trị cân Gross phải là số nguyên lớn hơn 0 !");
        txtValueGross.Focus();
        return;
      }
      if (grossWeight <= netWeight)
      {
        ShowWarning("Giá trị cân Gross phải lớn hơn giá trị cân Net !");
        txtValueGross.Focus();
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
      OnSendSuccess?.Invoke(new RecordTruck
      {
        NetTime01 = netWeight,
        NetTime02 = grossWeight,
        EnumTypeDataTruck = EnumTypeDataTruck.DoneTime02,
        NameDriver = txtNameDriver.Texts.Trim(),
        LicensePlate = validLicense.Plate,
        IdCard = txtIdCard.Texts.Trim(),
        Note = txtDocument.Text.Trim(),
        ClientId = _clientId,
        TypeGoodsId = _typeGoodsId,
        WarehouseId = _warehouseId,
        Tare = tare,
        NameTareForTruck = selectedTare?.Id == Guid.Empty ? null : selectedTare?.Name,
        NumberTare = decimal.ToInt32(numericUpDownNumberTare.Value),
        StationId = AppCore.Ins._station?.Id,
        UserId = AppCore.Ins._userCurrent?.Id,
        CreatedAt = now,
        UpdatedAt = now,
        WeighInAt = now,
        WeighOutAt = now,
        EnableFlag = true,
      });
      Close();
    }

    private static void IntegerTextBox_KeyPress(object? sender, KeyPressEventArgs e)
    {
      e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
    }

    private void IntegerTextBox_TextChanged(object? sender, EventArgs e)
    {
      if (sender is not TextBox textBox)
        return;
      string sanitized = new(textBox.Text.Where(char.IsDigit).ToArray());
      if (!string.Equals(textBox.Text, sanitized, StringComparison.Ordinal))
        textBox.Text = sanitized;
    }

    private static bool TryGetPositiveInteger(string value, out double result) =>
      TryGetNonNegativeInteger(value, out result) && result > 0;

    private static bool TryGetNonNegativeInteger(string value, out double result) =>
      double.TryParse(value.Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out result) &&
      double.IsFinite(result) && result >= 0;

    private void ShowWarning(string message)
    {
      using var popup = new PopupConfirm(message,
        EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
      popup.ShowDialog(this);
    }
  }
}
