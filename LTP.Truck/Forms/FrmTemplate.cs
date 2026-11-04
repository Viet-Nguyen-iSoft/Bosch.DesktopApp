using ApiSyncData;
using Common;
using HelperManager;
using iSoft.Database.DTO;
using iSoft.Database.Service;
using System.Diagnostics;
using LTP.Truck.Controls;
using LTP.Truck.Custom;
using LTP.Truck.MasterData;
using static Common.EnumData;

namespace LTP.Truck.Forms
{
  public partial class FrmTemplate : Form
  {
    private const string DetailButtonColumnName = "btnDetail";
    private const string DownloadButtonColumnName = "btnDownload";
    private readonly ApiService _apiService = new();
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
      MasterDataChangeNotifier.Changed += MasterDataChangeNotifier_Changed;
      Disposed += (_, _) =>
        MasterDataChangeNotifier.Changed -= MasterDataChangeNotifier_Changed;
    }

    private void MasterDataChangeNotifier_Changed(object? sender, Type entityType)
    {
      if (entityType != typeof(iSoft.Database.Models.Template) ||
          IsDisposed || Disposing)
      {
        return;
      }

      if (InvokeRequired)
      {
        BeginInvoke(new Action(() => MasterDataChangeNotifier_Changed(
          sender,
          entityType)));
        return;
      }

      if (Visible)
        _ = LoadData();
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
          Name = template.Name,
          PathLocal = string.IsNullOrWhiteSpace(template.PathLocal)
            ? null
            : Path.GetFileName(template.PathLocal),
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
      EnsureActionColumns();

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

    private void EnsureActionColumns()
    {
      if (!dgv.Columns.Contains(DownloadButtonColumnName))
      {
        dgv.Columns.Add(new DataGridViewButtonColumn
        {
          Name = DownloadButtonColumnName,
          HeaderText = string.Empty,
          Text = "Tải xuống",
          UseColumnTextForButtonValue = true,
          AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
          Width = 130,
          Resizable = DataGridViewTriState.False,
          SortMode = DataGridViewColumnSortMode.NotSortable,
        });
      }

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

      // Cố định thứ tự theo các cột đang hiển thị. Không tính vị trí dựa trên
      // Columns.Count vì tập cột có thể chứa thêm cột ẩn hoặc cột tạo tự động.
      dgv.Columns[nameof(TemplateDTO.No)].DisplayIndex = 0;
      dgv.Columns[nameof(TemplateDTO.Name)].DisplayIndex = 1;
      dgv.Columns[nameof(TemplateDTO.PathLocal)].DisplayIndex = 2;
      dgv.Columns[nameof(TemplateDTO.UpdatedAt)].DisplayIndex = 3;
      dgv.Columns[DownloadButtonColumnName].DisplayIndex = 4;
      dgv.Columns[DetailButtonColumnName].DisplayIndex = 5;
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
          dgv.Rows[e.RowIndex].DataBoundItem is not TemplateDTO row ||
          row.Template == null)
      {
        return;
      }

      string columnName = dgv.Columns[e.ColumnIndex].Name;
      if (columnName == DownloadButtonColumnName)
      {
        await DownloadTemplateAsync(row.Template);
      }
      else if (columnName == DetailButtonColumnName)
      {
        using var popup = new PopupTemplate(row.Template);
        popup.ShowDialog(this);
        if (popup.DialogResult == DialogResult.OK)
          await LoadData();
      }
    }

    private async Task DownloadTemplateAsync(
      iSoft.Database.Models.Template template)
    {
      string initialFolder = Path.GetDirectoryName(template.PathLocal ?? string.Empty)
        ?? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      if (!Directory.Exists(initialFolder))
      {
        initialFolder = Environment.GetFolderPath(
          Environment.SpecialFolder.MyDocuments);
      }

      using var folderDialog = new FolderBrowserDialog
      {
        Description = "Chọn thư mục lưu file template",
        InitialDirectory = initialFolder,
        UseDescriptionForTitle = true,
        ShowNewFolderButton = true,
      };
      if (folderDialog.ShowDialog(this) != DialogResult.OK ||
          string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
      {
        return;
      }

      try
      {
        dgv.Enabled = false;
        UseWaitCursor = true;

        string pathLocal = await _apiService.DownloadTemplateAsync(
          template.Id,
          template.Name,
          template.TemplateFile,
          folderDialog.SelectedPath);
        await _templateService.UpdatePathLocalAsync(template.Id, pathLocal);
        template.PathLocal = pathLocal;
        await LoadData();
        UseWaitCursor = false;

        bool openDownloadedFile = false;
        using (var successPopup = new PopupConfirm(
          $"Tải file {Path.GetFileName(pathLocal)} thành công. " +
          "Bạn có muốn mở file không?",
          EnumTypeMsg.Confirm,
          EnumImageMsg.Question))
        {
          successPopup.OnSendConfirm += (_, response) =>
            openDownloadedFile =
              response.EnumResponsible == EnumResponsible.Confirm;
          successPopup.ShowDialog(this);
        }

        if (openDownloadedFile)
        {
          try
          {
            Process.Start(new ProcessStartInfo
            {
              FileName = pathLocal,
              UseShellExecute = true,
            });
          }
          catch (Exception openException)
          {
            LogHelper.LogErrorToFileLog(
              openException,
              AppCore.Ins._folderFileLog);
            using var openErrorPopup = new PopupConfirm(
              "Đã tải file nhưng không thể mở file.",
              EnumTypeMsg.MessageManualClose,
              EnumImageMsg.Warning);
            openErrorPopup.ShowDialog(this);
          }
        }
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        using var errorPopup = new PopupConfirm(
          "Không thể tải file template từ server !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        errorPopup.ShowDialog(this);
      }
      finally
      {
        if (!dgv.IsDisposed)
          dgv.Enabled = true;
        UseWaitCursor = false;
      }
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
