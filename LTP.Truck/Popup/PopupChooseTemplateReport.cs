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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Common.EnumData;

namespace LTP.Truck.Popup
{
  public partial class PopupChooseTemplateReport : Form
  {
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
          "Không có dữ liệu HcP hợp lệ để xuất báo cáo !",
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

    private async void btnHcP_CN_Click(object sender, EventArgs e)
    {
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
          "Không có dữ liệu HcP hợp lệ để xuất báo cáo !",
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

    private async void btnHcP_SH_Click(object sender, EventArgs e)
    {
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
          "Không có dữ liệu HcP hợp lệ để xuất báo cáo !",
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



    private async void btnTGA_TH_Click(object sender, EventArgs e)
    {
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
          "Không có dữ liệu HcP hợp lệ để xuất báo cáo !",
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









    private static bool HasProductGroupName(
      RecordWeightDTO dto,
      string groupName)
    {
      return dto.RecordWeight?.Product?.ProductGroup?.Name?.Contains(
          groupName, StringComparison.OrdinalIgnoreCase) == true ||
        dto.RecordTruck?.TypeGoods?.ProductGroup?.Name?.Contains(
          groupName, StringComparison.OrdinalIgnoreCase) == true;
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


    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    
  }
}
