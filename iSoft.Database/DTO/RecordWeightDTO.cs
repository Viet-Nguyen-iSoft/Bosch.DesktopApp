using iSoft.Database.Models;
using System.ComponentModel;

namespace iSoft.Database.DTO
{
  public class RecordWeightDTO
  {
    [Browsable(false)]
    public RecordWeight? RecordWeight { get; set; }

    [Browsable(false)]
    public RecordTruck? RecordTruck { get; set; }

    [Browsable(false)]
    public DateTime SortAt { get; set; }

    [DisplayName("Stt")]
    public int No { get; set; }

    [DisplayName("Thời gian")]
    public string? Datetime { get; set; }

    [DisplayName("Nguồn")]
    public string? Source { get; set; }

    [DisplayName("Nhóm")]
    public string? ProductGroup { get; set; }

    [DisplayName("Loại")]
    public string? Type { get; set; }

    [DisplayName("Chất thải / Kho")]
    public string? Product { get; set; }

    [DisplayName("Loại bì")]
    public string? CategoryTare { get; set; }
    [DisplayName("Gross (Kg)")]
    public string? Gross { get; set; }

    [DisplayName("Net (Kg)")]
    public string? Net { get; set; }

    [DisplayName("Tare (Kg)")]
    public string? Tare { get; set; }

    [DisplayName("Bên nhận")]
    public string? Delivery { get; set; }
    [DisplayName("Biển số xe")]
    public string? LicensePlate { get; set; }

    [Browsable(false)]
    [DisplayName("Tên tài xế")]
    public string? NameDriver { get; set; }

    
  }
}
