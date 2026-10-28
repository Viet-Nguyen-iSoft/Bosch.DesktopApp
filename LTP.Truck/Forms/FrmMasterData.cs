using ApiSyncData.Req;
using ClosedXML.Excel;
using Common;
using HelperManager;
using iSoft.Database;
using iSoft.Database.DTO;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using LTP.Truck.Custom;
using LTP.Truck.MasterData;
using LTP.Truck.Popup;
using LTP.Truck.UserControls;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Common.EnumData;
using static HelperManager.EnumData;
using static iSoft.Database.EnumData;
using static LTP.Truck.EnumData;

namespace LTP.Truck.Forms
{
  public partial class FrmMasterData : Form
  {
    private const string EditButtonColumnName = "btnEdit";
    private const string DeleteButtonColumnName = "btnDelete";

    private CancellationTokenSource? _searchDebounceCancellation;
    private bool _isLoadingProductGroupFilter;
    private bool _isLoadingWasteTypeFilter;
    private int _loadDataVersion;
    private ApiJobsService _apiJobsService { get; set; } 
    private ClientService _clientService { get; set; }
    private WarehouseService _warehouseService { get; set; }
    private TypeGoodsService _typeGoodsService { get; set; }
    private TareService _tareService { get; set; }
    private ProductGroupService _productGroupService { get; set; }
    private ProductService _productService { get; set; }
    private DeliveryService _deliveryService { get; set; }
    private DocumentDataService _documentDataService { get; set; }
    public FrmMasterData()
    {
      InitializeComponent();
      SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
      CustomUI();

      RegisterService();

      txtSearch._TextChanged += txtSearch_TextChanged;
      dgv.CellContentClick += dgv_CellContentClick;
      btnImport.Click += btnImport_Click;
      cbbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
      cbbFilter.DisplayMember = nameof(ProductGroupFilterItem.Name);
      cbbFilter.ValueMember = nameof(ProductGroupFilterItem.Id);
      cbbFilter.Visible = false;
      cbbFilter.SelectedIndexChanged += cbbFilter_SelectedIndexChanged;
      cbbType.DropDownStyle = ComboBoxStyle.DropDownList;
      cbbType.DisplayMember = nameof(WasteTypeFilterItem.Name);
      cbbType.ValueMember = nameof(WasteTypeFilterItem.Value);
      cbbType.DataSource = Enum.GetValues<EnumWasteType>()
        .Where(value => value != EnumWasteType.None)
        .Select(value => new WasteTypeFilterItem(
          value, EnumHelper.GetDescription(value)))
        .Prepend(new WasteTypeFilterItem(null, "Tất cả loại"))
        .ToList();
      cbbType.SelectedIndex = 0;
      cbbType.Visible = false;
      cbbType.SelectedIndexChanged += cbbType_SelectedIndexChanged;
    }

    private void RegisterService()
    {
      _apiJobsService = new ApiJobsService();
      _clientService = new ClientService();
      _warehouseService = new WarehouseService();
      _typeGoodsService = new TypeGoodsService();
      _tareService = new TareService();
      _productGroupService = new ProductGroupService();
      _productService = new ProductService();
      _deliveryService = new DeliveryService();
      _documentDataService = new DocumentDataService();
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
        int loadVersion = Interlocked.Increment(ref _loadDataVersion);
        _enumTypeMasterDataCurrent = enumTypeMaster;
        btnImport.Visible = enumTypeMaster is
          EnumTypeMasterData.TypeGoods or
          EnumTypeMasterData.Product or
          EnumTypeMasterData.Tare or
          EnumTypeMasterData.TareForTruck;
        ConfigureWasteTypeFilter(enumTypeMaster);
        await ConfigureProductGroupFilterAsync(enumTypeMaster, loadVersion);
        if (!IsCurrentLoad(enumTypeMaster, loadVersion)) return;

        string searchKey = txtSearch.Texts.Trim();
        switch (enumTypeMaster)
        {
          case EnumTypeMasterData.Client:
            var rsClient = await AppCore.Ins._clientService.GetAllAsync();
            if (!IsCurrentLoad(enumTypeMaster, loadVersion)) return;
            var dtoClient = DTOHelper.ConvertClientDTO(rsClient);
            SetDgv(enumTypeMaster, FilterBySearchKey(dtoClient, searchKey));
            break;
          case EnumTypeMasterData.TypeGoods:
            var rsTypeGoods = await AppCore.Ins._typeGoodsService.GetAllAsync();
            if (!IsCurrentLoad(enumTypeMaster, loadVersion)) return;
            EnumWasteType? typeGoodsWasteType =
              (cbbType.SelectedItem as WasteTypeFilterItem)?.Value;
            if (typeGoodsWasteType.HasValue)
            {
              rsTypeGoods = rsTypeGoods
                .Where(typeGoods =>
                  typeGoods.EnumWasteType == typeGoodsWasteType.Value)
                .ToList();
            }
            var dtoTypeGoods = DTOHelper.ConvertTypeGoodsDTO(rsTypeGoods);
            SetDgv(enumTypeMaster, FilterBySearchKey(dtoTypeGoods, searchKey));
            break;
          case EnumTypeMasterData.Warehouse:
            var rsWarehouse = await AppCore.Ins._warehouseService.GetAllAsync();
            if (!IsCurrentLoad(enumTypeMaster, loadVersion)) return;
            var dtoWarehouse = DTOHelper.ConvertWareHouseDTO(rsWarehouse);
            SetDgv(enumTypeMaster, FilterBySearchKey(dtoWarehouse, searchKey));
            break;
          case EnumTypeMasterData.Tare:
            var rsTare = await AppCore.Ins._tareService
              .GetAllAsync(EnumTypeDataLocation.Good);
            if (!IsCurrentLoad(enumTypeMaster, loadVersion)) return;
            var dtoTare = DTOHelper.ConvertCategoryTareDTO(rsTare);
            SetDgv(enumTypeMaster, FilterBySearchKey(dtoTare, searchKey));
            break;
          case EnumTypeMasterData.TareForTruck:
            var rsTareTruck = await _tareService
              .GetAllAsync(EnumTypeDataLocation.Truck);
            if (!IsCurrentLoad(enumTypeMaster, loadVersion)) return;
            var dtoTareTruck = DTOHelper.ConvertTareTruckDTO(rsTareTruck);
            SetDgv(enumTypeMaster, FilterBySearchKey(dtoTareTruck, searchKey));
            break;
          case EnumTypeMasterData.GroupProduct:
            var rsGroupProduct = await AppCore.Ins._productGroupService.GetAllAsync();
            if (!IsCurrentLoad(enumTypeMaster, loadVersion)) return;
            var dtoGroupProduct = DTOHelper.ConvertProductGroupDTO(rsGroupProduct);
            SetDgv(enumTypeMaster, FilterBySearchKey(dtoGroupProduct, searchKey));
            break;
          case EnumTypeMasterData.Product:
            var rsProduct = await AppCore.Ins._productService.GetAllAsync();
            if (!IsCurrentLoad(enumTypeMaster, loadVersion)) return;
            Guid? productGroupId = (cbbFilter.SelectedItem as ProductGroupFilterItem)?.Id;
            if (productGroupId.HasValue)
            {
              rsProduct = rsProduct
                .Where(product => product.ProductGroupId == productGroupId.Value)
                .ToList();
            }
            EnumWasteType? productWasteType =
              (cbbType.SelectedItem as WasteTypeFilterItem)?.Value;
            if (productWasteType.HasValue)
            {
              rsProduct = rsProduct
                .Where(product => product.EnumWasteType == productWasteType.Value)
                .ToList();
            }
            var dtoProduct = DTOHelper.ConvertProductDTO(rsProduct);
            dtoProduct = dtoProduct?
              .OrderBy(product => product.TagMappingReport ?? string.Empty)
              //.OrderBy(product => product.Group ?? string.Empty)
              //.ThenBy(product => product.WasteType ?? string.Empty)
              //.ThenBy(product => product.Name ?? string.Empty)
              .Select((product, index) =>
              {
                product.No = index + 1;
                return product;
              })
              .ToList();
            SetDgv(enumTypeMaster, FilterBySearchKey(dtoProduct, searchKey));
            break;
          case EnumTypeMasterData.Delivery:
            var deliveries = await AppCore.Ins._deliveryService.GetAllAsync();
            if (!IsCurrentLoad(enumTypeMaster, loadVersion)) return;
            var deliveryDtos = DTOHelper.ConvertDeliveryDTO(deliveries);
            SetDgv(enumTypeMaster, FilterBySearchKey(deliveryDtos, searchKey));
            break;
          case EnumTypeMasterData.Document:
            var documents = await AppCore.Ins._documentDataService.GetAllAsync();
            if (!IsCurrentLoad(enumTypeMaster, loadVersion)) return;
            var documentDtos = DTOHelper.ConvertDocumentDataDTO(documents);
            SetDgv(enumTypeMaster, FilterBySearchKey(documentDtos, searchKey));
            break;
          default:
            break;
        }
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private bool IsCurrentLoad(EnumTypeMasterData enumTypeMaster, int loadVersion)
    {
      return loadVersion == _loadDataVersion &&
             enumTypeMaster == _enumTypeMasterDataCurrent;
    }

    private async Task ConfigureProductGroupFilterAsync(
      EnumTypeMasterData enumTypeMaster,
      int loadVersion)
    {
      bool isProduct = enumTypeMaster == EnumTypeMasterData.Product;
      cbbFilter.Visible = isProduct;

      if (!isProduct)
      {
        if (cbbFilter.DataSource != null)
        {
          _isLoadingProductGroupFilter = true;
          cbbFilter.DataSource = null;
          _isLoadingProductGroupFilter = false;
        }
        return;
      }

      if (cbbFilter.DataSource != null)
      {
        return;
      }

      var productGroups = await AppCore.Ins._productGroupService.GetAllAsync();
      if (!IsCurrentLoad(enumTypeMaster, loadVersion)) return;

      var filterItems = productGroups
        .OrderBy(group => group.Name)
        .Select(group => new ProductGroupFilterItem(group.Id, group.Name ?? string.Empty))
        .Prepend(new ProductGroupFilterItem(null, "Tất cả nhóm"))
        .ToList();

      _isLoadingProductGroupFilter = true;
      cbbFilter.DataSource = filterItems;
      cbbFilter.DisplayMember = nameof(ProductGroupFilterItem.Name);
      cbbFilter.ValueMember = nameof(ProductGroupFilterItem.Id);
      cbbFilter.SelectedIndex = 0;
      _isLoadingProductGroupFilter = false;
    }

    private void ConfigureWasteTypeFilter(EnumTypeMasterData enumTypeMaster)
    {
      bool supportsWasteTypeFilter = enumTypeMaster is
        EnumTypeMasterData.Product or EnumTypeMasterData.TypeGoods;
      cbbType.Visible = supportsWasteTypeFilter;

      if (!supportsWasteTypeFilter && cbbType.SelectedIndex != 0)
      {
        _isLoadingWasteTypeFilter = true;
        cbbType.SelectedIndex = 0;
        _isLoadingWasteTypeFilter = false;
      }
    }

    private async void cbbFilter_SelectedIndexChanged(object? sender, EventArgs e)
    {
      if (_isLoadingProductGroupFilter ||
          _enumTypeMasterDataCurrent != EnumTypeMasterData.Product)
      {
        return;
      }

      await LoadData(EnumTypeMasterData.Product);
    }

    private async void cbbType_SelectedIndexChanged(object? sender, EventArgs e)
    {
      if (_isLoadingWasteTypeFilter ||
          _enumTypeMasterDataCurrent is not
            (EnumTypeMasterData.Product or EnumTypeMasterData.TypeGoods))
      {
        return;
      }

      await LoadData(_enumTypeMasterDataCurrent);
    }

    private sealed class ProductGroupFilterItem
    {
      public Guid? Id { get; }
      public string Name { get; }

      public ProductGroupFilterItem(Guid? id, string name)
      {
        Id = id;
        Name = name;
      }

      public override string ToString() => Name;
    }

    private sealed class WasteTypeFilterItem
    {
      public EnumWasteType? Value { get; }
      public string Name { get; }

      public WasteTypeFilterItem(EnumWasteType? value, string name)
      {
        Value = value;
        Name = name;
      }

      public override string ToString() => Name;
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

    private async void btnImport_Click(object? sender, EventArgs e)
    {
      if (_enumTypeMasterDataCurrent is not
          (EnumTypeMasterData.TypeGoods or
           EnumTypeMasterData.Product or
           EnumTypeMasterData.Tare or
           EnumTypeMasterData.TareForTruck))
      {
        using var popup = new PopupConfirm(
          "Chức năng Import không áp dụng cho danh sách hiện tại !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popup.ShowDialog(this);
        return;
      }

      using var openFileDialog = new OpenFileDialog
      {
        Title = "Chọn file Excel import dữ liệu",
        Filter = "Excel Workbook (*.xlsx;*.xlsm)|*.xlsx;*.xlsm",
        CheckFileExists = true,
        Multiselect = false,
      };

      if (openFileDialog.ShowDialog(this) != DialogResult.OK)
        return;

      using var buttonLock = ButtonExecutionScope.Enter(sender);
      try
      {
        using var workbook = new XLWorkbook(openFileDialog.FileName);
        if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Product)
        {
          await ImportProductsAsync(workbook);
          return;
        }
        if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Tare)
        {
          await ImportTaresAsync(workbook);
          return;
        }
        if (_enumTypeMasterDataCurrent == EnumTypeMasterData.TareForTruck)
        {
          await ImportTruckTaresAsync(workbook);
          return;
        }

        var productGroups = await _productGroupService.GetAllAsync();
        var hcpProductGroup = productGroups
          .OrderBy(group => group.Name)
          .FirstOrDefault(group => group.Name?.Contains(
            "HcP", StringComparison.OrdinalIgnoreCase) == true);

        if (hcpProductGroup == null)
        {
          using var popup = new PopupConfirm(
            "Không tìm thấy nhóm chất thải có tên chứa HcP !",
            EnumTypeMsg.MessageManualClose,
            EnumImageMsg.Warning);
          popup.ShowDialog(this);
          return;
        }

        if (!workbook.Worksheets.TryGetWorksheet("HcP_Xetai", out var worksheet))
        {
          using var popup = new PopupConfirm(
            "Không tìm thấy sheet HcP_Xetai trong file Excel !",
            EnumTypeMsg.MessageManualClose,
            EnumImageMsg.Warning);
          popup.ShowDialog(this);
          return;
        }

        var existingTypeGoods = await _typeGoodsService.GetAllAsync(IsContainDelete: true);
        var typeGoodsByNameAndWasteType = existingTypeGoods
          .Where(item => item.ProductGroupId == hcpProductGroup.Id &&
            !string.IsNullOrWhiteSpace(item.Name))
          .GroupBy(item => BuildImportKey(
            hcpProductGroup.Id, item.Name!, item.EnumWasteType),
            StringComparer.OrdinalIgnoreCase)
          .ToDictionary(group => group.Key, group => group.First(),
            StringComparer.OrdinalIgnoreCase);

        var errors = new List<string>();
        var importedCount = 0;
        var addedCount = 0;
        var updatedCount = 0;
        var lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 2;

        for (var rowNumber = 3; rowNumber <= lastRow; rowNumber++)
        {
          var wasteTypeText = worksheet.Cell(rowNumber, 1).GetString().Trim();
          var code = worksheet.Cell(rowNumber, 2).GetString().Trim();
          var name = worksheet.Cell(rowNumber, 3).GetString().Trim();
          var tagMappingReport = worksheet.Cell(rowNumber, 4).GetString().Trim();

          if (string.IsNullOrWhiteSpace(wasteTypeText) &&
              string.IsNullOrWhiteSpace(code) &&
              string.IsNullOrWhiteSpace(name) &&
              string.IsNullOrWhiteSpace(tagMappingReport))
          {
            continue;
          }

          if (string.IsNullOrWhiteSpace(name))
          {
            errors.Add($"Dòng {rowNumber}: thiếu Tên.");
            continue;
          }

          if (!TryParseWasteType(wasteTypeText, out var wasteType))
          {
            errors.Add($"Dòng {rowNumber}: loại chất thải '{wasteTypeText}' không hợp lệ.");
            continue;
          }

          TypeGoods? typeGoods = null;
          typeGoodsByNameAndWasteType.TryGetValue(
            BuildImportKey(hcpProductGroup.Id, name, wasteType),
            out typeGoods);

          var isNew = typeGoods == null;
          typeGoods ??= new TypeGoods
          {
            CreatedAt = DateTime.UtcNow,
            EnableFlag = true,
          };

          typeGoods.Code = string.IsNullOrWhiteSpace(code) ? null : code;
          typeGoods.Name = name;
          typeGoods.ProductGroupId = hcpProductGroup.Id;
          typeGoods.EnumWasteType = wasteType;
          typeGoods.TagMappingReport = string.IsNullOrWhiteSpace(tagMappingReport)
            ? null
            : tagMappingReport;
          typeGoods.DeletedFlag = false;
          typeGoods.EnableFlag = true;
          typeGoods.SyncFlag = false;

          try
          {
            var savedTypeGoods = await _typeGoodsService.AddOrUpdateAsync(typeGoods);
            typeGoodsByNameAndWasteType[
              BuildImportKey(hcpProductGroup.Id, name, wasteType)] =
              savedTypeGoods;
            importedCount++;
            if (isNew)
              addedCount++;
            else
              updatedCount++;
          }
          catch (Exception rowException)
          {
            LogHelper.LogErrorToFileLog(rowException, AppCore.Ins._folderFileLog);
            errors.Add($"Dòng {rowNumber}: không thể lưu dữ liệu.");
          }
        }

        await LoadData(EnumTypeMasterData.TypeGoods);
        MasterDataChangeNotifier.Notify<TypeGoods>();

        var message =
          $"Import hoàn tất.\r\n" +
          $"- Tổng thành công: {importedCount}\r\n" +
          $"- Thêm mới: {addedCount}\r\n" +
          $"- Cập nhật: {updatedCount}\r\n" +
          $"- Bỏ qua: {errors.Count}";

        if (errors.Count > 0)
        {
          message += "\r\n\r\n" + string.Join("\r\n", errors.Take(10));
          if (errors.Count > 10)
            message += $"\r\n... và {errors.Count - 10} lỗi khác.";
        }

        using var resultPopup = new PopupConfirm(
          message,
          EnumTypeMsg.MessageManualClose,
          errors.Count == 0 ? EnumImageMsg.Information : EnumImageMsg.Warning,
          height: 616);
        resultPopup.ShowDialog(this);
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        using var popup = new PopupConfirm(
          "Không thể import file Excel. Vui lòng kiểm tra lại file !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popup.ShowDialog(this);
      }
    }

    private static bool TryParseWasteType(
      string value,
      out EnumWasteType wasteType)
    {
      var normalizedValue = NormalizeImportValue(value);
      wasteType = normalizedValue switch
      {
        "1" or "hazardous" or "nguyhai" => EnumWasteType.Hazardous,
        "2" or "nonrecyclable" or "khongtaiche" or "khongthetaiche" =>
          EnumWasteType.NonRecyclable,
        "3" or "recyclable" or "taiche" or "cothetaiche" =>
          EnumWasteType.Recyclable,
        _ => EnumWasteType.None,
      };

      return wasteType != EnumWasteType.None;
    }

    private static string NormalizeImportValue(string value)
    {
      var normalized = (value ?? string.Empty).Trim().Normalize(NormalizationForm.FormD);
      var builder = new StringBuilder(normalized.Length);

      foreach (var character in normalized)
      {
        if (CharUnicodeInfo.GetUnicodeCategory(character) !=
            UnicodeCategory.NonSpacingMark && char.IsLetterOrDigit(character))
        {
          builder.Append(char.ToLowerInvariant(character));
        }
      }

      return builder.ToString();
    }

    private async Task ImportProductsAsync(XLWorkbook workbook)
    {
      if (!workbook.Worksheets.TryGetWorksheet("TGA_ChatThai", out var tgaSheet) ||
          !workbook.Worksheets.TryGetWorksheet("HcP_ChatThai", out var hcpSheet))
      {
        using var popup = new PopupConfirm(
          "File Excel phải có đủ hai sheet TGA_ChatThai và HcP_ChatThai !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popup.ShowDialog(this);
        return;
      }

      var productGroups = await _productGroupService.GetAllAsync();
      var tgaProductGroup = FindProductGroup(productGroups, "TGA");
      var hcpProductGroup = FindProductGroup(productGroups, "HcP");
      if (tgaProductGroup == null || hcpProductGroup == null)
      {
        var missingGroups = new List<string>();
        if (tgaProductGroup == null) missingGroups.Add("TGA");
        if (hcpProductGroup == null) missingGroups.Add("HcP");

        using var popup = new PopupConfirm(
          $"Không tìm thấy nhóm chất thải: {string.Join(", ", missingGroups)} !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popup.ShowDialog(this);
        return;
      }

      var existingProducts = await _productService.GetAllAsync(IsContainDelete: true);
      var productsByGroupNameAndWasteType = existingProducts
        .Where(item => item.ProductGroupId.HasValue &&
          !string.IsNullOrWhiteSpace(item.Name))
        .GroupBy(item => BuildImportKey(
          item.ProductGroupId!.Value, item.Name!, item.EnumWasteType),
          StringComparer.OrdinalIgnoreCase)
        .ToDictionary(group => group.Key, group => group.First(),
          StringComparer.OrdinalIgnoreCase);

      var errors = new List<string>();
      var importedCount = 0;
      var addedCount = 0;
      var updatedCount = 0;

      async Task ImportSheetAsync(IXLWorksheet worksheet, ProductGroup productGroup)
      {
        var lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 2;
        for (var rowNumber = 3; rowNumber <= lastRow; rowNumber++)
        {
          var wasteTypeText = worksheet.Cell(rowNumber, 1).GetString().Trim();
          var code = worksheet.Cell(rowNumber, 2).GetString().Trim();
          var name = worksheet.Cell(rowNumber, 3).GetString().Trim();
          var tagMappingReport = worksheet.Cell(rowNumber, 4).GetString().Trim();

          if (string.IsNullOrWhiteSpace(wasteTypeText) &&
              string.IsNullOrWhiteSpace(code) &&
              string.IsNullOrWhiteSpace(name) &&
              string.IsNullOrWhiteSpace(tagMappingReport))
          {
            continue;
          }

          if (string.IsNullOrWhiteSpace(name))
          {
            errors.Add($"Sheet {worksheet.Name}, dòng {rowNumber}: thiếu Tên.");
            continue;
          }

          if (!TryParseWasteType(wasteTypeText, out var wasteType))
          {
            errors.Add(
              $"Sheet {worksheet.Name}, dòng {rowNumber}: loại chất thải '{wasteTypeText}' không hợp lệ.");
            continue;
          }

          Product? product = null;
          productsByGroupNameAndWasteType.TryGetValue(
            BuildImportKey(productGroup.Id, name, wasteType), out product);

          var isNew = product == null;
          product ??= new Product
          {
            CreatedAt = DateTime.UtcNow,
            EnableFlag = true,
          };

          product.Code = string.IsNullOrWhiteSpace(code) ? null : code;
          product.Name = name;
          product.ProductGroupId = productGroup.Id;
          product.EnumWasteType = wasteType;
          product.TagMappingReport = string.IsNullOrWhiteSpace(tagMappingReport)
            ? null
            : tagMappingReport;
          product.DeletedFlag = false;
          product.EnableFlag = true;
          product.SyncFlag = false;

          try
          {
            var savedProduct = await _productService.AddOrUpdateAsync(product);
            productsByGroupNameAndWasteType[
              BuildImportKey(productGroup.Id, name, wasteType)] =
              savedProduct;
            importedCount++;
            if (isNew)
              addedCount++;
            else
              updatedCount++;
          }
          catch (Exception rowException)
          {
            LogHelper.LogErrorToFileLog(rowException, AppCore.Ins._folderFileLog);
            errors.Add(
              $"Sheet {worksheet.Name}, dòng {rowNumber}: không thể lưu dữ liệu.");
          }
        }
      }

      await ImportSheetAsync(tgaSheet, tgaProductGroup);
      await ImportSheetAsync(hcpSheet, hcpProductGroup);

      await LoadData(EnumTypeMasterData.Product);
      MasterDataChangeNotifier.Notify<Product>();

      var message =
        $"Import chất thải hoàn tất.\r\n" +
        $"- Tổng thành công: {importedCount}\r\n" +
        $"- Thêm mới: {addedCount}\r\n" +
        $"- Cập nhật: {updatedCount}\r\n" +
        $"- Bỏ qua: {errors.Count}";

      if (errors.Count > 0)
      {
        message += "\r\n\r\n" + string.Join("\r\n", errors.Take(10));
        if (errors.Count > 10)
          message += $"\r\n... và {errors.Count - 10} lỗi khác.";
      }

      using var resultPopup = new PopupConfirm(
        message,
        EnumTypeMsg.MessageManualClose,
        errors.Count == 0 ? EnumImageMsg.Information : EnumImageMsg.Warning,
        height: 616);
      resultPopup.ShowDialog(this);
    }

    private static ProductGroup? FindProductGroup(
      IEnumerable<ProductGroup> productGroups,
      string groupName)
    {
      return productGroups
        .OrderBy(group => group.Name)
        .FirstOrDefault(group => group.Name?.Contains(
          groupName, StringComparison.OrdinalIgnoreCase) == true);
    }

    private static string BuildImportKey(
      Guid productGroupId,
      string name,
      EnumWasteType wasteType)
    {
      return $"{productGroupId:N}|{name.Trim()}|{wasteType}";
    }

    private async Task ImportTaresAsync(XLWorkbook workbook)
    {
      if (!workbook.Worksheets.TryGetWorksheet("Tare_ChatThai", out var worksheet))
      {
        using var popup = new PopupConfirm(
          "Không tìm thấy sheet Tare_ChatThai trong file Excel !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popup.ShowDialog(this);
        return;
      }

      var existingTares = await _tareService.GetAllAsync(
        EnumTypeDataLocation.Good, isContainDelete: true);
      var taresByName = existingTares
        .Where(item => !string.IsNullOrWhiteSpace(item.Name))
        .GroupBy(item => item.Name!.Trim(), StringComparer.OrdinalIgnoreCase)
        .ToDictionary(group => group.Key, group => group.First(),
          StringComparer.OrdinalIgnoreCase);

      var errors = new List<string>();
      var importedCount = 0;
      var addedCount = 0;
      var updatedCount = 0;
      var lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 2;

      for (var rowNumber = 3; rowNumber <= lastRow; rowNumber++)
      {
        var name = worksheet.Cell(rowNumber, 1).GetString().Trim();
        var valueText = worksheet.Cell(rowNumber, 2).GetFormattedString().Trim();

        if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(valueText))
          continue;

        if (string.IsNullOrWhiteSpace(name))
        {
          errors.Add($"Dòng {rowNumber}: thiếu Tên.");
          continue;
        }

        var normalizedValue = valueText.Replace(',', '.');
        if (!double.TryParse(
              normalizedValue,
              NumberStyles.Float,
              CultureInfo.InvariantCulture,
              out var tareValue) ||
            !double.IsFinite(tareValue) ||
            tareValue <= 0)
        {
          errors.Add($"Dòng {rowNumber}: giá trị Tare '{valueText}' không hợp lệ.");
          continue;
        }

        var isNew = !taresByName.TryGetValue(name, out var categoryTare);
        categoryTare ??= new Tare
        {
          CreatedAt = DateTime.UtcNow,
          EnableFlag = true,
        };

        categoryTare.Code = null;
        categoryTare.Name = name;
        categoryTare.Value = tareValue;
        categoryTare.Description = null;
        categoryTare.DeletedFlag = false;
        categoryTare.EnableFlag = true;
        categoryTare.SyncFlag = false;

        try
        {
          var savedTare = await _tareService.AddOrUpdateAsync(
            categoryTare, EnumTypeDataLocation.Good);
          taresByName[name] = savedTare;
          importedCount++;
          if (isNew)
            addedCount++;
          else
            updatedCount++;
        }
        catch (Exception rowException)
        {
          LogHelper.LogErrorToFileLog(rowException, AppCore.Ins._folderFileLog);
          errors.Add($"Dòng {rowNumber}: không thể lưu dữ liệu.");
        }
      }

      await LoadData(EnumTypeMasterData.Tare);
      MasterDataChangeNotifier.Notify<Tare>();

      var message =
        $"Import Tare hoàn tất.\r\n" +
        $"- Tổng thành công: {importedCount}\r\n" +
        $"- Thêm mới: {addedCount}\r\n" +
        $"- Cập nhật: {updatedCount}\r\n" +
        $"- Bỏ qua: {errors.Count}";

      if (errors.Count > 0)
      {
        message += "\r\n\r\n" + string.Join("\r\n", errors.Take(10));
        if (errors.Count > 10)
          message += $"\r\n... và {errors.Count - 10} lỗi khác.";
      }

      using var resultPopup = new PopupConfirm(
        message,
        EnumTypeMsg.MessageManualClose,
        errors.Count == 0 ? EnumImageMsg.Information : EnumImageMsg.Warning,
        height: 616);
      resultPopup.ShowDialog(this);
    }

    private async Task ImportTruckTaresAsync(XLWorkbook workbook)
    {
      if (!workbook.Worksheets.TryGetWorksheet("Tare_Xetai", out var worksheet))
      {
        using var popup = new PopupConfirm(
          "Không tìm thấy sheet Tare_Xetai trong file Excel !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popup.ShowDialog(this);
        return;
      }

      var existingTares = await _tareService.GetAllAsync(
        EnumTypeDataLocation.Truck, isContainDelete: true);
      var taresByName = existingTares
        .Where(item => !string.IsNullOrWhiteSpace(item.Name))
        .GroupBy(item => item.Name!.Trim(), StringComparer.OrdinalIgnoreCase)
        .ToDictionary(group => group.Key, group => group.First(),
          StringComparer.OrdinalIgnoreCase);

      var errors = new List<string>();
      var importedCount = 0;
      var addedCount = 0;
      var updatedCount = 0;
      var lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 2;

      for (var rowNumber = 3; rowNumber <= lastRow; rowNumber++)
      {
        var name = worksheet.Cell(rowNumber, 1).GetString().Trim();
        var valueText = worksheet.Cell(rowNumber, 2).GetFormattedString().Trim();

        if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(valueText))
          continue;

        if (string.IsNullOrWhiteSpace(name))
        {
          errors.Add($"Dòng {rowNumber}: thiếu Tên.");
          continue;
        }

        var normalizedValue = valueText.Replace(',', '.');
        if (!double.TryParse(
              normalizedValue,
              NumberStyles.Float,
              CultureInfo.InvariantCulture,
              out var tareValue) ||
            !double.IsFinite(tareValue) ||
            tareValue <= 0)
        {
          errors.Add($"Dòng {rowNumber}: giá trị Tare '{valueText}' không hợp lệ.");
          continue;
        }

        var isNew = !taresByName.TryGetValue(name, out var tareTruck);
        tareTruck ??= new Tare
        {
          CreatedAt = DateTime.UtcNow,
          EnableFlag = true,
        };

        tareTruck.Code = null;
        tareTruck.Name = name;
        tareTruck.Value = tareValue;
        tareTruck.Description = null;
        tareTruck.DeletedFlag = false;
        tareTruck.EnableFlag = true;
        tareTruck.SyncFlag = false;

        try
        {
          var savedTare = await _tareService.AddOrUpdateAsync(
            tareTruck, EnumTypeDataLocation.Truck);
          taresByName[name] = savedTare;
          importedCount++;
          if (isNew)
            addedCount++;
          else
            updatedCount++;
        }
        catch (Exception rowException)
        {
          LogHelper.LogErrorToFileLog(rowException, AppCore.Ins._folderFileLog);
          errors.Add($"Dòng {rowNumber}: không thể lưu dữ liệu.");
        }
      }

      await LoadData(EnumTypeMasterData.TareForTruck);
      MasterDataChangeNotifier.Notify<Tare>();

      var message =
        $"Import Tare xe tải hoàn tất.\r\n" +
        $"- Tổng thành công: {importedCount}\r\n" +
        $"- Thêm mới: {addedCount}\r\n" +
        $"- Cập nhật: {updatedCount}\r\n" +
        $"- Bỏ qua: {errors.Count}";

      if (errors.Count > 0)
      {
        message += "\r\n\r\n" + string.Join("\r\n", errors.Take(10));
        if (errors.Count > 10)
          message += $"\r\n... và {errors.Count - 10} lỗi khác.";
      }

      using var resultPopup = new PopupConfirm(
        message,
        EnumTypeMsg.MessageManualClose,
        errors.Count == 0 ? EnumImageMsg.Information : EnumImageMsg.Warning,
        height: 616);
      resultPopup.ShowDialog(this);
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
      if (Environment.GetEnvironmentVariable("STATION") == "1")
      {
        if (!AppCore.Ins.CheckPermission("0044"))
        {
          using var openErrorPopup = new PopupNoPermitRole();
          openErrorPopup.ShowDialog(this);
          return;
        }
      }
      else
      {
        if (!AppCore.Ins.CheckPermission("0051"))
        {
          using var openErrorPopup = new PopupNoPermitRole();
          openErrorPopup.ShowDialog(this);
          return;
        }
      }

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
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.TareForTruck)
      {
        var popupTareTruck = new PopupTareForTruck();
        popupTareTruck.OnSendSuccess += PopupTareTruck_OnSendSuccess;
        popupTareTruck.ShowDialog();
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
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Document)
      {
        var popupDocument = new PopupDocument();
        popupDocument.OnSendSuccess += PopupDocument_OnSendSuccess;
        popupDocument.ShowDialog();
      }
    }

    private async void PopupDocument_OnSendSuccess(DocumentData document)
    {
      try
      {
        await QueueDocumentUpsertAsync(document);
        await LoadData(_enumTypeMasterDataCurrent);
        MasterDataChangeNotifier.Notify<DocumentData>();
        ShowSaveSuccess(document.UpdatedAt.HasValue
          ? "Cập nhật thành công."
          : "Thêm thành công.");
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private async Task QueueDocumentUpsertAsync(DocumentData document)
    {
      if (!document.Date.HasValue)
        throw new InvalidOperationException("DocumentDate không được để trống.");

      var request = new DocumentUpsertRequest
      {
        Id = document.IdSrc ?? document.Id,
        DocumentDate = document.Date.Value,
        Name = document.Document ?? string.Empty,
        Description = document.Description,
        UpdatedAt = document.UpdatedAt ?? document.CreatedAt ?? DateTime.UtcNow,
        DeletedFlag = document.DeletedFlag,
      };
      var apiJob = new ApiJobs
      {
        Json = JsonHelper.ToJson(request),
        EnumTypeAPI = EnumTypeAPI.MD_Document,
        EnumStatusAPI = EnumStatusAPI.Created,
        CreatedAt = DateTime.UtcNow,
      };

      await _apiJobsService.AddOrUpdateAsync(apiJob);
    }

    private async void PopupDelivery_OnSendSuccess(Delivery delivery)
    {
      try
      {
        var deliveryUpsertRequest = new DeliveryUpsertRequest
        {
          Id = delivery.IdSrc ?? delivery.Id,
          Name = delivery.Name ?? string.Empty,
          OfficeAddress = delivery.OfficeAddress,
          PhoneForOfficeAddress = delivery.PhoneForOfficeAddress,
          AgentAddress = delivery.AgentAddress,
          PhoneForAgentAddress = delivery.PhoneForAgentAddress,
          Description = delivery.Description,
          DeletedFlag = delivery.DeletedFlag,
        };

        var apiJob = new ApiJobs
        {
          Json = JsonHelper.ToJson(deliveryUpsertRequest),
          EnumTypeAPI = EnumTypeAPI.MD_Delivery,
          EnumStatusAPI = EnumStatusAPI.Created,
          CreatedAt = DateTime.UtcNow,
        };

        await _apiJobsService.AddOrUpdateAsync(apiJob);
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

    private async void PopupTypeTare_OnSendSuccess(Tare obj)
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
        MasterDataChangeNotifier.Notify<Tare>();
        ShowSaveSuccess(obj.UpdatedAt.HasValue ? "Cập nhật thành công." : "Thêm thành công.");
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private async void PopupTareTruck_OnSendSuccess(Tare obj)
    {
      try
      {
        await LoadData(_enumTypeMasterDataCurrent);
        MasterDataChangeNotifier.Notify<Tare>();
        ShowSaveSuccess(obj.UpdatedAt.HasValue ? "Cập nhật thành công." : "Thêm thành công.");
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
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
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
      else if (enumTypeMasterData == EnumTypeMasterData.TareForTruck)
      {
        var autoSizeColumns = new[]
        {
          nameof(TareTruckDTO.No),
          nameof(TareTruckDTO.UpdatedAt),
          nameof(TareTruckDTO.Value),
        };
        foreach (var columnName in autoSizeColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        if (dgv.Columns.Contains(nameof(TareTruckDTO.No)))
          dgv.Columns[nameof(TareTruckDTO.No)].DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleCenter;

        var alignmentRightCenterColumns = new[]
        {
          nameof(TareTruckDTO.Value),
          nameof(TareTruckDTO.UpdatedAt),
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
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
          nameof(ProductDTO.TagMappingReport),
          nameof(ProductDTO.Description),
          //nameof(ProductDTO.Name),
        };
        foreach (var columnName in autoSizeColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
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
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        if (dgv.Columns.Contains(nameof(DeliveryDTO.No)))
          dgv.Columns[nameof(DeliveryDTO.No)].DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleCenter;

        if (dgv.Columns.Contains(nameof(DeliveryDTO.UpdatedAt)))
          dgv.Columns[nameof(DeliveryDTO.UpdatedAt)].DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight;
      }
      else if (enumTypeMasterData == EnumTypeMasterData.Document)
      {
        var autoSizeColumns = new[]
        {
          nameof(DocumentDataDTO.No),
          nameof(DocumentDataDTO.Date),
          nameof(DocumentDataDTO.UpdatedAt),
        };
        foreach (var columnName in autoSizeColumns)
        {
          if (dgv.Columns.Contains(columnName))
            dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        if (dgv.Columns.Contains(nameof(DocumentDataDTO.No)))
          dgv.Columns[nameof(DocumentDataDTO.No)].DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleCenter;

        if (dgv.Columns.Contains(nameof(DocumentDataDTO.UpdatedAt)))
          dgv.Columns[nameof(DocumentDataDTO.UpdatedAt)].DefaultCellStyle.Alignment =
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

      try
      {
        if (columnName == EditButtonColumnName)
          await EditMasterDataAsync(rowData);
        else if (columnName == DeleteButtonColumnName)
          await DeleteMasterDataAsync(rowData);
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private Task EditMasterDataAsync(object? rowData)
    {
      if (Environment.GetEnvironmentVariable("STATION") == "1")
      {
        if (!AppCore.Ins.CheckPermission("0044"))
        {
          using var openErrorPopup = new PopupNoPermitRole();
          openErrorPopup.ShowDialog(this);
          return Task.CompletedTask;
        }
      }
      else
      {
        if (!AppCore.Ins.CheckPermission("0051"))
        {
          using var openErrorPopup = new PopupNoPermitRole();
          openErrorPopup.ShowDialog(this);
          return Task.CompletedTask;
        }
      }

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
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.TareForTruck)
      {
        var data = rowData as TareTruckDTO;
        if (data?.TareTruck != null)
        {
          var popupTareTruck = new PopupTareForTruck(data.TareTruck);
          popupTareTruck.OnSendSuccess += PopupTareTruck_OnSendSuccess;
          popupTareTruck.ShowDialog();
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
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Document)
      {
        var data = rowData as DocumentDataDTO;
        if (data?.DocumentData != null)
        {
          var popupDocument = new PopupDocument(data.DocumentData);
          popupDocument.OnSendSuccess += PopupDocument_OnSendSuccess;
          popupDocument.ShowDialog();
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
      if (Environment.GetEnvironmentVariable("STATION") == "1")
      {
        if (!AppCore.Ins.CheckPermission("0044"))
        {
          using var openErrorPopup = new PopupNoPermitRole();
          openErrorPopup.ShowDialog(this);
          return Task.CompletedTask;
        }
      }
      else
      {
        if (!AppCore.Ins.CheckPermission("0051"))
        {
          using var openErrorPopup = new PopupNoPermitRole();
          openErrorPopup.ShowDialog(this);
          return Task.CompletedTask;
        }
      }

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
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.TareForTruck)
      {
        var data = rowData as TareTruckDTO;
        if (data?.TareTruck != null)
        {
          using var popupMsg = new PopupConfirm("Bạn có chắc chắn xóa dữ liệu này !",
            EnumTypeMsg.Confirm, EnumImageMsg.Warning, data.TareTruck);
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
      else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Document)
      {
        var data = rowData as DocumentDataDTO;
        if (data?.DocumentData != null)
        {
          using var popupMsg = new PopupConfirm("Bạn có chắc chắn xóa dữ liệu này !",
            EnumTypeMsg.Confirm, EnumImageMsg.Warning, data.DocumentData);
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
          var categoryTare = e.Obj as Tare;
          if (categoryTare != null)
          {
            categoryTare.DeletedFlag = true;
            await _tareService.AddOrUpdateAsync(
              categoryTare, EnumTypeDataLocation.Good);

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
            MasterDataChangeNotifier.Notify<Tare>();
          }
        }
        else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.TareForTruck)
        {
          var tareTruck = e.Obj as Tare;
          if (tareTruck != null)
          {
            tareTruck.DeletedFlag = true;
            tareTruck.UpdatedAt = DateTime.UtcNow;
            await _tareService.AddOrUpdateAsync(
              tareTruck, EnumTypeDataLocation.Truck);
            await LoadData(_enumTypeMasterDataCurrent);
            MasterDataChangeNotifier.Notify<Tare>();
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

            var deliveryUpsertRequest = new DeliveryUpsertRequest
            {
              Id = delivery.IdSrc ?? delivery.Id,
              Name = delivery.Name ?? string.Empty,
              OfficeAddress = delivery.OfficeAddress,
              PhoneForOfficeAddress = delivery.PhoneForOfficeAddress,
              AgentAddress = delivery.AgentAddress,
              PhoneForAgentAddress = delivery.PhoneForAgentAddress,
              Description = delivery.Description,
              DeletedFlag = true,
            };

            var apiJob = new ApiJobs
            {
              Json = JsonHelper.ToJson(deliveryUpsertRequest),
              EnumTypeAPI = EnumTypeAPI.MD_Delivery,
              EnumStatusAPI = EnumStatusAPI.Created,
              CreatedAt = DateTime.UtcNow,
            };

            await _apiJobsService.AddOrUpdateAsync(apiJob);
            await LoadData(_enumTypeMasterDataCurrent);
            MasterDataChangeNotifier.Notify<Delivery>();
          }
        }
        else if (_enumTypeMasterDataCurrent == EnumTypeMasterData.Document)
        {
          var document = e.Obj as DocumentData;
          if (document != null)
          {
            document.DeletedFlag = true;
            document.UpdatedAt = DateTime.UtcNow;
            await _documentDataService.AddOrUpdateAsync(document);
            await QueueDocumentUpsertAsync(document);
            await LoadData(_enumTypeMasterDataCurrent);
            MasterDataChangeNotifier.Notify<DocumentData>();
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
