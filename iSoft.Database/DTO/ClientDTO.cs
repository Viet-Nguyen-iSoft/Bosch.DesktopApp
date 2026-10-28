using iSoft.Database.Models;
using System.ComponentModel;

namespace iSoft.Database.DTO
{
  public class ClientDTO
  {
    [Browsable(false)]
    public ClientDelivery? Client { get; set; }


    [DisplayName("Stt")]
    public int? No { get; set; }

    [DisplayName("Tên")]
    public string? Name { get; set; }

    [DisplayName("Mô tả")]
    public string? Description { get; set; }
    [DisplayName("Cập nhật")]
    public string? UpdatedAt { get; set; }
  }

  public enum EnumStatusDataPO
  {
    None = 0,
    NotStarted,
    Active,
    Expired,
    NotAprove,
  }
}
