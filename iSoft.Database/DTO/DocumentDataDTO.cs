using iSoft.Database.Models;
using System.ComponentModel;

namespace iSoft.Database.DTO
{
  public class DocumentDataDTO
  {
    [Browsable(false)]
    public DocumentData? DocumentData { get; set; }

    [DisplayName("Stt")]
    public int No { get; set; }

    [DisplayName("Ngày")]
    public string? Date { get; set; }

    [DisplayName("Chứng từ")]
    public string? Document { get; set; }

    [DisplayName("Mô tả")]
    public string? Description { get; set; }

    [DisplayName("Cập nhật")]
    public string? UpdatedAt { get; set; }
  }
}
