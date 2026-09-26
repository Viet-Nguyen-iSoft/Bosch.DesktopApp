using Common;
using HelperManager;
using iSoft.Database;
using iSoft.Database.DTO;
using LTP.Truck.Controls;
using LTP.Truck.Custom;
using LTP.Truck.MasterData;
using LTP.Truck.Popup;
using static Common.EnumData;

namespace LTP.Truck.Forms
{
  public partial class FrmUser : Form
  {
    private const string EditButtonColumnName = "btnEdit";
    private const string DeleteButtonColumnName = "btnDelete";
    private const string ChangePasswordButtonColumnName = "btnChangePassword";
    private const string RolesButtonColumnName = "btnRoles";
    private CancellationTokenSource? _searchDebounceCancellation;
    private int _loadVersion;

    public FrmUser()
    {
      InitializeComponent();
      CustomUI();

      btnSearch.Click += btnSearch_Click;
      btnAddnew.Click += btnAddnew_Click;
      txtSearch._TextChanged += txtSearch_TextChanged;
      dgv.CellContentClick += dgv_CellContentClick;
      ucPage1.PageChanged += ucPage1_PageChanged;
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

    public async Task LoadData(bool resetPage = true)
    {
      int loadVersion = Interlocked.Increment(ref _loadVersion);

      try
      {
        btnSearch.Enabled = false;
        ucPage1.Enabled = false;

        if (resetPage)
          ucPage1.ResetToFirstPage();

        var users = await AppCore.Ins._userService.GetAllAsync();
        var userDtos = DTOHelper.ConvertUserDTO(users);
        var filteredUsers = TextSearchHelper.FilterBrowsableProperties(
          userDtos,
          txtSearch.Texts.Trim());

        if (loadVersion == _loadVersion)
        {
          var totalRecords = filteredUsers.Count;
          var pageSize = ucPage1.PageSize;
          var totalPages = Math.Max(
            1,
            (int)Math.Ceiling(totalRecords / (double)pageSize));
          var effectivePage = Math.Min(ucPage1.CurrentPage, totalPages);
          var pagedUsers = filteredUsers
            .Skip((effectivePage - 1) * pageSize)
            .Take(pageSize)
            .ToList();

          ucPage1.SetTotalRecords(totalRecords, effectivePage);
          SetDgv(pagedUsers);
        }
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
        {
          btnSearch.Enabled = true;
          ucPage1.Enabled = true;
        }
      }
    }

    private async void ucPage1_PageChanged(
      object? sender,
      UserControls.PageChangedEventArgs e)
    {
      await LoadData(resetPage: false);
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
      EnsureChangePasswordColumn();

      if (dgv.Columns.Contains(nameof(UserDTO.User)))
        dgv.Columns[nameof(UserDTO.User)].Visible = false;

      var autoSizeColumns = new[]
      {
        nameof(UserDTO.No),
        nameof(UserDTO.EmployeeCode),
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

    private void EnsureChangePasswordColumn()
    {
      if (!dgv.Columns.Contains(ChangePasswordButtonColumnName))
      {
        dgv.Columns.Add(new DataGridViewButtonColumn
        {
          Name = ChangePasswordButtonColumnName,
          HeaderText = string.Empty,
          Text = "Đổi mật khẩu",
          UseColumnTextForButtonValue = true,
          AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
          Width = 160,
          Resizable = DataGridViewTriState.False,
          SortMode = DataGridViewColumnSortMode.NotSortable,
        });
      }

      if (!dgv.Columns.Contains(RolesButtonColumnName))
      {
        dgv.Columns.Add(new DataGridViewButtonColumn
        {
          Name = RolesButtonColumnName,
          HeaderText = string.Empty,
          Text = "Phân quyền",
          UseColumnTextForButtonValue = true,
          AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
          Width = 140,
          Resizable = DataGridViewTriState.False,
          SortMode = DataGridViewColumnSortMode.NotSortable,
        });
      }

      if (!dgv.Columns.Contains(EditButtonColumnName))
      {
        dgv.Columns.Add(new DataGridViewButtonColumn
        {
          Name = EditButtonColumnName,
          HeaderText = string.Empty,
          Text = "Chỉnh sửa",
          UseColumnTextForButtonValue = true,
          AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
          Width = 130,
          Resizable = DataGridViewTriState.False,
          SortMode = DataGridViewColumnSortMode.NotSortable,
        });
      }

      if (!dgv.Columns.Contains(DeleteButtonColumnName))
      {
        dgv.Columns.Add(new DataGridViewButtonColumn
        {
          Name = DeleteButtonColumnName,
          HeaderText = string.Empty,
          Text = "Xóa",
          UseColumnTextForButtonValue = true,
          AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
          Width = 100,
          Resizable = DataGridViewTriState.False,
          SortMode = DataGridViewColumnSortMode.NotSortable,
        });
      }

      // Đặt từ cột cuối ngược về trước để DataGridView không dịch
      // một cột dữ liệu chen vào giữa các cột thao tác.
      dgv.Columns[ChangePasswordButtonColumnName].DisplayIndex =
        dgv.Columns.Count - 1;
      dgv.Columns[DeleteButtonColumnName].DisplayIndex = dgv.Columns.Count - 2;
      dgv.Columns[EditButtonColumnName].DisplayIndex = dgv.Columns.Count - 3;
      dgv.Columns[RolesButtonColumnName].DisplayIndex = dgv.Columns.Count - 4;
    }

    private async void dgv_CellContentClick(
      object? sender,
      DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0 ||
          e.ColumnIndex < 0 ||
          dgv.Rows[e.RowIndex].DataBoundItem is not UserDTO row ||
          row.User == null)
      {
        return;
      }

      string columnName = dgv.Columns[e.ColumnIndex].Name;
      try
      {
        dgv.Enabled = false;

        if (columnName == EditButtonColumnName)
          await EditUserAsync(row.User);
        else if (columnName == DeleteButtonColumnName)
          await DeleteUserAsync(row.User);
        else if (columnName == ChangePasswordButtonColumnName)
          await ChangePasswordAsync(row.User);
        else if (columnName == RolesButtonColumnName)
          await EditRolesAsync(row.User);
      }
      finally
      {
        dgv.Enabled = true;
      }
    }

    private async Task EditUserAsync(iSoft.Database.Models.User user)
    {
      bool isUpdated = false;
      using var popup = new PopupUser(user);

      popup.OnSendSuccess += _ => isUpdated = true;
      popup.ShowDialog(this);

      if (!isUpdated)
        return;

      await LoadData(resetPage: false);
      ShowSuccess("Cập nhật tài khoản thành công.");
    }

    private async Task DeleteUserAsync(iSoft.Database.Models.User user)
    {
      bool isConfirmed = false;
      using (var confirmPopup = new PopupConfirm(
        $"Bạn có chắc chắn muốn xóa tài khoản {user.Username} không?",
        EnumTypeMsg.Confirm,
        EnumImageMsg.Warning,
        user))
      {
        confirmPopup.OnSendConfirm += (_, _) => isConfirmed = true;
        confirmPopup.ShowDialog(this);
      }

      if (!isConfirmed)
        return;

      try
      {
        await AppCore.Ins._userService.DeleteAsync(user);
        await LoadData(resetPage: false);
        ShowSuccess("Xóa tài khoản thành công.");
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        using var errorPopup = new PopupConfirm(
          "Xóa tài khoản thất bại !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        errorPopup.ShowDialog(this);
      }
    }

    private async Task ChangePasswordAsync(iSoft.Database.Models.User user)
    {
      bool isUpdated = false;
      using var popup = new PopupUser(user, isChangePassword: true);

      popup.OnSendSuccess += _ => isUpdated = true;
      popup.ShowDialog(this);

      if (!isUpdated)
        return;

      await LoadData(resetPage: false);
      ShowSuccess("Cập nhật mật khẩu thành công.");
    }

    private async Task EditRolesAsync(iSoft.Database.Models.User user)
    {
      bool isUpdated = false;
      using var popup = new PopupRoles(user);

      popup.OnSendSuccess += _ => isUpdated = true;
      popup.ShowDialog(this);

      if (!isUpdated)
        return;

      await LoadData(resetPage: false);
      ShowSuccess("Cập nhật phân quyền thành công.");
    }

    private void ShowSuccess(string message)
    {
      using var successPopup = new PopupConfirm(
        message,
        EnumTypeMsg.MessageAutoClose,
        EnumImageMsg.Information);
      successPopup.ShowDialog(this);
    }

    private async void btnSearch_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      await LoadData(resetPage: true);
    }

    private async void btnAddnew_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      bool isAdded = false;
      using var popup = new PopupUser();

      popup.OnSendSuccess += _ => isAdded = true;
      popup.ShowDialog(this);

      if (!isAdded)
        return;

      await LoadData(resetPage: true);
      ShowSuccess("Thêm tài khoản thành công.");
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
        await LoadData(resetPage: true);
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
