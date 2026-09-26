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

    public PopupUser()
    {
      InitializeComponent();

      btnConfirm.Click += BtnConfirm_Click;
      btnClose.Click += BtnClose_Click;
      txtPassword.PasswordChar = true;
      txtRePassword.PasswordChar = true;
    }

    public PopupUser(User user, bool isChangePassword = false) : this()
    {
      _userUpdate = user ?? throw new ArgumentNullException(nameof(user));
      _enumTypePopup = EnumTypePopup.Update;
      _isChangePassword = isChangePassword;

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

      LoadDataUpdate(user);
    }

    private void LoadDataUpdate(User user)
    {
      txtUsername.Texts = user.Username ?? string.Empty;
      txtDisplayName.Texts = user.DisplayName ?? string.Empty;
      txtFullName.Texts = user.FullName ?? string.Empty;
      txtEmployeeCode.Texts = user.EmployeeCode ?? string.Empty;

      // Không hiển thị mật khẩu đã mã hóa. Người dùng phải nhập mật khẩu mới.
      txtPassword.Texts = string.Empty;
      txtRePassword.Texts = string.Empty;
    }

    private void BtnClose_Click(object? sender, EventArgs e)
    {
      Close();
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

        bool isPasswordRequired =
          _enumTypePopup == EnumTypePopup.Add || _isChangePassword;
        bool hasPasswordInput =
          !string.IsNullOrWhiteSpace(password) ||
          !string.IsNullOrWhiteSpace(rePassword);

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

        if (string.IsNullOrWhiteSpace(fullName))
        {
          ShowWarning("Vui lòng nhập họ và tên !");
          txtFullName.Focus();
          return;
        }

        if (fullName.Length > 255)
        {
          ShowWarning("Họ và tên không được vượt quá 255 ký tự !");
          txtFullName.Focus();
          return;
        }

        if (string.IsNullOrWhiteSpace(employeeCode))
        {
          ShowWarning("Vui lòng nhập mã nhân viên !");
          txtEmployeeCode.Focus();
          return;
        }

        if (employeeCode.Length > 255)
        {
          ShowWarning("Mã nhân viên không được vượt quá 255 ký tự !");
          txtEmployeeCode.Focus();
          return;
        }

        var users = await _userService.GetAllAsync(isContainDelete: true);
        bool isDuplicateUsername = users.Any(user =>
          !user.DeletedFlag &&
          user.Id != _userUpdate?.Id &&
          string.Equals(user.Username?.Trim(), username,
            StringComparison.CurrentCultureIgnoreCase));

        if (isDuplicateUsername)
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
        userToSave.EnableFlag = true;
        if (hasPasswordInput)
          userToSave.Password = SecurityHelper.EncodePassword(username, password);

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
  }
}
