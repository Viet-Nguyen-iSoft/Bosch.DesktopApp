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
      List<RecordWeight> exportData = SelectedData?.Where(x=>x.Source == "Cân hàng")
        .Select(dto => dto.RecordWeight!)
        .ToList();

      string licensePlate = exportData[0].LicensePlate?.Trim() ?? string.Empty;
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
        Company = exportData.FirstOrDefault()?.Delivery?.Name,
        OfficeAddress = exportData.FirstOrDefault()?.Delivery?.OfficeAddress,
        PhoneOfficeAddress = exportData.FirstOrDefault()?.Delivery?.PhoneForOfficeAddress,
        AgentAddress = exportData.FirstOrDefault()?.Delivery?.AgentAddress,
        PhoneAgentAddress = exportData.FirstOrDefault()?.Delivery?.PhoneForAgentAddress,
      };

      await DownloadFileWorkReport(exportData, licensePlate, inforCompanyDTO_A, inforCompanyDTO_B);
    }



    #region Export PDF Goods
    private async Task DownloadFileWorkReport(
      List<RecordWeight> exportData,
      string licensePlate,
      InforCompanyDTO inforCompanyA,
      InforCompanyDTO inforCompanyB)
    {
      try
      {
        DateTime dt = DateTime.Now;

        var pdfPath = await AppCore.Ins.ExportPdfGoods(dt, licensePlate, exportData, inforCompanyA, inforCompanyB);

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
  }
}
