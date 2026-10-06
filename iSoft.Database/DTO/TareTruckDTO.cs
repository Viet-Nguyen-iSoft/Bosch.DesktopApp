using iSoft.Database.Models;
using System.ComponentModel;

namespace iSoft.Database.DTO
{
  public class TareTruckDTO
  {
    [Browsable(false)]
    public TareTruck? TareTruck { get; set; }

    [DisplayName("Stt")]
    public int? No { get; set; }

    [DisplayName("Tên")]
    public string? Name { get; set; }

    [DisplayName("Mô tả")]
    public string? Description { get; set; }

    [DisplayName("Khối lượng (Kg)")]
    public string? Value { get; set; }

    [DisplayName("Cập nhật")]
    public string? UpdatedAt { get; set; }
  }
}
