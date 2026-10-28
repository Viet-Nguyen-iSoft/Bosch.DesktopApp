using Common;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using static Common.EnumData;
using static LTP.Truck.EnumData;
using static iSoft.Database.EnumData;

namespace LTP.Truck.MasterData
{
  public partial class PopupClientDelivery : Form
  {
    public event Action<ClientDelivery>? OnSendSuccess;

    private readonly ClientDeliveryService _clientDeliveryService = new();
    private readonly EnumTypeDataLocation _enumTypeData;
    private ClientDelivery? _clientDeliveryUpdate;
    private EnumTypePopup _enumTypePopup = EnumTypePopup.Add;

    public PopupClientDelivery() : this(EnumTypeDataLocation.Good)
    {
    }

    public PopupClientDelivery(EnumTypeDataLocation enumTypeData)
    {
      if (enumTypeData == EnumTypeDataLocation.All)
        throw new ArgumentOutOfRangeException(nameof(enumTypeData));

      _enumTypeData = enumTypeData;
      InitializeComponent();
      ConfigureForDataType();
      btnConfirm.Click += BtnConfirm_Click;
      btnClose.Click += (_, _) => Close();
    }

    public PopupClientDelivery(ClientDelivery clientDelivery)
      : this(clientDelivery, EnumTypeDataLocation.Good)
    {
    }

    public PopupClientDelivery(
      ClientDelivery clientDelivery,
      EnumTypeDataLocation enumTypeData) : this(enumTypeData)
    {
      ArgumentNullException.ThrowIfNull(clientDelivery);
      _clientDeliveryUpdate = clientDelivery;
      _enumTypePopup = EnumTypePopup.Update;
      btnConfirm.Text = "Cập nhật";
      txtCompanyName.Texts = clientDelivery.Name ?? string.Empty;
      txtOfficeAddress.Texts = clientDelivery.OfficeAddress ?? string.Empty;
      txtPhoneForOfficeAddress.Texts =
        clientDelivery.PhoneForOfficeAddress ?? string.Empty;
      txtAgentAddress.Texts = clientDelivery.AgentAddress ?? string.Empty;
      txtPhoneForAgentAddress.Texts =
        clientDelivery.PhoneForAgentAddress ?? string.Empty;
      txtDescription.Texts = clientDelivery.Description ?? string.Empty;
    }

    private void ConfigureForDataType()
    {
      bool isClient = _enumTypeData == EnumTypeDataLocation.Truck;
      lbTitle.Text = isClient
        ? "Thông tin Khách hàng"
        : "Thông tin Khách hàng giao";
      label2.Text = isClient ? "Tên khách hàng" : "Tên công ty";
    }

    private async void BtnConfirm_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      try
      {
        var name = txtCompanyName.Texts.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
          ShowWarning(_enumTypeData == EnumTypeDataLocation.Truck
            ? "Vui lòng nhập tên khách hàng !"
            : "Vui lòng nhập tên công ty !");
          txtCompanyName.Focus();
          return;
        }

        var originalName = _clientDeliveryUpdate?.Name?.Trim();
        var isNameChanged = _clientDeliveryUpdate == null ||
          !string.Equals(originalName, name,
            StringComparison.CurrentCultureIgnoreCase);
        if (isNameChanged)
        {
          var items = await _clientDeliveryService.GetAllAsync(
            _enumTypeData, isContainDelete: true);
          var duplicateName = items.Any(item =>
            !item.DeletedFlag &&
            !IsSameRecord(item, _clientDeliveryUpdate) &&
            string.Equals(item.Name?.Trim(), name,
              StringComparison.CurrentCultureIgnoreCase));
          if (duplicateName)
          {
            ShowWarning(_enumTypeData == EnumTypeDataLocation.Truck
              ? "Tên khách hàng đã tồn tại !"
              : "Tên công ty đã tồn tại !");
            txtCompanyName.Focus();
            return;
          }
        }

        var itemToSave = _clientDeliveryUpdate ?? new ClientDelivery
        {
          CreatedAt = DateTime.UtcNow,
          EnumTypeData = _enumTypeData,
        };
        itemToSave.Name = name;
        itemToSave.OfficeAddress = txtOfficeAddress.Texts.Trim();
        itemToSave.PhoneForOfficeAddress = txtPhoneForOfficeAddress.Texts.Trim();
        itemToSave.AgentAddress = txtAgentAddress.Texts.Trim();
        itemToSave.PhoneForAgentAddress = txtPhoneForAgentAddress.Texts.Trim();
        itemToSave.Description = txtDescription.Texts.Trim();
        if (_enumTypePopup == EnumTypePopup.Update)
          itemToSave.UpdatedAt = DateTime.UtcNow;

        itemToSave.EnumTypeData = _enumTypeData;
        var result = await _clientDeliveryService.AddOrUpdateAsync(
          itemToSave, _enumTypeData);
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

    private static bool IsSameRecord(
      ClientDelivery item,
      ClientDelivery? current)
    {
      if (current == null)
        return false;

      if (item.Id != Guid.Empty && item.Id == current.Id)
        return true;

      if (item.IdSrc.HasValue && item.IdSrc != Guid.Empty &&
          current.IdSrc.HasValue && current.IdSrc != Guid.Empty &&
          item.IdSrc == current.IdSrc)
      {
        return true;
      }

      return item.IdSrc.HasValue && item.IdSrc != Guid.Empty &&
          item.IdSrc == current.Id ||
        current.IdSrc.HasValue && current.IdSrc != Guid.Empty &&
          current.IdSrc == item.Id;
    }

    private void ShowWarning(string message)
    {
      using var popup = new PopupConfirm(message,
        EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
      popup.ShowDialog(this);
    }
  }
}
