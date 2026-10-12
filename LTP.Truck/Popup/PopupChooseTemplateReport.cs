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
      List<RecordWeight> recordWeights = SelectedData
        .Where(dto => string.Equals(dto.Source, "Cân hàng", StringComparison.OrdinalIgnoreCase))
        .Select(dto => dto.RecordWeight)
        .Where(record => record is not null)
        .Select(record => record!)
        .ToList();

      List<RecordTruck> recordTrucks = SelectedData
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

      await DownloadFileWorkReport(
        recordWeights,
        recordTrucks,
        licensePlates,
        inforCompanyDTO_A,
        inforCompanyDTO_B);
    }



    #region Export PDF Goods
    private async Task DownloadFileWorkReport(
      List<RecordWeight> recordWeights,
      List<RecordTruck> recordTrucks,
      List<string> licensePlates,
      InforCompanyDTO inforCompanyA,
      InforCompanyDTO inforCompanyB)
    {
      try
      {
        DateTime dt = DateTime.Now;

        var pdfPath = await AppCore.Ins.ExportPdfGoods(
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

    private static string FormatWeight(double value)
    {
      return WeightFormatHelper.Format(value);
    }

    #endregion

    private void btnClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }
}
