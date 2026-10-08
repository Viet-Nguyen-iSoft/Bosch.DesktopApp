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

    public PopupChangePasswork(User user)
    {
      _user = user ?? throw new ArgumentNullException(nameof(user));
      InitializeComponent();

      txtPassOld.PasswordChar = true;
      txtPassNew.PasswordChar = true;
      txtRePassNew.PasswordChar = true;

      btnConfirm.Click += BtnConfirm_Click;
      btnClose.Click += (_, _) => Close();
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
