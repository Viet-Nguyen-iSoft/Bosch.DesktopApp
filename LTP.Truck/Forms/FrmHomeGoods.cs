using ApiSyncData.Req;
using Common;
using HelperManager;
using iSoft.Communication.Interface;
using iSoft.Communication.Mode;
using iSoft.Database;
using iSoft.Database.DTO;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;
using LTP.Truck.Controls;
using LTP.Truck.Custom;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using static Common.EnumData;
using static HelperManager.EnumData;

namespace LTP.Truck.Forms
{
  public partial class FrmHomeGoods : Form
  {
    private const string SelectColumnName = "SelectRecord";
    private List<Product> _products = new();
    private int _productGroupRefreshVersion;
    private int _tareRefreshVersion;
    private int _sumWeightLoadVersion;
    private DataWeightInterface _msgDataWeight { get; set; } = new DataWeightInterface();
    private RecordTruckDTO _recordTruckDTO { get; set; }
    private CategoryTare? _categoryTare { get; set; }
    public FrmHomeGoods()
    {
      InitializeComponent();
      CustomUI();

      cbbTare.SelectedValueChanged += cbbTare_SelectedValueChanged;
      btnSearchHistorical.Click += btnSearchHistorical_Click;
      txtLicensePlate._TextChanged += TxtLicensePlate__TextChanged;
      lbTare.Text = "0.000";
      this.Load += FrmHomeGoods_Load;
    }

    #region Instance
    private static FrmHomeGoods _Instance = null;
    public static FrmHomeGoods Instance
    {
      get
      {
        if (_Instance == null) _Instance = new FrmHomeGoods();
        return _Instance;
      }
    }
    #endregion

    private void CustomUI()
    {
      ucTimeSearchFrom.Value = DateTime.Today;
      ucTimeSearchTo.Value = DateTime.Today.AddDays(1).AddMinutes(-1);

      ElipseControl elipseControl = new ElipseControl();
      elipseControl.TargetControl = tableLayoutPanel3;
      elipseControl.CornerRadius = 20;

      ElipseControl elipseControl01 = new ElipseControl();
      elipseControl01.TargetControl = tableLayoutPanel4;
      elipseControl01.CornerRadius = 20;

      ElipseControl elipseControl02 = new ElipseControl();
      elipseControl02.TargetControl = tableLayoutPanel9;
      elipseControl02.CornerRadius = 20;

      ElipseControl elipseControl03 = new ElipseControl();
      elipseControl03.TargetControl = tableLayoutPanel14;
      elipseControl03.CornerRadius = 20;

      ElipseControl elipseControl04 = new ElipseControl();
      elipseControl04.TargetControl = tableLayoutPanel17;
      elipseControl04.CornerRadius = 20;

      ElipseControl elipseControl05 = new ElipseControl();
      elipseControl05.TargetControl = tableLayoutPanel12;
      elipseControl05.CornerRadius = 20;

      dgv.EnableHeadersVisualStyles = false;
      dgv.ColumnHeadersHeight = 50;
      dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
      dgv.RowTemplate.Height = 60;
      dgv.MultiSelect = false;
      dgv.ReadOnly = true;
      dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(64, 107, 177);
      dgv.DefaultCellStyle.SelectionForeColor = Color.White;

      var selectColumn = new DataGridViewCheckBoxColumn
      {
        Name = SelectColumnName,
        HeaderText = string.Empty,
        Width = 50,
        AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
        ReadOnly = true,
        Frozen = true
      };
      dgv.Columns.Insert(0, selectColumn);
      dgv.CellClick += dgv_CellClick;
    }

    private async void FrmHomeGoods_Load(object? sender, EventArgs e)
    {
      try
      {
        await LoadDataFirst();
        await LoadHistorical();
        await LoadLicensePlateSuggestionsAsync();

        cbbProductGroup.SelectedIndex = -1;
        cbbTare.SelectedIndex = -1;

        //Đăng kí sự kiện
        cbbProductGroup.SelectedValueChanged += cbbProductGroup_SelectedValueChanged;

        FrmMain.Instance.OnChangeProductGroup += Instance_OnChangeProductGroup;
        FrmMain.Instance.OnChangeProduct += Instance_OnChangeProduct;
        FrmMain.Instance.OnChangeTare += Instance_OnChangeTare;
        AppCore.Ins.OnSendDataWeightGoods += Ins_OnSendDataWeightGoods;
        AppCore.Ins.OnSendStatusWeightGoods += Ins_OnSendStatusWeightGoods;
        ResetWeightDisplay();
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private async Task LoadLicensePlateSuggestionsAsync()
    {
      try
      {
        var licensePlates = await AppCore.Ins._licensePlateService
          .GetAllAsync(IsContainDelete: false);

        txtLicensePlate.SetAutoCompleteSource(
          licensePlates.Select(licensePlate => licensePlate.Plate));
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private void Ins_OnSendDataWeightGoods(object? sender, DataWeightInterface e)
    {
      _msgDataWeight = e;
      SetDataWeight(e);
    }

    private void Ins_OnSendStatusWeightGoods(object? sender, CommunicationStatusChangedEventArgs e)
    {
      if (!e.IsConnected)
        ResetWeightDisplay();
    }

    private void ResetWeightDisplay()
    {
      if (InvokeRequired)
      {
        BeginInvoke(new Action(ResetWeightDisplay));
        return;
      }

      _msgDataWeight = new DataWeightInterface();
      lbWeightValue.Text = "---";
      lbGross.Text = "---";
    }

    private void SetDataWeight(DataWeightInterface messageData)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDataWeight(messageData);
        }));
        return;
      }

      //Net
      lbWeightValue.Text = WeightFormatHelper.Format(messageData.IndicatedWeight, 3);

      //Tare
      lbTareSrc.Text = WeightFormatHelper.Format(messageData.TareWeight, 3);

      //Tare
      if (_categoryTare != null)
      {
        lbGross.Text = WeightFormatHelper.Format(messageData.IndicatedWeight + (messageData?.TareWeight ?? 0.0), 3);
      }
      else
      {
        lbGross.Text = WeightFormatHelper.Format(messageData.IndicatedWeight, 3);
      }
    }

    private async void Instance_OnChangeTare(object? sender, EventArgs e)
    {
      if (InvokeRequired)
      {
        BeginInvoke(new Action(() => Instance_OnChangeTare(sender, e)));
        return;
      }

      var refreshVersion = ++_tareRefreshVersion;

      try
      {
        var categoryTares = await AppCore.Ins._categoryTareService.GetAllAsync();
        if (IsDisposed || Disposing || refreshVersion != _tareRefreshVersion)
          return;

        var selectedTareId = (cbbTare.SelectedItem as CategoryTare)?.Id;
        var selectedTareIndex = categoryTares.FindIndex(tare => tare.Id == selectedTareId);

        SetTare(categoryTares);
        cbbTare.SelectedIndex = selectedTareIndex;
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private async void Instance_OnChangeProduct(object? sender, EventArgs e)
    {
      if (InvokeRequired)
      {
        BeginInvoke(new Action(() => Instance_OnChangeProduct(sender, e)));
        return;
      }

      var selectedProductId = (cbbProduct.SelectedItem as Product)?.Id;
      _products = await AppCore.Ins._productService.GetAllAsync();
      FillProduct(selectedProductId, preserveSelection: true);
    }

    private async void Instance_OnChangeProductGroup(object? sender, EventArgs e)
    {
      if (InvokeRequired)
      {
        BeginInvoke(new Action(() => Instance_OnChangeProductGroup(sender, e)));
        return;
      }

      var refreshVersion = ++_productGroupRefreshVersion;

      try
      {
        var productGroups = await AppCore.Ins._productGroupService.GetAllAsync();
        if (IsDisposed || Disposing || refreshVersion != _productGroupRefreshVersion)
          return;

        var selectedProductGroupId = (cbbProductGroup.SelectedItem as ProductGroup)?.Id;
        var selectedProductId = (cbbProduct.SelectedItem as Product)?.Id;
        var selectedGroupIndex = productGroups.FindIndex(group => group.Id == selectedProductGroupId);

        cbbProductGroup.SelectedValueChanged -= cbbProductGroup_SelectedValueChanged;
        try
        {
          SetProductGroup(productGroups);
          cbbProductGroup.SelectedIndex = selectedGroupIndex;
          FillProduct(selectedProductId, preserveSelection: true);
        }
        finally
        {
          cbbProductGroup.SelectedValueChanged += cbbProductGroup_SelectedValueChanged;
        }
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private async Task LoadDataFirst()
    {
      _products = await AppCore.Ins._productService.GetAllAsync();
      var productGroups = await AppCore.Ins._productGroupService.GetAllAsync();
      var categoryTares = await AppCore.Ins._categoryTareService.GetAllAsync();

      SetProductGroup(productGroups);
      SetTare(categoryTares);
    }

    private void SetProductGroup(List<ProductGroup> productGroups)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetProductGroup(productGroups);
        }));
        return;
      }

      cbbProductGroup.DisplayMember = nameof(ProductGroup.Name);
      cbbProductGroup.ValueMember = nameof(ProductGroup.Id);
      cbbProductGroup.DataSource = productGroups;
    }
    private void SetTare(List<CategoryTare> categoryTares)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetTare(categoryTares);
        }));
        return;
      }

      cbbTare.DisplayMember = nameof(CategoryTare.Name);
      cbbTare.ValueMember = nameof(CategoryTare.Id);
      cbbTare.DataSource = categoryTares;
    }

    private void cbbTare_SelectedValueChanged(object? sender, EventArgs e)
    {
      _categoryTare = cbbTare.SelectedItem as CategoryTare;
      if (_categoryTare != null)
      {
        lbTare.Text = _categoryTare?.Value is double tareValue
          ? WeightFormatHelper.Format(tareValue, 3)
          : string.Empty;
      }
      else
      {
        lbTare.Text = WeightFormatHelper.Format(0.0, 3);
      }
    }

    private void cbbProductGroup_SelectedValueChanged(object? sender, EventArgs e)
    {
      FillProduct();
    }

    private void FillProduct()
    {
      FillProduct(null, preserveSelection: false);
    }

    private void FillProduct(Guid? selectedProductId, bool preserveSelection)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          FillProduct(selectedProductId, preserveSelection);
        }));
        return;
      }

      if (cbbProductGroup.SelectedItem is not ProductGroup selectedProductGroup)
      {
        cbbProduct.DataSource = null;
        return;
      }

      var products = _products
        .Where(product => product.ProductGroupId == selectedProductGroup.Id)
        .ToList();

      cbbProduct.DisplayMember = nameof(Product.Name);
      cbbProduct.ValueMember = nameof(Product.Id);
      cbbProduct.DataSource = products;

      if (!preserveSelection)
        return;

      if (selectedProductId.HasValue &&
        products.Any(product => product.Id == selectedProductId.Value))
      {
        cbbProduct.SelectedValue = selectedProductId.Value;
      }
      else
      {
        cbbProduct.SelectedIndex = -1;
      }
    }

    private async void btnLoadLicensePlate_Click(object sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      btnLoadLicensePlate.Enabled = false;
      try
      {
        await ShowFirstWeighingRecordsAsync();
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        if (!IsDisposed && !Disposing)
        {
          using var popupMsg = new PopupConfirm("Không thể tải danh sách phiếu cân lần 1. Vui lòng thử lại !",
          EnumTypeMsg.MessageManualClose, EnumImageMsg.Information);
          popupMsg.ShowDialog();
        }
      }
      finally
      {
        if (!IsDisposed && !Disposing)
          btnLoadLicensePlate.Enabled = true;
      }
    }

    private async Task ShowFirstWeighingRecordsAsync()
    {
      var filtered = await AppCore.Ins._recordTruckService.GetFirstWeighingRecordsAsync();
      if (IsDisposed || Disposing)
        return;

      if (filtered.Count == 0)
      {
        using var popupMsg = new PopupConfirm("Không có dữ liệu cân lần 1 !",
          EnumTypeMsg.MessageManualClose, EnumImageMsg.Information);
        popupMsg.ShowDialog();
        return;
      }

      using var popup = new LTP.Truck.Popup.PopupLoadMD();
      var records = iSoft.Database.DTOHelper.ConvertRecordTruckDTO(filtered.ToList());
      popup.SetData(records);
      popup.OnSendData += async (data, type) =>
      {
        if (data is iSoft.Database.DTO.RecordTruckDTO selectedRecord)
        {
          _recordTruckDTO = selectedRecord;
          txtLicensePlate.Texts = selectedRecord.LicensePlate ?? string.Empty;
          txtNameDriver.Texts = selectedRecord.NameDriver ?? string.Empty;
          txtIdCard.Texts = selectedRecord.IdCard ?? string.Empty;
          await LoadSumWeightAsync(selectedRecord.RecordTruck?.LicensePlate ?? string.Empty);
        }
      };
      popup.ShowDialog(this);
    }

    private async Task LoadSumWeightAsync(string plate)
    {
      var loadVersion = ++_sumWeightLoadVersion;

      try
      {
        var totalWeight = !string.IsNullOrEmpty(plate)
          ? await AppCore.Ins._recordWeightService.SumNetByRecordTruckIdAsync(plate)
          : 0.0;

        if (IsDisposed || Disposing || loadVersion != _sumWeightLoadVersion)
          return;

        if (InvokeRequired)
        {
          BeginInvoke(new Action(() =>
          {
            if (loadVersion == _sumWeightLoadVersion)
              lbSumWeight.Text = WeightFormatHelper.Format(totalWeight, 3);
          }));
          return;
        }

        lbSumWeight.Text = WeightFormatHelper.Format(totalWeight, 3);
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private async void TxtLicensePlate__TextChanged(object? sender, EventArgs e)
    {
      var validLicense = LicensePlateHelper.IsValidVietnamLicensePlate(txtLicensePlate.Texts.Trim());
      if (validLicense.IsValid)
      {
        await LoadSumWeightAsync(validLicense.Plate);
      }
      else
      {
        if (InvokeRequired)
        {
          BeginInvoke(new Action(() =>
          {
            lbSumWeight.Text = "0.000";
          }));
          return;
        }

        lbSumWeight.Text = "0.000";
      }
    }


    private async void btnSaveData_Click(object sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      if (string.IsNullOrEmpty(txtLicensePlate.Texts.Trim()))
      {
        PopupConfirm popupConfirm = new PopupConfirm("Vui lòng chọn hoặc điền biển số xe !", EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popupConfirm.ShowDialog();
        return;
      }

      var validLicense = LicensePlateHelper.IsValidVietnamLicensePlate(txtLicensePlate.Texts.Trim());
      if (!validLicense.IsValid)
      {
        PopupConfirm popupConfirm = new PopupConfirm("Biển số xe không hợp lệ !", EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popupConfirm.ShowDialog();
        return;
      }

      //if (_recordTruckDTO?.RecordTruck is not RecordTruck selectedRecordTruck)
      //{
      //  using var popupMsg = new PopupConfirm("Vui lòng chọn biển số xe !",
      //    EnumTypeMsg.MessageManualClose, EnumImageMsg.Information);
      //  popupMsg.ShowDialog();
      //  return;
      //}

      if (cbbProductGroup.SelectedItem is not ProductGroup selectedProductGroup)
      {
        using var popupMsg = new PopupConfirm("Vui lòng chọn nhóm sản phẩm !",
          EnumTypeMsg.MessageManualClose, EnumImageMsg.Information);
        popupMsg.ShowDialog(this);
        cbbProductGroup.Focus();
        return;
      }

      if (cbbProduct.SelectedItem is not Product selectedProduct)
      {
        using var popupMsg = new PopupConfirm("Vui lòng chọn sản phẩm !",
          EnumTypeMsg.MessageManualClose, EnumImageMsg.Information);
        popupMsg.ShowDialog(this);
        cbbProduct.Focus();
        return;
      }

      if (cbbTare.SelectedItem is not CategoryTare selectedTare)
      {
        using var popupMsg = new PopupConfirm("Vui lòng chọn Tare !",
          EnumTypeMsg.MessageManualClose, EnumImageMsg.Information);
        popupMsg.ShowDialog(this);
        cbbTare.Focus();
        return;
      }

      var recordWeight = new RecordWeight
      {
        ProductId = selectedProduct.Id,
        CategoryTareId = selectedTare.Id,
        //RecordTruckId = selectedRecordTruck.Id,
        Net = _msgDataWeight.IndicatedWeight,
        Tare = selectedTare.Value ?? 0.0,
        UserId = AppCore.Ins._userCurrent?.Id,
        StationId = AppCore.Ins._station?.Id,
        LicensePlate = validLicense.Plate,
        CreatedAt = DateTime.UtcNow,
        EnableFlag = true
      };

      btnSaveData.Enabled = false;
      try
      {
        var licensePlateResult = await AppCore.Ins._licensePlateService
          .EnsureExistsAsync(validLicense.Plate);

        if (!licensePlateResult.Exist)
        {
          var licensePlateRequest = new LicensePlateUpsertRequest
          {
            Id = licensePlateResult.LicensePlate?.Id,
            LicensePlateCode = licensePlateResult.LicensePlate?.Plate ?? string.Empty,
            Description = licensePlateResult.LicensePlate?.Description
          };

          var apiJob = new ApiJobs
          {
            Json = JsonHelper.ToJson(licensePlateRequest),
            EnumTypeAPI = EnumTypeAPI.Plate,
            EnumStatusAPI = EnumStatusAPI.Created,
            CreatedAt = DateTime.UtcNow
          };

          await AppCore.Ins._apiJobsService.AddOrUpdateAsync(apiJob);
          await LoadLicensePlateSuggestionsAsync();
        }

        await AppCore.Ins._recordWeightService.AddOrUpdateAsync(recordWeight);
        await LoadSumWeightAsync(validLicense.Plate);
        await LoadHistorical();

        ////In máy in
        //var printDTO = new DTOPrintLabel()
        //{
        //  ProductGroup = selectedProductGroup?.Name ?? string.Empty,
        //  Product = selectedProduct?.Name ?? string.Empty,
        //  TypeTare = selectedTare?.Name ?? string.Empty,
        //  Net = recordWeight?.Net ?? 0.0,
        //  Tare = recordWeight?.Tare ?? 0.0,
        //  Datetime = recordWeight?.CreatedAt?.ToString("dd-MM-yyyy HH:mm:ss"),
        //  Operator = "Admin"
        //};
        //AppCore.Ins.PrinterLabel(AppCore.Ins._appConfig?.NamePrint, printDTO);

        //try
        //{
          
        //}
        //catch (Exception ex)
        //{
        //  HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        //}
        //if (!IsDisposed && !Disposing)
        //{
        //  using var popupMsg = new PopupConfirm("Lưu phiếu cân thành công.",
        //  EnumTypeMsg.MessageAutoClose, EnumImageMsg.Information);
        //  popupMsg.ShowDialog(this);
        //}
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        if (!IsDisposed && !Disposing)
        {
          using var popupMsg = new PopupConfirm("Không thể lưu phiếu cân. Vui lòng thử lại !",
          EnumTypeMsg.MessageManualClose, EnumImageMsg.Information);
          popupMsg.ShowDialog(this);
        }
      }
      finally
      {
        if (!IsDisposed && !Disposing)
          btnSaveData.Enabled = true;
      }
    }

    private async void btnSearchHistorical_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      btnSearchHistorical.Enabled = false;
      try
      {
        await LoadHistorical();
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        if (!IsDisposed && !Disposing)
        {
          using var popupMsg = new PopupConfirm("Không thể tải lịch sử cân. Vui lòng thử lại !",
            EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsg.ShowDialog(this);
        }
      }
      finally
      {
        if (!IsDisposed && !Disposing)
          btnSearchHistorical.Enabled = true;
      }
    }

    private async Task LoadHistorical()
    {
      var fromDateTime = ucTimeSearchFrom.Value;
      var toDateTime = ucTimeSearchTo.Value;
      if (fromDateTime > toDateTime)
      {
        using var popup = new PopupConfirm("Thời gian bắt đầu không được lớn hơn thời gian kết thúc.",
          EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popup.ShowDialog(this);
        return;
      }

      // Records are saved in UTC; the search controls represent local date and time.
      var fromUtc = fromDateTime.ToUniversalTime();
      // Include records occurring anywhere within the selected ending minute.
      var toUtcExclusive = toDateTime.AddMinutes(1).ToUniversalTime();
      var searchKey = txtSearchKey.Texts.Trim();
      var records = await AppCore.Ins._recordWeightService.GetReportAsync(
        fromUtc,
        toUtcExclusive,
        searchKey);

      SetDgvHistorical(DTOHelper.ConvertRecordWeightDTO(records));
    }

    private void SetDgvHistorical(List<RecordWeightDTO> records)
    {
      if (InvokeRequired)
      {
        Invoke(new Action(() => SetDgvHistorical(records)));
        return;
      }

      dgv.DataSource = records;
      dgv.ClearSelection();
      dgv.CurrentCell = null;

      if (dgv.Columns.Contains(nameof(RecordWeightDTO.RecordWeight)))
        dgv.Columns[nameof(RecordWeightDTO.RecordWeight)].Visible = false;

      var autoSizeColumns = new[]
      {
        nameof(RecordWeightDTO.No),
        nameof(RecordWeightDTO.Datetime),
        nameof(RecordWeightDTO.LicensePlate),
        nameof(RecordWeightDTO.ProductGroup),
        nameof(RecordWeightDTO.CategoryTare),
        nameof(RecordWeightDTO.Net),
        nameof(RecordWeightDTO.Tare),
        nameof(RecordWeightDTO.Gross),
      };
      foreach (var columnName in autoSizeColumns)
      {
        if (dgv.Columns.Contains(columnName))
          dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      }

      var weightColumns = new[]
      {
        nameof(RecordWeightDTO.Net),
        nameof(RecordWeightDTO.Tare),
        nameof(RecordWeightDTO.Gross),
      };
      foreach (var columnName in weightColumns)
      {
        if (dgv.Columns.Contains(columnName))
          dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
      }

      if (dgv.Columns.Contains(nameof(RecordWeightDTO.No)))
        dgv.Columns[nameof(RecordWeightDTO.No)].DefaultCellStyle.Alignment =
          DataGridViewContentAlignment.MiddleCenter;
    }

    private void dgv_CellClick(object? sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0 ||
          e.ColumnIndex < 0 ||
          dgv.Columns[e.ColumnIndex].Name != SelectColumnName)
        return;

      var checkBoxCell = (DataGridViewCheckBoxCell)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
      checkBoxCell.Value = !Convert.ToBoolean(checkBoxCell.Value);
    }

    private void btnPrint_Click(object sender, EventArgs e)
    {
      if (dgv.SelectedRows.Count == 0 ||
          dgv.SelectedRows[0].DataBoundItem is not RecordWeightDTO selectedRecord ||
          selectedRecord.RecordWeight is null)
      {
        using var popupMsg = new PopupConfirm("Vui lòng chọn phiếu cân cần in !",
          EnumTypeMsg.MessageManualClose, EnumImageMsg.Information);
        popupMsg.ShowDialog(this);
        return;
      }

      RecordWeight selectedData = selectedRecord.RecordWeight;
    }

    private async void btnExport_Click(object sender, EventArgs e)
    {
      dgv.EndEdit();

      List<RecordWeightDTO> selectedData = dgv.Rows
        .Cast<DataGridViewRow>()
        .Where(row => Convert.ToBoolean(row.Cells[SelectColumnName].Value))
        .Select(row => row.DataBoundItem as RecordWeightDTO)
        .Where(dto => dto?.RecordWeight is not null)
        .Select(dto => dto!)
        .ToList();

      if (selectedData.Count == 0)
      {
        using var popupMsg = new PopupConfirm("Vui lòng chọn dữ liệu cần xuất !",
          EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popupMsg.ShowDialog(this);
        return;
      }

      int licensePlateCount = selectedData
        .Select(dto => LicensePlateRepository.Normalize(dto.LicensePlate))
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .Count();

      if (licensePlateCount > 1)
      {
        using var popupMsg = new PopupConfirm("Các dữ liệu được chọn phải cùng biển số xe !",
          EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popupMsg.ShowDialog(this);
        return;
      }

      List<RecordWeight> exportData = selectedData
        .Select(dto => dto.RecordWeight!)
        .ToList();

      await DownloadFileWorkReport(exportData);
    }


    private async Task DownloadFileWorkReport(List<RecordWeight> exportData)
    {
      try
      {
        DateTime dt = DateTime.Now;
        string pathFileTemplateTable = Application.StartupPath + "Template\\TemplateTableHtml.html";
        string pathFileTemplate = Application.StartupPath + "Template\\TemplateHtml.html";
        string folderOutput = Application.StartupPath + "ReportGoods";
        if (!Directory.Exists(folderOutput))
        {
          Directory.CreateDirectory(folderOutput);
        }

        string licensePlate = exportData.FirstOrDefault()?.LicensePlate ?? string.Empty;
        string template = File.ReadAllText(pathFileTemplate);
        string table = File.ReadAllText(pathFileTemplateTable);
        string result = template.Replace("{{documentNo}}", "A26-00001")
                                .Replace("{documentNo}", "A26-00001")
                                .Replace("{{day}}", dt.Day.ToString())
                                .Replace("{day}", dt.Day.ToString())
                                .Replace("{{month}}", dt.Month.ToString())
                                .Replace("{month}", dt.Month.ToString())
                                .Replace("{{year}}", dt.Year.ToString())
                                .Replace("{year}", dt.Year.ToString())
                                .Replace("{{vehiclePlate}}", licensePlate)
                                .Replace("{{sealNo}}", "")

                                .Replace("{{signPlace}}", "Đồng Nai")
                                .Replace("{{signDay}}", dt.Day.ToString())
                                .Replace("{{signMonth}}", dt.Month.ToString())
                                .Replace("{{signYear}}", dt.Year.ToString())
                                .Replace("{{sender.deptCode}}", "FCM")
                                .Replace("{{receiver.deptCode}}", "SES");


        var recordWeightsByProduct = exportData
          .GroupBy(recordWeight => recordWeight.ProductId)
          .Select(group => new
          {
            ProductGroup = group.First().Product.ProductGroup?.Name,
            ProductName = group.First().Product?.Name ?? string.Empty,
            ProductCode = group.First().Product?.Code ?? string.Empty,
            SumNet = group.Sum(recordWeight => recordWeight.Net)
          })
          .ToList();


        string tableDetails = string.Empty;
        double value = 0.0;
        if (recordWeightsByProduct?.Count() > 0)
        {
          for (int no = 1; no <= recordWeightsByProduct?.Count(); no++)
          {
            string tempTableDetal = table;
            tempTableDetal = tempTableDetal.Replace("{{no}}", (no).ToString("D2"));
            tempTableDetal = tempTableDetal.Replace("{{name}}", recordWeightsByProduct[no - 1].ProductName);
            tempTableDetal = tempTableDetal.Replace("{{code}}", recordWeightsByProduct[no - 1].ProductCode);
            tempTableDetal = tempTableDetal.Replace("{{quantity}}", WeightFormatHelper.Format(recordWeightsByProduct[no - 1].SumNet, 3));
            tempTableDetal = tempTableDetal.Replace("{{note}}", "");


            tableDetails = tableDetails + tempTableDetal;
            value += recordWeightsByProduct[no - 1].SumNet;
          }
        }

        result = result.Replace("{{totalQuantity}}", FormatWeight(value));
        result = result.Replace("{table}", tableDetails);

        string outputPath = Path.Combine(folderOutput, $"{dt.ToString("yyMMddHHmmss")}.html");
        File.WriteAllText(outputPath, result);

        string pdfPath = Path.ChangeExtension(outputPath, ".pdf");
        await PdfHelper.HtmlToPdfWithoutConsoleAsync(outputPath, pdfPath);

        var openReportFile = false;
        using (var popup = new PopupConfirm(
            "Tạo phiếu thành công. Bạn có muốn mở file không?",
            EnumTypeMsg.Confirm,
            EnumImageMsg.Question))
        {
          popup.OnSendConfirm += (_, response) =>
            openReportFile = response.EnumResponsible == EnumResponsible.Confirm;
          popup.ShowDialog(this);
        }

        if (openReportFile)
        {
          try
          {
            Process.Start(new ProcessStartInfo
            {
              FileName = pdfPath,
              UseShellExecute = true,
            });
          }
          catch (Exception openException)
          {
            HelperManager.LogHelper.LogErrorToFileLog(openException, AppCore.Ins._folderFileLog);
            using var openErrorPopup = new PopupConfirm(
              "Đã tạo phiếu nhưng không thể mở file.",
              EnumTypeMsg.MessageManualClose,
              EnumImageMsg.Warning);
            openErrorPopup.ShowDialog(this);
          }
        }
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        using var popupMsg = new PopupConfirm(
          "Không thể tạo phiếu. Vui lòng thử lại !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupMsg.ShowDialog(this);
      }
    }

    private static string FormatWeight(double value)
    {
      return WeightFormatHelper.Format(value);
    }

  }
}
