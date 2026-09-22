using ClosedXML.Excel;
using Common;
using HelperManager;
using iSoft.Database;
using iSoft.Database.DTO;
using LTP.Truck.Controls;
using LTP.Truck.Custom;
using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static Common.EnumData;
using static iSoft.Database.EnumData;

namespace LTP.Truck.Forms
{
  public partial class FrmReportTruck : Form
  {
    private int _statusFilterIndex;
    private int _typeFilterIndex;
    private bool _isLoadingPage;

    public FrmReportTruck()
    {
      InitializeComponent();

      CustomUI();

      btnSearchHistorical.Click += btnSearchHistorical_Click;
      btnFilter.Click += btnFilter_Click;
      txtSearchKey.KeyPress += txtSearchKey_KeyPress;
      ucPage1.PageChanged += ucPage1_PageChanged;
      Shown += FrmReportTruck_Shown;
      dgv.CellPainting += dgv_CellPainting;
    }

    private void CustomUI()
    {
      ElipseControl elipseControl = new ElipseControl();
      elipseControl.TargetControl = this;
      elipseControl.CornerRadius = 20;

      ElipseControl elipseControl01 = new ElipseControl();
      elipseControl01.TargetControl = tableLayoutPanel7;
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

    #region Instance
    private static FrmReportTruck _Instance = null;

    public static FrmReportTruck Instance
    {
      get
      {
        if (_Instance == null) _Instance = new FrmReportTruck();
        return _Instance;
      }
    }
    #endregion

    private async void FrmReportTruck_Shown(object? sender, EventArgs e)
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

    private void btnFilter_Click(object? sender, EventArgs e)
    {
      using var popupFilter = new PopupFilter(_statusFilterIndex, _typeFilterIndex);
      popupFilter.OnSendData += PopupFilter_OnSendData;
      popupFilter.ShowDialog(this);
    }

    private async void PopupFilter_OnSendData(int statusIndex, int typeIndex)
    {
      _statusFilterIndex = statusIndex;
      _typeFilterIndex = typeIndex;
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
        btnFilter.Enabled = false;
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
        var (records, totalRecords) = await AppCore.Ins._recordTruckService.GetReportPageAsync(
          fromUtc,
          toUtcExclusive,
          searchKey,
          _statusFilterIndex,
          _typeFilterIndex,
          pageNumber,
          pageSize);

        var totalPages = Math.Max(1, (int)Math.Ceiling(totalRecords / (double)pageSize));
        var effectivePage = Math.Min(pageNumber, totalPages);
        var recordsDto = DTOHelper.ConvertRecordTruckDTO(records);
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
        btnFilter.Enabled = true;
        ucPage1.Enabled = true;
      }
    }

    private void SetDgvHistorical(List<RecordTruckDTO> records)
    {
      if (InvokeRequired)
      {
        Invoke(new Action(() => SetDgvHistorical(records)));
        return;
      }

      dgv.DataSource = records;

      var hiddenColumns = new[]
      {
        nameof(RecordTruckDTO.RecordTruck),
        nameof(RecordTruckDTO.EnumTypeDataTruck)
      };
      foreach (var columnName in hiddenColumns)
      {
        if (dgv.Columns.Contains(columnName))
          dgv.Columns[columnName].Visible = false;
      }

      var autoSizeColumns = new[]
      {
        nameof(RecordTruckDTO.No),
        nameof(RecordTruckDTO.LicensePlate),
        nameof(RecordTruckDTO.NetTime01),
        nameof(RecordTruckDTO.NetTime02),
        nameof(RecordTruckDTO.Time01),
        nameof(RecordTruckDTO.Time02),
        nameof(RecordTruckDTO.Status)
      };
      foreach (var columnName in autoSizeColumns)
      {
        if (dgv.Columns.Contains(columnName))
          dgv.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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

      if (dgv.Columns.Contains(nameof(RecordTruckDTO.Status)))
      {
        var statusColumn = dgv.Columns[nameof(RecordTruckDTO.Status)];
        statusColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        statusColumn.Width = 200;
        statusColumn.Resizable = DataGridViewTriState.False;
      }

      dgv.ClearSelection();
      dgv.CurrentCell = null;
    }

    private async void btnExport_Click(object? sender, EventArgs e)
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
        FileName = $"BaoCaoCanXeTai_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
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
        var exportRecords = await AppCore.Ins._recordTruckService.GetReportAsync(
          fromDateTime.ToUniversalTime(),
          toDateTime.AddMinutes(1).ToUniversalTime(),
          searchKey,
          _statusFilterIndex,
          _typeFilterIndex);
        var exportData = DTOHelper.ConvertRecordTruckDTO(exportRecords);

        var exporterName = AppCore.Ins._userCurrent?.DisplayName;
        if (string.IsNullOrWhiteSpace(exporterName))
          exporterName = AppCore.Ins._userCurrent?.FullName;
        if (string.IsNullOrWhiteSpace(exporterName))
          exporterName = AppCore.Ins._userCurrent?.Username ?? string.Empty;

        await Task.Run(() => ExportTruckReport(
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

    private static void ExportTruckReport(
      string templatePath,
      string outputPath,
      IReadOnlyList<RecordTruckDTO> records,
      DateTime fromDateTime,
      DateTime toDateTime,
      string exporterName)
    {
      using var workbook = new XLWorkbook(templatePath);
      var worksheet = workbook.Worksheet("CanXeTai");
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
      worksheet.Range("F2:I3").Style.DateFormat.Format = "dd/MM/yyyy HH:mm:ss";
      worksheet.Range("F5:I5").Style.DateFormat.Format = "dd/MM/yyyy HH:mm:ss";

      const int firstDataRow = 10;
      for (var index = 0; index < records.Count; index++)
      {
        var dto = records[index];
        var record = dto.RecordTruck!;
        var row = firstDataRow + index;
        var operatorName = record.User?.DisplayName;
        if (string.IsNullOrWhiteSpace(operatorName))
          operatorName = record.User?.FullName;
        if (string.IsNullOrWhiteSpace(operatorName))
          operatorName = record.User?.Username;

        worksheet.Cell(row, 1).Value = index + 1;
        worksheet.Cell(row, 2).Value = record.NoLabelAuto ?? string.Empty;
        worksheet.Cell(row, 3).Value = record.NoLabelManual ?? string.Empty;
        worksheet.Cell(row, 4).Value = record.LicensePlate ?? string.Empty;
        worksheet.Cell(row, 5).Value = record.NameDriver ?? string.Empty;
        worksheet.Cell(row, 6).Value = record.IdCard ?? string.Empty;
        worksheet.Cell(row, 7).Value = record.NetTime01;
        worksheet.Cell(row, 8).Value = record.NetTime02;
        worksheet.Cell(row, 9).Value = Math.Abs(record.NetTime01 - record.NetTime02);
        worksheet.Cell(row, 10).Value = dto.Status ?? string.Empty;

        if (record.WeighInAt.HasValue)
          worksheet.Cell(row, 11).Value = record.WeighInAt.Value.AddHours(DTOHelper.utc);
        if (record.WeighOutAt.HasValue)
          worksheet.Cell(row, 12).Value = record.WeighOutAt.Value.AddHours(DTOHelper.utc);

        worksheet.Cell(row, 13).Value = record.Client?.Name ?? string.Empty;
        worksheet.Cell(row, 14).Value = record.Warehouse?.Name ?? string.Empty;
        worksheet.Cell(row, 15).Value = record.TypeGoods?.Name ?? string.Empty;
        worksheet.Cell(row, 16).Value = record.Document ?? string.Empty;
        worksheet.Cell(row, 17).Value = record.Station?.Name ?? string.Empty;
        worksheet.Cell(row, 18).Value = operatorName ?? string.Empty;
        worksheet.Cell(row, 19).Value = record.ReasonDelete ?? string.Empty;
      }

      var lastDataRow = firstDataRow + records.Count - 1;
      var dataRange = worksheet.Range(firstDataRow, 1, lastDataRow, 19);
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
      worksheet.Range(firstDataRow, 7, lastDataRow, 9).Style.NumberFormat.Format = "#,##0";
      worksheet.Range(firstDataRow, 7, lastDataRow, 9).Style.Alignment.Horizontal =
        XLAlignmentHorizontalValues.Right;
      worksheet.Range(firstDataRow, 11, lastDataRow, 12).Style.DateFormat.Format =
        "dd/MM/yyyy HH:mm:ss";

      worksheet.SheetView.FreezeRows(9);
      worksheet.PageSetup.PageOrientation = XLPageOrientation.Landscape;
      worksheet.PageSetup.FitToPages(1, 0);
      worksheet.PageSetup.PrintAreas.Clear();
      worksheet.PageSetup.PrintAreas.Add($"A1:S{lastDataRow}");

      workbook.SaveAs(outputPath);
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

            //borderColor = Color.FromArgb(254, 206, 49);
            //backColor = Color.FromArgb(249, 243, 220);
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
  }
}
