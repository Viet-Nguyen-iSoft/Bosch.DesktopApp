using ClosedXML.Excel;
using Common;
using HelperManager;
using iSoft.Database;
using iSoft.Database.DTO;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;
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
    private List<RecordWeightDTO> _currentRecords = new();
    private readonly FlowLayoutPanel _licensePlateGroups = new();

    public event Action<string, List<RecordWeightDTO>>? ExportGroupRequested;

    public FrmReportGoods()
    {
      InitializeComponent();
      CustomUI();

      btnSearchHistorical.Click += btnSearchHistorical_Click;
      txtSearchKey.KeyPress += txtSearchKey_KeyPress;
      ucPage1.PageChanged += ucPage1_PageChanged;
      Shown += FrmReportGoods_Shown;
      cbbType.DropDownStyle = ComboBoxStyle.DropDownList;
      cbbType.SelectedIndex = 0;
      cbbType.SelectedIndexChanged += cbbType_SelectedIndexChanged;
      InitializeLicensePlateGroups();
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

    private void InitializeLicensePlateGroups()
    {
      _licensePlateGroups.Dock = DockStyle.Fill;
      _licensePlateGroups.AutoScroll = true;
      _licensePlateGroups.WrapContents = false;
      _licensePlateGroups.FlowDirection = FlowDirection.TopDown;
      _licensePlateGroups.BackColor = dgv.BackgroundColor;
      _licensePlateGroups.Padding = Padding.Empty;
      _licensePlateGroups.Visible = false;
      _licensePlateGroups.SizeChanged += (_, _) => ResizeLicensePlateGroupPanels();
      tableLayoutPanel9.Controls.Add(_licensePlateGroups, 0, 2);
    }
    private async void FrmReportGoods_Shown(object? sender, EventArgs e)
    {
      await LoadHistorical(resetPage: true);
    }

    private async void btnSearchHistorical_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
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
        var recordsDto = DTOHelper.ConvertRecordWeightDTO(records)
          .OrderBy(record => record.RecordWeight?.CreatedAt)
          .ThenBy(record => record.RecordWeight?.Id)
          .ToList();
        for (var index = 0; index < recordsDto.Count; index++)
          recordsDto[index].No = (effectivePage - 1) * pageSize + index + 1;

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

      _currentRecords = records;
      var showGroupedView = cbbType.SelectedIndex == 1;
      dgv.Visible = !showGroupedView;
      _licensePlateGroups.Visible = showGroupedView;

      if (showGroupedView)
      {
        BuildLicensePlateGroups(records);
        return;
      }

      dgv.DataSource = records;
      ApplyGridColumnFormatting(dgv);
    }

    private static void ApplyGridColumnFormatting(DataGridView grid)
    {
      if (grid.Columns.Contains(nameof(RecordWeightDTO.RecordWeight)))
        grid.Columns[nameof(RecordWeightDTO.RecordWeight)].Visible = false;

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
        if (grid.Columns.Contains(columnName))
          grid.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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
        if (grid.Columns.Contains(columnName))
          grid.Columns[columnName].DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight;
      }

      var middleCenterColumns = new[]
      {
        nameof(RecordWeightDTO.No),
        nameof(RecordWeightDTO.ProductGroup),
      };
      foreach (var columnName in middleCenterColumns)
      {
        if (grid.Columns.Contains(columnName))
          grid.Columns[columnName].DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleCenter;
      }

      grid.ClearSelection();
      grid.CurrentCell = null;
    }

    private void cbbType_SelectedIndexChanged(object? sender, EventArgs e)
    {
      SetDgvHistorical(_currentRecords);
    }

    private void BuildLicensePlateGroups(IEnumerable<RecordWeightDTO> records)
    {
      _licensePlateGroups.SuspendLayout();
      _licensePlateGroups.Controls.Clear();

      foreach (var group in records.GroupBy(
        record => string.IsNullOrWhiteSpace(record.LicensePlate)
          ? "Không có biển số"
          : record.LicensePlate.Trim(),
        StringComparer.CurrentCultureIgnoreCase))
      {
        var groupRecords = group.ToList();
        var groupPanel = new Panel
        {
          BackColor = dgv.BackgroundColor,
          BorderStyle = BorderStyle.FixedSingle,
          Height = 54,
          Margin = new Padding(0, 0, 0, 8),
          Tag = false,
        };
        var headerPanel = new Panel
        {
          Dock = DockStyle.Top,
          Height = 52,
          BackColor = Color.FromArgb(218, 218, 218),
        };
        var header = new Label
        {
          Dock = DockStyle.Fill,
          Font = new Font(dgv.Font.FontFamily, 14F, FontStyle.Regular),
          ForeColor = Color.Black,
          Text = $"  ▶    {group.Key}",
          TextAlign = ContentAlignment.MiddleLeft,
          Cursor = Cursors.Hand,
          Tag = group.Key,
        };
        var exportGroupButton = new RJButton
        {
          Dock = DockStyle.Right,
          Width = 160,
          Margin = new Padding(6),
          BackgroundColor = Color.FromArgb(0, 122, 204),
          BorderRadius = 5,
          BorderSize = 0,
          Font = new Font(dgv.Font.FontFamily, 11F, FontStyle.Bold),
          TextColor = Color.White,
          Text = "Xuất báo cáo",
          Cursor = Cursors.Hand,
        };
        exportGroupButton.FlatAppearance.BorderSize = 0;
        var detailGrid = CreateGroupDetailGrid(groupRecords);
        detailGrid.Dock = DockStyle.Fill;
        detailGrid.Visible = false;

        void ToggleGroup(object? sender, EventArgs e)
        {
          var expanded = !(bool)groupPanel.Tag;
          groupPanel.Tag = expanded;
          detailGrid.Visible = expanded;
          header.Text = expanded ? $"  ▼    {header.Tag}" : $"  ▶    {header.Tag}";
          groupPanel.Height = expanded ? 54 + detailGrid.ColumnHeadersHeight +
            detailGrid.RowTemplate.Height * groupRecords.Count + 2 : 54;
        }

        header.Click += ToggleGroup;
        exportGroupButton.Click += async (_, _) =>
        {
          using var buttonLock = ButtonExecutionScope.Enter(exportGroupButton);
          var selectedRecords = detailGrid.Rows
            .Cast<DataGridViewRow>()
            .Where(row => Convert.ToBoolean(row.Cells["Selected"].Value ?? false))
            .Select(row => row.DataBoundItem as RecordWeightDTO)
            .Where(record => record != null)
            .Cast<RecordWeightDTO>()
            .ToList();

          if (selectedRecords.Count == 0)
          {
            using var popup = new PopupConfirm(
              "Vui lòng chọn ít nhất một dữ liệu cần xuất báo cáo.",
              EnumTypeMsg.MessageManualClose,
              EnumImageMsg.Warning);
            popup.ShowDialog(this);
            return;
          }

          ExportGroupRequested?.Invoke(group.Key, selectedRecords);
          await ExportRecordsAsync(selectedRecords, group.Key);
        };

        headerPanel.Controls.Add(header);
        headerPanel.Controls.Add(exportGroupButton);
        groupPanel.Controls.Add(detailGrid);
        groupPanel.Controls.Add(headerPanel);
        _licensePlateGroups.Controls.Add(groupPanel);
      }

      ResizeLicensePlateGroupPanels();
      _licensePlateGroups.ResumeLayout();
    }

    private DataGridView CreateGroupDetailGrid(List<RecordWeightDTO> records)
    {
      var grid = new DataGridView
      {
        BindingContext = new BindingContext(),
        AllowUserToAddRows = false,
        AllowUserToDeleteRows = false,
        AllowUserToResizeColumns = false,
        AllowUserToResizeRows = false,
        AutoGenerateColumns = true,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
        AutoSizeRowsMode = dgv.AutoSizeRowsMode,
        AlternatingRowsDefaultCellStyle = dgv.AlternatingRowsDefaultCellStyle.Clone(),
        BackgroundColor = dgv.BackgroundColor,
        BorderStyle = dgv.BorderStyle,
        CellBorderStyle = dgv.CellBorderStyle,
        ColumnHeadersDefaultCellStyle = dgv.ColumnHeadersDefaultCellStyle.Clone(),
        ColumnHeadersBorderStyle = dgv.ColumnHeadersBorderStyle,
        ColumnHeadersHeight = dgv.ColumnHeadersHeight,
        ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
        DefaultCellStyle = dgv.DefaultCellStyle.Clone(),
        EnableHeadersVisualStyles = false,
        Font = dgv.Font,
        ForeColor = dgv.ForeColor,
        GridColor = dgv.GridColor,
        MultiSelect = false,
        ReadOnly = true,
        RowHeadersBorderStyle = dgv.RowHeadersBorderStyle,
        RowHeadersDefaultCellStyle = dgv.RowHeadersDefaultCellStyle.Clone(),
        RowHeadersVisible = false,
        RowsDefaultCellStyle = dgv.RowsDefaultCellStyle.Clone(),
        ScrollBars = ScrollBars.None,
        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
      };
      grid.RowTemplate.Height = dgv.RowTemplate.Height;
      grid.DataSource = records;
      foreach (DataGridViewRow row in grid.Rows)
        row.Height = dgv.RowTemplate.Height;
      ApplyGridColumnFormatting(grid);
      foreach (DataGridViewColumn column in grid.Columns)
        column.ReadOnly = true;

      var selectedColumn = new DataGridViewCheckBoxColumn
      {
        Name = "Selected",
        HeaderText = "Chọn",
        Width = 60,
        MinimumWidth = 60,
        AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
        ReadOnly = true,
        FalseValue = false,
        TrueValue = true,
      };
      grid.Columns.Insert(0, selectedColumn);
      ConfigureGroupGridColumnWidths(grid);
      grid.CellClick += (_, e) =>
      {
        if (e.RowIndex < 0 || e.ColumnIndex != selectedColumn.Index)
          return;

        var cell = grid.Rows[e.RowIndex].Cells[selectedColumn.Index];
        cell.Value = !Convert.ToBoolean(cell.Value ?? false);
        grid.ClearSelection();
        grid.CurrentCell = null;
      };
      return grid;
    }

    private static void ConfigureGroupGridColumnWidths(DataGridView grid)
    {
      foreach (DataGridViewColumn column in grid.Columns)
        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

      var selectedColumn = FindGridColumn(grid, "Selected");
      if (selectedColumn != null)
      {
        selectedColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        selectedColumn.Width = 60;
      }

      var autoSizeColumns = new[]
      {
        nameof(RecordWeightDTO.No),
        nameof(RecordWeightDTO.Datetime),
        nameof(RecordWeightDTO.LicensePlate),
        nameof(RecordWeightDTO.ProductGroup),
        nameof(RecordWeightDTO.CategoryTare),
        nameof(RecordWeightDTO.Net),
        nameof(RecordWeightDTO.Gross),
        nameof(RecordWeightDTO.Tare),
      };

      foreach (var columnName in autoSizeColumns)
      {
        var column = FindGridColumn(grid, columnName);
        if (column != null)
          column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      }
    }

    private static DataGridViewColumn? FindGridColumn(DataGridView grid, string columnName)
    {
      return grid.Columns
        .Cast<DataGridViewColumn>()
        .FirstOrDefault(column =>
          string.Equals(column.Name, columnName, StringComparison.Ordinal) ||
          string.Equals(column.DataPropertyName, columnName, StringComparison.Ordinal));
    }

    private void ResizeLicensePlateGroupPanels()
    {
      var width = Math.Max(100, _licensePlateGroups.ClientSize.Width -
        _licensePlateGroups.Padding.Horizontal -
        (_licensePlateGroups.VerticalScroll.Visible ? SystemInformation.VerticalScrollBarWidth : 0));
      foreach (Control control in _licensePlateGroups.Controls)
        control.Width = width;
    }

    private async void btnExport_Click(object sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
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

        if (exportRecords.Count == 0)
        {
          using var popup = new PopupConfirm(
            "Không có dữ liệu phù hợp với điều kiện lọc để xuất báo cáo.",
            EnumTypeMsg.MessageManualClose,
            EnumImageMsg.Information);
          popup.ShowDialog(this);
          return;
        }

        var exportData = DTOHelper.ConvertRecordWeightDTO(exportRecords)
          .OrderBy(record => record.RecordWeight?.CreatedAt)
          .ThenBy(record => record.RecordWeight?.Id)
          .ToList();

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

    private async Task ExportRecordsAsync(
      IReadOnlyList<RecordWeightDTO> records,
      string licensePlate)
    {
      int licensePlateCount = records
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

      int productGroupNameCount = records
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

      List<RecordWeight> exportData = records
        .Select(dto => dto.RecordWeight!)
        .ToList();

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
