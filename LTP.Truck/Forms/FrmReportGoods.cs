using ClosedXML.Excel;
using Common;
using HelperManager;
using iSoft.Database;
using iSoft.Database.DTO;
using LTP.Truck.Controls;
using LTP.Truck.Custom;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using static Common.EnumData;

namespace LTP.Truck.Forms
{
  public partial class FrmReportGoods : Form
  {
    private bool _isLoadingPage;

    public FrmReportGoods()
    {
      InitializeComponent();
      CustomUI();

      btnSearchHistorical.Click += btnSearchHistorical_Click;
      txtSearchKey.KeyPress += txtSearchKey_KeyPress;
      ucPage1.PageChanged += ucPage1_PageChanged;
      Shown += FrmReportGoods_Shown;
    }

    #region Instance
    private static FrmReportGoods _Instance = null;

    public static FrmReportGoods Instance
    {
      get
      {
        if (_Instance == null) _Instance = new FrmReportGoods();
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
      elipseControl01.TargetControl = tableLayoutPanel9;
      elipseControl01.CornerRadius = 20;

      ucTimeSearchFrom.Value = DateTime.Today;
      ucTimeSearchTo.Value = DateTime.Today.AddDays(1).AddMinutes(-1);

      dgv.EnableHeadersVisualStyles = false;
      dgv.ColumnHeadersHeight = 50;
      dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
      dgv.RowTemplate.Height = 60;
      dgv.MultiSelect = false;
    }
    private async void FrmReportGoods_Shown(object? sender, EventArgs e)
    {
      await LoadHistorical(resetPage: true);
    }

    private async void btnSearchHistorical_Click(object? sender, EventArgs e)
    {
      await LoadHistorical(resetPage: true);
    }

    private async void txtSearchKey_KeyPress(object? sender, KeyPressEventArgs e)
    {
      if (e.KeyChar != (char)Keys.Enter)
        return;

      e.Handled = true;
      await LoadHistorical(resetPage: true);
    }

    private async void ucPage1_PageChanged(object? sender, UserControls.PageChangedEventArgs e)
    {
      await LoadHistorical();
    }

    private async Task LoadHistorical(bool resetPage = false)
    {
      if (_isLoadingPage)
        return;

      try
      {
        _isLoadingPage = true;
        btnSearchHistorical.Enabled = false;
        ucPage1.Enabled = false;

        if (resetPage)
          ucPage1.ResetToFirstPage();

        var fromDateTime = ucTimeSearchFrom.Value;
        var toDateTime = ucTimeSearchTo.Value;
        if (fromDateTime > toDateTime)
        {
          using var popup = new PopupConfirm(
            "Thời gian bắt đầu không được lớn hơn thời gian kết thúc.",
            EnumTypeMsg.MessageManualClose,
            EnumImageMsg.Warning);
          popup.ShowDialog(this);
          return;
        }

        var fromUtc = fromDateTime.ToUniversalTime();
        var toUtcExclusive = toDateTime.AddMinutes(1).ToUniversalTime();
        var searchKey = txtSearchKey.Texts.Trim();
        var pageNumber = ucPage1.CurrentPage;
        var pageSize = ucPage1.PageSize;
        var (records, totalRecords) = await AppCore.Ins._recordWeightService.GetReportPageAsync(
          fromUtc,
          toUtcExclusive,
          searchKey,
          pageNumber,
          pageSize);

        var totalPages = Math.Max(1, (int)Math.Ceiling(totalRecords / (double)pageSize));
        var effectivePage = Math.Min(pageNumber, totalPages);
        var recordsDto = DTOHelper.ConvertRecordWeightDTO(records);
        for (var index = 0; index < recordsDto.Count; index++)
          recordsDto[index].No = totalRecords - ((effectivePage - 1) * pageSize + index);

        ucPage1.SetTotalRecords(totalRecords, effectivePage);
        SetDgvHistorical(recordsDto);
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        using var popup = new PopupConfirm(
          "Không thể tải dữ liệu báo cáo. Vui lòng thử lại !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popup.ShowDialog(this);
      }
      finally
      {
        _isLoadingPage = false;
        btnSearchHistorical.Enabled = true;
        ucPage1.Enabled = true;
      }
    }

    private void SetDgvHistorical(List<RecordWeightDTO> records)
    {
      if (InvokeRequired)
      {
        Invoke(new Action(() => SetDgvHistorical(records)));
        return;
      }

      dgv.DataSource = records;

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
        nameof(RecordWeightDTO.Gross)
      };
      foreach (var columnName in autoSizeColumns)
      {
        if (dgv.Columns.Contains(columnName))
          dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      }

      var weightColumns = new[]
      {
        nameof(RecordWeightDTO.No),
        nameof(RecordWeightDTO.Net),
        nameof(RecordWeightDTO.Tare),
        nameof(RecordWeightDTO.Gross)
      };
      foreach (var columnName in weightColumns)
      {
        if (dgv.Columns.Contains(columnName))
          dgv.Columns[columnName].DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight;
      }

      var middleCenterColumns = new[]
      {
        nameof(RecordWeightDTO.No),
        nameof(RecordWeightDTO.ProductGroup),
      };
      foreach (var columnName in middleCenterColumns)
      {
        if (dgv.Columns.Contains(columnName))
          dgv.Columns[columnName].DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleCenter;
      }

      dgv.ClearSelection();
      dgv.CurrentCell = null;
    }

    private async void btnExport_Click(object sender, EventArgs e)
    {
      if (ucPage1.TotalRecords == 0)
      {
        using var popup = new PopupConfirm(
          "Không có dữ liệu để xuất báo cáo.",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Information);
        popup.ShowDialog(this);
        return;
      }

      var templatePath = Path.Combine(AppContext.BaseDirectory, "Template", "TemplateReport.xlsx");
      if (!File.Exists(templatePath))
      {
        using var popup = new PopupConfirm(
          "Không tìm thấy file mẫu TemplateReport.xlsx.",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popup.ShowDialog(this);
        return;
      }

      using var saveDialog = new SaveFileDialog
      {
        Filter = "Excel Workbook (*.xlsx)|*.xlsx",
        FileName = $"BaoCaoCanHang_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
        DefaultExt = "xlsx",
        AddExtension = true,
        OverwritePrompt = true
      };

      if (saveDialog.ShowDialog(this) != DialogResult.OK)
        return;

      try
      {
        btnExport.Enabled = false;
        var fromDateTime = ucTimeSearchFrom.Value;
        var toDateTime = ucTimeSearchTo.Value;
        var searchKey = txtSearchKey.Texts.Trim();
        var exportRecords = await AppCore.Ins._recordWeightService.GetReportAsync(
          fromDateTime.ToUniversalTime(),
          toDateTime.AddMinutes(1).ToUniversalTime(),
          searchKey);
        var exportData = DTOHelper.ConvertRecordWeightDTO(exportRecords);

        var exporterName = AppCore.Ins._userCurrent?.DisplayName;
        if (string.IsNullOrWhiteSpace(exporterName))
          exporterName = AppCore.Ins._userCurrent?.FullName;
        if (string.IsNullOrWhiteSpace(exporterName))
          exporterName = AppCore.Ins._userCurrent?.Username ?? string.Empty;

        await Task.Run(() => ExportGoodsReport(
          templatePath,
          saveDialog.FileName,
          exportData,
          fromDateTime,
          toDateTime,
          exporterName));

        var openExportedFile = false;
        using (var popup = new PopupConfirm(
          "Xuất báo cáo Excel thành công. Bạn có muốn mở file không?",
          EnumTypeMsg.Confirm,
          EnumImageMsg.Question))
        {
          popup.OnSendConfirm += (_, response) =>
            openExportedFile = response.EnumResponsible == EnumResponsible.Confirm;
          popup.ShowDialog(this);
        }

        if (openExportedFile)
        {
          try
          {
            Process.Start(new ProcessStartInfo
            {
              FileName = saveDialog.FileName,
              UseShellExecute = true
            });
          }
          catch (Exception openException)
          {
            LogHelper.LogErrorToFileLog(openException, AppCore.Ins._folderFileLog);
            using var openErrorPopup = new PopupConfirm(
              "Đã xuất báo cáo nhưng không thể mở file.",
              EnumTypeMsg.MessageManualClose,
              EnumImageMsg.Warning);
            openErrorPopup.ShowDialog(this);
          }
        }
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        using var popup = new PopupConfirm(
          "Không thể xuất báo cáo Excel. Vui lòng thử lại !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popup.ShowDialog(this);
      }
      finally
      {
        btnExport.Enabled = true;
      }
    }

    private static void ExportGoodsReport(
      string templatePath,
      string outputPath,
      IReadOnlyList<RecordWeightDTO> records,
      DateTime fromDateTime,
      DateTime toDateTime,
      string exporterName)
    {
      using var workbook = new XLWorkbook(templatePath);
      var worksheet = workbook.Worksheet("CanHang");
      foreach (var unrelatedWorksheet in workbook.Worksheets
        .Where(sheet => sheet.Name != worksheet.Name)
        .ToList())
      {
        unrelatedWorksheet.Delete();
      }

      worksheet.Cell("F2").Value = fromDateTime;
      worksheet.Cell("F3").Value = toDateTime;
      worksheet.Cell("F4").Value = exporterName;
      worksheet.Cell("F5").Value = DateTime.Now;
      worksheet.Cell("F2").Style.DateFormat.Format = "dd/MM/yyyy HH:mm:ss";
      worksheet.Cell("F3").Style.DateFormat.Format = "dd/MM/yyyy HH:mm:ss";
      worksheet.Cell("F5").Style.DateFormat.Format = "dd/MM/yyyy HH:mm:ss";

      const int firstDataRow = 10;
      for (var index = 0; index < records.Count; index++)
      {
        var record = records[index].RecordWeight!;
        var row = firstDataRow + index;
        var operatorName = record.User?.DisplayName;
        if (string.IsNullOrWhiteSpace(operatorName))
          operatorName = record.User?.FullName;
        if (string.IsNullOrWhiteSpace(operatorName))
          operatorName = record.User?.Username;

        worksheet.Cell(row, 1).Value = index + 1;
        if (record.CreatedAt.HasValue)
          worksheet.Cell(row, 2).Value = record.CreatedAt.Value.AddHours(DTOHelper.utc);
        worksheet.Cell(row, 3).Value = record.LicensePlate ?? string.Empty;
        worksheet.Cell(row, 4).Value = record.Product?.ProductGroup?.Name ?? string.Empty;
        worksheet.Cell(row, 5).Value = record.Product?.Code ?? string.Empty;
        worksheet.Cell(row, 6).Value = record.Product?.Name ?? string.Empty;
        worksheet.Cell(row, 7).Value = record.CategoryTare?.Name ?? string.Empty;
        worksheet.Cell(row, 8).Value = record.Net + record.Tare;
        worksheet.Cell(row, 9).Value = record.Net;
        worksheet.Cell(row, 10).Value = record.Tare;
        worksheet.Cell(row, 11).Value = operatorName ?? string.Empty;
      }

      var lastDataRow = firstDataRow + records.Count - 1;
      var dataRange = worksheet.Range(firstDataRow, 1, lastDataRow, 11);
      dataRange.Style.Font.FontName = "Arial";
      dataRange.Style.Font.FontSize = 10;
      dataRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
      dataRange.Style.Alignment.WrapText = true;
      dataRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;
      dataRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
      dataRange.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
      dataRange.Style.Border.RightBorder = XLBorderStyleValues.Thin;
      worksheet.Range(firstDataRow, 1, lastDataRow, 1).Style.Alignment.Horizontal =
        XLAlignmentHorizontalValues.Center;
      worksheet.Range(firstDataRow, 2, lastDataRow, 2).Style.DateFormat.Format =
        "dd/MM/yyyy HH:mm:ss";
      worksheet.Range(firstDataRow, 8, lastDataRow, 10).Style.NumberFormat.Format = "#,##0.000";
      worksheet.Range(firstDataRow, 8, lastDataRow, 10).Style.Alignment.Horizontal =
        XLAlignmentHorizontalValues.Right;

      worksheet.SheetView.FreezeRows(9);
      worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
      worksheet.PageSetup.FitToPages(1, 0);
      worksheet.PageSetup.PrintAreas.Clear();
      worksheet.PageSetup.PrintAreas.Add($"A1:K{lastDataRow}");

      workbook.SaveAs(outputPath);
    }
  }
}
