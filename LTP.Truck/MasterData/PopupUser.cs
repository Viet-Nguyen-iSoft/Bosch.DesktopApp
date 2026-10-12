using Common;
using HelperManager;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using static Common.EnumData;
using static LTP.Truck.EnumData;

namespace LTP.Truck.MasterData
{
  public partial class PopupUser : Form
  {
    public event Action<User>? OnSendSuccess;

    private readonly UserService _userService = new();
    private readonly User? _userUpdate;
    private readonly EnumTypePopup _enumTypePopup = EnumTypePopup.Add;
    private readonly bool _isChangePassword;
    private readonly bool _canChangePassword = true;
    private bool _isActive = true;
    private bool _isPasswordHidden = true;
    private bool _isRePasswordHidden = true;
    private readonly Common.Custom.RJTextBox _txtSsoLoginId = new();

    public PopupUser()
    {
      InitializeComponent();
      // Tạm thời không hiển thị cấu hình Microsoft SSO.

      btnConfirm.Click += BtnConfirm_Click;
      btnClose.Click += BtnClose_Click;
      btnHidePassword.Click += BtnHidePassword_Click;
      btnHideRePassword.Click += BtnHideRePassword_Click;
      picActive.Click += PicActive_Click;
      picActive.Cursor = Cursors.Hand;
      txtPassword.PasswordChar = true;
      txtRePassword.PasswordChar = true;
    }

    public PopupUser(User user, bool isChangePassword = false) : this()
    {
      _userUpdate = user ?? throw new ArgumentNullException(nameof(user));
      _enumTypePopup = EnumTypePopup.Update;
      _isChangePassword = isChangePassword;
      _canChangePassword = AppCore.Ins._userCurrent?.Id == user.Id;

      lbTitle.Text = isChangePassword
        ? "Đổi mật khẩu tài khoản"
        : "Chỉnh sửa tài khoản";
      btnConfirm.Text = "Cập nhật";
      txtUsername.Enabled = false;

      if (!isChangePassword)
      {
        label15.Visible = false;
        label2.Visible = false;
        txtPassword.PlaceholderText = "Để trống nếu không đổi mật khẩu";
        txtRePassword.PlaceholderText = "Để trống nếu không đổi mật khẩu";
      }

      ConfigurePasswordAccess();

      LoadDataUpdate(user);
    }

    private void ConfigurePasswordAccess()
    {
      if (_canChangePassword)
        return;

      txtPassword.Enabled = false;
      txtRePassword.Enabled = false;
      btnHidePassword.Enabled = false;
      btnHideRePassword.Enabled = false;
      txtPassword.PlaceholderText = "Chỉ được đổi mật khẩu tài khoản đang đăng nhập";
      txtRePassword.PlaceholderText = "Chỉ được đổi mật khẩu tài khoản đang đăng nhập";
    }

    private void LoadDataUpdate(User user)
    {
      txtUsername.Texts = user.Username ?? string.Empty;
      txtDisplayName.Texts = user.DisplayName ?? string.Empty;
      txtFullName.Texts = user.FullName ?? string.Empty;
      txtEmployeeCode.Texts = user.EmployeeCode ?? string.Empty;
      _txtSsoLoginId.Texts = user.SsoLoginId ?? string.Empty;
      SetActiveUser(user.EnableFlag);

      // Không hiển thị mật khẩu đã mã hóa. Người dùng phải nhập mật khẩu mới.
      txtPassword.Texts = string.Empty;
      txtRePassword.Texts = string.Empty;
    }

    private void BtnClose_Click(object? sender, EventArgs e)
    {
      Close();
    }

    private void BtnHidePassword_Click(object? sender, EventArgs e)
    {
      _isPasswordHidden = !_isPasswordHidden;
      txtPassword.PasswordChar = _isPasswordHidden;
      btnHidePassword.Image = _isPasswordHidden
        ? Properties.Resources.icon_hide
        : Properties.Resources.icon_unhide;
    }

    private void BtnHideRePassword_Click(object? sender, EventArgs e)
    {
      _isRePasswordHidden = !_isRePasswordHidden;
      txtRePassword.PasswordChar = _isRePasswordHidden;
      btnHideRePassword.Image = _isRePasswordHidden
        ? Properties.Resources.icon_hide
        : Properties.Resources.icon_unhide;
    }

    private async void BtnConfirm_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);

      try
      {
        string username = txtUsername.Texts.Trim();
        string displayName = txtDisplayName.Texts.Trim();
        string password = txtPassword.Texts;
        string rePassword = txtRePassword.Texts;
        string fullName = txtFullName.Texts.Trim();
        string employeeCode = txtEmployeeCode.Texts.Trim();
        string ssoLoginId = _txtSsoLoginId.Texts.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(username))
        {
          ShowWarning("Vui lòng nhập tên đăng nhập !");
          txtUsername.Focus();
          return;
        }

        if (username.Length > 50)
        {
          ShowWarning("Tên đăng nhập không được vượt quá 50 ký tự !");
          txtUsername.Focus();
          return;
        }

        if (string.IsNullOrWhiteSpace(displayName))
        {
          ShowWarning("Vui lòng nhập tên hiển thị !");
          txtDisplayName.Focus();
          return;
        }

        if (displayName.Length > 50)
        {
          ShowWarning("Tên hiển thị không được vượt quá 50 ký tự !");
          txtDisplayName.Focus();
          return;
        }

        bool isPasswordRequired = _canChangePassword &&
          (_enumTypePopup == EnumTypePopup.Add || _isChangePassword);
        bool hasPasswordInput = _canChangePassword &&
          (!string.IsNullOrWhiteSpace(password) ||
           !string.IsNullOrWhiteSpace(rePassword));

        if (isPasswordRequired && string.IsNullOrWhiteSpace(password))
        {
          ShowWarning(_enumTypePopup == EnumTypePopup.Add
            ? "Vui lòng nhập mật khẩu !"
            : "Vui lòng nhập mật khẩu mới !");
          txtPassword.Focus();
          return;
        }

        if (isPasswordRequired && string.IsNullOrWhiteSpace(rePassword))
        {
          ShowWarning("Vui lòng nhập lại mật khẩu !");
          txtRePassword.Focus();
          return;
        }

        if (hasPasswordInput &&
            !string.Equals(password, rePassword, StringComparison.Ordinal))
        {
          ShowWarning("Mật khẩu nhập lại không khớp !");
          txtRePassword.Focus();
          return;
        }

        if (hasPasswordInput)
        {
          var passwordErrors = PasswordPolicy.ValidateAll(password,
            username, displayName, fullName, employeeCode,
            _userUpdate?.IdCardCode).ToList();

          if (_userUpdate != null && UserService.GetPasswordHistory(_userUpdate)
            .Any(hash => PasswordPolicy.HistoryHashMatches(
              username, password, hash)))
            passwordErrors.Add("Không được sử dụng lại 5 mật khẩu gần nhất.");

          if (passwordErrors.Count > 0)
          {
            ShowWarning("Mật khẩu chưa đáp ứng các quy tắc sau:\n- " +
              string.Join("\n- ", passwordErrors));
            txtPassword.Focus();
            return;
          }
        }

        if (fullName.Length > 255)
        {
          ShowWarning("Họ và tên không được vượt quá 255 ký tự !");
          txtFullName.Focus();
          return;
        }

        if (employeeCode.Length > 255)
        {
          ShowWarning("Mã nhân viên không được vượt quá 255 ký tự !");
          txtEmployeeCode.Focus();
          return;
        }

        if (ssoLoginId.Length > 255)
        {
          ShowWarning("Microsoft SSO ID không được vượt quá 255 ký tự !");
          _txtSsoLoginId.Focus();
          return;
        }

        if (!string.IsNullOrEmpty(ssoLoginId) &&
            await _userService.ExistsSsoLoginIdAsync(
              ssoLoginId, _userUpdate?.Id))
        {
          ShowWarning("Microsoft SSO ID đã được liên kết với tài khoản khác !");
          _txtSsoLoginId.Focus();
          return;
        }

        // Khi chỉnh sửa, username bị khóa nên không cần truy vấn DB kiểm tra lại.
        if (_enumTypePopup == EnumTypePopup.Add &&
            await _userService.ExistsUsernameAsync(username))
        {
          ShowWarning("Tên đăng nhập đã tồn tại !");
          txtUsername.Focus();
          return;
        }

        User userToSave;
        if (_enumTypePopup == EnumTypePopup.Add)
        {
          userToSave = new User
          {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
          };
        }
        else
        {
          userToSave = _userUpdate!;
          userToSave.UpdatedAt = DateTime.UtcNow;
        }

        userToSave.Username = username;
        userToSave.DisplayName = displayName;
        userToSave.FullName = fullName;
        userToSave.EmployeeCode = employeeCode;
        userToSave.SsoLoginId = string.IsNullOrEmpty(ssoLoginId)
          ? null
          : ssoLoginId;
        userToSave.EnableFlag = _isActive;
        if (hasPasswordInput)
        {
          UserService.ApplyNewPassword(userToSave, password,
            mustChangePassword: _enumTypePopup == EnumTypePopup.Add);
        }

        User result = await _userService.AddOrUpdateAsync(userToSave);

        Close();
        OnSendSuccess?.Invoke(result);
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        ShowWarning(_enumTypePopup == EnumTypePopup.Add
          ? "Thêm tài khoản thất bại !"
          : "Cập nhật tài khoản thất bại !");
      }
    }

    private void ShowWarning(string message)
    {
      using var popup = new PopupConfirm(
        message,
        EnumTypeMsg.MessageManualClose,
        EnumImageMsg.Warning);
      popup.ShowDialog(this);
    }

    private void ConfigureSsoLoginIdField()
    {
      tableLayoutPanel5.RowCount = 8;
      tableLayoutPanel5.RowStyles.Add(
        new RowStyle(SizeType.Percent, 14.2857141F));
      tableLayoutPanel5.SetRow(label4, 7);
      tableLayoutPanel5.SetRow(picActive, 7);

      var label = new Label
      {
        Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
          AnchorStyles.Left | AnchorStyles.Right,
        AutoSize = true,
        BackColor = Color.Transparent,
        Font = new Font("Roboto", 14F, FontStyle.Regular,
          GraphicsUnit.Point, 0),
        Margin = new Padding(0),
        Text = "Microsoft SSO ID",
        TextAlign = ContentAlignment.MiddleLeft
      };

      _txtSsoLoginId.Anchor = AnchorStyles.Left | AnchorStyles.Right;
      _txtSsoLoginId.BackColor = SystemColors.Window;
      _txtSsoLoginId.BorderColor = Color.Black;
      _txtSsoLoginId.BorderFocusColor = Color.HotPink;
      _txtSsoLoginId.BorderRadius = 5;
      _txtSsoLoginId.BorderSize = 2;
      _txtSsoLoginId.Font = new Font("Roboto", 14F);
      _txtSsoLoginId.ForeColor = Color.FromArgb(64, 64, 64);
      _txtSsoLoginId.Margin = new Padding(4);
      _txtSsoLoginId.Multiline = false;
      _txtSsoLoginId.Padding = new Padding(10, 7, 10, 7);
      _txtSsoLoginId.PasswordChar = false;
      _txtSsoLoginId.PlaceholderColor = Color.DarkGray;
      _txtSsoLoginId.PlaceholderText = "Ví dụ: user@bosch.com";

      tableLayoutPanel5.Controls.Add(label, 0, 6);
      tableLayoutPanel5.Controls.Add(_txtSsoLoginId, 1, 6);
    }

    private void SetActiveUser(bool check)
    {
      if (this.InvokeRequired)
      {
        this.Invoke(new Action(() =>
        {
          SetActiveUser(check);
        }));
        return;
      }

      _isActive = check;
      picActive.Image = !check
        ? Properties.Resources.icon_toggle_on
        : Properties.Resources.icon_toggle_off;
    }

    private void PicActive_Click(object? sender, EventArgs e)
    {
      SetActiveUser(!_isActive);
    }
  }
}
