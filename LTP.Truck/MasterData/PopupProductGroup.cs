using Common;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using static Common.EnumData;
using static LTP.Truck.EnumData;

namespace LTP.Truck.MasterData
{
  public partial class PopupProductGroup : Form
  {
    public event Action<ProductGroup>? OnSendSuccess;

    private ProductGroupService _productGroupService { get; set; }
    private ProductGroup _productGroupUpdate { get; set; }
    private EnumTypePopup _enumTypePopup = EnumTypePopup.Add;

    public PopupProductGroup()
    {
      InitializeComponent();
      Load += PopupProductGroup_Load;
      btnConfirm.Click += BtnConfirm_Click;
      btnClose.Click += BtnClose_Click;
    }

    public PopupProductGroup(ProductGroup productGroup) : this()
    {
      _productGroupUpdate = productGroup;
      _enumTypePopup = EnumTypePopup.Update;
      btnConfirm.Text = "Cập nhật";
      LoadDataUpdate(productGroup);
    }

    private void PopupProductGroup_Load(object? sender, EventArgs e)
    {
      _productGroupService = new ProductGroupService();
    }

    private void LoadDataUpdate(ProductGroup productGroup)
    {
      if (InvokeRequired)
      {
        Invoke(new Action(() => LoadDataUpdate(productGroup)));
        return;
      }

      txtName.Texts = productGroup.Name ?? string.Empty;
      txtDescription.Texts = productGroup.Description ?? string.Empty;
    }

    private void BtnClose_Click(object? sender, EventArgs e)
    {
      Close();
    }

    private async void BtnConfirm_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      try
      {
        if (string.IsNullOrWhiteSpace(txtName.Texts))
        {
          using var popupMsgAlarm = new PopupConfirm("Vui lòng nhập tên !",
            EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsgAlarm.ShowDialog(this);
          return;
        }

        ProductGroup productGroup;
        if (_enumTypePopup == EnumTypePopup.Add)
        {
          productGroup = new ProductGroup { CreatedAt = DateTime.UtcNow };
        }
        else
        {
          productGroup = _productGroupUpdate;
          productGroup.UpdatedAt = DateTime.UtcNow;
        }

        productGroup.Name = txtName.Texts.Trim();
        productGroup.Description = txtDescription.Texts.Trim();

        var result = await _productGroupService.AddOrUpdateAsync(productGroup);

        Close();
        OnSendSuccess?.Invoke(result);
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        using var popupMsgAlarm = new PopupConfirm(
          _enumTypePopup == EnumTypePopup.Add ? "Thêm thất bại !" : "Cập nhật thất bại !",
          EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popupMsgAlarm.ShowDialog(this);
      }
    }
  }
}
