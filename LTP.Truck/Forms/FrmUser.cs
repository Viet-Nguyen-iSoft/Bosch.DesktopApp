using Common;
using ApiSyncData.Req;
using ClosedXML.Excel;
using HelperManager;
using iSoft.Database;
using iSoft.Database.DTO;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using LTP.Truck.Custom;
using LTP.Truck.MasterData;
using LTP.Truck.Popup;
using LTP.Truck.Services;
using static Common.EnumData;
using static HelperManager.EnumData;

namespace LTP.Truck.Forms
{
  public partial class FrmUser : Form
  {
    private const string EditButtonColumnName = "btnEdit";
    private const string DeleteButtonColumnName = "btnDelete";
    private const string RolesButtonColumnName = "btnRoles";
    private CancellationTokenSource? _searchDebounceCancellation;
    private int _loadVersion;

    public FrmUser()
    {
      InitializeComponent();
      SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
      CustomUI();

      btnSearch.Click += btnSearch_Click;
      btnAddnew.Click += btnAddnew_Click;
      btnImport.Click += btnImport_Click;
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
      EnsureActionColumns();

      if (dgv.Columns.Contains(nameof(UserDTO.User)))
        dgv.Columns[nameof(UserDTO.User)].Visible = false;

      var autoSizeColumns = new[]
      {
        nameof(UserDTO.No),
        nameof(UserDTO.EmployeeCode),
        nameof(UserDTO.FullName),
        nameof(UserDTO.UpdatedAt),
      };

      foreach (string columnName in autoSizeColumns)
      {
        if (dgv.Columns.Contains(columnName))
          dgv.Columns[columnName].AutoSizeMode =
            DataGridViewAutoSizeColumnMode.DisplayedCells;
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

    private void EnsureActionColumns()
    {
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
      dgv.Columns[DeleteButtonColumnName].DisplayIndex = dgv.Columns.Count - 1;
      dgv.Columns[EditButtonColumnName].DisplayIndex = dgv.Columns.Count - 2;
      dgv.Columns[RolesButtonColumnName].DisplayIndex = dgv.Columns.Count - 3;
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

      try
      {
        string columnName = dgv.Columns[e.ColumnIndex].Name;
        if (columnName == EditButtonColumnName)
          await EditUserAsync(row.User);
        else if (columnName == DeleteButtonColumnName)
          await DeleteUserAsync(row.User);
        else if (columnName == RolesButtonColumnName)
          await EditRolesAsync(row.User);
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private async Task EditUserAsync(iSoft.Database.Models.User user)
    {
      if (Environment.GetEnvironmentVariable("STATION") == "1")
      {
        if (!AppCore.Ins.CheckPermission("0102"))
        {
          using var openErrorPopup = new PopupNoPermitRole();
          openErrorPopup.ShowDialog(this);
          return;
        }
      }
      else
      {
        if (!AppCore.Ins.CheckPermission("0112"))
        {
          using var openErrorPopup = new PopupNoPermitRole();
          openErrorPopup.ShowDialog(this);
          return;
        }
      }

      bool isUpdated = false;
      iSoft.Database.Models.User? updatedUser = null;
      using var popup = new PopupUser(user);

      popup.OnSendSuccess += value =>
      {
        isUpdated = true;
        updatedUser = value;
      };
      popup.ShowDialog(this);

      if (!isUpdated)
        return;

      await Task.WhenAll(
        QueueUserUpsertJobAsync(updatedUser ?? user),
        LoadData(resetPage: false));
      ShowSuccess("Cập nhật tài khoản thành công.");
    }

    private async Task DeleteUserAsync(iSoft.Database.Models.User user)
    {
      if (Environment.GetEnvironmentVariable("STATION") == "1")
      {
        if (!AppCore.Ins.CheckPermission("0103"))
        {
          using var openErrorPopup = new PopupNoPermitRole();
          openErrorPopup.ShowDialog(this);
          return;
        }
      }
      else
      {
        if (!AppCore.Ins.CheckPermission("0113"))
        {
          using var openErrorPopup = new PopupNoPermitRole();
          openErrorPopup.ShowDialog(this);
          return;
        }
      }

      if (AppCore.Ins._userCurrent?.Id == user.Id)
      {
        using var warningPopup = new PopupConfirm(
          "Không thể xóa tài khoản đang đăng nhập !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        warningPopup.ShowDialog(this);
        return;
      }

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
        var auditBefore = new
        {
          user.Id,
          user.Username,
          user.DisplayName,
          user.FullName,
          user.EmployeeCode,
          user.DeletedFlag,
          user.EnableFlag
        };
        await AppCore.Ins._userService.DeleteAsync(user);
        await AuditTrail.WriteAsync(
          AuditActions.Delete,
          nameof(iSoft.Database.Models.User),
          user.Id,
          user.Username,
          nameof(FrmUser),
          before: auditBefore,
          after: new { user.DeletedFlag, user.EnableFlag });
        await QueueUserUpsertJobAsync(user);
        await LoadData(resetPage: false);
        ShowSuccess("Xóa tài khoản thành công.");
      }
      catch (Exception ex)
      {
        await AuditTrail.WriteAsync(
          AuditActions.Delete,
          nameof(iSoft.Database.Models.User),
          user.Id,
          user.Username,
          nameof(FrmUser),
          isSuccess: false,
          exception: ex);
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        using var errorPopup = new PopupConfirm(
          "Xóa tài khoản thất bại !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        errorPopup.ShowDialog(this);
      }
    }

    private async Task EditRolesAsync(iSoft.Database.Models.User user)
    {
      if (Environment.GetEnvironmentVariable("STATION") == "1")
      {
        if (!AppCore.Ins.CheckPermission("0104"))
        {
          using var openErrorPopup = new PopupNoPermitRole();
          openErrorPopup.ShowDialog(this);
          return;
        }
      }
      else
      {
        if (!AppCore.Ins.CheckPermission("0114"))
        {
          using var openErrorPopup = new PopupNoPermitRole();
          openErrorPopup.ShowDialog(this);
          return;
        }
      }

      bool isUpdated = false;
      iSoft.Database.Models.User? updatedUser = null;
      using var popup = new PopupRoles(user);

      popup.OnSendSuccess += value =>
      {
        isUpdated = true;
        updatedUser = value;
      };
      popup.ShowDialog(this);

      if (!isUpdated)
        return;

      await QueueUserUpsertJobAsync(updatedUser ?? user);
      await LoadData(resetPage: false);
      ShowSuccess("Cập nhật phân quyền thành công.");
    }

    private static async Task QueueUserUpsertJobAsync(User user)
    {
      var userUpsertRequest = new UserUpsertRequest
      {
        // User mới chưa có IdSrc: không gửi Id để API tự tạo như payload web.
        Id = user.Id,
        TranslateFormDatas = new List<UserTranslateFormData>
        {
          new() { Lang = "VI", FullName = user.FullName },
          new() { Lang = "EN", FullName = user.FullName },
        },

        Username = user.Username,
        Password = user.Password,
        EmployeeCode = user.EmployeeCode,
        IdCardCode = user.IdCardCode,
        EnableFlag = user.EnableFlag,
        SyncFlag = user.SyncFlag,
        DeletedFlag = user.DeletedFlag,
      };

      var apiJob = new ApiJobs
      {
        Id = Guid.NewGuid(),
        Json = JsonHelper.ToJson(userUpsertRequest),
        Description = $"Đồng bộ tài khoản {user.Username}",
        EnumTypeAPI = EnumTypeAPI.MD_User,
        EnumStatusAPI = EnumStatusAPI.Created,
        CreatedAt = DateTime.UtcNow,
      };

      await AppCore.Ins._apiJobsService.AddOrUpdateAsync(apiJob);
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
      if (Environment.GetEnvironmentVariable("STATION") == "1")
      {
        if (!AppCore.Ins.CheckPermission("0101"))
        {
          using var openErrorPopup = new PopupNoPermitRole();
          openErrorPopup.ShowDialog(this);
          return;
        }
      }
      else
      {
        if (!AppCore.Ins.CheckPermission("0111"))
        {
          using var openErrorPopup = new PopupNoPermitRole();
          openErrorPopup.ShowDialog(this);
          return;
        }
      }

      using var buttonLock = ButtonExecutionScope.Enter(sender);
      try
      {
        bool isAdded = false;
        Task queueApiJobTask = Task.CompletedTask;
        using var popup = new PopupUser();

        popup.OnSendSuccess += value =>
        {
          isAdded = true;
          // Lưu job ngay sau khi User được ghi DB, trước khi reload giao diện.
          queueApiJobTask = QueueUserUpsertJobAsync(value);
        };
        popup.ShowDialog(this);

        if (!isAdded)
          return;

        await queueApiJobTask;
        await LoadData(resetPage: true);
        ShowSuccess("Thêm tài khoản thành công.");
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
      }
    }

    private async void btnImport_Click(object? sender, EventArgs e)
    {
      if (Environment.GetEnvironmentVariable("STATION") == "1")
      {
        if (!AppCore.Ins.CheckPermission("0101"))
        {
          using var openErrorPopup = new PopupNoPermitRole();
          openErrorPopup.ShowDialog(this);
          return;
        }
      }
      else if (!AppCore.Ins.CheckPermission("0111"))
      {
        using var openErrorPopup = new PopupNoPermitRole();
        openErrorPopup.ShowDialog(this);
        return;
      }

      using var openFileDialog = new OpenFileDialog
      {
        Title = "Chọn file Excel import tài khoản",
        Filter = "Excel Workbook (*.xlsx;*.xlsm)|*.xlsx;*.xlsm",
        CheckFileExists = true,
        Multiselect = false,
      };

      if (openFileDialog.ShowDialog(this) != DialogResult.OK)
        return;

      using var buttonLock = ButtonExecutionScope.Enter(sender);
      try
      {
        using var workbook = new XLWorkbook(openFileDialog.FileName);
        if (!workbook.Worksheets.TryGetWorksheet("Account", out var worksheet))
        {
          using var popup = new PopupConfirm(
            "Không tìm thấy sheet Account trong file Excel !",
            EnumTypeMsg.MessageManualClose,
            EnumImageMsg.Warning);
          popup.ShowDialog(this);
          return;
        }

        var existingUsers = await AppCore.Ins._userService.GetAllAsync(
          isContainDelete: true);
        var usersByUsername = existingUsers
          .Where(user => !string.IsNullOrWhiteSpace(user.Username))
          .GroupBy(user => user.Username.Trim(), StringComparer.OrdinalIgnoreCase)
          .ToDictionary(group => group.Key, group => group.First(),
            StringComparer.OrdinalIgnoreCase);

        var errors = new List<string>();
        var importedCount = 0;
        var addedCount = 0;
        var updatedCount = 0;
        var lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 2;

        for (var rowNumber = 3; rowNumber <= lastRow; rowNumber++)
        {
          var username = worksheet.Cell(rowNumber, 1).GetFormattedString().Trim();
          var password = worksheet.Cell(rowNumber, 2).GetFormattedString().Trim();
          var displayName = worksheet.Cell(rowNumber, 3).GetFormattedString().Trim();
          var fullName = worksheet.Cell(rowNumber, 4).GetFormattedString().Trim();
          var employeeCode = worksheet.Cell(rowNumber, 5).GetFormattedString().Trim();

          if (string.IsNullOrWhiteSpace(username) &&
              string.IsNullOrWhiteSpace(password) &&
              string.IsNullOrWhiteSpace(displayName) &&
              string.IsNullOrWhiteSpace(fullName) &&
              string.IsNullOrWhiteSpace(employeeCode))
          {
            continue;
          }

          var rowErrors = new List<string>();
          if (string.IsNullOrWhiteSpace(username))
            rowErrors.Add("thiếu Account");
          else if (username.Length > 50)
            rowErrors.Add("Account vượt quá 50 ký tự");

          if (string.IsNullOrWhiteSpace(password))
            rowErrors.Add("thiếu Mật khẩu");

          if (string.IsNullOrWhiteSpace(displayName))
            rowErrors.Add("thiếu Tên hiển thị");
          else if (displayName.Length > 50)
            rowErrors.Add("Tên hiển thị vượt quá 50 ký tự");

          if (fullName.Length > 255)
            rowErrors.Add("Họ và tên vượt quá 255 ký tự");
          if (employeeCode.Length > 255)
            rowErrors.Add("Mã nhân viên vượt quá 255 ký tự");

          if (string.Equals(username, PasswordPolicy.BuiltInAccount,
                StringComparison.OrdinalIgnoreCase) ||
              string.Equals(username, "root", StringComparison.OrdinalIgnoreCase))
          {
            rowErrors.Add("không được import tài khoản hệ thống");
          }

          if (!string.IsNullOrWhiteSpace(password))
          {
            rowErrors.AddRange(PasswordPolicy.ValidateAll(
              password, username, displayName, fullName, employeeCode));
          }

          usersByUsername.TryGetValue(username, out var existingUser);
          if (existingUser != null &&
              UserService.GetPasswordHistory(existingUser).Any(hash =>
                PasswordPolicy.HistoryHashMatches(username, password, hash)))
          {
            rowErrors.Add("không được sử dụng lại 5 mật khẩu gần nhất");
          }

          if (rowErrors.Count > 0)
          {
            errors.Add($"Dòng {rowNumber}: {string.Join("; ", rowErrors)}.");
            continue;
          }

          var isNew = existingUser == null;
          var user = existingUser;
          user ??= new User
          {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
          };

          user.Username = username;
          user.DisplayName = displayName;
          user.FullName = fullName;
          user.EmployeeCode = string.IsNullOrWhiteSpace(employeeCode)
            ? null
            : employeeCode;
          user.DeletedFlag = false;
          user.EnableFlag = true;
          user.SyncFlag = false;
          UserService.ApplyNewPassword(
            user,
            password,
            mustChangePassword: isNew);

          try
          {
            var savedUser = await AppCore.Ins._userService.AddOrUpdateAsync(user);
            await QueueUserUpsertJobAsync(savedUser);
            usersByUsername[username] = savedUser;
            importedCount++;
            if (isNew)
              addedCount++;
            else
              updatedCount++;
          }
          catch (Exception rowException)
          {
            LogHelper.LogErrorToFileLog(rowException, AppCore.Ins._folderFileLog);
            errors.Add($"Dòng {rowNumber}: không thể lưu tài khoản.");
          }
        }

        await LoadData(resetPage: true);

        var message =
          $"Import tài khoản hoàn tất.\r\n" +
          $"- Tổng thành công: {importedCount}\r\n" +
          $"- Thêm mới: {addedCount}\r\n" +
          $"- Cập nhật: {updatedCount}\r\n" +
          $"- Bỏ qua: {errors.Count}";

        if (errors.Count > 0)
        {
          message += "\r\n\r\n" + string.Join("\r\n", errors.Take(10));
          if (errors.Count > 10)
            message += $"\r\n... và {errors.Count - 10} lỗi khác.";
        }

        using var resultPopup = new PopupConfirm(
          message,
          EnumTypeMsg.MessageManualClose,
          errors.Count == 0 ? EnumImageMsg.Information : EnumImageMsg.Warning,
          height: 616);
        resultPopup.ShowDialog(this);
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        using var popup = new PopupConfirm(
          "Không thể import tài khoản. Vui lòng kiểm tra lại file Excel !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popup.ShowDialog(this);
      }
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
