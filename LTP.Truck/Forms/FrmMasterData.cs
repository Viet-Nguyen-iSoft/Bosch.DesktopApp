using ApiSyncData.Req;
using Common;
using HelperManager;
using iSoft.Database;
using iSoft.Database.DTO;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using LTP.Truck.Custom;
using LTP.Truck.MasterData;
using LTP.Truck.UserControls;
using System.ComponentModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Common.EnumData;
using static HelperManager.EnumData;
using static LTP.Truck.EnumData;

namespace LTP.Truck.Forms
{
  public partial class FrmMasterData : Form
  {
    private const string EditButtonColumnName = "btnEdit";
    private const string DeleteButtonColumnName = "btnDelete";

    private CancellationTokenSource? _searchDebounceCancellation;
    private ApiJobsService _apiJobsService { get; set; } 
    private ClientService _clientService { get; set; }
    private WarehouseService _warehouseService { get; set; }
    private TypeGoodsService _typeGoodsService { get; set; }
    private CategoryTareService _categoryTareService { get; set; }
    private ProductGroupService _productGroupService { get; set; }
    private ProductService _productService { get; set; }
    private DeliveryService _deliveryService { get; set; }
    public FrmMasterData()
    {
      InitializeComponent();
      CustomUI();

      RegisterService();

      txtSearch._TextChanged += txtSearch_TextChanged;
      dgv.CellContentClick += dgv_CellContentClick;
    }

    private void RegisterService()
    {
      _apiJobsService = new ApiJobsService();
      _clientService = new ClientService();
      _warehouseService = new WarehouseService();
      _typeGoodsService = new TypeGoodsService();
      _categoryTareService = new CategoryTareService();
      _productGroupService = new ProductGroupService();
      _productService = new ProductService();
      _deliveryService = new DeliveryService();
    }
    #region Instance
    private static FrmMasterData _Instance = null;
    public static FrmMasterData Instance
    {
      get
      {
        if (_Instance == null) _Instance = new FrmMasterData();
        return _Instance;
      }
    }
    #endregion


    private void CustomUI()
    {
      ElipseControl elipseControl = new ElipseControl();
      elipseControl.TargetControl = this;
      elipseControl.CornerRadius = 20;

      ElipseControl elipseControl01 = new ElipseControl();
      elipseControl01.TargetControl = tableLayoutPanel7;
      elipseControl01.CornerRadius = 20;

      dgv.EnableHeadersVisualStyles = false;
      dgv.ColumnHeadersHeight = 50;
      dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
      dgv.RowTemplate.Height = 60;
      dgv.BorderStyle = BorderStyle.None;
      dgv.MultiSelect = false;
      dgv.DefaultCellStyle.SelectionBackColor = dgv.DefaultCellStyle.BackColor;
      dgv.DefaultCellStyle.SelectionForeColor = dgv.DefaultCellStyle.ForeColor;
      dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
      dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor;
      dgv.RowHeadersDefaultCellStyle.SelectionBackColor = dgv.RowHeadersDefaultCellStyle.BackColor;
      dgv.RowHeadersDefaultCellStyle.SelectionForeColor = dgv.RowHeadersDefaultCellStyle.ForeColor;

    }

    private EnumTypeMasterData _enumTypeMasterDataCurrent { get; set; }

    public async Task LoadData(EnumTypeMasterData enumTypeMaster)
    {
      try
      {
        _enumTypeMasterDataCurrent = enumTypeMaster;
        string searchKey = txtSearch.Texts.Trim();
        switch (enumTypeMaster)
        {
          case EnumTypeMasterData.Client:
            var rsClient = await AppCore.Ins._clientService.GetAllAsync();
            var dtoClient = DTOHelper.ConvertClientDTO(rsClient);
            SetDgv(enumTypeMaster, FilterBySearchKey(dtoClient, searchKey));
            break;
          case EnumTypeMasterData.TypeGoods:
            var rsTypeGoods = await AppCore.Ins._typeGoodsService.GetAllAsync();
            var dtoTypeGoods = DTOHelper.ConvertTypeGoodsDTO(rsTypeGoods);
            SetDgv(enumTypeMaster, FilterBySearchKey(dtoTypeGoods, searchKey));
            break;
          case EnumTypeMasterData.Warehouse:
            var rsWarehouse = await AppCore.Ins._warehouseService.GetAllAsync();
            var dtoWarehouse = DTOHelper.ConvertWareHouseDTO(rsWarehouse);
            SetDgv(enumTypeMaster, FilterBySearchKey(dtoWarehouse, searchKey));
            break;
          case EnumTypeMasterData.Tare:
            var rsTare = await AppCore.Ins._categoryTareService.GetAllAsync();
            var dtoTare = DTOHelper.ConvertCategoryTareDTO(rsTare);
            SetDgv(enumTypeMaster, FilterBySearchKey(dtoTare, searchKey));
            break;
          case EnumTypeMasterData.GroupProduct:
            var rsGroupProduct = await AppCore.Ins._productGroupService.GetAllAsync();
            var dtoGroupProduct = DTOHelper.ConvertProductGroupDTO(rsGroupProduct);
            SetDgv(enumTypeMaster, FilterBySearchKey(dtoGroupProduct, searchKey));
            break;
          case EnumTypeMasterData.Product:
            var rsProduct = await AppCore.Ins._productService.GetAllAsync();
            var dtoProduct = DTOHelper.ConvertProductDTO(rsProduct);
            SetDgv(enumTypeMaster, FilterBySearchKey(dtoProduct, searchKey));
            break;
          case EnumTypeMasterData.Delivery:
            var deliveries = await AppCore.Ins._deliveryService.GetAllAsync();
            var deliveryDtos = DTOHelper.ConvertDeliveryDTO(deliveries);
            SetDgv(enumTypeMaster, FilterBySearchKey(deliveryDtos, searchKey));
            break;
          default:
            break;
        }
      }
      catch (Exception ex)
      {

      }
    }

    private static List<T> FilterBySearchKey<T>(List<T>? values, string searchKey)
    {
      return TextSearchHelper.FilterBrowsableProperties(values, searchKey);
    }

    private async void btnSearch_Click(object sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      await LoadData(_enumTypeMasterDataCurrent);
    }

    private async void txtSearch_TextChanged(object? sender, EventArgs e)
    {
      _searchDebounceCancellation?.Cancel();
      _searchDebounceCancellation?.Dispose();

      var cancellation = new CancellationTokenSource();
      _searchDebounceCancellation = cancellation;

      try
      {
        await Task.Delay(300, cancellation.Token);
        await LoadData(_enumTypeMasterDataCurrent);
      }
      catch (OperationCanceledException)
      {
        // Người dùng vẫn đang nhập, chờ lần thay đổi mới nhất.
      }
      finally
      {
        if (ReferenceEquals(_searchDebounceCancellation, cancellation))
        {
          _searchDebounceCancellation.Dispose();
          _searchDebounceCancellation = null;
        }
      }
    }

    private void btnAddnew_Click(object sender, EventArgs e)
    {
      if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Client)
      {
        PopupClient popupAddClient = new PopupClient();
        popupAddClient.OnSendSuccess += PopupAddClient_OnSendAddSuccess;
        popupAddClient.ShowDialog();
      }
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Warehouse)
      {
        PopupWarehouse  popupWarehouse = new PopupWarehouse();
        popupWarehouse.OnSendSuccess += PopupWarehouse_OnSendSuccess;
        popupWarehouse.ShowDialog();
      }
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.TypeGoods)
      {
        PopupTypeGoods  popupTypeGoods = new PopupTypeGoods();
        popupTypeGoods.OnSendSuccess += PopupTypeGoods_OnSendSuccess;
        popupTypeGoods.ShowDialog();
      }
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Tare)
      {
        PopupTypeTare popupTypeTare = new PopupTypeTare();
        popupTypeTare.OnSendSuccess += PopupTypeTare_OnSendSuccess;
        popupTypeTare.ShowDialog();
      }
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.GroupProduct)
      {
        PopupProductGroup popupProductGroup = new PopupProductGroup();
        popupProductGroup.OnSendSuccess += PopupProductGroup_OnSendSuccess;
        popupProductGroup.ShowDialog();
      }
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Product)
      {
        PopupProduct popupProduct = new PopupProduct();
        popupProduct.OnSendSuccess += PopupProduct_OnSendSuccess;
        popupProduct.ShowDialog();
      }
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Delivery)
      {
        PopupDelivery popupDelivery = new PopupDelivery();
        popupDelivery.OnSendSuccess += PopupDelivery_OnSendSuccess;
        popupDelivery.ShowDialog();
      }
    }

    private async void PopupDelivery_OnSendSuccess(Delivery delivery)
    {
      try
      {
        await LoadData(_enumTypeMasterDataCurrent);
        MasterDataChangeNotifier.Notify<Delivery>();
        ShowSaveSuccess(delivery.UpdatedAt.HasValue
          ? "Cập nhật thành công."
          : "Thêm thành công.");
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private async void PopupProduct_OnSendSuccess(Product obj)
    {
      try
      {
        var apiJobs = new ApiJobs
        {
          Json = JsonHelper.ToJson(await CreateProductUpsertRequestAsync(obj)),
          EnumTypeAPI = EnumTypeAPI.MD_Product,
          EnumStatusAPI = EnumStatusAPI.Created,
          CreatedAt = DateTime.UtcNow,
        };

        await _apiJobsService.AddOrUpdateAsync(apiJobs);
        await LoadData(_enumTypeMasterDataCurrent);
        MasterDataChangeNotifier.Notify<Product>();
        ShowSaveSuccess(obj.UpdatedAt.HasValue ? "Cập nhật thành công." : "Thêm thành công.");
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private async Task<ProductUpsertRequest> CreateProductUpsertRequestAsync(Product product)
    {
      Guid? productGroupId = product.ProductGroupId;
      if (product.ProductGroupId.HasValue)
      {
        var productGroup = (await _productGroupService.GetAllAsync(true))
          .FirstOrDefault(item => item.Id == product.ProductGroupId.Value);
        productGroupId = productGroup?.IdSrc ?? product.ProductGroupId;
      }

      return new ProductUpsertRequest
      {
        Id = product.IdSrc ?? product.Id,
        SerialCode = product.Code ?? string.Empty,
        Name = product.Name ?? string.Empty,
        Description = product.Description,
        ProductGroupId = productGroupId,
        WasteType = (int)product.EnumWasteType,
        DeletedFlag = product.DeletedFlag,
      };
    }

    private async void PopupProductGroup_OnSendSuccess(ProductGroup obj)
    {
      try
      {
        var productGroupUpsertRequest = new ProductGroupUpsertRequest
        {
          Id = obj.IdSrc ?? obj.Id,
          SerialCode = obj.Code ?? string.Empty,
          Name = obj.Name ?? string.Empty,
          Description = obj.Description ?? string.Empty,
          DeletedFlag = obj.DeletedFlag,
        };

        var apiJobs = new ApiJobs
        {
          Json = JsonHelper.ToJson(productGroupUpsertRequest),
          EnumStatusAPI = EnumStatusAPI.Created,
          EnumTypeAPI = EnumTypeAPI.MD_ProductGroup,
          CreatedAt = DateTime.UtcNow,
        };

        await _apiJobsService.AddOrUpdateAsync(apiJobs);
        await LoadData(_enumTypeMasterDataCurrent);
        MasterDataChangeNotifier.Notify<ProductGroup>();
        ShowSaveSuccess(obj.UpdatedAt.HasValue ? "Cập nhật thành công." : "Thêm thành công.");
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private async void PopupTypeTare_OnSendSuccess(CategoryTare obj)
    {
      try
      {
        var categoryTareUpsertRequest = new CategoryTareUpsertRequest
        {
          Id = obj.IdSrc ?? obj.Id,
          SerialCode = obj.Code ?? string.Empty,
          Name = obj.Name ?? string.Empty,
          WeightTare = obj.Value.HasValue ? Convert.ToDecimal(obj.Value.Value) : null,
          Description = obj.Description ?? string.Empty,
          DeletedFlag = obj.DeletedFlag,
        };

        var apiJobs = new ApiJobs
        {
          Json = JsonHelper.ToJson(categoryTareUpsertRequest),
          EnumStatusAPI = EnumStatusAPI.Created,
          EnumTypeAPI = EnumTypeAPI.MD_Tare,
          CreatedAt = DateTime.UtcNow,
        };

        await _apiJobsService.AddOrUpdateAsync(apiJobs);
        await LoadData(_enumTypeMasterDataCurrent);
        MasterDataChangeNotifier.Notify<CategoryTare>();
        ShowSaveSuccess(obj.UpdatedAt.HasValue ? "Cập nhật thành công." : "Thêm thành công.");
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private async void PopupTypeGoods_OnSendSuccess(TypeGoods obj)
    {
      try
      {
        TypeGoodsUpsertRequest  typeGoodsUpsertRequest = new TypeGoodsUpsertRequest();
        typeGoodsUpsertRequest.Id = obj.Id;
        typeGoodsUpsertRequest.SerialCode = obj?.Code ?? string.Empty;
        typeGoodsUpsertRequest.Name = obj?.Name ?? string.Empty;
        typeGoodsUpsertRequest.Description = obj?.Description ?? string.Empty;
        typeGoodsUpsertRequest.DeletedFlag = false;

        ApiJobs apiJobs = new ApiJobs();
        apiJobs.Json = JsonHelper.ToJson(typeGoodsUpsertRequest);
        apiJobs.EnumStatusAPI = EnumStatusAPI.Created;
        apiJobs.EnumTypeAPI = EnumTypeAPI.MD_TypeGoods;
        apiJobs.CreatedAt = DateTime.UtcNow;

        await _apiJobsService.AddOrUpdateAsync(apiJobs);

        await LoadData(_enumTypeMasterDataCurrent);
        ShowSaveSuccess("Thêm thành công.");
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private async void PopupWarehouse_OnSendSuccess(Warehouse obj)
    {
      try
      {
        WarehouseUpsertRequest  warehouseUpsertRequest = new WarehouseUpsertRequest();
        warehouseUpsertRequest.Id = obj.Id;
        warehouseUpsertRequest.Name = obj?.Name ?? string.Empty;
        warehouseUpsertRequest.Description = obj?.Description ?? string.Empty;
        warehouseUpsertRequest.DeletedFlag = false;

        ApiJobs apiJobs = new ApiJobs();
        apiJobs.Json = JsonHelper.ToJson(warehouseUpsertRequest);
        apiJobs.EnumStatusAPI = EnumStatusAPI.Created;
        apiJobs.EnumTypeAPI = EnumTypeAPI.MD_WareHouse;
        apiJobs.CreatedAt = DateTime.UtcNow;

        await _apiJobsService.AddOrUpdateAsync(apiJobs);

        await LoadData(_enumTypeMasterDataCurrent);
        ShowSaveSuccess("Thêm thành công.");
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private async void PopupAddClient_OnSendAddSuccess(Client client)
    {
      try
      {
        ClientUpsertRequest clientUpsertRequest = new ClientUpsertRequest();
        clientUpsertRequest.Id = client.Id;
        clientUpsertRequest.Name = client?.Name??string.Empty;
        clientUpsertRequest.Description = client?.Description ?? string.Empty;
        clientUpsertRequest.DeletedFlag = false;

        ApiJobs apiJobs = new ApiJobs();
        apiJobs.Json = JsonHelper.ToJson(clientUpsertRequest);
        apiJobs.EnumStatusAPI = EnumStatusAPI.Created;
        apiJobs.EnumTypeAPI = EnumTypeAPI.MD_Client;
        apiJobs.CreatedAt = DateTime.UtcNow;

        await _apiJobsService.AddOrUpdateAsync(apiJobs);

        await LoadData(_enumTypeMasterDataCurrent);
        ShowSaveSuccess("Thêm thành công.");
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    public void SetDgv<T>(EnumTypeMasterData enumTypeMasterData, List<T>? values)
    {
      dgv.DataSource = null;
      dgv.DataSource = values;
      EnsureActionColumns();

      if (enumTypeMasterData == EnumTypeMasterData.Client)
      {
        var autoSizeColumns = new[]
         {
            nameof(ClientDTO.No),
            nameof(ClientDTO.UpdatedAt),
          };
        foreach (var columnName in autoSizeColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        var alignmentMiddleCenterColumns = new[]
        {
            nameof(ClientDTO.No),
          };
        foreach (var columnName in alignmentMiddleCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        var alignmentRightCenterColumns = new[]
        {
          nameof(ClientDTO.UpdatedAt),
        };
        foreach (var columnName in alignmentRightCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
      }
      else if (enumTypeMasterData == EnumTypeMasterData.TypeGoods)
      {
        var autoSizeColumns = new[]
        {
          nameof(TypeGoodsDTO.No),
          nameof(TypeGoodsDTO.Code),
          nameof(TypeGoodsDTO.UpdatedAt),
        };
        foreach (var columnName in autoSizeColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        var alignmentMiddleCenterColumns = new[]
        {
            nameof(TypeGoodsDTO.No),
          };
        foreach (var columnName in alignmentMiddleCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        var alignmentRightCenterColumns = new[]
        {
          nameof(TypeGoodsDTO.UpdatedAt),
        };
        foreach (var columnName in alignmentRightCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
      }
      else if (enumTypeMasterData == EnumTypeMasterData.Warehouse)
      {
        var autoSizeColumns = new[]
        {
          nameof(WareHouseDTO.No),
          nameof(WareHouseDTO.UpdatedAt),
        };
        foreach (var columnName in autoSizeColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        var alignmentMiddleCenterColumns = new[]
        {
          nameof(WareHouseDTO.No),
        };
        foreach (var columnName in alignmentMiddleCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        var alignmentRightCenterColumns = new[]
        {
          nameof(WareHouseDTO.UpdatedAt),
        };
        foreach (var columnName in alignmentRightCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
      }
      else if (enumTypeMasterData == EnumTypeMasterData.Tare)
      {
        var autoSizeColumns = new[]
        {
          nameof(CategoryTareDTO.No),
          nameof(CategoryTareDTO.Code),
          nameof(CategoryTareDTO.UpdatedAt),
          nameof(CategoryTareDTO.Value),
        };
        foreach (var columnName in autoSizeColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        var alignmentMiddleCenterColumns = new[]
        {
          nameof(CategoryTareDTO.No),
        };
        foreach (var columnName in alignmentMiddleCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        var alignmentRightCenterColumns = new[]
        {
          nameof(CategoryTareDTO.Value),
          nameof(CategoryTareDTO.UpdatedAt),
        };
        foreach (var columnName in alignmentRightCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
      }
      else if (enumTypeMasterData == EnumTypeMasterData.GroupProduct)
      {
        var autoSizeColumns = new[]
        {
          nameof(CategoryTareDTO.No),
          nameof(ProductGroupDTO.UpdatedAt),
        };
        foreach (var columnName in autoSizeColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        var alignmentMiddleCenterColumns = new[]
        {
          nameof(ProductGroupDTO.No),
        };
        foreach (var columnName in alignmentMiddleCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        var alignmentRightCenterColumns = new[]
        {
          nameof(ProductGroupDTO.UpdatedAt),
        };
        foreach (var columnName in alignmentRightCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
      }
      else if (enumTypeMasterData == EnumTypeMasterData.Product)
      {
        var autoSizeColumns = new[]
        {
          nameof(ProductDTO.No),
          nameof(ProductDTO.UpdatedAt),
          nameof(ProductDTO.Group),
          nameof(ProductDTO.Code),
          nameof(ProductDTO.WasteType),
        };
        foreach (var columnName in autoSizeColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        var alignmentMiddleCenterColumns = new[]
        {
          nameof(ProductDTO.No),
        };
        foreach (var columnName in alignmentMiddleCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        var alignmentRightCenterColumns = new[]
        {
          nameof(ProductDTO.UpdatedAt),
        };
        foreach (var columnName in alignmentRightCenterColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
      }
      else if (enumTypeMasterData == EnumTypeMasterData.Delivery)
      {
        var autoSizeColumns = new[]
        {
          nameof(DeliveryDTO.No),
          nameof(DeliveryDTO.UpdatedAt),
        };
        foreach (var columnName in autoSizeColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        if (dgv.Columns.Contains(nameof(DeliveryDTO.No)))
          dgv.Columns[nameof(DeliveryDTO.No)].DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleCenter;

        if (dgv.Columns.Contains(nameof(DeliveryDTO.UpdatedAt)))
          dgv.Columns[nameof(DeliveryDTO.UpdatedAt)].DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight;
      }
    }

    private void EnsureActionColumns()
    {
      if (!dgv.Columns.Contains(EditButtonColumnName))
      {
        dgv.Columns.Add(new DataGridViewButtonColumn
        {
          Name = EditButtonColumnName,
          HeaderText = "",
          Text = "Chỉnh sửa",
          UseColumnTextForButtonValue = true,
          AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
          Width = 130,
          Resizable = DataGridViewTriState.False,
          SortMode = DataGridViewColumnSortMode.NotSortable,
        });
      }

      if (!dgv.Columns.Contains(DeleteButtonColumnName))
      {
        dgv.Columns.Add(new DataGridViewButtonColumn
        {
          Name = DeleteButtonColumnName,
          HeaderText = "",
          Text = "Xóa",
          UseColumnTextForButtonValue = true,
          AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
          Width = 100,
          Resizable = DataGridViewTriState.False,
          SortMode = DataGridViewColumnSortMode.NotSortable,
        });
      }

      // Đặt cột cuối trước để DataGridView không dịch cột dữ liệu vào giữa hai nút.
      dgv.Columns[DeleteButtonColumnName].DisplayIndex = dgv.Columns.Count - 1;
      dgv.Columns[EditButtonColumnName].DisplayIndex = dgv.Columns.Count - 2;
    }

    private async void dgv_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
        return;

      var columnName = dgv.Columns[e.ColumnIndex].Name;
      var rowData = dgv.Rows[e.RowIndex].DataBoundItem;

      if (columnName == EditButtonColumnName)
        await EditMasterDataAsync(rowData);
      else if (columnName == DeleteButtonColumnName)
        await DeleteMasterDataAsync(rowData);
    }

    private Task EditMasterDataAsync(object? rowData)
    {
      if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Client)
      {
        var data = rowData as ClientDTO;
        if (data?.Client != null)
        {
          PopupClient popupUpdateClient = new PopupClient(data.Client);
          popupUpdateClient.OnSendSuccess += PopupUpdateClient_OnSendAddSuccess;
          popupUpdateClient.ShowDialog();
        }
        else
        {
          using var popupMsg = new PopupConfirm("Không tìm thấy thông tin dữ liệu !",
           EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsg.ShowDialog(this);
        }  
      }
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Warehouse)
      {
        var data = rowData as WareHouseDTO;
        if (data?.Warehouse != null)
        {
          PopupWarehouse popupWarehouse = new PopupWarehouse(data.Warehouse);
          popupWarehouse.OnSendSuccess += PopupUpdateWarehouse_OnSendSuccess;
          popupWarehouse.ShowDialog();
        }
        else
        {
          using var popupMsg = new PopupConfirm("Không tìm thấy thông tin dữ liệu !",
           EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsg.ShowDialog(this);
        }
      }
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.TypeGoods)
      {
        var data = rowData as TypeGoodsDTO;
        if (data?.TypeGoods != null)
        {
          PopupTypeGoods popupTypeGoods = new PopupTypeGoods(data.TypeGoods);
          popupTypeGoods.OnSendSuccess += PopupUpdateTypeGoods_OnSendSuccess;
          popupTypeGoods.ShowDialog();
        }
        else
        {
          using var popupMsg = new PopupConfirm("Không tìm thấy thông tin dữ liệu !",
           EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsg.ShowDialog(this);
        }
      }
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Tare)
      {
        var data = rowData as CategoryTareDTO;
        if (data?.CategoryTare != null)
        {
          PopupTypeTare popupTypeTare = new PopupTypeTare(data.CategoryTare);
          popupTypeTare.OnSendSuccess += PopupTypeTare_OnSendSuccess;
          popupTypeTare.ShowDialog();
        }
        else
        {
          using var popupMsg = new PopupConfirm("Không tìm thấy thông tin dữ liệu !",
           EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsg.ShowDialog(this);
        }
      }
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.GroupProduct)
      {
        var data = rowData as ProductGroupDTO;
        if (data?.ProductGroup != null)
        {
          PopupProductGroup popupProductGroup = new PopupProductGroup(data.ProductGroup);
          popupProductGroup.OnSendSuccess += PopupProductGroup_OnSendSuccess;
          popupProductGroup.ShowDialog();
        }
        else
        {
          using var popupMsg = new PopupConfirm("Không tìm thấy thông tin dữ liệu !",
           EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsg.ShowDialog(this);
        }
      }
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Product)
      {
        var data = rowData as ProductDTO;
        if (data?.Product != null)
        {
          PopupProduct popupProduct = new PopupProduct(data.Product);
          popupProduct.OnSendSuccess += PopupProduct_OnSendSuccess;
          popupProduct.ShowDialog();
        }
        else
        {
          using var popupMsg = new PopupConfirm("Không tìm thấy thông tin dữ liệu !",
           EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsg.ShowDialog(this);
        }
      }
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Delivery)
      {
        var data = rowData as DeliveryDTO;
        if (data?.Delivery != null)
        {
          PopupDelivery popupDelivery = new PopupDelivery(data.Delivery);
          popupDelivery.OnSendSuccess += PopupDelivery_OnSendSuccess;
          popupDelivery.ShowDialog();
        }
        else
        {
          using var popupMsg = new PopupConfirm("Không tìm thấy thông tin dữ liệu !",
            EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsg.ShowDialog(this);
        }
      }


      return Task.CompletedTask;
    }

    private async void PopupUpdateTypeGoods_OnSendSuccess(TypeGoods obj)
    {
      try
      {
        TypeGoodsUpsertRequest typeGoodsUpsertRequest = new TypeGoodsUpsertRequest();
        typeGoodsUpsertRequest.Id = obj.Id;
        typeGoodsUpsertRequest.SerialCode = obj?.Code ?? string.Empty;
        typeGoodsUpsertRequest.Name = obj?.Name ?? string.Empty;
        typeGoodsUpsertRequest.Description = obj?.Description ?? string.Empty;
        typeGoodsUpsertRequest.DeletedFlag = false;

        ApiJobs apiJobs = new ApiJobs();
        apiJobs.Json = JsonHelper.ToJson(typeGoodsUpsertRequest);
        apiJobs.EnumStatusAPI = EnumStatusAPI.Created;
        apiJobs.EnumTypeAPI = EnumTypeAPI.MD_TypeGoods;
        apiJobs.CreatedAt = DateTime.UtcNow;

        await _apiJobsService.AddOrUpdateAsync(apiJobs);

        await LoadData(_enumTypeMasterDataCurrent);
        ShowSaveSuccess("Cập nhật thành công.");
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private async void PopupUpdateWarehouse_OnSendSuccess(Warehouse obj)
    {
      try
      {
        WarehouseUpsertRequest warehouseUpsertRequest = new WarehouseUpsertRequest();
        warehouseUpsertRequest.Id = obj.Id;
        warehouseUpsertRequest.Name = obj?.Name ?? string.Empty;
        warehouseUpsertRequest.Description = obj?.Description ?? string.Empty;
        warehouseUpsertRequest.DeletedFlag = false;

        ApiJobs apiJobs = new ApiJobs();
        apiJobs.Json = JsonHelper.ToJson(warehouseUpsertRequest);
        apiJobs.EnumStatusAPI = EnumStatusAPI.Created;
        apiJobs.EnumTypeAPI = EnumTypeAPI.MD_WareHouse;
        apiJobs.CreatedAt = DateTime.UtcNow;

        await _apiJobsService.AddOrUpdateAsync(apiJobs);

        await LoadData(_enumTypeMasterDataCurrent);
        ShowSaveSuccess("Cập nhật thành công.");
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private async void PopupUpdateClient_OnSendAddSuccess(Client client)
    {
      try
      {
        ClientUpsertRequest clientUpsertRequest = new ClientUpsertRequest();
        clientUpsertRequest.Id = client.Id;
        clientUpsertRequest.Name = client?.Name ?? string.Empty;
        clientUpsertRequest.Description = client?.Description ?? string.Empty;
        clientUpsertRequest.DeletedFlag = false;

        ApiJobs apiJobs = new ApiJobs();
        apiJobs.Json = JsonHelper.ToJson(clientUpsertRequest);
        apiJobs.EnumStatusAPI = EnumStatusAPI.Created;
        apiJobs.EnumTypeAPI = EnumTypeAPI.MD_Client;
        apiJobs.CreatedAt = DateTime.UtcNow;

        await _apiJobsService.AddOrUpdateAsync(apiJobs);

        await LoadData(_enumTypeMasterDataCurrent);
        ShowSaveSuccess("Cập nhật thành công.");
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private void ShowSaveSuccess(string message)
    {
      using var popupMsg = new PopupConfirm(message,
        EnumTypeMsg.MessageAutoClose, EnumImageMsg.Information);
      popupMsg.ShowDialog(this);
    }

    private Task DeleteMasterDataAsync(object? rowData)
    {
      if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Client)
      {
        var data = rowData as ClientDTO;
        if (data?.Client != null)
        {
          using var popupMsg = new PopupConfirm("Bạn có chắc chắn xóa dữ liệu này !",
           EnumTypeMsg.Confirm, EnumImageMsg.Warning, data.Client);
          popupMsg.OnSendConfirm += PopupMsg_OnSendConfirm;
          popupMsg.ShowDialog(this);
          popupMsg.OnSendConfirm -= PopupMsg_OnSendConfirm;
        }
        else
        {
          using var popupMsg = new PopupConfirm("Không tìm thấy thông tin dữ liệu !",
           EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsg.ShowDialog(this);
        }
      }
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Warehouse)
      {
        var data = rowData as WareHouseDTO;
        if (data?.Warehouse != null)
        {
          using var popupMsg = new PopupConfirm("Bạn có chắc chắn xóa dữ liệu này !",
           EnumTypeMsg.Confirm, EnumImageMsg.Warning, data.Warehouse);
          popupMsg.OnSendConfirm += PopupMsg_OnSendConfirm;
          popupMsg.ShowDialog(this);
          popupMsg.OnSendConfirm -= PopupMsg_OnSendConfirm;
        }
        else
        {
          using var popupMsg = new PopupConfirm("Không tìm thấy thông tin dữ liệu !",
           EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsg.ShowDialog(this);
        }
      }
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.TypeGoods)
      {
        var data = rowData as TypeGoodsDTO;
        if (data?.TypeGoods != null)
        {
          using var popupMsg = new PopupConfirm("Bạn có chắc chắn xóa dữ liệu này !",
           EnumTypeMsg.Confirm, EnumImageMsg.Warning, data.TypeGoods);
          popupMsg.OnSendConfirm += PopupMsg_OnSendConfirm;
          popupMsg.ShowDialog(this);
          popupMsg.OnSendConfirm -= PopupMsg_OnSendConfirm;
        }
        else
        {
          using var popupMsg = new PopupConfirm("Không tìm thấy thông tin dữ liệu !",
           EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsg.ShowDialog(this);
        }
      }
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Tare)
      {
        var data = rowData as CategoryTareDTO;
        if (data?.CategoryTare != null)
        {
          using var popupMsg = new PopupConfirm("Bạn có chắc chắn xóa dữ liệu này !",
           EnumTypeMsg.Confirm, EnumImageMsg.Warning, data.CategoryTare);
          popupMsg.OnSendConfirm += PopupMsg_OnSendConfirm;
          popupMsg.ShowDialog(this);
          popupMsg.OnSendConfirm -= PopupMsg_OnSendConfirm;
        }
        else
        {
          using var popupMsg = new PopupConfirm("Không tìm thấy thông tin dữ liệu !",
           EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsg.ShowDialog(this);
        }
      }
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.GroupProduct)
      {
        var data = rowData as ProductGroupDTO;
        if (data?.ProductGroup != null)
        {
          using var popupMsg = new PopupConfirm("Bạn có chắc chắn xóa dữ liệu này !",
           EnumTypeMsg.Confirm, EnumImageMsg.Warning, data.ProductGroup);
          popupMsg.OnSendConfirm += PopupMsg_OnSendConfirm;
          popupMsg.ShowDialog(this);
          popupMsg.OnSendConfirm -= PopupMsg_OnSendConfirm;
        }
        else
        {
          using var popupMsg = new PopupConfirm("Không tìm thấy thông tin dữ liệu !",
           EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsg.ShowDialog(this);
        }
      }
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Product)
      {
        var data = rowData as ProductDTO;
        if (data?.Product != null)
        {
          using var popupMsg = new PopupConfirm("Bạn có chắc chắn xóa dữ liệu này !",
           EnumTypeMsg.Confirm, EnumImageMsg.Warning, data.Product);
          popupMsg.OnSendConfirm += PopupMsg_OnSendConfirm;
          popupMsg.ShowDialog(this);
          popupMsg.OnSendConfirm -= PopupMsg_OnSendConfirm;
        }
        else
        {
          using var popupMsg = new PopupConfirm("Không tìm thấy thông tin dữ liệu !",
           EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsg.ShowDialog(this);
        }
      }
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Delivery)
      {
        var data = rowData as DeliveryDTO;
        if (data?.Delivery != null)
        {
          using var popupMsg = new PopupConfirm("Bạn có chắc chắn xóa dữ liệu này !",
            EnumTypeMsg.Confirm, EnumImageMsg.Warning, data.Delivery);
          popupMsg.OnSendConfirm += PopupMsg_OnSendConfirm;
          popupMsg.ShowDialog(this);
          popupMsg.OnSendConfirm -= PopupMsg_OnSendConfirm;
        }
        else
        {
          using var popupMsg = new PopupConfirm("Không tìm thấy thông tin dữ liệu !",
            EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsg.ShowDialog(this);
        }
      }

      return Task.CompletedTask;
    }

    private async void PopupMsg_OnSendConfirm(object? sender, ResponMsg e)
    {
      try
      {
        if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Client)
        {
          var client = e.Obj as Client;
          if (client != null)
          {
            client.DeletedFlag = true;
            await _clientService.AddOrUpdateAsync(client);

            ClientUpsertRequest clientUpsertRequest = new ClientUpsertRequest();
            clientUpsertRequest.Id = client.IdSrc!=null ? client.IdSrc : client.Id;
            clientUpsertRequest.Name = client?.Name ?? string.Empty;
            clientUpsertRequest.Description = client?.Description ?? string.Empty;
            clientUpsertRequest.DeletedFlag = client?.DeletedFlag??false;

            ApiJobs apiJobs = new ApiJobs();
            apiJobs.Json = JsonHelper.ToJson(clientUpsertRequest);
            apiJobs.EnumStatusAPI = EnumStatusAPI.Created;
            apiJobs.EnumTypeAPI = EnumTypeAPI.MD_Client;
            apiJobs.CreatedAt = DateTime.UtcNow;

            await _apiJobsService.AddOrUpdateAsync(apiJobs);

            await LoadData(_enumTypeMasterDataCurrent);
          }
        }
        else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Warehouse)
        {
          var warehouse = e.Obj as Warehouse;
          if (warehouse != null)
          {
            warehouse.DeletedFlag = true;
            await _warehouseService.AddOrUpdateAsync(warehouse);

            WarehouseUpsertRequest  warehouseUpsertRequest = new WarehouseUpsertRequest();
            warehouseUpsertRequest.Id = warehouse.IdSrc != null ? warehouse.IdSrc : warehouse.Id;
            warehouseUpsertRequest.Name = warehouse?.Name ?? string.Empty;
            warehouseUpsertRequest.Description = warehouse?.Description ?? string.Empty;
            warehouseUpsertRequest.DeletedFlag = warehouse?.DeletedFlag ?? false;

            ApiJobs apiJobs = new ApiJobs();
            apiJobs.Json = JsonHelper.ToJson(warehouseUpsertRequest);
            apiJobs.EnumStatusAPI = EnumStatusAPI.Created;
            apiJobs.EnumTypeAPI = EnumTypeAPI.MD_WareHouse;
            apiJobs.CreatedAt = DateTime.UtcNow;

            await _apiJobsService.AddOrUpdateAsync(apiJobs);

            await LoadData(_enumTypeMasterDataCurrent);
          }
        }
        else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.TypeGoods)
        {
          var  typeGoods = e.Obj as TypeGoods;
          if (typeGoods != null)
          {
            typeGoods.DeletedFlag = true;
            await _typeGoodsService.AddOrUpdateAsync(typeGoods);

            WarehouseUpsertRequest warehouseUpsertRequest = new WarehouseUpsertRequest();
            warehouseUpsertRequest.Id = typeGoods.IdSrc != null ? typeGoods.IdSrc : typeGoods.Id;
            warehouseUpsertRequest.SerialCode = typeGoods?.Code ?? string.Empty;
            warehouseUpsertRequest.Name = typeGoods?.Name ?? string.Empty;
            warehouseUpsertRequest.Description = typeGoods?.Description ?? string.Empty;
            warehouseUpsertRequest.DeletedFlag = typeGoods?.DeletedFlag ?? false;

            ApiJobs apiJobs = new ApiJobs();
            apiJobs.Json = JsonHelper.ToJson(warehouseUpsertRequest);
            apiJobs.EnumStatusAPI = EnumStatusAPI.Created;
            apiJobs.EnumTypeAPI = EnumTypeAPI.MD_TypeGoods;
            apiJobs.CreatedAt = DateTime.UtcNow;

            await _apiJobsService.AddOrUpdateAsync(apiJobs);

            await LoadData(_enumTypeMasterDataCurrent);
          }
        }
        else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Tare)
        {
          var categoryTare = e.Obj as CategoryTare;
          if (categoryTare != null)
          {
            categoryTare.DeletedFlag = true;
            await _categoryTareService.AddOrUpdateAsync(categoryTare);

            var categoryTareUpsertRequest = new CategoryTareUpsertRequest
            {
              Id = categoryTare.IdSrc ?? categoryTare.Id,
              SerialCode = categoryTare.Code ?? string.Empty,
              Name = categoryTare.Name ?? string.Empty,
              WeightTare = categoryTare.Value.HasValue
                ? Convert.ToDecimal(categoryTare.Value.Value)
                : null,
              Description = categoryTare.Description ?? string.Empty,
              DeletedFlag = true,
            };

            var apiJobs = new ApiJobs
            {
              Json = JsonHelper.ToJson(categoryTareUpsertRequest),
              EnumStatusAPI = EnumStatusAPI.Created,
              EnumTypeAPI = EnumTypeAPI.MD_Tare,
              CreatedAt = DateTime.UtcNow,
            };

            await _apiJobsService.AddOrUpdateAsync(apiJobs);
            await LoadData(_enumTypeMasterDataCurrent);
            MasterDataChangeNotifier.Notify<CategoryTare>();
          }
        }
        else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.GroupProduct)
        {
          var productGroup = e.Obj as ProductGroup;
          if (productGroup != null)
          {
            productGroup.DeletedFlag = true;
            await _productGroupService.AddOrUpdateAsync(productGroup);

            var productGroupUpsertRequest = new ProductGroupUpsertRequest
            {
              Id = productGroup.IdSrc ?? productGroup.Id,
              SerialCode = productGroup.Code ?? string.Empty,
              Name = productGroup.Name ?? string.Empty,
              Description = productGroup.Description ?? string.Empty,
              DeletedFlag = true,
            };

            var apiJobs = new ApiJobs
            {
              Json = JsonHelper.ToJson(productGroupUpsertRequest),
              EnumStatusAPI = EnumStatusAPI.Created,
              EnumTypeAPI = EnumTypeAPI.MD_ProductGroup,
              CreatedAt = DateTime.UtcNow,
            };

            await _apiJobsService.AddOrUpdateAsync(apiJobs);
            await LoadData(_enumTypeMasterDataCurrent);
            MasterDataChangeNotifier.Notify<ProductGroup>();
          }
        }
        else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Product)
        {
          var product = e.Obj as Product;
          if (product != null)
          {
            product.DeletedFlag = true;
            await _productService.AddOrUpdateAsync(product);

            var apiJobs = new ApiJobs
            {
              Json = JsonHelper.ToJson(await CreateProductUpsertRequestAsync(product)),
              EnumTypeAPI = EnumTypeAPI.MD_Product,
              EnumStatusAPI = EnumStatusAPI.Created,
              CreatedAt = DateTime.UtcNow,
            };

            await _apiJobsService.AddOrUpdateAsync(apiJobs);
            await LoadData(_enumTypeMasterDataCurrent);
            MasterDataChangeNotifier.Notify<Product>();
          }
        }
        else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Delivery)
        {
          var delivery = e.Obj as Delivery;
          if (delivery != null)
          {
            delivery.DeletedFlag = true;
            delivery.UpdatedAt = DateTime.UtcNow;
            await _deliveryService.AddOrUpdateAsync(delivery);
            await LoadData(_enumTypeMasterDataCurrent);
            MasterDataChangeNotifier.Notify<Delivery>();
          }
        }
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
      
    }
  }
}
