using Common;
using HelperManager;
using iSoft.Database.DTO;
using iSoft.Database.Models;
using LTP.Truck.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Common.EnumData;

namespace LTP.Truck.Popup
{
  public partial class PopupChooseTemplateReport : Form
  {
    private static readonly Regex ReportPlaceholderRegex = new(
      @"\{(?<tag>[^{}\r\n]+)_[ \t]*(?<kind>weight|qty)\}",
      RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex LicensePlatePlaceholderRegex = new(
      @"\{bien_so_(?<index>\d{2})\}",
      RegexOptions.IgnoreCase | RegexOptions.Compiled);
    private static readonly Regex ExportDatePlaceholderRegex = new(
      @"\{(?<part>ngay|thang|nam)\}",
      RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public IReadOnlyList<RecordWeightDTO> SelectedData { get; }

    public PopupChooseTemplateReport()
      : this(Array.Empty<RecordWeightDTO>())
    {
    }

    public PopupChooseTemplateReport(IReadOnlyList<RecordWeightDTO> selectedData)
    {
      ArgumentNullException.ThrowIfNull(selectedData);

      InitializeComponent();
      SelectedData = selectedData;
    }

    private void PopupChooseTemplateReport_Load(object sender, EventArgs e)
    {

    }

    private async void btnHcP_TH_Click(object sender, EventArgs e)
    {
      using var uiLock = LockExportUi();

      var hcpData = SelectedData
        .Where(dto => HasProductGroupName(dto, "HcP"))
        .ToList();
      var tgaCount = SelectedData.Count(dto => HasProductGroupName(dto, "TGA"));

      if (hcpData.Count != SelectedData.Count)
      {
        var continueExport = false;
        using (var popup = new PopupConfirm(
          $"Dữ liệu đã chọn gồm:\r\n" +
          $"- HcP: {hcpData.Count}\r\n" +
          $"- TGA: {tgaCount}\r\n\r\n" +
          "Có dữ liệu không phải HcP. Bạn có muốn tiếp tục và chỉ xuất các dữ liệu HcP không?",
          EnumTypeMsg.Confirm,
          EnumImageMsg.Question,
          height: 400))
        {
          popup.OnSendConfirm += (_, response) =>
            continueExport = response.EnumResponsible == EnumResponsible.Confirm;
          popup.ShowDialog(this);
        }

        if (!continueExport)
          return;
      }

      List<RecordWeight> recordWeights = hcpData
        .Where(dto => string.Equals(dto.Source, "Cân hàng", StringComparison.OrdinalIgnoreCase))
        .Select(dto => dto.RecordWeight)
        .Where(record => record is not null)
        .Select(record => record!)
        .ToList();

      List<RecordTruck> recordTrucks = hcpData
        .Where(dto => string.Equals(dto.Source, "Cân xe tải", StringComparison.OrdinalIgnoreCase))
        .Select(dto => dto.RecordTruck)
        .Where(record => record is not null)
        .Select(record => record!)
        .ToList();

      if (recordWeights.Count == 0 && recordTrucks.Count == 0)
      {
        using var popupMsg = new PopupConfirm(
          "Không có dữ liệu HcP hợp lệ để xuất báo cáo !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupMsg.ShowDialog(this);
        return;
      }

      await DownloadHcpSummaryReportAsync(recordWeights, recordTrucks);
    }

    private async void btnHcP_CN_Click(object sender, EventArgs e)
    {
      using var uiLock = LockExportUi();

      var hcpData = SelectedData
        .Where(dto => HasProductGroupName(dto, "HcP"))
        .ToList();
      var tgaCount = SelectedData.Count(dto => HasProductGroupName(dto, "TGA"));

      if (hcpData.Count != SelectedData.Count)
      {
        var continueExport = false;
        using (var popup = new PopupConfirm(
          $"Dữ liệu đã chọn gồm:\r\n" +
          $"- HcP: {hcpData.Count}\r\n" +
          $"- TGA: {tgaCount}\r\n\r\n" +
          "Có dữ liệu không phải HcP. Bạn có muốn tiếp tục và chỉ xuất các dữ liệu HcP không?",
          EnumTypeMsg.Confirm,
          EnumImageMsg.Question,
          height: 400))
        {
          popup.OnSendConfirm += (_, response) =>
            continueExport = response.EnumResponsible == EnumResponsible.Confirm;
          popup.ShowDialog(this);
        }

        if (!continueExport)
          return;
      }

      List<RecordWeight> recordWeights = hcpData
        .Where(dto => string.Equals(dto.Source, "Cân hàng", StringComparison.OrdinalIgnoreCase))
        .Select(dto => dto.RecordWeight)
        .Where(record => record is not null)
        .Select(record => record!)
        .ToList();

      List<RecordTruck> recordTrucks = hcpData
        .Where(dto => string.Equals(dto.Source, "Cân xe tải", StringComparison.OrdinalIgnoreCase))
        .Select(dto => dto.RecordTruck)
        .Where(record => record is not null)
        .Select(record => record!)
        .ToList();

      if (recordWeights.Count == 0 && recordTrucks.Count == 0)
      {
        using var popupMsg = new PopupConfirm(
          "Không có dữ liệu HcP hợp lệ để xuất báo cáo !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupMsg.ShowDialog(this);
        return;
      }

      await DownloadHcpSummaryReportAsync(
        recordWeights,
        recordTrucks,
        "Template_BBGN KNH Bosch HCP-CN.doc");
    }

    private async void btnHcP_SH_Click(object sender, EventArgs e)
    {
      using var uiLock = LockExportUi();

      var hcpData = SelectedData
       .Where(dto => HasProductGroupName(dto, "HcP"))
       .ToList();
      var tgaCount = SelectedData.Count(dto => HasProductGroupName(dto, "TGA"));

      if (hcpData.Count != SelectedData.Count)
      {
        var continueExport = false;
        using (var popup = new PopupConfirm(
          $"Dữ liệu đã chọn gồm:\r\n" +
          $"- HcP: {hcpData.Count}\r\n" +
          $"- TGA: {tgaCount}\r\n\r\n" +
          "Có dữ liệu không phải HcP. Bạn có muốn tiếp tục và chỉ xuất các dữ liệu HcP không?",
          EnumTypeMsg.Confirm,
          EnumImageMsg.Question,
          height: 400))
        {
          popup.OnSendConfirm += (_, response) =>
            continueExport = response.EnumResponsible == EnumResponsible.Confirm;
          popup.ShowDialog(this);
        }

        if (!continueExport)
          return;
      }

      List<RecordWeight> recordWeights = hcpData
        .Where(dto => string.Equals(dto.Source, "Cân hàng", StringComparison.OrdinalIgnoreCase))
        .Select(dto => dto.RecordWeight)
        .Where(record => record is not null)
        .Select(record => record!)
        .ToList();

      List<RecordTruck> recordTrucks = hcpData
        .Where(dto => string.Equals(dto.Source, "Cân xe tải", StringComparison.OrdinalIgnoreCase))
        .Select(dto => dto.RecordTruck)
        .Where(record => record is not null)
        .Select(record => record!)
        .ToList();

      if (recordWeights.Count == 0 && recordTrucks.Count == 0)
      {
        using var popupMsg = new PopupConfirm(
          "Không có dữ liệu HcP hợp lệ để xuất báo cáo !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupMsg.ShowDialog(this);
        return;
      }

      await DownloadHcpSummaryReportAsync(
        recordWeights,
        recordTrucks,
        "Template_BBGN KNH Bosch HCP-SH.doc");
    }



    private async void btnTGA_TH_Click(object sender, EventArgs e)
    {
      using var uiLock = LockExportUi();

      var tgaData = SelectedData
        .Where(dto => HasProductGroupName(dto, "TGA"))
        .ToList();
      var hcpCount = SelectedData.Count(dto => HasProductGroupName(dto, "HcP"));

      if (tgaData.Count != SelectedData.Count)
      {
        var continueExport = false;
        using (var popup = new PopupConfirm(
          $"Dữ liệu đã chọn gồm:\r\n" +
          $"- HcP: {hcpCount}\r\n" +
          $"- TGA: {tgaData.Count}\r\n\r\n" +
          "Có dữ liệu không phải TGA. Bạn có muốn tiếp tục và chỉ xuất các dữ liệu TGA không?",
          EnumTypeMsg.Confirm,
          EnumImageMsg.Question,
          height: 400))
        {
          popup.OnSendConfirm += (_, response) =>
            continueExport = response.EnumResponsible == EnumResponsible.Confirm;
          popup.ShowDialog(this);
        }

        if (!continueExport)
          return;
      }

      List<RecordWeight> recordWeights = tgaData
        .Where(dto => string.Equals(dto.Source, "Cân hàng", StringComparison.OrdinalIgnoreCase))
        .Select(dto => dto.RecordWeight)
        .Where(record => record is not null)
        .Select(record => record!)
        .ToList();

      List<RecordTruck> recordTrucks = tgaData
        .Where(dto => string.Equals(dto.Source, "Cân xe tải", StringComparison.OrdinalIgnoreCase))
        .Select(dto => dto.RecordTruck)
        .Where(record => record is not null)
        .Select(record => record!)
        .ToList();

      List<string> licensePlates = recordWeights
        .Select(record => record.LicensePlate)
        .Concat(recordTrucks.Select(record => record.LicensePlate))
        .Where(licensePlate => !string.IsNullOrWhiteSpace(licensePlate))
        .Select(licensePlate => licensePlate!.Trim())
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .ToList();

      if (recordWeights.Count == 0 && recordTrucks.Count == 0)
      {
        using var popupMsg = new PopupConfirm(
          "Không có dữ liệu TGA hợp lệ để xuất báo cáo !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupMsg.ShowDialog(this);
        return;
      }

      InforCompanyDTO inforCompanyDTO_A = new InforCompanyDTO()
      {
        Company = AppCore.Ins._appConfig?.Company,
        OfficeAddress = AppCore.Ins._appConfig?.OfficeAddress,
        PhoneOfficeAddress = AppCore.Ins._appConfig?.PhoneForOfficeAddress,
        AgentAddress = AppCore.Ins._appConfig?.AgentAddress,
        PhoneAgentAddress = AppCore.Ins._appConfig?.PhoneForAgentAddress,
      };

      InforCompanyDTO inforCompanyDTO_B = new InforCompanyDTO()
      {
        Company = recordWeights.FirstOrDefault()?.Delivery?.Name
          ?? recordTrucks.FirstOrDefault()?.Client?.Name,
        OfficeAddress = recordWeights.FirstOrDefault()?.Delivery?.OfficeAddress,
        PhoneOfficeAddress = recordWeights.FirstOrDefault()?.Delivery?.PhoneForOfficeAddress,
        AgentAddress = recordWeights.FirstOrDefault()?.Delivery?.AgentAddress,
        PhoneAgentAddress = recordWeights.FirstOrDefault()?.Delivery?.PhoneForAgentAddress,
      };

      await DownloadFileWorkReport_TongHop(
        "Template_TongHop",
        "TemplateTableCN",
        recordWeights,
        recordTrucks,
        licensePlates,
        inforCompanyDTO_A,
        inforCompanyDTO_B);

    }

    private async void btnTGA_CN_Click(object sender, EventArgs e)
    {
      using var uiLock = LockExportUi();

      var tgaData = SelectedData
        .Where(dto => HasProductGroupName(dto, "TGA"))
        .ToList();
      var hcpCount = SelectedData.Count(dto => HasProductGroupName(dto, "HcP"));

      if (tgaData.Count != SelectedData.Count)
      {
        var continueExport = false;
        using (var popup = new PopupConfirm(
          $"Dữ liệu đã chọn gồm:\r\n" +
          $"- HcP: {hcpCount}\r\n" +
          $"- TGA: {tgaData.Count}\r\n\r\n" +
          "Có dữ liệu không phải TGA. Bạn có muốn tiếp tục và chỉ xuất các dữ liệu TGA không?",
          EnumTypeMsg.Confirm,
          EnumImageMsg.Question,
          height: 400))
        {
          popup.OnSendConfirm += (_, response) =>
            continueExport = response.EnumResponsible == EnumResponsible.Confirm;
          popup.ShowDialog(this);
        }

        if (!continueExport)
          return;
      }

      List<RecordWeight> recordWeights = tgaData
        .Where(dto => string.Equals(dto.Source, "Cân hàng", StringComparison.OrdinalIgnoreCase))
        .Select(dto => dto.RecordWeight)
        .Where(record => record is not null)
        .Select(record => record!)
        .ToList();

      List<RecordTruck> recordTrucks = tgaData
        .Where(dto => string.Equals(dto.Source, "Cân xe tải", StringComparison.OrdinalIgnoreCase))
        .Select(dto => dto.RecordTruck)
        .Where(record => record is not null)
        .Select(record => record!)
        .ToList();

      List<string> licensePlates = recordWeights
        .Select(record => record.LicensePlate)
        .Concat(recordTrucks.Select(record => record.LicensePlate))
        .Where(licensePlate => !string.IsNullOrWhiteSpace(licensePlate))
        .Select(licensePlate => licensePlate!.Trim())
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .ToList();

      if (recordWeights.Count == 0 && recordTrucks.Count == 0)
      {
        using var popupMsg = new PopupConfirm(
          "Không có dữ liệu TGA hợp lệ để xuất báo cáo !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupMsg.ShowDialog(this);
        return;
      }

      InforCompanyDTO inforCompanyDTO_A = new InforCompanyDTO()
      {
        Company = AppCore.Ins._appConfig?.Company,
        OfficeAddress = AppCore.Ins._appConfig?.OfficeAddress,
        PhoneOfficeAddress = AppCore.Ins._appConfig?.PhoneForOfficeAddress,
        AgentAddress = AppCore.Ins._appConfig?.AgentAddress,
        PhoneAgentAddress = AppCore.Ins._appConfig?.PhoneForAgentAddress,
      };

      InforCompanyDTO inforCompanyDTO_B = new InforCompanyDTO()
      {
        Company = recordWeights.FirstOrDefault()?.Delivery?.Name
          ?? recordTrucks.FirstOrDefault()?.Client?.Name,
        OfficeAddress = recordWeights.FirstOrDefault()?.Delivery?.OfficeAddress,
        PhoneOfficeAddress = recordWeights.FirstOrDefault()?.Delivery?.PhoneForOfficeAddress,
        AgentAddress = recordWeights.FirstOrDefault()?.Delivery?.AgentAddress,
        PhoneAgentAddress = recordWeights.FirstOrDefault()?.Delivery?.PhoneForAgentAddress,
      };

      await DownloadFileWorkReport_TongHop(
        "Template_CN",
        "TemplateTableCN",
        recordWeights,
        recordTrucks,
        licensePlates,
        inforCompanyDTO_A,
        inforCompanyDTO_B);
    }

    private async void btnTGA_SH_Click(object sender, EventArgs e)
    {
      using var uiLock = LockExportUi();

      var tgaData = SelectedData
        .Where(dto => HasProductGroupName(dto, "TGA"))
        .ToList();
      var hcpCount = SelectedData.Count(dto => HasProductGroupName(dto, "HcP"));

      if (tgaData.Count != SelectedData.Count)
      {
        var continueExport = false;
        using (var popup = new PopupConfirm(
          $"Dữ liệu đã chọn gồm:\r\n" +
          $"- HcP: {hcpCount}\r\n" +
          $"- TGA: {tgaData.Count}\r\n\r\n" +
          "Có dữ liệu không phải TGA. Bạn có muốn tiếp tục và chỉ xuất các dữ liệu TGA không?",
          EnumTypeMsg.Confirm,
          EnumImageMsg.Question,
          height: 400))
        {
          popup.OnSendConfirm += (_, response) =>
            continueExport = response.EnumResponsible == EnumResponsible.Confirm;
          popup.ShowDialog(this);
        }

        if (!continueExport)
          return;
      }

      List<RecordWeight> recordWeights = tgaData
        .Where(dto => string.Equals(dto.Source, "Cân hàng", StringComparison.OrdinalIgnoreCase))
        .Select(dto => dto.RecordWeight)
        .Where(record => record is not null)
        .Select(record => record!)
        .ToList();

      List<RecordTruck> recordTrucks = tgaData
        .Where(dto => string.Equals(dto.Source, "Cân xe tải", StringComparison.OrdinalIgnoreCase))
        .Select(dto => dto.RecordTruck)
        .Where(record => record is not null)
        .Select(record => record!)
        .ToList();

      List<string> licensePlates = recordWeights
        .Select(record => record.LicensePlate)
        .Concat(recordTrucks.Select(record => record.LicensePlate))
        .Where(licensePlate => !string.IsNullOrWhiteSpace(licensePlate))
        .Select(licensePlate => licensePlate!.Trim())
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .ToList();

      if (recordWeights.Count == 0 && recordTrucks.Count == 0)
      {
        using var popupMsg = new PopupConfirm(
          "Không có dữ liệu TGA hợp lệ để xuất báo cáo !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupMsg.ShowDialog(this);
        return;
      }

      InforCompanyDTO inforCompanyDTO_A = new InforCompanyDTO()
      {
        Company = AppCore.Ins._appConfig?.Company,
        OfficeAddress = AppCore.Ins._appConfig?.OfficeAddress,
        PhoneOfficeAddress = AppCore.Ins._appConfig?.PhoneForOfficeAddress,
        AgentAddress = AppCore.Ins._appConfig?.AgentAddress,
        PhoneAgentAddress = AppCore.Ins._appConfig?.PhoneForAgentAddress,
      };

      InforCompanyDTO inforCompanyDTO_B = new InforCompanyDTO()
      {
        Company = recordWeights.FirstOrDefault()?.Delivery?.Name
          ?? recordTrucks.FirstOrDefault()?.Client?.Name,
        OfficeAddress = recordWeights.FirstOrDefault()?.Delivery?.OfficeAddress,
        PhoneOfficeAddress = recordWeights.FirstOrDefault()?.Delivery?.PhoneForOfficeAddress,
        AgentAddress = recordWeights.FirstOrDefault()?.Delivery?.AgentAddress,
        PhoneAgentAddress = recordWeights.FirstOrDefault()?.Delivery?.PhoneForAgentAddress,
      };

      await DownloadFileWorkReport_TongHop(
        "Template_SH",
        "TemplateTableSH",
        recordWeights,
        recordTrucks,
        licensePlates,
        inforCompanyDTO_A,
        inforCompanyDTO_B);
    }




    private async void btnBiwaseScrapSale_Click(object sender, EventArgs e)
    {
      using var uiLock = LockExportUi();

      List<RecordWeight> recordWeights = SelectedData
        .Select(dto => dto.RecordWeight)
        .Where(record => record is not null)
        .Select(record => record!)
        .ToList();

      List<RecordTruck> recordTrucks = SelectedData
        .Select(dto => dto.RecordTruck)
        .Where(record => record is not null)
        .Select(record => record!)
        .ToList();

      List<string> licensePlates = recordWeights
        .Select(record => record.LicensePlate)
        .Concat(recordTrucks.Select(record => record.LicensePlate))
        .Where(licensePlate => !string.IsNullOrWhiteSpace(licensePlate))
        .Select(licensePlate => licensePlate!.Trim())
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .ToList();

      if (recordWeights.Count == 0 && recordTrucks.Count == 0)
      {
        using var popupMsg = new PopupConfirm(
          "Không có dữ liệu hợp lệ để xuất báo cáo !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupMsg.ShowDialog(this);
        return;
      }

      InforCompanyDTO inforCompanyDTO_A = new InforCompanyDTO()
      {
        Company = AppCore.Ins._appConfig?.Company,
        OfficeAddress = AppCore.Ins._appConfig?.OfficeAddress,
        PhoneOfficeAddress = AppCore.Ins._appConfig?.PhoneForOfficeAddress,
        AgentAddress = AppCore.Ins._appConfig?.AgentAddress,
        PhoneAgentAddress = AppCore.Ins._appConfig?.PhoneForAgentAddress,
      };

      InforCompanyDTO inforCompanyDTO_B = new InforCompanyDTO()
      {
        Company = recordWeights.FirstOrDefault()?.Delivery?.Name
          ?? recordTrucks.FirstOrDefault()?.Client?.Name,
        OfficeAddress = recordWeights.FirstOrDefault()?.Delivery?.OfficeAddress,
        PhoneOfficeAddress = recordWeights.FirstOrDefault()?.Delivery?.PhoneForOfficeAddress,
        AgentAddress = recordWeights.FirstOrDefault()?.Delivery?.AgentAddress,
        PhoneAgentAddress = recordWeights.FirstOrDefault()?.Delivery?.PhoneForAgentAddress,
      };

      await DownloadFileWorkReport_Biwase(
        "Template_W1",
        "TemplateTableSH",
        recordWeights,
        recordTrucks,
        licensePlates,
        inforCompanyDTO_A,
        inforCompanyDTO_B);
    }


    private async void btnBiwaseCNTT_Click(object sender, EventArgs e)
    {
      using var uiLock = LockExportUi();

      List<RecordWeight> recordWeights = SelectedData
        .Select(dto => dto.RecordWeight)
        .Where(record => record is not null)
        .Select(record => record!)
        .ToList();

      List<RecordTruck> recordTrucks = SelectedData
        .Select(dto => dto.RecordTruck)
        .Where(record => record is not null)
        .Select(record => record!)
        .ToList();

      List<string> licensePlates = recordWeights
        .Select(record => record.LicensePlate)
        .Concat(recordTrucks.Select(record => record.LicensePlate))
        .Where(licensePlate => !string.IsNullOrWhiteSpace(licensePlate))
        .Select(licensePlate => licensePlate!.Trim())
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .ToList();

      if (recordWeights.Count == 0 && recordTrucks.Count == 0)
      {
        using var popupMsg = new PopupConfirm(
          "Không có dữ liệu hợp lệ để xuất báo cáo !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupMsg.ShowDialog(this);
        return;
      }

      InforCompanyDTO inforCompanyDTO_A = new InforCompanyDTO()
      {
        Company = AppCore.Ins._appConfig?.Company,
        OfficeAddress = AppCore.Ins._appConfig?.OfficeAddress,
        PhoneOfficeAddress = AppCore.Ins._appConfig?.PhoneForOfficeAddress,
        AgentAddress = AppCore.Ins._appConfig?.AgentAddress,
        PhoneAgentAddress = AppCore.Ins._appConfig?.PhoneForAgentAddress,
      };

      InforCompanyDTO inforCompanyDTO_B = new InforCompanyDTO()
      {
        Company = recordWeights.FirstOrDefault()?.Delivery?.Name
          ?? recordTrucks.FirstOrDefault()?.Client?.Name,
        OfficeAddress = recordWeights.FirstOrDefault()?.Delivery?.OfficeAddress,
        PhoneOfficeAddress = recordWeights.FirstOrDefault()?.Delivery?.PhoneForOfficeAddress,
        AgentAddress = recordWeights.FirstOrDefault()?.Delivery?.AgentAddress,
        PhoneAgentAddress = recordWeights.FirstOrDefault()?.Delivery?.PhoneForAgentAddress,
      };

      await DownloadFileWorkReport_Biwase(
        "Template_W2",
        "TemplateTableSH",
        recordWeights,
        recordTrucks,
        licensePlates,
        inforCompanyDTO_A,
        inforCompanyDTO_B);
    }









    private static bool HasProductGroupName(
      RecordWeightDTO dto,
      string groupName)
    {
      return dto.RecordWeight?.Product?.ProductGroup?.Name?.Contains(
          groupName, StringComparison.OrdinalIgnoreCase) == true ||
        dto.RecordTruck?.TypeGoods?.ProductGroup?.Name?.Contains(
          groupName, StringComparison.OrdinalIgnoreCase) == true;
    }

    private IDisposable LockExportUi()
    {
      return new ExportUiExecutionScope(
        this,
        panel1,
        panel2,
        panel3,
        btnClose);
    }

    private sealed class ExportUiExecutionScope : IDisposable
    {
      private readonly Form _form;
      private readonly (Control Control, bool WasEnabled)[] _controls;
      private readonly bool _wasUseWaitCursor;
      private bool _disposed;

      public ExportUiExecutionScope(Form form, params Control[] controls)
      {
        _form = form;
        _wasUseWaitCursor = form.UseWaitCursor;
        _controls = controls
          .Select(control => (control, control.Enabled))
          .ToArray();

        foreach (var item in _controls)
          item.Control.Enabled = false;

        form.UseWaitCursor = true;
      }

      public void Dispose()
      {
        if (_disposed)
          return;

        _disposed = true;
        if (_form.IsDisposed)
          return;

        foreach (var item in _controls)
        {
          if (!item.Control.IsDisposed)
            item.Control.Enabled = item.WasEnabled;
        }

        _form.UseWaitCursor = _wasUseWaitCursor;
      }
    }



    #region Export PDF Biwase
    private async Task DownloadFileWorkReport_Biwase(
      string nameFileTemplate,
      string nameFileTable,
      List<RecordWeight> recordWeights,
      List<RecordTruck> recordTrucks,
      List<string> licensePlates,
      InforCompanyDTO inforCompanyA,
      InforCompanyDTO inforCompanyB)
    {
      try
      {
        DateTime dt = DateTime.Now;

        var pdfPath = await AppCore.Ins.ExportPdfGoods_Biwase(
          nameFileTemplate,
          nameFileTable,
          dt,
          licensePlates,
          recordWeights,
          inforCompanyA,
          inforCompanyB,
          recordTrucks);

        var openReportFile = false;
        using (var popup = new PopupConfirm(
            "Tạo phiếu thành công. Bạn có muốn mở file không?",
            EnumTypeMsg.Confirm,
            EnumImageMsg.Question))
        {
          popup.OnSendConfirm += (_, response) =>
            openReportFile = response.EnumResponsible == EnumResponsible.Confirm;
          popup.ShowDialog(this);
        }

        if (openReportFile)
        {
          try
          {
            Process.Start(new ProcessStartInfo
            {
              FileName = pdfPath,
              UseShellExecute = true,
            });
          }
          catch (Exception openException)
          {
            HelperManager.LogHelper.LogErrorToFileLog(openException, AppCore.Ins._folderFileLog);
            using var openErrorPopup = new PopupConfirm(
              "Đã tạo phiếu nhưng không thể mở file.",
              EnumTypeMsg.MessageManualClose,
              EnumImageMsg.Warning);
            openErrorPopup.ShowDialog(this);
          }
        }
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        using var popupMsg = new PopupConfirm(
          "Không thể tạo phiếu. Vui lòng thử lại !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupMsg.ShowDialog(this);
      }
    }
    #endregion

    private async Task DownloadFileWorkReport_TongHop(
      string nameFileTemplate,
      string nameFileTable,
      List<RecordWeight> recordWeights,
      List<RecordTruck> recordTrucks,
      List<string> licensePlates,
      InforCompanyDTO inforCompanyA,
      InforCompanyDTO inforCompanyB)
    {
      try
      {
        DateTime dt = DateTime.Now;

        var pdfPath = await AppCore.Ins.ExportPdfGoods_TongHop(
          nameFileTemplate,
          nameFileTable,
          dt,
          licensePlates,
          recordWeights,
          inforCompanyA,
          inforCompanyB,
          recordTrucks);

        var openReportFile = false;
        using (var popup = new PopupConfirm(
            "Tạo phiếu thành công. Bạn có muốn mở file không?",
            EnumTypeMsg.Confirm,
            EnumImageMsg.Question))
        {
          popup.OnSendConfirm += (_, response) =>
            openReportFile = response.EnumResponsible == EnumResponsible.Confirm;
          popup.ShowDialog(this);
        }

        if (openReportFile)
        {
          try
          {
            Process.Start(new ProcessStartInfo
            {
              FileName = pdfPath,
              UseShellExecute = true,
            });
          }
          catch (Exception openException)
          {
            HelperManager.LogHelper.LogErrorToFileLog(openException, AppCore.Ins._folderFileLog);
            using var openErrorPopup = new PopupConfirm(
              "Đã tạo phiếu nhưng không thể mở file.",
              EnumTypeMsg.MessageManualClose,
              EnumImageMsg.Warning);
            openErrorPopup.ShowDialog(this);
          }
        }
      }
      catch (Exception ex)
      {
        HelperManager.LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        using var popupMsg = new PopupConfirm(
          "Không thể tạo phiếu. Vui lòng thử lại !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popupMsg.ShowDialog(this);
      }
    }

    private Task DownloadHcpSummaryReportAsync(
      List<RecordWeight> recordWeights,
      List<RecordTruck> recordTrucks,
      string templateFileName = "Template_BBGN TỔNG HCP.doc")
    {
      try
      {
        var summariesByTag = recordWeights
          .Where(record => !string.IsNullOrWhiteSpace(
            record.Product?.TagMappingReport))
          .Select(record => new
          {
            Tag = record.Product.TagMappingReport!.Trim(),
            Weight = record.Net,
            Quantity = 1,
          })
          .Concat(recordTrucks
            .Where(record => !string.IsNullOrWhiteSpace(
              record.TypeGoods?.TagMappingReport))
            .Select(record => new
            {
              Tag = record.TypeGoods!.TagMappingReport!.Trim(),
              Weight = Math.Max(0, Math.Abs(record.NetTime01)),
              Quantity = 1,
            }))
          .GroupBy(item => item.Tag, StringComparer.OrdinalIgnoreCase)
          .ToDictionary(
            group => group.Key,
            group => new TagReportSummary(
              group.Sum(item => item.Weight),
              group.Sum(item => item.Quantity)),
            StringComparer.OrdinalIgnoreCase);

        var vehicleReportEntries = recordWeights
          .Where(record => !string.IsNullOrWhiteSpace(
            record.Product?.TagMappingReport))
          .Select(record => (
            Tag: record.Product!.TagMappingReport!.Trim(),
            LicensePlate: record.LicensePlate))
          .Concat(recordTrucks
            .Where(record => !string.IsNullOrWhiteSpace(
              record.TypeGoods?.TagMappingReport))
            .Select(record => (
              Tag: record.TypeGoods!.TagMappingReport!.Trim(),
              LicensePlate: record.LicensePlate)))
          .ToList();

        var pdfPath = ExportWordTemplateToPdf(
          summariesByTag, vehicleReportEntries, templateFileName);
        var openReportFile = false;
        using (var popup = new PopupConfirm(
          "Tạo phiếu thành công. Bạn có muốn mở file không?",
          EnumTypeMsg.Confirm,
          EnumImageMsg.Question))
        {
          popup.OnSendConfirm += (_, response) =>
            openReportFile = response.EnumResponsible == EnumResponsible.Confirm;
          popup.ShowDialog(this);
        }

        if (openReportFile)
        {
          Process.Start(new ProcessStartInfo
          {
            FileName = pdfPath,
            UseShellExecute = true,
          });
        }
      }
      catch (Exception ex)
      {
        LogHelper.LogErrorToFileLog(ex, AppCore.Ins._folderFileLog);
        using var popup = new PopupConfirm(
          "Không thể tạo phiếu từ template Word. Vui lòng kiểm tra Microsoft Word và thử lại !",
          EnumTypeMsg.MessageManualClose,
          EnumImageMsg.Warning);
        popup.ShowDialog(this);
      }

      return Task.CompletedTask;
    }

    private static string ExportWordTemplateToPdf(
      IReadOnlyDictionary<string, TagReportSummary> summariesByTag,
      IReadOnlyList<(string Tag, string? LicensePlate)> vehicleReportEntries,
      string templateFileName)
    {
      var exportDate = DateTime.Now;
      const int wdAlertsNone = 0;
      const int wdDoNotSaveChanges = 0;
      const int wdExportFormatPdf = 17;

      var templatePath = Path.Combine(
        Application.StartupPath,
        "Template",
        templateFileName);
      if (!File.Exists(templatePath))
        throw new FileNotFoundException("Không tìm thấy template báo cáo HCP.", templatePath);

      var outputFolder = Path.Combine(Application.StartupPath, "ReportGoods");
      Directory.CreateDirectory(outputFolder);
      var fileName = $"{Path.GetFileNameWithoutExtension(templateFileName)}_{exportDate:yyMMddHHmmssfff}";
      var workingDocPath = Path.Combine(outputFolder, $"{fileName}.doc");
      var pdfPath = Path.Combine(outputFolder, $"{fileName}.pdf");
      File.Copy(templatePath, workingDocPath, overwrite: true);

      dynamic? wordApplication = null;
      dynamic? document = null;
      try
      {
        var wordType = Type.GetTypeFromProgID("Word.Application")
          ?? throw new InvalidOperationException("Microsoft Word chưa được cài đặt.");
        wordApplication = Activator.CreateInstance(wordType)
          ?? throw new InvalidOperationException("Không thể khởi động Microsoft Word.");
        wordApplication.Visible = false;
        wordApplication.DisplayAlerts = wdAlertsNone;
        document = wordApplication.Documents.Open(workingDocPath);

        var (templateTags, templateWeightTags) =
          GetReportTagsInDocument((object)document);
        ReplaceQuantityCellPlaceholders(document, summariesByTag);
        var licensePlates = vehicleReportEntries
          .Where(entry => templateTags.Contains(entry.Tag) &&
            !string.IsNullOrWhiteSpace(entry.LicensePlate))
          .Select(entry => entry.LicensePlate!.Trim())
          .Distinct(StringComparer.OrdinalIgnoreCase)
          .ToList();
        var totalWeight = summariesByTag
          .Where(entry => templateWeightTags.Contains(entry.Key))
          .Sum(entry => entry.Value.Weight);
        ReplaceWordTexts(
          document, summariesByTag, licensePlates, exportDate, totalWeight);

        document.ExportAsFixedFormat(pdfPath, wdExportFormatPdf);
        document.Close(wdDoNotSaveChanges);
        Marshal.FinalReleaseComObject(document);
        document = null;
        wordApplication.Quit(wdDoNotSaveChanges);
        Marshal.FinalReleaseComObject(wordApplication);
        wordApplication = null;

        if (!File.Exists(pdfPath) || new FileInfo(pdfPath).Length == 0)
          throw new InvalidOperationException("Microsoft Word không tạo được file PDF.");

        return pdfPath;
      }
      finally
      {
        if (document is not null)
        {
          try { document.Close(wdDoNotSaveChanges); } catch { }
          if (Marshal.IsComObject(document))
            Marshal.FinalReleaseComObject(document);
        }

        if (wordApplication is not null)
        {
          try { wordApplication.Quit(wdDoNotSaveChanges); } catch { }
          if (Marshal.IsComObject(wordApplication))
            Marshal.FinalReleaseComObject(wordApplication);
        }

        try { File.Delete(workingDocPath); } catch { }
      }
    }

    private sealed class TagReportSummary
    {
      public double Weight { get; }
      public int Quantity { get; }

      public TagReportSummary(double weight, int quantity)
      {
        Weight = weight;
        Quantity = quantity;
      }
    }

    private static (HashSet<string> Tags, HashSet<string> WeightTags)
      GetReportTagsInDocument(dynamic document)
    {
      var tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
      var weightTags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

      for (var storyType = 1; storyType <= 17; storyType++)
      {
        dynamic? storyRange = null;
        try
        {
          storyRange = document.StoryRanges[storyType];
          while (storyRange is not null)
          {
            var storyText = Convert.ToString(storyRange.Text) ?? string.Empty;
            foreach (Match match in ReportPlaceholderRegex.Matches(storyText))
            {
              var tag = match.Groups["tag"].Value.Trim();
              var kind = match.Groups["kind"].Value;
              if (string.Equals(tag, "tong", StringComparison.OrdinalIgnoreCase) &&
                  string.Equals(kind, "weight", StringComparison.OrdinalIgnoreCase))
              {
                continue;
              }

              tags.Add(tag);
              if (string.Equals(kind, "weight", StringComparison.OrdinalIgnoreCase))
              {
                weightTags.Add(tag);
              }
            }

            dynamic? nextRange = storyRange.NextStoryRange;
            if (Marshal.IsComObject(storyRange))
              Marshal.FinalReleaseComObject(storyRange);
            storyRange = nextRange;
          }
        }
        catch (COMException)
        {
          if (storyRange is not null && Marshal.IsComObject(storyRange))
            Marshal.FinalReleaseComObject(storyRange);
        }
      }

      return (tags, weightTags);
    }

    private static void ReplaceWordTexts(
      dynamic document,
      IReadOnlyDictionary<string, TagReportSummary> summariesByTag,
      IReadOnlyList<string> licensePlates,
      DateTime exportDate,
      double totalWeight)
    {
      const int wdFindContinue = 1;
      const int wdReplaceAll = 2;

      for (var storyType = 1; storyType <= 17; storyType++)
      {
        dynamic? storyRange = null;
        try
        {
          storyRange = document.StoryRanges[storyType];
          while (storyRange is not null)
          {
            var storyText = Convert.ToString(storyRange.Text) ?? string.Empty;
            var storyReplacements = new Dictionary<string, string>(
              StringComparer.OrdinalIgnoreCase);
            foreach (Match match in ReportPlaceholderRegex.Matches(storyText))
            {
              var placeholder = match.Value;
              if (storyReplacements.ContainsKey(placeholder))
                continue;

              var tag = match.Groups["tag"].Value.Trim();
              var kind = match.Groups["kind"].Value;
              if (string.Equals(tag, "tong", StringComparison.OrdinalIgnoreCase) &&
                  string.Equals(kind, "weight", StringComparison.OrdinalIgnoreCase))
              {
                storyReplacements[placeholder] =
                  WeightFormatHelper.Format(totalWeight, 3);
              }
              else if (summariesByTag.TryGetValue(tag, out var summary))
              {
                storyReplacements[placeholder] =
                  string.Equals(kind, "qty", StringComparison.OrdinalIgnoreCase)
                    ? summary.Quantity.ToString()
                    : WeightFormatHelper.Format(summary.Weight, 3);
              }
              else
              {
                storyReplacements[placeholder] =
                  string.Equals(kind, "qty", StringComparison.OrdinalIgnoreCase)
                    ? "..."
                    : "0.0";
              }
            }

            foreach (Match match in LicensePlatePlaceholderRegex.Matches(storyText))
            {
              var placeholder = match.Value;
              if (storyReplacements.ContainsKey(placeholder))
                continue;

              var plateIndex = int.Parse(match.Groups["index"].Value) - 1;
              storyReplacements[placeholder] =
                plateIndex >= 0 && plateIndex < licensePlates.Count &&
                plateIndex < 4
                  ? licensePlates[plateIndex]
                  : "...................";
            }

            foreach (Match match in ExportDatePlaceholderRegex.Matches(storyText))
            {
              var placeholder = match.Value;
              if (storyReplacements.ContainsKey(placeholder))
                continue;

              storyReplacements[placeholder] = match.Groups["part"].Value
                .ToLowerInvariant() switch
                {
                  "ngay" => exportDate.Day.ToString(),
                  "thang" => exportDate.Month.ToString(),
                  "nam" => exportDate.Year.ToString(),
                  _ => string.Empty,
                };
            }

            foreach (var replacement in storyReplacements)
            {
              dynamic? searchRange = null;
              dynamic? find = null;
              try
              {
                searchRange = storyRange.Duplicate;
                find = searchRange.Find;
                find.ClearFormatting();
                find.Replacement.ClearFormatting();
                find.Text = replacement.Key;
                find.Replacement.Text = replacement.Value;
                find.Forward = true;
                find.Wrap = wdFindContinue;
                find.Format = false;
                find.MatchCase = false;
                find.MatchWholeWord = false;
                find.Execute(Replace: wdReplaceAll);
              }
              finally
              {
                if (find is not null && Marshal.IsComObject(find))
                  Marshal.FinalReleaseComObject(find);
                if (searchRange is not null && Marshal.IsComObject(searchRange))
                  Marshal.FinalReleaseComObject(searchRange);
              }
            }

            dynamic? nextRange = storyRange.NextStoryRange;
            Marshal.FinalReleaseComObject(storyRange);
            storyRange = nextRange;
          }
        }
        catch (COMException)
        {
          if (storyRange is not null && Marshal.IsComObject(storyRange))
            Marshal.FinalReleaseComObject(storyRange);
        }
      }
    }

    private static void ReplaceQuantityCellPlaceholders(
      dynamic document,
      IReadOnlyDictionary<string, TagReportSummary> summariesByTag)
    {
      for (var tableIndex = 1; tableIndex <= document.Tables.Count; tableIndex++)
      {
        dynamic? table = null;
        try
        {
          table = document.Tables.Item(tableIndex);
          var cellCount = table.Range.Cells.Count;
          for (var cellIndex = 1; cellIndex <= cellCount; cellIndex++)
          {
            dynamic? cell = null;
            dynamic? cellRange = null;
            try
            {
              cell = table.Range.Cells.Item(cellIndex);
              cellRange = cell.Range;
              string cellText = Convert.ToString((object)cellRange.Text)
                ?? string.Empty;
              if (!cellText.Contains(
                    "Số lượng", StringComparison.OrdinalIgnoreCase))
              {
                continue;
              }

              var matches = ReportPlaceholderRegex.Matches(cellText)
                .Cast<Match>()
                .OrderByDescending(match => match.Index)
                .ToList();
              foreach (var match in matches)
              {
                var tag = match.Groups["tag"].Value.Trim();
                var replacement = summariesByTag.TryGetValue(tag, out var summary)
                  ? summary.Quantity.ToString()
                  : "...";

                dynamic? replacementRange = null;
                try
                {
                  replacementRange = cellRange.Duplicate;
                  var start = cellRange.Start + match.Index;
                  replacementRange.SetRange(start, start + match.Length);
                  replacementRange.Text = replacement;
                }
                finally
                {
                  if (replacementRange is not null &&
                      Marshal.IsComObject(replacementRange))
                  {
                    Marshal.FinalReleaseComObject(replacementRange);
                  }
                }
              }
            }
            finally
            {
              if (cellRange is not null && Marshal.IsComObject(cellRange))
                Marshal.FinalReleaseComObject(cellRange);
              if (cell is not null && Marshal.IsComObject(cell))
                Marshal.FinalReleaseComObject(cell);
            }
          }
        }
        finally
        {
          if (table is not null && Marshal.IsComObject(table))
            Marshal.FinalReleaseComObject(table);
        }
      }
    }


    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    
  }
}
