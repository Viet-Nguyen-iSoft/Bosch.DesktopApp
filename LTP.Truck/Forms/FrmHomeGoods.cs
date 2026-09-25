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
using System.Globalization;
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
    private int _productRefreshVersion;
    private int _tareRefreshVersion;
    private int _deliveryRefreshVersion;
    private int _sumWeightLoadVersion;
    private bool _waitingForWeightReset;
    private DataWeightInterface _msgDataWeight { get; set; } = new DataWeightInterface();
    private RecordTruckDTO _recordTruckDTO { get; set; }
    private CategoryTare? _categoryTare { get; set; }
    public FrmHomeGoods()
    {
      InitializeComponent();
      CustomUI();

      cbbTare.SelectionChangeCommitted += cbbTare_SelectedValueChanged;
      btnSearchHistorical.Click += btnSearchHistorical_Click;
      txtLicensePlate._TextChanged += TxtLicensePlate__TextChanged;
      lbTare.Text = "---";
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
        cbbDelivery.SelectedIndex = -1;

        //Đăng kí sự kiện
        cbbProductGroup.SelectedValueChanged += cbbProductGroup_SelectedValueChanged;

        FrmMain.Instance.OnChangeProductGroup += Instance_OnChangeProductGroup;
        FrmMain.Instance.OnChangeProduct += Instance_OnChangeProduct;
        FrmMain.Instance.OnChangeTare += Instance_OnChangeTare;
        MasterDataChangeNotifier.Changed += MasterDataChangeNotifier_Changed;
        AppCore.Ins.OnSendDataWeight += Ins_OnSendDataWeight;
        AppCore.Ins.OnSendStatusWeight += Ins_OnSendStatusWeight;
        ResetWeightDisplay();
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private void MasterDataChangeNotifier_Changed(object? sender, Type entityType)
    {
      if (entityType == typeof(ProductGroup))
        Instance_OnChangeProductGroup(sender, EventArgs.Empty);
      else if (entityType == typeof(Product))
        Instance_OnChangeProduct(sender, EventArgs.Empty);
      else if (entityType == typeof(CategoryTare))
        Instance_OnChangeTare(sender, EventArgs.Empty);
      else if (entityType == typeof(Delivery))
        Instance_OnChangeDelivery(sender, EventArgs.Empty);
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

    private void Ins_OnSendDataWeight(object? sender, DataWeightInterface e)
    {
      _msgDataWeight = e;
      SetDataWeight(e);
    }

    private void Ins_OnSendStatusWeight(object? sender, CommunicationStatusChangedEventArgs e)
    {
      if (!e.IsConnected)
        ResetWeightDisplay();

      ShowStatusWeight(e.IsConnected);
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

    private void ShowStatusWeight(bool isConneted)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          ShowStatusWeight(isConneted);
        }));
        return;
      }

      if (isConneted)
      {
        lbStatusWeight.Text = "Kết nối";
        lbStatusWeight.ForeColor = Color.DarkGreen;
      } 
      else
      {
        lbStatusWeight.Text =  "Mất kết nối";
        lbStatusWeight.ForeColor = Color.Red;
      }  
      
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
      lbWeightValue.Text = WeightFormatHelper.Format(messageData.IndicatedWeight, 2);

      var resetThreshold = AppCore.Ins._appConfig?.ValueWeightGoodsCheckPermitConfirm;
      if (_waitingForWeightReset &&
          resetThreshold.HasValue && resetThreshold.Value > 0 &&
          TryGetDisplayedWeight(out var displayedWeight) &&
          displayedWeight <= resetThreshold.Value)
      {
        _waitingForWeightReset = false;
        btnSaveData.Enabled = true;
      }

      //Tare
      lbTareSrc.Text = WeightFormatHelper.Format(messageData.TareWeight, 2);

      //Tare
      if (_categoryTare != null)
      {
        lbGross.Text = WeightFormatHelper.Format(messageData.IndicatedWeight + (messageData?.TareWeight ?? 0.0), 2);
      }
      else
      {
        lbGross.Text = WeightFormatHelper.Format(messageData.IndicatedWeight, 2);
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

    private async void Instance_OnChangeDelivery(object? sender, EventArgs e)
    {
      if (InvokeRequired)
      {
        BeginInvoke(new Action(() => Instance_OnChangeDelivery(sender, e)));
        return;
      }

      var refreshVersion = ++_deliveryRefreshVersion;

      try
      {
        var deliveries = await AppCore.Ins._deliveryService.GetAllAsync();
        if (IsDisposed || Disposing || refreshVersion != _deliveryRefreshVersion)
          return;

        var selectedDeliveryId = (cbbDelivery.SelectedItem as Delivery)?.Id;
        var selectedDeliveryIndex = deliveries.FindIndex(delivery => delivery.Id == selectedDeliveryId);

        SetDelivery(deliveries);
        cbbDelivery.SelectedIndex = selectedDeliveryIndex;
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

      var refreshVersion = ++_productRefreshVersion;
      var selectedProductId = (cbbProduct.SelectedItem as Product)?.Id;

      try
      {
        var products = await AppCore.Ins._productService.GetAllAsync();
        if (IsDisposed || Disposing || refreshVersion != _productRefreshVersion)
          return;

        _products = products;
        FillProduct(selectedProductId, preserveSelection: true);
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
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
      var deliveries = await AppCore.Ins._deliveryService.GetAllAsync();

      SetProductGroup(productGroups);
      SetTare(categoryTares);
      SetDelivery(deliveries);
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

    private void SetDelivery(List<Delivery> deliveries)
    {
      if (InvokeRequired)
      {
        Invoke(new Action(() => SetDelivery(deliveries)));
        return;
      }

      cbbDelivery.DisplayMember = nameof(Delivery.Name);
      cbbDelivery.ValueMember = nameof(Delivery.Id);
      cbbDelivery.DataSource = deliveries;
    }

    private void cbbTare_SelectedValueChanged(object? sender, EventArgs e)
    {
      _categoryTare = cbbTare.SelectedItem as CategoryTare;
      if (_categoryTare?.Value is double tareValue)
      {
        lbTare.Text = WeightFormatHelper.Format(tareValue, 2);
        ExecuteScaleCommand(
          sender ?? cbbTare,
          () => AppCore.Ins.SetTareWeight(tareValue),
          $"Đã gửi giá trị tare {WeightFormatHelper.Format(tareValue, 2)} kg xuống cân thành công.",
          "Không thể gửi giá trị tare xuống cân. Vui lòng thử lại !");
      }
      else
      {
        lbTare.Text = WeightFormatHelper.Format(0.0, 2);
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
              lbSumWeight.Text = WeightFormatHelper.Format(totalWeight, 2);
          }));
          return;
        }

        lbSumWeight.Text = WeightFormatHelper.Format(totalWeight, 2);
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
      var resetThreshold = AppCore.Ins._appConfig?.ValueWeightGoodsCheckPermitConfirm;
      if (!resetThreshold.HasValue || resetThreshold.Value <= 0)
      {
        using var popupMsg = new PopupConfirm(
          "Vui lòng cấu hình khối lượng xác nhận cân tiếp tục lớn hơn 0 !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupMsg.ShowDialog(this);
        return;
      }

      if (_waitingForWeightReset)
      {
        using var popupMsg = new PopupConfirm(
          $"Vui lòng chờ khối lượng trên cân giảm xuống nhỏ hơn hoặc bằng " +
          $"{WeightFormatHelper.Format(resetThreshold.Value, 2)} kg trước khi cân tiếp tục !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupMsg.ShowDialog(this);
        return;
      }

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

      if (cbbDelivery.SelectedItem is not Delivery selectedDelivery)
      {
        using var popupMsg = new PopupConfirm("Vui lòng chọn bên giao hàng !",
          EnumTypeMsg.MessageManualClose, EnumImageMsg.Information);
        popupMsg.ShowDialog(this);
        cbbDelivery.Focus();
        return;
      }

      var recordWeight = new RecordWeight
      {
        ProductId = selectedProduct.Id,
        CategoryTareId = selectedTare.Id,
        DeliveryId = selectedDelivery.Id,
        //RecordTruckId = selectedRecordTruck.Id,
        Net = _msgDataWeight.IndicatedWeight,
        Tare = selectedTare.Value ?? 0.0,
        UserId = AppCore.Ins._userCurrent?.Id,
        StationId = AppCore.Ins._station?.Id,
        LicensePlate = validLicense.Plate,
        IdCard = txtIdCard.Texts,
        NameDriver = txtNameDriver.Texts,
        Note = txtNote.Text,
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
        _waitingForWeightReset = true;
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
          btnSaveData.Enabled = !_waitingForWeightReset;
      }
    }

    private bool TryGetDisplayedWeight(out double weight)
    {
      return double.TryParse(
        lbWeightValue.Text,
        NumberStyles.Float | NumberStyles.AllowThousands,
        CultureInfo.InvariantCulture,
        out weight);
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
        nameof(RecordWeightDTO.NameDriver),
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
        using var popupMsg = new PopupConfirm("Vui lòng chọn dữ liệu cần in phiếu !",
          EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popupMsg.ShowDialog(this);
        return;
      }

      string printerName = AppCore.Ins._appConfig.NamePrintA4;
      foreach (var row in selectedData)
      {
        AppCore.Ins.PrinterLabelGoods(printerName, row);
      }
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

      int productGroupNameCount = selectedData
        .Select(dto => (dto.ProductGroup ?? string.Empty).Trim())
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .Count();

      if (productGroupNameCount > 1)
      {
        using var popupMsg = new PopupConfirm("Các dữ liệu được chọn phải cùng tên nhóm phế phẩm !",
          EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popupMsg.ShowDialog(this);
        return;
      }

      List<RecordWeight> exportData = selectedData
        .Select(dto => dto.RecordWeight!)
        .ToList();

      string licensePlate = selectedData[0].LicensePlate?.Trim() ?? string.Empty;
      string productGroupName = selectedData[0].ProductGroup?.Trim() ?? string.Empty;

      await DownloadFileWorkReport(exportData, licensePlate);
    }


    #region Export PDF Goods
    private async Task DownloadFileWorkReport(
      List<RecordWeight> exportData,
      string licensePlate)
    {
      try
      {
        DateTime dt = DateTime.Now;

        var pdfPath = await AppCore.Ins.ExportPdfGoods(dt, licensePlate, exportData);

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

    #endregion

    private void btnZero_Click(object sender, EventArgs e)
    {
      ExecuteScaleCommand(
        sender,
        AppCore.Ins.ZeroWeight,
        "Zero cân thành công.",
        "Không thể gửi lệnh Zero xuống cân. Vui lòng thử lại !");
    }

    private void btnTare_Click(object sender, EventArgs e)
    {
      ExecuteScaleCommand(
        sender,
        AppCore.Ins.TareWeight,
        "Tare cân thành công.",
        "Không thể gửi lệnh Tare xuống cân. Vui lòng thử lại !");
    }

    private void ExecuteScaleCommand(
      object sender,
      Action command,
      string successMessage,
      string errorMessage)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      try
      {
        command();
        using var popup = new PopupConfirm(
          successMessage,
          EnumTypeMsg.MessageAutoClose,
          EnumImageMsg.Information);
        popup.ShowDialog(this);
      }
      catch (InvalidOperationException ex)
      {
        using var popup = new PopupConfirm(
          ex.Message,
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popup.ShowDialog(this);
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        using var popup = new PopupConfirm(
          errorMessage,
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popup.ShowDialog(this);
      }
    }
  }
}
