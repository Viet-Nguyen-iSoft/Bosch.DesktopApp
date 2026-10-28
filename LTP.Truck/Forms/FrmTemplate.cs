using Common;
using HelperManager;
using iSoft.Database.DTO;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using LTP.Truck.Custom;
using LTP.Truck.MasterData;
using static Common.EnumData;

namespace LTP.Truck.Forms
{
  public partial class FrmTemplate : Form
  {
    private const string DetailButtonColumnName = "btnDetail";
    private readonly TemplateService _templateService = new();
    private int _loadVersion;

    public FrmTemplate()
    {
      InitializeComponent();
      SetStyle(
        ControlStyles.OptimizedDoubleBuffer |
        ControlStyles.AllPaintingInWmPaint,
        true);
      CustomUI();

      btnSearch.Click += btnSearch_Click;
      dgv.CellContentClick += dgv_CellContentClick;
    }

    #region Instance
    private static FrmTemplate? _instance;
    public static FrmTemplate Instance => _instance ??= new FrmTemplate();
    #endregion

    private void CustomUI()
    {
      _ = new ElipseControl
      {
        TargetControl = this,
        CornerRadius = 20,
      };
      _ = new ElipseControl
      {
        TargetControl = tableLayoutPanel7,
        CornerRadius = 20,
      };

      dgv.EnableHeadersVisualStyles = false;
      dgv.ColumnHeadersHeight = 50;
      dgv.ColumnHeadersHeightSizeMode =
        DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
      dgv.RowTemplate.Height = 60;
      dgv.BorderStyle = BorderStyle.None;
      dgv.MultiSelect = false;
      dgv.DefaultCellStyle.SelectionBackColor = dgv.DefaultCellStyle.BackColor;
      dgv.DefaultCellStyle.SelectionForeColor = dgv.DefaultCellStyle.ForeColor;
    }

    public async Task LoadData()
    {
      int loadVersion = Interlocked.Increment(ref _loadVersion);
      try
      {
        btnSearch.Enabled = false;
        var templates = await _templateService.GetAllAsync();
        var rows = templates.Select((template, index) => new TemplateDTO
        {
          Template = template,
          No = index + 1,
          SerialCode = template.SerialCode,
          Name = template.Name,
          PathLocal = template.PathLocal,
          Order = template.Order,
          Type = template.IsBBGN ? "BBGN" : "Khác",
          UpdatedAt = FormatDateTime(template.UpdatedAt),
        }).ToList();

        if (loadVersion == _loadVersion)
          SetDgv(rows);
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        if (loadVersion == _loadVersion)
        {
          using var popup = new PopupConfirm(
            "Không thể tải danh sách template. Vui lòng thử lại !",
            EnumTypeMsg.MessageManualClose,
            EnumImageMsg.Warning);
          popup.ShowDialog(this);
        }
      }
      finally
      {
        if (loadVersion == _loadVersion && !btnSearch.IsDisposed)
          btnSearch.Enabled = true;
      }
    }

    private void SetDgv(List<TemplateDTO> templates)
    {
      if (InvokeRequired)
      {
        BeginInvoke(new Action(() => SetDgv(templates)));
        return;
      }

      dgv.DataSource = null;
      dgv.DataSource = templates;
      EnsureDetailColumn();

      if (dgv.Columns.Contains(nameof(TemplateDTO.Template)))
        dgv.Columns[nameof(TemplateDTO.Template)].Visible = false;

      foreach (string columnName in new[]
      {
        nameof(TemplateDTO.SerialCode),
        nameof(TemplateDTO.Order),
        nameof(TemplateDTO.Type),
      })
      {
        if (dgv.Columns.Contains(columnName))
          dgv.Columns[columnName].Visible = false;
      }

      foreach (string columnName in new[]
      {
        nameof(TemplateDTO.No),
        nameof(TemplateDTO.UpdatedAt),
      })
      {
        if (dgv.Columns.Contains(columnName))
          dgv.Columns[columnName].AutoSizeMode =
            DataGridViewAutoSizeColumnMode.DisplayedCells;
      }

      foreach (string columnName in new[]
      {
        nameof(TemplateDTO.No),
      })
      {
        if (dgv.Columns.Contains(columnName))
          dgv.Columns[columnName].DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleCenter;
      }

      dgv.ClearSelection();
      dgv.CurrentCell = null;
    }

    private void EnsureDetailColumn()
    {
      if (!dgv.Columns.Contains(DetailButtonColumnName))
      {
        dgv.Columns.Add(new DataGridViewButtonColumn
        {
          Name = DetailButtonColumnName,
          HeaderText = string.Empty,
          Text = "Chi tiết",
          UseColumnTextForButtonValue = true,
          AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
          Width = 130,
          Resizable = DataGridViewTriState.False,
          SortMode = DataGridViewColumnSortMode.NotSortable,
        });
      }

      dgv.Columns[DetailButtonColumnName].DisplayIndex = dgv.Columns.Count - 1;
    }

    private async void btnSearch_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      await LoadData();
    }

    private async void dgv_CellContentClick(
      object? sender,
      DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
          dgv.Columns[e.ColumnIndex].Name != DetailButtonColumnName ||
          dgv.Rows[e.RowIndex].DataBoundItem is not TemplateDTO row ||
          row.Template == null)
      {
        return;
      }

      using var popup = new PopupTemplate(row.Template);
      popup.ShowDialog(this);
      if (popup.DialogResult == DialogResult.OK)
        await LoadData();
    }

    private static string? FormatDateTime(DateTime? value)
    {
      if (!value.HasValue)
        return null;

      DateTime localValue = value.Value.Kind == DateTimeKind.Utc
        ? value.Value.ToLocalTime()
        : value.Value;
      return localValue.ToString("dd-MM-yyyy HH:mm:ss");
    }
  }
}
