using ApiSyncData.Req;
using Common;
using HelperManager;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using static Common.EnumData;
using static HelperManager.EnumData;

namespace LTP.Truck.MasterData
{
  public partial class PopupTemplate : Form
  {
    private readonly TemplateService _templateService = new();
    private readonly Template _template;
    private string? _selectedPath;

    public PopupTemplate() : this(new Template())
    {
    }

    public PopupTemplate(Template template)
    {
      ArgumentNullException.ThrowIfNull(template);

      InitializeComponent();
      _template = template;
      _selectedPath = template.PathLocal;
      InitializeData();

      btnUpfile.Click += btnUpfile_Click;
      btnConfirm.Click += btnConfirm_Click;
      btnClose.Click += btnClose_Click;
    }

    private void InitializeData()
    {
      lbTitle.Text = "Chi tiết template";
      label5.Text = "Tên template";
      label8.Text = "File local";
      label4.Text = "File server";
      btnUpfile.Text = "Chọn file";
      btnConfirm.Text = "       Lưu";

      txtName.Texts = _template.Name ?? string.Empty;
      txtNameFile.Texts = _template.PathLocal ?? string.Empty;
      txtDescription.Texts = _template.TemplateFile ?? string.Empty;

      txtName.Enabled = false;
      txtNameFile.Enabled = false;
      txtDescription.Enabled = false;
    }

    private void btnUpfile_Click(object? sender, EventArgs e)
    {
      using var openFileDialog = new OpenFileDialog
      {
        Title = $"Chọn file local cho {_template.Name}",
        Filter =
          "Template (*.doc;*.docx;*.xlsx)|*.doc;*.docx;*.xlsx|" +
          "Tất cả file (*.*)|*.*",
        CheckFileExists = true,
        Multiselect = false,
      };

      if (!string.IsNullOrWhiteSpace(_selectedPath))
      {
        string? directory = Path.GetDirectoryName(_selectedPath);
        if (!string.IsNullOrWhiteSpace(directory) && Directory.Exists(directory))
          openFileDialog.InitialDirectory = directory;
      }

      if (openFileDialog.ShowDialog(this) != DialogResult.OK)
        return;

      _selectedPath = Path.GetFullPath(openFileDialog.FileName);
      txtNameFile.Texts = _selectedPath;
    }

    private async void btnConfirm_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);

      if (string.IsNullOrWhiteSpace(_selectedPath) ||
          !File.Exists(_selectedPath))
      {
        using var warningPopup = new PopupConfirm(
          "Vui lòng chọn file template tồn tại trên máy !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        warningPopup.ShowDialog(this);
        return;
      }

      try
      {
        string fullPath = Path.GetFullPath(_selectedPath);
        await _templateService.UpdatePathLocalAsync(_template.Id, fullPath);
        _template.PathLocal = fullPath;

        var upsertRequest = new TemplateUpsertRequest
        {
          Id = _template.Id,
          Name = _template.Name,
          SerialCode = _template.SerialCode,
          IsBBGN = _template.IsBBGN,
          TemplateFilePath = fullPath,
        };
        await new ApiJobsService().AddOrUpdateAsync(new ApiJobs
        {
          Id = Guid.NewGuid(),
          Json = JsonHelper.ToJson(upsertRequest),
          Description = $"Đồng bộ file template {_template.Name}",
          EnumTypeAPI = EnumTypeAPI.MD_Template,
          EnumStatusAPI = EnumStatusAPI.Created,
          CreatedAt = DateTime.UtcNow,
        });

        DialogResult = DialogResult.OK;
        Close();
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(
          ex,
          Path.Combine(AppContext.BaseDirectory, "Logs"));
        using var errorPopup = new PopupConfirm(
          "Không thể lưu hoặc tạo yêu cầu đồng bộ file template !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        errorPopup.ShowDialog(this);
      }
    }

    private void btnClose_Click(object? sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }
  }
}
