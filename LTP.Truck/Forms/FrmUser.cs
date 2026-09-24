using Common;
using HelperManager;
using iSoft.Database;
using iSoft.Database.DTO;
using LTP.Truck.Controls;
using LTP.Truck.Custom;
using static Common.EnumData;

namespace LTP.Truck.Forms
{
  public partial class FrmUser : Form
  {
    private CancellationTokenSource? _searchDebounceCancellation;
    private int _loadVersion;

    public FrmUser()
    {
      InitializeComponent();
      CustomUI();

      btnSearch.Click += btnSearch_Click;
      txtSearch._TextChanged += txtSearch_TextChanged;
    }

    #region Instance
    private static FrmUser _Instance = null;
    public static FrmUser Instance
    {
      get
      {
        if (_Instance == null) _Instance = new FrmUser();
        return _Instance;
      }
    }
    #endregion

    private void CustomUI()
    {
      var formElipse = new ElipseControl
      {
        TargetControl = this,
        CornerRadius = 20,
      };

      var contentElipse = new ElipseControl
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
      dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor =
        dgv.ColumnHeadersDefaultCellStyle.BackColor;
      dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor =
        dgv.ColumnHeadersDefaultCellStyle.ForeColor;
      dgv.RowHeadersDefaultCellStyle.SelectionBackColor =
        dgv.RowHeadersDefaultCellStyle.BackColor;
      dgv.RowHeadersDefaultCellStyle.SelectionForeColor =
        dgv.RowHeadersDefaultCellStyle.ForeColor;
    }

    public async Task LoadData()
    {
      int loadVersion = Interlocked.Increment(ref _loadVersion);

      try
      {
        btnSearch.Enabled = false;

        var users = await AppCore.Ins._userService.GetAllAsync();
        var userDtos = DTOHelper.ConvertUserDTO(users);
        var filteredUsers = TextSearchHelper.FilterBrowsableProperties(
          userDtos,
          txtSearch.Texts.Trim());

        if (loadVersion == _loadVersion)
          SetDgv(filteredUsers);
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);

        if (loadVersion == _loadVersion)
        {
          var popup = new PopupConfirm(
            "Không thể tải danh sách tài khoản. Vui lòng thử lại !",
            EnumTypeMsg.MessageManualClose,
            EnumImageMsg.Warning);
          popup.ShowDialog();
        }
      }
      finally
      {
        if (loadVersion == _loadVersion && !btnSearch.IsDisposed)
          btnSearch.Enabled = true;
      }
    }

    private void SetDgv(List<UserDTO> users)
    {
      if (InvokeRequired)
      {
        BeginInvoke(new Action(() => SetDgv(users)));
        return;
      }

      dgv.DataSource = null;
      dgv.DataSource = users;

      if (dgv.Columns.Contains(nameof(UserDTO.User)))
        dgv.Columns[nameof(UserDTO.User)].Visible = false;

      var autoSizeColumns = new[]
      {
        nameof(UserDTO.No),
        nameof(UserDTO.Username),
        nameof(UserDTO.EmployeeCode),
        nameof(UserDTO.IdCardCode),
        nameof(UserDTO.UpdatedAt),
      };

      foreach (string columnName in autoSizeColumns)
      {
        if (dgv.Columns.Contains(columnName))
          dgv.Columns[columnName].AutoSizeMode =
            DataGridViewAutoSizeColumnMode.AllCells;
      }

      if (dgv.Columns.Contains(nameof(UserDTO.No)))
        dgv.Columns[nameof(UserDTO.No)].DefaultCellStyle.Alignment =
          DataGridViewContentAlignment.MiddleCenter;

      if (dgv.Columns.Contains(nameof(UserDTO.UpdatedAt)))
        dgv.Columns[nameof(UserDTO.UpdatedAt)].DefaultCellStyle.Alignment =
          DataGridViewContentAlignment.MiddleRight;

      dgv.ClearSelection();
      dgv.CurrentCell = null;
    }

    private async void btnSearch_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      await LoadData();
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
        await LoadData();
      }
      catch (OperationCanceledException)
      {
        // Chờ lần thay đổi mới nhất khi người dùng vẫn đang nhập.
      }
      finally
      {
        if (ReferenceEquals(_searchDebounceCancellation, cancellation))
        {
          cancellation.Dispose();
          _searchDebounceCancellation = null;
        }
      }
    }
  }
}
