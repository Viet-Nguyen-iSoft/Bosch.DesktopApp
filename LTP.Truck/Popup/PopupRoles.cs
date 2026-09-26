using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LTP.Truck.Popup
{
  public partial class PopupRoles : Form
  {
    private const string SelectedColumnName = "Selected";
    private readonly iSoft.Database.Service.RoleService _roleService = new();
    private readonly iSoft.Database.Service.UserService _userService = new();
    private readonly iSoft.Database.Models.User _user;
    private readonly HashSet<string> _selectedCodes;
    private List<iSoft.Database.Models.Role> _roles = new();

    public event Action<iSoft.Database.Models.User>? OnSendSuccess;

    public PopupRoles(iSoft.Database.Models.User user)
    {
      _user = user ?? throw new ArgumentNullException(nameof(user));
      _selectedCodes = DeserializeRoleCodes(user.Role);
      InitializeComponent();

      lbTitle.Text = $"Phân quyền: {_user.DisplayName ?? _user.Username}";
      btnAdd.Visible = false;
      dgv.ReadOnly = false;

      Shown += async (_, _) => await LoadRolesAsync();
      btnSearch.Click += async (_, _) => await LoadRolesAsync();
      txtSearch._TextChanged += (_, _) => BindRoles();
      btnConfirm.Click += BtnConfirm_Click;
      btnClose.Click += (_, _) => Close();
      dgv.CurrentCellDirtyStateChanged += Dgv_CurrentCellDirtyStateChanged;
      dgv.CellValueChanged += Dgv_CellValueChanged;
    }

    private async Task LoadRolesAsync()
    {
      try
      {
        btnSearch.Enabled = false;
        btnConfirm.Enabled = false;
        _roles = await _roleService.GetAllAsync();
        BindRoles();
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, LTP.Truck.Controls.AppCore.Ins._folderFileLog);
        using var popup = new Common.PopupConfirm(
          "Không thể tải danh sách quyền. Vui lòng thử lại!",
          Common.EnumData.EnumTypeMsg.MessageManualClose,
          Common.EnumData.EnumImageMsg.Warning);
        popup.ShowDialog(this);
      }
      finally
      {
        btnSearch.Enabled = true;
        btnConfirm.Enabled = true;
      }
    }

    private void BindRoles()
    {
      var searchText = txtSearch.Texts.Trim();
      var filteredRoles = string.IsNullOrEmpty(searchText)
        ? _roles
        : _roles.Where(role =>
            (role.Code?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
            (role.Name?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
            (role.Description?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false))
          .ToList();

      dgv.DataSource = null;
      dgv.DataSource = filteredRoles;

      if (!dgv.Columns.Contains(SelectedColumnName))
      {
        dgv.Columns.Insert(0, new DataGridViewCheckBoxColumn
        {
          Name = SelectedColumnName,
          HeaderText = "Chọn",
          Width = 65,
          AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
        });
      }

      foreach (DataGridViewColumn column in dgv.Columns)
      {
        column.Visible = column.Name is SelectedColumnName or "Code" or "Name" or "Description";
        column.ReadOnly = column.Name != SelectedColumnName;
      }

      dgv.Columns[SelectedColumnName].DisplayIndex = 0;
      dgv.Columns["Code"].HeaderText = "Mã quyền";
      dgv.Columns["Name"].HeaderText = "Tên quyền";
      dgv.Columns["Description"].HeaderText = "Mô tả";

      foreach (DataGridViewRow row in dgv.Rows)
      {
        if (row.DataBoundItem is iSoft.Database.Models.Role role &&
            !string.IsNullOrWhiteSpace(role.Code))
        {
          row.Cells[SelectedColumnName].Value = _selectedCodes.Contains(role.Code);
        }
      }
    }

    private void Dgv_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
    {
      if (dgv.IsCurrentCellDirty)
        dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
    }

    private void Dgv_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
      if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
          dgv.Columns[e.ColumnIndex].Name != SelectedColumnName ||
          dgv.Rows[e.RowIndex].DataBoundItem is not iSoft.Database.Models.Role role ||
          string.IsNullOrWhiteSpace(role.Code))
      {
        return;
      }

      if (Convert.ToBoolean(dgv.Rows[e.RowIndex].Cells[SelectedColumnName].Value))
        _selectedCodes.Add(role.Code);
      else
        _selectedCodes.Remove(role.Code);
    }

    private async void BtnConfirm_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      try
      {
        _user.Role = Newtonsoft.Json.JsonConvert.SerializeObject(
          _selectedCodes.OrderBy(code => code, StringComparer.Ordinal));
        var updatedUser = await _userService.AddOrUpdateAsync(_user);
        OnSendSuccess?.Invoke(updatedUser);
        Close();
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, LTP.Truck.Controls.AppCore.Ins._folderFileLog);
        using var popup = new Common.PopupConfirm(
          "Cập nhật phân quyền thất bại!",
          Common.EnumData.EnumTypeMsg.MessageManualClose,
          Common.EnumData.EnumImageMsg.Warning);
        popup.ShowDialog(this);
      }
    }

    private static HashSet<string> DeserializeRoleCodes(string? roleJson)
    {
      if (string.IsNullOrWhiteSpace(roleJson))
        return new HashSet<string>(StringComparer.Ordinal);

      try
      {
        var codes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(roleJson) ?? new List<string>();
        return codes
          .Where(code => !string.IsNullOrWhiteSpace(code))
          .ToHashSet(StringComparer.Ordinal);
      }
      catch (Newtonsoft.Json.JsonException)
      {
        return new HashSet<string>(StringComparer.Ordinal);
      }
    }
  }
}
