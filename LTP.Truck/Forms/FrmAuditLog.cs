using Common;
using HelperManager;
using iSoft.Database.DTO;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Custom;
using static Common.EnumData;

namespace LTP.Truck.Forms
{
  public partial class FrmAuditLog : Form
  {
    private readonly AuditLogService _auditLogService = new();
    private int _loadVersion;

    private static FrmAuditLog? _instance;
    public static FrmAuditLog Instance => _instance ??= new FrmAuditLog();

    public FrmAuditLog()
    {
      InitializeComponent();
      SetStyle(ControlStyles.OptimizedDoubleBuffer |
        ControlStyles.AllPaintingInWmPaint, true);
      ConfigureUi();
      btnSearchHistorical.Click += btnSearchHistorical_Click;
      ucPage1.PageChanged += ucPage1_PageChanged;
    }

    private void ConfigureUi()
    {
      ucTimeSearchFrom.Value = DateTime.Today.AddDays(-7);
      ucTimeSearchTo.Value = DateTime.Today.AddDays(1).AddMinutes(-1);
      cbbType.DropDownStyle = ComboBoxStyle.DropDownList;
      cbbType.DataSource = new List<AuditActionOption>
      {
        new("Tất cả thao tác", null),
        new("Thêm mới", AuditActions.Create),
        new("Cập nhật", AuditActions.Update),
        new("Xóa", AuditActions.Delete),
        new("Khôi phục", AuditActions.Restore),
        new("Import", AuditActions.Import),
        new("Thao tác không hợp lệ", AuditActions.InvalidOperation),
        new("Lỗi", AuditActions.Error),
        new("Đăng nhập thành công", AuditActions.LoginSuccess),
        new("Đăng nhập thất bại", AuditActions.LoginFailed),
        new("Đăng xuất", AuditActions.Logout)
      };
      cbbType.DisplayMember = nameof(AuditActionOption.Text);

      dgv.EnableHeadersVisualStyles = false;
      dgv.ColumnHeadersHeight = 50;
      dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
      dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
      dgv.ScrollBars = ScrollBars.Both;
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

    public async Task LoadData(bool resetPage = true)
    {
      var loadVersion = Interlocked.Increment(ref _loadVersion);
      try
      {
        btnSearchHistorical.Enabled = false;
        ucPage1.Enabled = false;
        if (resetPage)
          ucPage1.ResetToFirstPage();

        var from = ucTimeSearchFrom.Value;
        var to = ucTimeSearchTo.Value;
        if (from > to)
        {
          ShowWarning("Thời gian bắt đầu không được lớn hơn thời gian kết thúc.");
          return;
        }

        // Bộ chọn thời gian chỉ có độ chính xác đến phút, nên lấy trọn phút kết thúc.
        var toInclusive = to.AddMinutes(1).AddTicks(-1);
        var action = (cbbType.SelectedItem as AuditActionOption)?.Value;
        var result = await _auditLogService.SearchAsync(
          from.ToUniversalTime(), toInclusive.ToUniversalTime(), action,
          ucPage1.CurrentPage, ucPage1.PageSize);

        if (loadVersion != _loadVersion)
          return;

        var totalPages = Math.Max(1,
          (int)Math.Ceiling(result.TotalRecords / (double)ucPage1.PageSize));
        var effectivePage = Math.Min(ucPage1.CurrentPage, totalPages);
        if (effectivePage != ucPage1.CurrentPage)
        {
          result = await _auditLogService.SearchAsync(
            from.ToUniversalTime(), toInclusive.ToUniversalTime(), action,
            effectivePage, ucPage1.PageSize);
        }

        ucPage1.SetTotalRecords(result.TotalRecords, effectivePage);
        dgv.DataSource = result.Items.Select(AuditLogDTO.FromModel).ToList();
        ConfigureGridColumns();
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(
          ex, LTP.Truck.Controls.AppCore.Ins._folderFileLog);
        ShowWarning("Không thể tải dữ liệu Audit Log. Vui lòng thử lại!");
      }
      finally
      {
        if (loadVersion == _loadVersion && !IsDisposed)
        {
          btnSearchHistorical.Enabled = true;
          ucPage1.Enabled = true;
        }
      }
    }

    private void ConfigureGridColumns()
    {
      SetColumnWidth(nameof(AuditLogDTO.OccurredAt), 180);
      SetColumnWidth(nameof(AuditLogDTO.Action), 190);
      SetColumnWidth(nameof(AuditLogDTO.EntityType), 170);
      if (dgv.Columns.Contains(nameof(AuditLogDTO.EntityId)))
        dgv.Columns[nameof(AuditLogDTO.EntityId)].Visible = false;
      SetColumnWidth(nameof(AuditLogDTO.EntityName), 220);
      SetColumnWidth(nameof(AuditLogDTO.Username), 160);
      SetColumnWidth(nameof(AuditLogDTO.UserDisplayName), 220);
      SetColumnWidth(nameof(AuditLogDTO.Source), 180);
      SetColumnWidth(nameof(AuditLogDTO.Reason), 300);
      SetColumnWidth(nameof(AuditLogDTO.Result), 130);
      SetColumnWidth(nameof(AuditLogDTO.BeforeData), 420);
      SetColumnWidth(nameof(AuditLogDTO.AfterData), 420);
      SetColumnWidth(nameof(AuditLogDTO.ErrorMessage), 420);

      if (dgv.Columns.Contains(nameof(AuditLogDTO.OccurredAt)))
        dgv.Columns[nameof(AuditLogDTO.OccurredAt)].DefaultCellStyle.Format =
          "dd/MM/yyyy HH:mm:ss";
    }

    private void SetColumnWidth(string columnName, int width)
    {
      if (!dgv.Columns.Contains(columnName))
        return;

      dgv.Columns[columnName].AutoSizeMode =
        DataGridViewAutoSizeColumnMode.None;
      dgv.Columns[columnName].Width = width;
    }

    private async void btnSearchHistorical_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      await LoadData();
    }

    private async void ucPage1_PageChanged(
      object? sender, UserControls.PageChangedEventArgs e)
    {
      await LoadData(resetPage: false);
    }

    private void ShowWarning(string message)
    {
      using var popup = new PopupConfirm(
        message, EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
      popup.ShowDialog(this);
    }

    private sealed record AuditActionOption(string Text, string? Value);
  }
}
