using Common;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using static Common.EnumData;
using static LTP.Truck.EnumData;
using static iSoft.Database.EnumData;

namespace LTP.Truck.MasterData
{
  public partial class PopupDelivery : Form
  {
    public event Action<ClientDelivery>? OnSendSuccess;

    private readonly ClientDeliveryService _clientDeliveryService = new();
    private ClientDelivery? _deliveryUpdate;
    private EnumTypePopup _enumTypePopup = EnumTypePopup.Add;

    public PopupDelivery()
    {
      InitializeComponent();
      btnConfirm.Click += BtnConfirm_Click;
      btnClose.Click += (_, _) => Close();
    }

    public PopupDelivery(ClientDelivery delivery) : this()
    {
      _deliveryUpdate = delivery;
      _enumTypePopup = EnumTypePopup.Update;
      btnConfirm.Text = "Cập nhật";
      txtCompanyName.Texts = delivery.Name ?? string.Empty;
      txtOfficeAddress.Texts = delivery.OfficeAddress ?? string.Empty;
      txtPhoneForOfficeAddress.Texts = delivery.PhoneForOfficeAddress ?? string.Empty;
      txtAgentAddress.Texts = delivery.AgentAddress ?? string.Empty;
      txtPhoneForAgentAddress.Texts = delivery.PhoneForAgentAddress ?? string.Empty;
      txtDescription.Texts = delivery.Description ?? string.Empty;
    }

    private async void BtnConfirm_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      try
      {
        var deliveryName = txtCompanyName.Texts.Trim();
        if (string.IsNullOrWhiteSpace(deliveryName))
        {
          ShowWarning("Vui lòng nhập tên công ty !");
          txtCompanyName.Focus();
          return;
        }

        var deliveries = await _clientDeliveryService.GetAllAsync(
          EnumTypeDataLocation.Good, isContainDelete: true);
        var duplicateName = deliveries.Any(delivery =>
          !delivery.DeletedFlag &&
          delivery.Id != _deliveryUpdate?.Id &&
          string.Equals(delivery.Name?.Trim(), deliveryName,
            StringComparison.CurrentCultureIgnoreCase));
        if (duplicateName)
        {
          ShowWarning("Tên công ty đã tồn tại !");
          txtCompanyName.Focus();
          return;
        }

        var deliveryToSave = _deliveryUpdate ?? new ClientDelivery
        {
          CreatedAt = DateTime.UtcNow,
          EnumTypeData = EnumTypeDataLocation.Good,
        };
        deliveryToSave.Name = deliveryName;
        deliveryToSave.OfficeAddress = txtOfficeAddress.Texts.Trim();
        deliveryToSave.PhoneForOfficeAddress = txtPhoneForOfficeAddress.Texts.Trim();
        deliveryToSave.AgentAddress = txtAgentAddress.Texts.Trim();
        deliveryToSave.PhoneForAgentAddress = txtPhoneForAgentAddress.Texts.Trim();
        deliveryToSave.Description = txtDescription.Texts.Trim();
        if (_enumTypePopup == EnumTypePopup.Update)
          deliveryToSave.UpdatedAt = DateTime.UtcNow;

        deliveryToSave.EnumTypeData = EnumTypeDataLocation.Good;
        var result = await _clientDeliveryService.AddOrUpdateAsync(
          deliveryToSave, EnumTypeDataLocation.Good);
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
  }
}
