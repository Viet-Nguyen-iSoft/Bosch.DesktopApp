using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using iSoft.Database.Models;
using iSoft.Database.Service;
using LTP.Truck.Controls;
using static Common.EnumData;
using static LTP.Truck.EnumData;

namespace LTP.Truck.MasterData
{
  public partial class PopupDocument : Form
  {
    public event Action<DocumentData>? OnSendSuccess;

    private readonly DocumentDataService _documentDataService = new();
    private DocumentData? _documentUpdate;
    private EnumTypePopup _enumTypePopup = EnumTypePopup.Add;

    public PopupDocument()
    {
      InitializeComponent();
      dateTimePicker1.Format = DateTimePickerFormat.Custom;
      dateTimePicker1.CustomFormat = "dd/MM/yyyy";
      btnConfirm.Click += BtnConfirm_Click;
      btnClose.Click += (_, _) => Close();
    }

    public PopupDocument(DocumentData document) : this()
    {
      _documentUpdate = document;
      _enumTypePopup = EnumTypePopup.Update;
      lbTitle.Text = "Cập nhật chứng từ";
      btnConfirm.Text = "       Cập nhật";
      dateTimePicker1.Value = document.Date?.ToLocalTime() ?? DateTime.Today;
      txtValueTare.Texts = document.Document ?? string.Empty;
      txtDescription.Texts = document.Description ?? string.Empty;
    }

    private async void BtnConfirm_Click(object? sender, EventArgs e)
    {
      using var buttonLock = ButtonExecutionScope.Enter(sender);
      try
      {
        var documentName = txtValueTare.Texts.Trim();
        if (string.IsNullOrWhiteSpace(documentName))
        {
          ShowWarning("Vui lòng nhập chứng từ !");
          txtValueTare.Focus();
          return;
        }

        var documentDate = DateTime.SpecifyKind(
          dateTimePicker1.Value.Date, DateTimeKind.Utc);
        var documents = await _documentDataService.GetAllAsync(isContainDelete: true);
        var duplicate = documents.Any(document =>
          document.Id != _documentUpdate?.Id &&
          document.Date?.Date == documentDate.Date);
        if (duplicate)
        {
          ShowWarning("Ngày đã chọn đã có chứng từ. Mỗi ngày chỉ được tạo một bản ghi !");
          dateTimePicker1.Focus();
          return;
        }

        var documentToSave = _documentUpdate ?? new DocumentData
        {
          CreatedAt = DateTime.UtcNow,
          EnableFlag = true,
        };
        documentToSave.Date = documentDate;
        documentToSave.Document = documentName;
        documentToSave.Description = txtDescription.Texts.Trim();
        if (_enumTypePopup == EnumTypePopup.Update)
          documentToSave.UpdatedAt = DateTime.UtcNow;

        var result = await _documentDataService.AddOrUpdateAsync(documentToSave);
        Close();
        OnSendSuccess?.Invoke(result);
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        ShowWarning(_enumTypePopup == EnumTypePopup.Add
          ? "Thêm thất bại !"
          : "Cập nhật thất bại !");
      }
    }

    private void ShowWarning(string message)
    {
      using var popup = new PopupConfirm(message,
        EnumTypeMsg.MessageManualClose, EnumImageMsg.Warning);
      popup.ShowDialog(this);
    }
  }
}
