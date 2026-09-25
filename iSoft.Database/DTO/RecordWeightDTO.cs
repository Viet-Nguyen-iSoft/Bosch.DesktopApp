using iSoft.Database.Models;
using System.ComponentModel;

namespace iSoft.Database.DTO
{
  public class RecordWeightDTO
  {
    [Browsable(false)]
    public RecordWeight? RecordWeight { get; set; }

    [DisplayName("Stt")]
    public int No { get; set; }

    [DisplayName("Thời gian")]
    public string? Datetime { get; set; }
    [DisplayName("Bên giao")]
    public string? Delivery { get; set; }
    [DisplayName("Biển số xe")]
    public string? LicensePlate { get; set; }
    [DisplayName("Tên tài xế")]
    public string? NameDriver { get; set; }

    [DisplayName("Nhóm")]
    public string? ProductGroup { get; set; }

    [DisplayName("Phế phẩm")]
    public string? Product { get; set; }

    [DisplayName("Loại bì")]
    public string? CategoryTare { get; set; }
    [DisplayName("Gross (Kg)")]
    public string? Gross { get; set; }

    [DisplayName("Net (Kg)")]
    public string? Net { get; set; }

    [DisplayName("Tare (Kg)")]
    public string? Tare { get; set; }
  }
}
