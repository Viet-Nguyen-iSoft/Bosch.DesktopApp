using ApiSyncData;
using ApiSyncData.Req;
using Common;
using HelperManager;
using iSoft.Communication.Interface;
using iSoft.Communication.Mode;
using iSoft.Database;
using iSoft.Database.DTO;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using LTP.Truck.Custom;
using LTP.Truck.Popup;
using System.Data;
using System.Drawing.Drawing2D;
using System.Globalization;
using static Common.EnumData;
using static HelperManager.EnumData;
using static iSoft.Database.EnumData;

namespace LTP.Truck.Forms
{
  public partial class FrmHomeTruck : Form
  {
    private readonly ToolTip _deleteReasonToolTip = new()
    {
      OwnerDraw = true,
      ShowAlways = true
    };
    private readonly Font _deleteReasonToolTipFont = new("Segoe UI", 16F);
    private string _deleteReasonToolTipText = string.Empty;
    private int _statusFilterIndex = 1;
    private int _typeFilterIndex;

    public FrmHomeTruck()
    {
      InitializeComponent();
      CustomUI();
      this.Load += FrmHome_Load;
      this.Shown += FrmHomeTruck_Shown;
      this.Disposed += (_, _) =>
      {
        _deleteReasonToolTip.Dispose();
        _deleteReasonToolTipFont.Dispose();
      };
    }

    #region Instance
    private static FrmHomeTruck _Instance = null;
    public static FrmHomeTruck Instance
    {
      get
      {
        if (_Instance == null) _Instance = new FrmHomeTruck();
        return _Instance;
      }
    }
    #endregion

    private void CustomUI()
    {
      ucTimeSearchFrom.Value = DateTime.Today;
      ucTimeSearchTo.Value = DateTime.Today.AddDays(1).AddMinutes(-1);

      ucItemWeight01.Title = "KL cân lần 1";
      ucItemWeight02.Title = "KL cân lần 2";
      ucItemWeightGoods.Title = "KL hàng";
      ucItemWeightGoods.Visible = false;
      ucItemOffsetWeight.Title = "KL chênh lệch xe";

      ElipseControl elipseControl = new ElipseControl();
      elipseControl.TargetControl = tableLayoutPanel3;
      elipseControl.CornerRadius = 20;

      ElipseControl elipseControl01 = new ElipseControl();
      elipseControl01.TargetControl = tableLayoutPanel4;
      elipseControl01.CornerRadius = 20;

      ElipseControl elipseControl02 = new ElipseControl();
      elipseControl02.TargetControl = tableLayoutPanel7;
      elipseControl02.CornerRadius = 20;

      ElipseControl elipseControl03 = new ElipseControl();
      elipseControl03.TargetControl = tableLayoutPanel9;
      elipseControl03.CornerRadius = 20;

      dgv.EnableHeadersVisualStyles = false;
      dgv.ColumnHeadersHeight = 50;
      dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
      dgv.RowTemplate.Height = 60;
      dgv.BorderStyle = BorderStyle.None;
      dgv.MultiSelect = false;
      dgv.ShowCellToolTips = false;
      dgv.DefaultCellStyle.SelectionBackColor = dgv.DefaultCellStyle.BackColor;
      dgv.DefaultCellStyle.SelectionForeColor = dgv.DefaultCellStyle.ForeColor;
      dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = dgv.ColumnHeadersDefaultCellStyle.BackColor;
      dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = dgv.ColumnHeadersDefaultCellStyle.ForeColor;
      dgv.RowHeadersDefaultCellStyle.SelectionBackColor = dgv.RowHeadersDefaultCellStyle.BackColor;
      dgv.RowHeadersDefaultCellStyle.SelectionForeColor = dgv.RowHeadersDefaultCellStyle.ForeColor;
      dgv.CellPainting += dgv_CellPainting;
      dgv.CellContentClick += dgv_CellContentClick;
      dgv.CellMouseEnter += dgv_CellMouseEnter;
      dgv.CellMouseLeave += (_, _) => _deleteReasonToolTip.Hide(dgv);
      _deleteReasonToolTip.Popup += DeleteReasonToolTip_Popup;
      _deleteReasonToolTip.Draw += DeleteReasonToolTip_Draw;
    }

    private void FrmHome_Load(object? sender, EventArgs e)
    {
      btnFilter.Click += BtnFilter_Click;
      AppCore.Ins.OnSendDataWeightTruck += Ins_OnSendDataWeightTruck;
      AppCore.Ins.OnSendStatusWeightTruck += Ins_OnSendStatusWeightTruck;
      ResetWeightDisplay();
      CheckShowStatusButton(_recordTruck);
    }

    private void Ins_OnSendStatusWeightTruck(object? sender, CommunicationStatusChangedEventArgs e)
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
    }

    private void BtnFilter_Click(object? sender, EventArgs e)
    {
      using var popupFilter = new PopupFilter(_statusFilterIndex, _typeFilterIndex);
      popupFilter.OnSendData += PopupFilter_OnSendData;
      popupFilter.ShowDialog();
    }

    private async void PopupFilter_OnSendData(int arg1, int arg2)
    {
      _statusFilterIndex = arg1;
      _typeFilterIndex = arg2;
      await LoadHistorical();
    }

    private async void FrmHomeTruck_Shown(object? sender, EventArgs e)
    {
      await LoadHistorical();
      await LoadLicensePlateSuggestionsAsync();
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

    private void Ins_OnSendDataWeightTruck(object? sender, DataWeightInterface e)
    {
      _msgDataWeight = e;
      SetDataWeight(e);
    }

    private async void btnLoadClient_Click(object sender, EventArgs e)
    {
      var clients = await AppCore.Ins._clientService.GetAllAsync(IsContainDelete: false);
      PopupLoadMD popupLoadMD = new PopupLoadMD();
      popupLoadMD.SetData(clients);
      popupLoadMD.OnSendData += PopupLoadMD_OnSendData;
      popupLoadMD.ShowDialog();
    }

    private async void btnLoadTypeGoods_Click(object sender, EventArgs e)
    {
      var typeGoods = await AppCore.Ins._typeGoodsService.GetAllAsync(IsContainDelete: false);
      PopupLoadMD popupLoadMD = new PopupLoadMD();
      popupLoadMD.SetData(typeGoods);
      popupLoadMD.OnSendData += PopupLoadMD_OnSendData;
      popupLoadMD.ShowDialog();
    }

    private async void btnLoadWarehouse_Click(object sender, EventArgs e)
    {
      var warehouses = await AppCore.Ins._warehouseService.GetAllAsync(IsContainDelete: false);
      PopupLoadMD popupLoadMD = new PopupLoadMD();
      popupLoadMD.SetData(warehouses);
      popupLoadMD.OnSendData += PopupLoadMD_OnSendData;
      popupLoadMD.ShowDialog();
    }

    private void PopupLoadMD_OnSendData(object arg1, Common.EnumData.EnumTypeData arg2)
    {
      switch (arg2)
      {
        case Common.EnumData.EnumTypeData.Client:
          var rsClient = arg1 as ClientDTO;
          SetData(txtClient, rsClient?.Name ?? string.Empty);

          if (rsClient != null)
          {
            _recordTruck.ClientId = rsClient?.Client?.Id;
          }
          break;
        case Common.EnumData.EnumTypeData.TypeGoods:
          var rsTypeGoods = arg1 as TypeGoodsDTO;
          SetData(txtTypeGoods, rsTypeGoods?.Name ?? string.Empty);

          if (rsTypeGoods != null)
          {
            _recordTruck.TypeGoodsId = rsTypeGoods?.TypeGoods?.Id;
          }
          break;
        case Common.EnumData.EnumTypeData.Warehouse:
          var rsWarehouse = arg1 as WareHouseDTO;
          SetData(txtWareHouse, rsWarehouse?.Name ?? string.Empty);

          if (rsWarehouse != null)
          {
            _recordTruck.WarehouseId = rsWarehouse?.Warehouse?.Id;
          }
          break;
        default:
          break;
      }
    }

    private void SetData(RJTextBox rJTextBox, string data)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetData(rJTextBox, data);
        }));
        return;
      }

      rJTextBox.Texts = data;
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

      lbWeightValue.Text = messageData.IndicatedWeight.ToString("F0");
    }

    private RecordTruck _recordTruck { get; set; } = new RecordTruck();
    private DataWeightInterface _msgDataWeight { get; set; } = new DataWeightInterface();
    private int _weightGoodsLoadVersion;
    private void btnTriggerWeight_Click(object sender, EventArgs e)
    {
      try
      {
        var rs = LicensePlateHelper.IsValidVietnamLicensePlate(txtLicensePlate.Texts);
        if (!rs.IsValid)
        {
          using var popupMsg = new PopupConfirm("Vui lòng nhập biển số xe. \r\nHoặc biển số xe không hợp lệ !",
            EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsg.ShowDialog(this);
          return;
        }

        if (_recordTruck.TypeGoodsId == null)
        {
          using var popupMsg = new PopupConfirm("Vui lòng chọn Loại hàng trước khi cân !",
            EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupMsg.ShowDialog(this);
          return;
        }

        if (_msgDataWeight.IndicatedWeight <= 0)
        {
          PopupConfirm popupConfirm = new PopupConfirm("Giá trị cân ≤ 0 Kg !", EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupConfirm.ShowDialog();
          return;
        }

        _recordTruck.NetTimeTemp = _msgDataWeight.IndicatedWeight;
        if (_recordTruck.EnumTypeDataTruck == EnumTypeDataTruck.None)
        {
          _recordTruck.EnumTypeDataTruck = EnumTypeDataTruck.WeightedTime01;
        }
        else if (_recordTruck.EnumTypeDataTruck == EnumTypeDataTruck.DoneTime01)
        {
          _recordTruck.EnumTypeDataTruck = EnumTypeDataTruck.WeightedTime02;
        }

        CheckShowStatusButton(_recordTruck);
      }
      catch (Exception ex)
      {

      }
    }

    private async void btnWeightTime01_Click(object sender, EventArgs e)
    {
      if (_recordTruck.NetTimeTemp <= 0)
      {
        PopupConfirm popupConfirm = new PopupConfirm("Giá trị cân ≤ 0 Kg !", EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popupConfirm.ShowDialog();
        return;
      }

      var validLicense = LicensePlateHelper.IsValidVietnamLicensePlate(txtLicensePlate.Texts);
      if (!validLicense.IsValid)
      {
        PopupConfirm popupConfirm = new PopupConfirm("Biển số xe không hợp lệ !", EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popupConfirm.ShowDialog();
        return;
      }

      _recordTruck.NetTime01 = _recordTruck.NetTimeTemp;
      _recordTruck.NetTimeTemp = 0.0;
      _recordTruck.EnumTypeDataTruck = EnumTypeDataTruck.DoneTime01;
      CheckShowStatusButton(_recordTruck);

      //Save DB
      _recordTruck.NoLabelAuto = KeyHelper.CreateLabel(AppCore.Ins._appConfig?.Key);
      _recordTruck.NoLabelManual = txtNoLabel.Texts;
      _recordTruck.NameDriver = txtNameDriver.Texts;
      _recordTruck.LicensePlate = validLicense.Plate;
      _recordTruck.IdCard = txtIdCard.Texts;
      _recordTruck.Document = txtDocument.Text;
      _recordTruck.StationId = AppCore.Ins._station?.Id;
      _recordTruck.UserId = AppCore.Ins._userCurrent?.Id;
      _recordTruck.CreatedAt = DateTime.UtcNow;
      _recordTruck.UpdatedAt = DateTime.UtcNow;
      _recordTruck.WeighInAt = DateTime.UtcNow;

      var rs = await AppCore.Ins._recordTruckService.AddOrUpdateAsync(_recordTruck);
      //Push API biển số xe
      if (rs.Exist == false)
      {
        LicensePlateUpsertRequest licensePlate = new LicensePlateUpsertRequest();
        licensePlate.LicensePlateCode = rs.LicensePlate?.Plate ?? string.Empty;
        licensePlate.Id = rs.LicensePlate?.Id;
        licensePlate.Description = rs.LicensePlate?.Description;

        ApiJobs apiJobs = new ApiJobs();
        apiJobs.Json = JsonHelper.ToJson(licensePlate);
        apiJobs.EnumTypeAPI = EnumTypeAPI.Plate;
        apiJobs.EnumStatusAPI = EnumStatusAPI.Created;
        apiJobs.CreatedAt = DateTime.UtcNow;
        await AppCore.Ins._apiJobsService.AddOrUpdateAsync(apiJobs);
      }

      await LoadLicensePlateSuggestionsAsync();
      await LoadHistorical();

      ////POST PDF
      RecordTruck? record = await _recordTruckService.GetDetailByIdAsync(_recordTruck.Id);
      if (record != null)
      {
        var pathPdf = await DownloadReportTruck(DateTime.Now, record);
        //await (new ApiService()).UploadReportTruckPdf(record.Id, pathPdf);
      }
    }

    private async void btnWeightTime02_Click(object sender, EventArgs e)
    {
      try
      {
        if (_recordTruck.NetTimeTemp <= 0)
        {
          PopupConfirm popupConfirm = new PopupConfirm("Giá trị cân ≤ 0 Kg !", EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupConfirm.ShowDialog();
          return;
        }

        var validLicense = LicensePlateHelper.IsValidVietnamLicensePlate(txtLicensePlate.Texts);
        if (!validLicense.IsValid)
        {
          PopupConfirm popupConfirm = new PopupConfirm("Biển số xe không hợp lệ !", EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
          popupConfirm.ShowDialog();
          return;
        }

        _recordTruck.NetTime02 = _recordTruck.NetTimeTemp;
        _recordTruck.NetTimeTemp = 0.0;
        _recordTruck.EnumTypeDataTruck = EnumTypeDataTruck.DoneTime02;
        CheckShowStatusButton(_recordTruck);

        //Save DB
        _recordTruck.NoLabelAuto = DateTime.Now.ToString("yyyyMMddHHmmss");
        _recordTruck.NoLabelManual = txtNoLabel.Texts;
        _recordTruck.NameDriver = txtNameDriver.Texts;
        _recordTruck.LicensePlate = validLicense.Plate;
        _recordTruck.IdCard = txtIdCard.Texts;
        _recordTruck.Document = txtDocument.Text;
        _recordTruck.StationId = AppCore.Ins._station?.Id;
        _recordTruck.UserId = AppCore.Ins._userCurrent?.Id;
        _recordTruck.UpdatedAt = DateTime.UtcNow;
        _recordTruck.WeighOutAt = DateTime.UtcNow;

        await AppCore.Ins._recordTruckService.AddOrUpdateAsync(_recordTruck);
        await LoadHistorical();

        //POST PDF
        RecordTruck? record = await _recordTruckService.GetDetailByIdAsync(_recordTruck.Id);
        if (record != null)
        {
          var pathPdf = await DownloadReportTruck(DateTime.Now, record);
          //await (new ApiService()).UploadReportTruckPdf(record.Id, pathPdf);
        }
      }
      catch (Exception)
      {

      }
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      if ((_recordTruck.EnumTypeDataTruck == EnumTypeDataTruck.DoneTime01) ||
          (_recordTruck.EnumTypeDataTruck == EnumTypeDataTruck.WeightedTime02))
      {
        _recordTruck.EnumTypeDataTruck = EnumTypeDataTruck.WeightedTime01;
        _recordTruck.NetTime01 = 0.0;
        CheckShowStatusButton(_recordTruck);
      }
      else if (_recordTruck.EnumTypeDataTruck == EnumTypeDataTruck.DoneTime02)
      {
        _recordTruck.NetTime02 = 0.0;
        _recordTruck.EnumTypeDataTruck = EnumTypeDataTruck.WeightedTime02;
        CheckShowStatusButton(_recordTruck);
      }
    }

    private void CheckShowStatusButton(RecordTruck recordTruck)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          CheckShowStatusButton(recordTruck);
        }));
        return;
      }

      switch (recordTruck.EnumTypeDataTruck)
      {
        case iSoft.Database.EnumData.EnumTypeDataTruck.None:
          btnWeightTime01.Enabled = true;
          btnWeightTime02.Enabled = false;
          btnPrint.Enabled = false;

          ucItemWeight01.Value = "...";
          ucItemWeight02.Value = "...";
          break;
        case iSoft.Database.EnumData.EnumTypeDataTruck.WeightedTime01:
          btnWeightTime01.Enabled = true;
          btnWeightTime02.Enabled = false;
          btnPrint.Enabled = false;

          ucItemWeight01.Value = "...";
          ucItemWeight02.Value = "...";
          break;
        case iSoft.Database.EnumData.EnumTypeDataTruck.DoneTime01:
          btnWeightTime01.Enabled = false;
          btnWeightTime02.Enabled = true;
          btnPrint.Enabled = true;

          ucItemWeight01.Value = recordTruck.NetTime01.ToString("F3");
          ucItemWeight02.Value = "...";
          break;
        case iSoft.Database.EnumData.EnumTypeDataTruck.WeightedTime02:
          btnWeightTime01.Enabled = false;
          btnWeightTime02.Enabled = true;
          btnPrint.Enabled = true;

          ucItemWeight01.Value = recordTruck.NetTime01.ToString("F3");
          ucItemWeight02.Value = "...";
          break;
        case iSoft.Database.EnumData.EnumTypeDataTruck.DoneTime02:
          btnWeightTime01.Enabled = false;
          btnWeightTime02.Enabled = false;
          btnPrint.Enabled = true;

          ucItemWeight01.Value = recordTruck.NetTime01.ToString("F3");
          ucItemWeight02.Value = recordTruck.NetTime02.ToString("F3");
          break;
        default:
          break;
      }

      UpdateOffsetWeight(recordTruck);
      _ = LoadWeightGoodsAsync(recordTruck.Id);
      lbWeightTrigger.Text = recordTruck.NetTimeTemp.ToString("F3");
      ApplyRecordAccess(recordTruck);
    }

    private void ShowDataHistorical(RecordTruck recordTruck)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          ShowDataHistorical(recordTruck);
        }));
        return;
      }

      switch (recordTruck.EnumTypeDataTruck)
      {
        case iSoft.Database.EnumData.EnumTypeDataTruck.None:
          btnWeightTime01.Enabled = true;
          btnWeightTime02.Enabled = false;
          btnPrint.Enabled = false;

          ucItemWeight01.Value = "...";
          ucItemWeight02.Value = "...";
          break;
        case iSoft.Database.EnumData.EnumTypeDataTruck.WeightedTime01:
          btnWeightTime01.Enabled = true;
          btnWeightTime02.Enabled = false;
          btnPrint.Enabled = false;

          ucItemWeight01.Value = "...";
          ucItemWeight02.Value = "...";
          break;
        case iSoft.Database.EnumData.EnumTypeDataTruck.DoneTime01:
          btnWeightTime01.Enabled = false;
          btnWeightTime02.Enabled = true;
          btnPrint.Enabled = true;

          ucItemWeight01.Value = recordTruck.NetTime01.ToString("F3");
          ucItemWeight02.Value = "...";
          break;
        case iSoft.Database.EnumData.EnumTypeDataTruck.WeightedTime02:
          btnWeightTime01.Enabled = false;
          btnWeightTime02.Enabled = true;
          btnPrint.Enabled = true;

          ucItemWeight01.Value = recordTruck.NetTime01.ToString("F3");
          ucItemWeight02.Value = "...";
          break;
        case iSoft.Database.EnumData.EnumTypeDataTruck.DoneTime02:
          btnWeightTime01.Enabled = false;
          btnWeightTime02.Enabled = false;
          btnPrint.Enabled = true;

          ucItemWeight01.Value = recordTruck.NetTime01.ToString("F3");
          ucItemWeight02.Value = recordTruck.NetTime02.ToString("F3");
          break;
        default:
          break;
      }

      double valueGoods = (recordTruck.NetTime02 - recordTruck.NetTime01);
      UpdateOffsetWeight(recordTruck);
      _ = LoadWeightGoodsAsync(recordTruck.Id);
      lbWeightTrigger.Text = recordTruck.NetTimeTemp.ToString("F3");

      if (valueGoods > 0 && recordTruck.NetTime01 > 0 && recordTruck.NetTime02 > 0)
      {
        txtTypeWeight.Texts = "Xuất hàng";
      }
      else if (valueGoods < 0 && recordTruck.NetTime01 > 0 && recordTruck.NetTime02 > 0)
      {
        txtTypeWeight.Texts = "Nhập hàng";
      }
      else
      {
        txtTypeWeight.Texts = "Chưa xác định";
      }


      //Show thông tin
      txtNoLabelAuto.Texts = recordTruck.NoLabelAuto ?? string.Empty;
      txtNoLabel.Texts = recordTruck.NoLabelManual ?? string.Empty;

      txtNameDriver.Texts = recordTruck.NameDriver ?? string.Empty;
      txtLicensePlate.Texts = recordTruck.LicensePlate ?? string.Empty;
      txtIdCard.Texts = recordTruck.IdCard ?? string.Empty;
      txtDocument.Text = recordTruck.Document ?? string.Empty;

      txtClient.Texts = recordTruck.Client?.Name ?? string.Empty;
      txtWareHouse.Texts = recordTruck.Warehouse?.Name ?? string.Empty;
      txtTypeGoods.Texts = recordTruck.TypeGoods?.Name ?? string.Empty;

      ApplyRecordAccess(recordTruck);
    }

    private bool CanModifyRecord(RecordTruck? recordTruck)
    {
      if (recordTruck == null)
        return false;

      // Phiếu mới chưa có station sẽ được gán station hiện tại khi lưu.
      if (recordTruck.Id == Guid.Empty)
        return true;

      return AppCore.Ins._station != null &&
        recordTruck.StationId == AppCore.Ins._station.Id;
    }

    private void ApplyRecordAccess(RecordTruck recordTruck)
    {
      bool canModify = CanModifyRecord(recordTruck);

      // Các nút phụ thuộc trạng thái chỉ bị khóa thêm; không bật lại
      // nếu trạng thái cân hiện tại không cho phép thao tác.
      btnWeightTime01.Enabled &= canModify;
      btnWeightTime02.Enabled &= canModify;
      btnPrint.Enabled &= canModify;

      btnTriggerWeight.Enabled = canModify;
      btnBack.Enabled = canModify;
      btnZero.Enabled = canModify;
      btnLoadClient.Enabled = canModify;
      btnLoadTypeGoods.Enabled = canModify;
      btnLoadWarehouse.Enabled = canModify;

      txtNoLabel.Enabled = canModify;
      txtNameDriver.Enabled = canModify;
      txtLicensePlate.Enabled = canModify;
      txtIdCard.Enabled = canModify;
      txtClient.Enabled = canModify;
      txtTypeGoods.Enabled = canModify;
      txtWareHouse.Enabled = canModify;
      txtDocument.ReadOnly = !canModify;
    }

    private void UpdateOffsetWeight(RecordTruck recordTruck)
    {
      if (recordTruck.NetTime01 <= 0 || recordTruck.NetTime02 <= 0)
      {
        ucItemOffsetWeight.Value = "...";
        return;
      }

      var offsetWeight = Math.Abs(recordTruck.NetTime02 - recordTruck.NetTime01);
      ucItemOffsetWeight.Value = offsetWeight.ToString("F3");
    }

    private async Task LoadWeightGoodsAsync(Guid recordTruckId)
    {
      var loadVersion = ++_weightGoodsLoadVersion;

      try
      {
        var totalNet = recordTruckId != Guid.Empty
          ? await AppCore.Ins._recordWeightService.SumNetByRecordTruckIdAsync(recordTruckId)
          : 0.0;
        var hasWeightGoods = Math.Abs(totalNet) >= 0.0005;

        if (IsDisposed || Disposing || loadVersion != _weightGoodsLoadVersion)
          return;

        if (InvokeRequired)
        {
          BeginInvoke(new Action(() =>
          {
            if (loadVersion == _weightGoodsLoadVersion)
            {
              ucItemWeightGoods.Value = totalNet.ToString("F3");
              ucItemWeightGoods.Visible = hasWeightGoods;
            }
          }));
          return;
        }

        ucItemWeightGoods.Value = totalNet.ToString("F3");
        ucItemWeightGoods.Visible = hasWeightGoods;
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private async void btnSearchHistorical_Click(object sender, EventArgs e)
    {
      await LoadHistorical();
    }

    private async Task LoadHistorical()
    {
      var fromDateTime = ucTimeSearchFrom.Value;
      var toDateTime = ucTimeSearchTo.Value;
      if (fromDateTime > toDateTime)
      {
        using var popup = new PopupConfirm("Thời gian bắt đầu không được lớn hơn thời gian kết thúc.",
          EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popup.ShowDialog();
        return;
      }

      // Records are saved in UTC; the search controls represent local date and time.
      var fromUtc = fromDateTime.ToUniversalTime();
      // Include records occurring anywhere within the selected ending minute.
      var toUtcExclusive = toDateTime.AddMinutes(1).ToUniversalTime();
      var statusIndex = _statusFilterIndex;
      var typeIndex = _typeFilterIndex;
      var searchKey = txtSearchKey.Texts.Trim();

      // Hiển thị cả bản ghi đã xóa để người dùng có thể phục hồi.
      var rs = await AppCore.Ins._recordTruckService.GetAllAsync(true);
      var filtered = rs.Where(record =>
      {
        // Normalize both sides to UTC before comparing their clock values.
        var updatedAtUtc = record.UpdatedAt?.ToUniversalTime();
        return updatedAtUtc >= fromUtc && updatedAtUtc < toUtcExclusive;
      });

      filtered = typeIndex switch
      {
        1 => filtered.Where(record => !record.DeletedFlag),
        2 => filtered.Where(record => record.DeletedFlag),
        _ => filtered
      };

      filtered = statusIndex switch
      {
        1 => filtered.Where(record =>
          record.EnumTypeDataTruck == EnumTypeDataTruck.WeightedTime01 ||
          record.EnumTypeDataTruck == EnumTypeDataTruck.DoneTime01 ||
          record.EnumTypeDataTruck == EnumTypeDataTruck.WeightedTime02),
        2 => filtered.Where(record => record.EnumTypeDataTruck == EnumTypeDataTruck.DoneTime02),
        _ => filtered
      };

      if (!string.IsNullOrEmpty(searchKey))
      {
        filtered = filtered.Where(record => new[]
        {
          record.NoLabelAuto, record.NoLabelManual, record.LicensePlate,
          record.NameDriver, record.IdCard, record.Document,
          record.Client?.Name, record.TypeGoods?.Name, record.Warehouse?.Name
        }.Any(value => value?.Contains(searchKey, StringComparison.OrdinalIgnoreCase) == true));
      }

      var dto = DTOHelper.ConvertRecordTruckDTO(filtered.ToList());
      SetDgvHistorical(dto);
    }

    private async void dgv_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
        return;

      if (dgv.Rows[e.RowIndex].DataBoundItem is not RecordTruckDTO recordTruckDto)
        return;

      switch (dgv.Columns[e.ColumnIndex].Name)
      {
        case "btnDetail":
          btnDetail_Click(recordTruckDto);
          break;
        case "btnDelete":
          await ToggleDeletedFlagAsync(recordTruckDto);
          break;
      }
    }

    private async Task ToggleDeletedFlagAsync(RecordTruckDTO recordTruckDto)
    {
      if (recordTruckDto.RecordTruck is not RecordTruck recordTruck)
        return;

      if (!CanModifyRecord(recordTruck))
        return;

      try
      {
        if (!recordTruck.DeletedFlag)
        {
          using var inputReason = new PopupInputReason();
          if (inputReason.ShowDialog(this) != DialogResult.OK)
            return;
          recordTruck.ReasonDelete = inputReason.Reason;
          recordTruck.DeletedFlag = true;

          recordTruck.UpdatedAt = DateTime.UtcNow;
          await AppCore.Ins._recordTruckService.AddOrUpdateAsync(recordTruck);
          await LoadHistorical();
        }
        else
        {
          using var popup = new PopupConfirm(
          "Có chắn chắn phục hồi dữ liệu này ?",
          EnumTypeMsg.Confirm,
          EnumImageMsg.Warning, recordTruck);
          popup.OnSendConfirm += Popup_OnSendConfirm;
          popup.ShowDialog(this);
        }
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        using var popup = new PopupConfirm(
          "Không thể cập nhật trạng thái bản ghi. Vui lòng thử lại!",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popup.ShowDialog(this);
      }
    }

    private async void Popup_OnSendConfirm(object? sender, ResponMsg e)
    {
      RecordTruck recordTruck = e.Obj as RecordTruck;
      if (recordTruck != null)
      {
        recordTruck.ReasonDelete = string.Empty;
        recordTruck.DeletedFlag = false;

        recordTruck.UpdatedAt = DateTime.UtcNow;
        await AppCore.Ins._recordTruckService.AddOrUpdateAsync(recordTruck);
        await LoadHistorical();
      }
    }

    public void SetDgvHistorical(List<RecordTruckDTO> dto)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetDgvHistorical(dto);
        }));
        return;
      }

      dgv.DataSource = dto;

      if (!dgv.Columns.Contains("btnDetail"))
      {
        dgv.Columns.Add(new DataGridViewButtonColumn
        {
          Name = "btnDetail",
          HeaderText = "",
          Text = "Chi tiết",
          UseColumnTextForButtonValue = true,
          AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
          Width = 150,
          Resizable = DataGridViewTriState.False,
          SortMode = DataGridViewColumnSortMode.NotSortable,
        });
      }

      if (!dgv.Columns.Contains("btnDelete"))
      {
        dgv.Columns.Add(new DataGridViewButtonColumn
        {
          Name = "btnDelete",
          HeaderText = "",
          UseColumnTextForButtonValue = false,
          AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
          Width = 120,
          Resizable = DataGridViewTriState.False,
          SortMode = DataGridViewColumnSortMode.NotSortable,
        });
      }

      dgv.Columns["btnDetail"].DisplayIndex = dgv.Columns.Count - 2;
      dgv.Columns["btnDelete"].DisplayIndex = dgv.Columns.Count - 1;

      foreach (DataGridViewRow row in dgv.Rows)
      {
        if (row.DataBoundItem is RecordTruckDTO item && item.RecordTruck != null)
        {
          var isDeleted = item.RecordTruck.DeletedFlag;
          var canModify = CanModifyRecord(item.RecordTruck);
          row.Cells["btnDelete"].Value = !canModify
            ? "Chỉ xem"
            : isDeleted
              ? "Phục hồi"
              : "Xóa";
          row.Cells["btnDelete"].Style.ForeColor = canModify
            ? dgv.DefaultCellStyle.ForeColor
            : Color.Gray;
          var rowBackColor = isDeleted
            ? Color.Tomato
            : dgv.DefaultCellStyle.BackColor;
          row.DefaultCellStyle.BackColor = rowBackColor;
          row.DefaultCellStyle.SelectionBackColor = rowBackColor;
        }
      }

      var hiddenColumns = new[]
      {
        nameof(RecordTruckDTO.RecordTruck),
        nameof(RecordTruckDTO.EnumTypeDataTruck),
        nameof(RecordTruckDTO.Client),
        nameof(RecordTruckDTO.TypeGoods),
        nameof(RecordTruckDTO.Warehouse),
        nameof(RecordTruckDTO.NameDriver),
        nameof(RecordTruckDTO.IdCard),
        nameof(RecordTruckDTO.Document)
      };
      foreach (var columnName in hiddenColumns)
      {
        if (dgv.Columns.Contains(columnName))
          dgv.Columns[columnName].Visible = false;
      }

      var autoSizeColumns = new[]
      {
        nameof(RecordTruckDTO.No),
        nameof(RecordTruckDTO.Datetime),
        nameof(RecordTruckDTO.LicensePlate),
        nameof(RecordTruckDTO.NameDriver),
        nameof(RecordTruckDTO.IdCard),
        nameof(RecordTruckDTO.NetTime01),
        nameof(RecordTruckDTO.NetTime02),
        nameof(RecordTruckDTO.NoLabelAuto),
      };
      foreach (var columnName in autoSizeColumns)
      {
        if (dgv.Columns.Contains(columnName))
          dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      }

      if (dgv.Columns.Contains(nameof(RecordTruckDTO.Status)))
      {
        var statusColumn = dgv.Columns[nameof(RecordTruckDTO.Status)];
        statusColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        statusColumn.Width = 150;
        statusColumn.Resizable = DataGridViewTriState.False;
      }

      var alignmentMiddleRightColumns = new[]
      {
        nameof(RecordTruckDTO.NetTime01),
        nameof(RecordTruckDTO.NetTime02),
      };
      foreach (var columnName in alignmentMiddleRightColumns)
      {
        if (dgv.Columns.Contains(columnName))
          dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
      }

      var alignmentMiddleCenterColumns = new[]
      {
        nameof(RecordTruckDTO.No),
      };
      foreach (var columnName in alignmentMiddleCenterColumns)
      {
        if (dgv.Columns.Contains(columnName))
          dgv.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
      }
    }

    private void dgv_CellMouseEnter(object? sender, DataGridViewCellEventArgs e)
    {
      _deleteReasonToolTip.Hide(dgv);
      _deleteReasonToolTipText = string.Empty;

      if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
        dgv.Columns[e.ColumnIndex].Name != "btnDelete" ||
        dgv.Rows[e.RowIndex].DataBoundItem is not RecordTruckDTO item ||
        item.RecordTruck?.DeletedFlag != true ||
        string.IsNullOrWhiteSpace(item.RecordTruck.ReasonDelete))
      {
        return;
      }

      var cellBounds = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
      _deleteReasonToolTipText = $"Lý do xóa: {item.RecordTruck.ReasonDelete}";
      _deleteReasonToolTip.Show(
        _deleteReasonToolTipText,
        dgv,
        cellBounds.Left,
        cellBounds.Bottom,
        10000);
    }

    private void DeleteReasonToolTip_Popup(object? sender, PopupEventArgs e)
    {
      if (string.IsNullOrEmpty(_deleteReasonToolTipText))
        return;

      var textSize = TextRenderer.MeasureText(
        _deleteReasonToolTipText,
        _deleteReasonToolTipFont,
        new Size(600, 0),
        TextFormatFlags.WordBreak);
      e.ToolTipSize = new Size(textSize.Width + 24, textSize.Height + 16);
    }

    private void DeleteReasonToolTip_Draw(object? sender, DrawToolTipEventArgs e)
    {
      e.Graphics.FillRectangle(Brushes.LightYellow, e.Bounds);
      e.DrawBorder();
      TextRenderer.DrawText(
        e.Graphics,
        e.ToolTipText,
        _deleteReasonToolTipFont,
        Rectangle.Inflate(e.Bounds, -12, -8),
        Color.Black,
        TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
    }

    private void btnDetail_Click(RecordTruckDTO recordTruckDto)
    {
      if (recordTruckDto.RecordTruck is not RecordTruck recordTruck)
        return;

      _recordTruck = recordTruck;
      ShowDataHistorical(_recordTruck);
    }

    private void btnCreate_Click(object sender, EventArgs e)
    {
      _recordTruck = new RecordTruck();
      ShowDataHistorical(_recordTruck);
    }


    #region Đường bo Status
    private void dgv_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
    {
      if (e.RowIndex < 0)
        return;

      Color borderColor;
      Color backColor;
      Color textColor;

      if (dgv.Columns[e.ColumnIndex].DataPropertyName != nameof(RecordTruckDTO.Status))
        return;

      var itemMaterial = dgv.Rows[e.RowIndex].DataBoundItem as RecordTruckDTO;
      if (itemMaterial == null)
        return;

      e.PaintBackground(e.CellBounds, true);

      if (itemMaterial.RecordTruck?.DeletedFlag == true)
      {
        borderColor = Color.DarkRed;
        backColor = Color.Tomato;
        textColor = Color.White;
      }
      else switch (itemMaterial.EnumTypeDataTruck)
        {
          case EnumTypeDataTruck.WeightedTime01:
          case EnumTypeDataTruck.DoneTime01:
            //Xanh dương
            borderColor = Color.FromArgb(30, 64, 175);
            backColor = Color.FromArgb(219, 234, 254);
            textColor = borderColor;
            break;
          case EnumTypeDataTruck.WeightedTime02:
          case EnumTypeDataTruck.DoneTime02:
            //Xanh lá
            borderColor = Color.FromArgb(40, 167, 69);
            backColor = Color.FromArgb(220, 245, 228);
            textColor = borderColor;
            break;

          default:
            //Xám
            borderColor = Color.FromArgb(73, 80, 87);
            backColor = Color.FromArgb(222, 226, 230);
            textColor = borderColor;
            break;
        }

      var rectMaterial = new Rectangle(
          e.CellBounds.X + 8,
          e.CellBounds.Y + 8,
          e.CellBounds.Width - 16,
          e.CellBounds.Height - 16);

      using (GraphicsPath path = GetRoundRectangle(rectMaterial, 20))
      using (SolidBrush brush = new SolidBrush(backColor))
      using (Pen pen = new Pen(borderColor))
      using (SolidBrush textBrush = new SolidBrush(textColor))
      {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        e.Graphics.FillPath(brush, path);
        e.Graphics.DrawPath(pen, path);

        TextRenderer.DrawText(
            e.Graphics,
            itemMaterial.Status,
            e.CellStyle.Font,
            rectMaterial,
            textColor,
            TextFormatFlags.HorizontalCenter |
            TextFormatFlags.VerticalCenter);
      }
      e.Handled = true;
    }

    private GraphicsPath GetRoundRectangle(Rectangle rect, int radius)
    {
      GraphicsPath path = new GraphicsPath();

      int d = radius * 2;

      path.AddArc(rect.X, rect.Y, d, d, 180, 90);
      path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
      path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
      path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);

      path.CloseFigure();

      return path;
    }
    #endregion


    public readonly RecordTruckService _recordTruckService = new();
    private void btnZero_Click(object sender, EventArgs e)
    {
      //RecordTruck? record = await _recordTruckService.GetDetailByIdAsync(_recordTruck.Id);

      //if (record == null)
      //  return;

      //Download(DateTime.Now, record);
      var rs = LicensePlateHelper.IsValidVietnamLicensePlate(txtLicensePlate.Texts);
      if (!rs.IsValid)
      {
        using var popupMsg = new PopupConfirm("Biển số xe không hợp lệ !",
          EnumTypeMsg.MessageManualClose, EnumImageMsg.Information);
        popupMsg.ShowDialog(this);
        return;
      }

      txtNoLabel.Texts = rs.Plate;
    }

    private async void btnPrint_Click(object sender, EventArgs e)
    {
      try
      {
        RecordTruck? record = await _recordTruckService.GetDetailByIdAsync(_recordTruck.Id);

        if (record == null)
        {
          PopupConfirm popupWarning = new PopupConfirm("Không tìm thấy thông tin !", EnumTypeMsg.MessageAutoClose, EnumImageMsg.Warning);
          popupWarning.ShowDialog();
          return;
        }

        var rs = await DownloadReportTruck(DateTime.Now, record);

        //POST PDF
        await (new ApiService()).UploadReportTruckPdf(record.Id, rs);


        //var recordWeightsByProduct = (record.RecordWeights ?? Enumerable.Empty<RecordWeight>())
        //.GroupBy(recordWeight => recordWeight.ProductId)
        //.Select(group => new
        //{
        //  ProductGroup = group.First().Product.ProductGroup?.Name,
        //  ProductName = group.First().Product?.Name ?? string.Empty,
        //  ProductCode = group.First().Product?.Code ?? string.Empty,
        //  SumNet = group.Sum(recordWeight => recordWeight.Net)
        //})
        //.ToList();

        //await Download(DateTime.Now, record);



        PopupConfirm popupConfirm = new PopupConfirm("In phiếu giao nhận thành công.", EnumTypeMsg.MessageAutoClose, EnumImageMsg.Information);
        popupConfirm.ShowDialog();
      }
      catch (Exception ex)
      {
        PopupConfirm popupConfirm = new PopupConfirm("In phiếu giao nhận thất bại !", EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
        popupConfirm.ShowDialog();
      }
    }


    private async Task Download(DateTime dt, RecordTruck recordTruck)
    {
      string pathFileTemplateTable = Application.StartupPath + "Template\\TemplateTableHtml.html";
      string pathFileTemplate = Application.StartupPath + "Template\\TemplateHtml.html";
      string folderOutput = Application.StartupPath + "Template\\OutputFiles";


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
                              .Replace("{{vehiclePlate}}", recordTruck.LicensePlate)
                              .Replace("{{sealNo}}", "")

                              .Replace("{{signPlace}}", "Đồng Nai")
                              .Replace("{{signDay}}", dt.Day.ToString())
                              .Replace("{{signMonth}}", dt.Month.ToString())
                              .Replace("{{signYear}}", dt.Year.ToString())
                              .Replace("{{sender.deptCode}}", "FCM")
                              .Replace("{{receiver.deptCode}}", "SES");


      var recordWeightsByProduct = (recordTruck.RecordWeights ?? Enumerable.Empty<RecordWeight>())
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
          tempTableDetal = tempTableDetal.Replace("{{quantity}}", recordWeightsByProduct[no - 1].SumNet.ToString("F3"));
          tempTableDetal = tempTableDetal.Replace("{{note}}", "");


          tableDetails = tableDetails + tempTableDetal;
          value += recordWeightsByProduct[no - 1].SumNet;
        }
      }

      result = result.Replace("{{totalQuantity}}", value.ToString("F3"));
      result = result.Replace("{table}", tableDetails);

      string outputPath = Path.Combine(folderOutput, $"{dt.ToString("yyMMddHHmmss")}.html");
      File.WriteAllText(outputPath, result);

      await CreateFile(outputPath);
    }

    private async Task<string> DownloadReportTruck(DateTime dt, RecordTruck recordTruck)
    {
      try
      {
        //PdfHelper.InitAsync().GetAwaiter().GetResult();
        string pathFileTemplate = Application.StartupPath + "Template\\TemplateTruck.html";
        string folderOutput = Application.StartupPath + "Report";
        if (!Directory.Exists(folderOutput))
        {
          Directory.CreateDirectory(folderOutput);
        }

        string template = File.ReadAllText(pathFileTemplate);
        string company = AppCore.Ins._appConfig?.Company??string.Empty;
        string address = AppCore.Ins._appConfig?.Address ?? string.Empty;
        string phone = AppCore.Ins._appConfig?.Phone ?? string.Empty;
        string timePrint = dt.ToString(
                                        "HH:mm 'Ngày' dd 'tháng' MM 'năm' yyyy",
                                        CultureInfo.GetCultureInfo("vi-VN")
                                      );

        double firstWeight = recordTruck.NetTime01;
        double secondWeight = recordTruck.NetTime02;
        bool hasFirstWeight = firstWeight > 0;
        bool hasSecondWeight = secondWeight > 0;

        string gross = "...";
        string tare = hasFirstWeight ? firstWeight.ToString("F3") : "...";
        string net = "...";
        string importExport = "Chưa xác định";
        string timeTare = recordTruck.WeighInAt != null ? ((DateTime)(recordTruck.WeighInAt)).AddHours(AppCore.Ins._time).ToString("dd/MM/yyyy HH:mm") : "";
        string timeGross = "...";

        if (hasFirstWeight && hasSecondWeight)
        {
          // Trọng lượng xe và hàng luôn là số cân lớn hơn, trọng lượng xe là số nhỏ hơn.
          double grossWeight = Math.Max(firstWeight, secondWeight);
          double tareWeight = Math.Min(firstWeight, secondWeight);
          double netWeight = grossWeight - tareWeight;

          gross = grossWeight.ToString("F3");
          tare = tareWeight.ToString("F3");
          net = netWeight.ToString("F3");
          importExport = secondWeight > firstWeight
            ? "Xuất hàng"
            : secondWeight < firstWeight
              ? "Nhập hàng"
              : "Chưa xác định";

          timeGross = recordTruck.WeighOutAt != null ? ((DateTime)(recordTruck.WeighOutAt)).AddHours(AppCore.Ins._time).ToString("dd/MM/yyyy HH:mm") : "";
        }

        string result = template.Replace("{company}", company)
                                .Replace("{address}", address)
                                .Replace("{phone}", phone)
                                .Replace("{time_print}", timePrint)
                                .Replace("{ticket_no}", recordTruck.NoLabelAuto)
                                .Replace("{date}", dt.ToString("dd/MM/yyyy"))
                                .Replace("{plate}", recordTruck.LicensePlate)
                                .Replace("{import_export}", importExport)
                                .Replace("{client}", recordTruck.Client?.Name)
                                .Replace("{goods}", recordTruck.TypeGoods?.Name)
                                .Replace("{gross}", gross)
                                .Replace("{tare}", tare)
                                .Replace("{net}", net)
                                .Replace("{time_tare}", timeTare)
                                .Replace("{time_gross}", timeGross)
                                .Replace("{note}", recordTruck.Document)
                                ;

        //string outputPath = Path.Combine(folderOutput, $"REPORT_TRUCK_{dt.ToString("yyMMddHHmmss")}.html");
        string outputPath = Path.Combine(folderOutput, $"{recordTruck.Id.ToString().Replace("-", "").Replace(" ", "")}.html");
        File.WriteAllText(outputPath, result);

        return await CreateFile(outputPath);
      }
      catch (Exception)
      {
        throw;
      }
    }


    private async Task<string> CreateFile(string path)
    {
      try
      {
        string pdf = path.Replace(".html", ".pdf");
        await PdfHelper.HtmlToPdfAsync(path, pdf);
        return pdf;
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}
