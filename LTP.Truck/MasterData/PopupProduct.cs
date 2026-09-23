using Common;
using iSoft.Database.Models;
using iSoft.Database.Service;
using static Common.EnumData;
using static LTP.Truck.EnumData;

namespace LTP.Truck.MasterData
{
  public partial class PopupProduct : Form
  {
    public event Action<Product>? OnSendSuccess;

    private ProductService _productService { get; set; }
    private ProductGroupService _productGroupService { get; set; }
    private Product _productUpdate { get; set; }
    private EnumTypePopup _enumTypePopup = EnumTypePopup.Add;

    public PopupProduct()
    {
      InitializeComponent();
      Load += PopupProduct_Load;
      btnConfirm.Click += BtnConfirm_Click;
      btnClose.Click += BtnClose_Click;
      cbbProductGroup.DropDownStyle = ComboBoxStyle.DropDownList;
    }

    public PopupProduct(Product product) : this()
    {
      _productUpdate = product;
      _enumTypePopup = EnumTypePopup.Update;
      btnConfirm.Text = "Cập nhật";

      txtCode.Texts = product.Code ?? string.Empty;
      txtName.Texts = product.Name ?? string.Empty;
      txtDescription.Texts = product.Description ?? string.Empty;
    }

    private async void PopupProduct_Load(object? sender, EventArgs e)
    {
      _productService = new ProductService();
      _productGroupService = new ProductGroupService();

      try
      {
        var productGroups = await _productGroupService.GetAllAsync();
        cbbProductGroup.DisplayMember = nameof(ProductGroup.Name);
        cbbProductGroup.ValueMember = nameof(ProductGroup.Id);
        cbbProductGroup.DataSource = productGroups;

        if (_enumTypePopup == EnumTypePopup.Update && _productUpdate.ProductGroupId.HasValue)
        {
          cbbProductGroup.SelectedItem = productGroups.FirstOrDefault(group =>
            group.Id == _productUpdate.ProductGroupId.Value);
        }
        else
        {
          cbbProductGroup.SelectedIndex = -1;
        }
      }
      catch (Exception)
      {
        using var popupMsg = new PopupConfirm("Không thể tải danh sách nhóm phế phẩm !",
          EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popupMsg.ShowDialog(this);
      }
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
          ShowWarning("Vui lòng nhập tên !");
          return;
        }

        if (cbbProductGroup.SelectedItem is not ProductGroup selectedProductGroup)
        {
          ShowWarning("Vui lòng chọn nhóm phế phẩm !");
          return;
        }

        Product product;
        string successMessage;
        if (_enumTypePopup == EnumTypePopup.Add)
        {
          product = new Product { CreatedAt = DateTime.UtcNow };
          successMessage = "Thêm thành công.";
        }
        else
        {
          product = _productUpdate;
          product.UpdatedAt = DateTime.UtcNow;
          successMessage = "Cập nhật thành công.";
        }

        product.Code = txtCode.Texts.Trim();
        product.Name = txtName.Texts.Trim();
        product.Description = txtDescription.Texts.Trim();
        product.ProductGroupId = selectedProductGroup.Id;

        var result = await _productService.AddOrUpdateAsync(product);

        using var popupMsg = new PopupConfirm(successMessage,
          EnumTypeMsg.MessageAutoClose, EnumImageMsg.Information);
        popupMsg.ShowDialog(this);

        OnSendSuccess?.Invoke(result);
        Close();
      }
      catch (Exception)
      {
        ShowWarning(_enumTypePopup == EnumTypePopup.Add ? "Thêm thất bại !" : "Cập nhật thất bại !");
      }
    }

    private void ShowWarning(string message)
    {
      using var popupMsg = new PopupConfirm(message,
        EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
      popupMsg.ShowDialog(this);
    }
  }
}
