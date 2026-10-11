using System;
using HelperManager;
using iSoft.Database.Models;
using static iSoft.Database.EnumData;

namespace LTP.Truck.Popup
{
  public partial class PopupFilterGoods : Form
  {
    public event Action<Guid?, EnumWasteType?>? OnSendData;

    public PopupFilterGoods(
      IEnumerable<ProductGroup> productGroups,
      Guid? selectedProductGroupId = null,
      EnumWasteType? selectedWasteType = null)
    {
      InitializeComponent();

      var productGroupItems = productGroups
        .OrderBy(group => group.Name)
        .Select(group => new ProductGroupFilterItem(group.Id, group.Name ?? string.Empty))
        .Prepend(new ProductGroupFilterItem(null, "Tất cả"))
        .ToList();

      cbbProductGroup.DisplayMember = nameof(ProductGroupFilterItem.Name);
      cbbProductGroup.ValueMember = nameof(ProductGroupFilterItem.Id);
      cbbProductGroup.DataSource = productGroupItems;
      cbbProductGroup.SelectedItem = productGroupItems.FirstOrDefault(item =>
        item.Id == selectedProductGroupId) ?? productGroupItems[0];

      var wasteTypeItems = Enum.GetValues<EnumWasteType>()
        .Where(type => type != EnumWasteType.None)
        .Select(type => new WasteTypeFilterItem(type, EnumHelper.GetDescription(type)))
        .Prepend(new WasteTypeFilterItem(null, "Tất cả"))
        .ToList();

      cbbType.DisplayMember = nameof(WasteTypeFilterItem.Name);
      cbbType.ValueMember = nameof(WasteTypeFilterItem.Value);
      cbbType.DataSource = wasteTypeItems;
      cbbType.SelectedItem = wasteTypeItems.FirstOrDefault(item =>
        item.Value == selectedWasteType) ?? wasteTypeItems[0];

      btnConfirm.Click += btnConfirm_Click;
      btnClose.Click += btnClose_Click;
      AcceptButton = btnConfirm;
      CancelButton = btnClose;
    }

    private void btnConfirm_Click(object? sender, EventArgs e)
    {
      var productGroupId = (cbbProductGroup.SelectedItem as ProductGroupFilterItem)?.Id;
      var wasteType = (cbbType.SelectedItem as WasteTypeFilterItem)?.Value;

      OnSendData?.Invoke(productGroupId, wasteType);
      DialogResult = DialogResult.OK;
      Close();
    }

    private void btnClose_Click(object? sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }

    private sealed record ProductGroupFilterItem(Guid? Id, string Name);
    private sealed record WasteTypeFilterItem(EnumWasteType? Value, string Name);
  }
}
