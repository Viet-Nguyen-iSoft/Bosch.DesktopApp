using iSoft.Database.Models;
using System.ComponentModel;

namespace iSoft.Database.DTO
{
  public class DeliveryDTO
  {
    [Browsable(false)]
    public Delivery? Delivery { get; set; }

    [DisplayName("Stt")]
    public int? No { get; set; }

    [DisplayName("Tên công ty")]
    public string? Name { get; set; }

    [DisplayName("Địa chỉ văn phòng")]
    public string? OfficeAddress { get; set; }

    [DisplayName("ĐT văn phòng")]
    public string? PhoneForOfficeAddress { get; set; }

    [DisplayName("Địa chỉ cơ sở/đại lý")]
    public string? AgentAddress { get; set; }

    [DisplayName("ĐT cơ sở/đại lý")]
    public string? AgentAddressForOfficeAddress { get; set; }

    [DisplayName("Mô tả")]
    public string? Description { get; set; }

    [DisplayName("Cập nhật")]
    public string? UpdatedAt { get; set; }
  }
}
