using System;
using System.Windows.Forms;
using Common;
using HelperManager;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using static Common.EnumData;

namespace LTP.Truck.Popup
{
  public partial class PopupChangePasswork : Form
  {
    private readonly User _user;
    private readonly UserService _userService = new();
    private bool _isOldPasswordHidden = true;
    private bool _isNewPasswordHidden = true;
    private bool _isRePasswordHidden = true;

    public PopupChangePasswork(User user)
    {
      _user = user ?? throw new ArgumentNullException(nameof(user));
      InitializeComponent();

      txtPassOld.PasswordChar = true;
      txtPassNew.PasswordChar = true;
      txtRePassNew.PasswordChar = true;

      btnConfirm.Click += BtnConfirm_Click;
      btnClose.Click += (_, _) => Close();
      btnHidePasswordOld.Click += BtnHidePasswordOld_Click;
      btnHidePasswordNew.Click += BtnHidePasswordNew_Click;
      btnHideRePasswordNew.Click += BtnHideRePasswordNew_Click;
    }

    private void BtnHidePasswordOld_Click(object? sender, EventArgs e)
    {
      _isOldPasswordHidden = !_isOldPasswordHidden;
      txtPassOld.PasswordChar = _isOldPasswordHidden;
      btnHidePasswordOld.Image = _isOldPasswordHidden
        ? Properties.Resources.icon_hide
        : Properties.Resources.icon_unhide;
    }

    private void BtnHidePasswordNew_Click(object? sender, EventArgs e)
    {
      _isNewPasswordHidden = !_isNewPasswordHidden;
      txtPassNew.PasswordChar = _isNewPasswordHidden;
      btnHidePasswordNew.Image = _isNewPasswordHidden
        ? Properties.Resources.icon_hide
        : Properties.Resources.icon_unhide;
    }

    private void BtnHideRePasswordNew_Click(object? sender, EventArgs e)
    {
      _isRePasswordHidden = !_isRePasswordHidden;
      txtRePassNew.PasswordChar = _isRePasswordHidden;
      btnHideRePasswordNew.Image = _isRePasswordHidden
        ? Properties.Resources.icon_hide
        : Properties.Resources.icon_unhide;
    }

    private async void BtnConfirm_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      try
      {
        string oldPassword = txtPassOld.Texts;
        string newPassword = txtPassNew.Texts;
        string confirmPassword = txtRePassNew.Texts;

        if (string.IsNullOrWhiteSpace(oldPassword))
        {
          ShowWarning("Vui lòng nhập mật khẩu cũ !");
          txtPassOld.Focus();
          return;
        }

        if (!string.Equals(oldPassword, _user.PW, StringComparison.Ordinal))
        {
          ShowWarning("Mật khẩu cũ không chính xác !");
          txtPassOld.Focus();
          return;
        }

        if (string.IsNullOrWhiteSpace(newPassword))
        {
          ShowWarning("Vui lòng nhập mật khẩu mới !");
          txtPassNew.Focus();
          return;
        }

        if (string.Equals(oldPassword, newPassword, StringComparison.Ordinal))
        {
          ShowWarning("Mật khẩu mới phải khác mật khẩu cũ !");
          txtPassNew.Focus();
          return;
        }

        if (!string.Equals(newPassword, confirmPassword,
              StringComparison.Ordinal))
        {
          ShowWarning("Mật khẩu nhập lại không khớp !");
          txtRePassNew.Focus();
          return;
        }

        _user.Password = SecurityHelper.Encrypt(newPassword);
        _user.PW = newPassword;
        _user.UpdatedAt = DateTime.UtcNow;
        await _userService.AddOrUpdateAsync(_user);

        using var successPopup = new PopupConfirm(
          "Đổi mật khẩu thành công.",
          EnumTypeMsg.MessageAutoClose,
          EnumImageMsg.Information);
        successPopup.ShowDialog(this);
        Close();
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        ShowWarning("Đổi mật khẩu thất bại. Vui lòng thử lại !");
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
