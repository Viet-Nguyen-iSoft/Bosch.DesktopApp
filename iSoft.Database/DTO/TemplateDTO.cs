using System.ComponentModel;
using iSoft.Database.Models;

namespace iSoft.Database.DTO
{
  public class TemplateDTO
  {
    [Browsable(false)]
    public Template? Template { get; set; }

    [DisplayName("Stt")]
    public int No { get; set; }

    [DisplayName("Mã")]
    public string? SerialCode { get; set; }

    [DisplayName("Tên template")]
    public string? Name { get; set; }

    [DisplayName("File template")]
    public string? TemplateFile { get; set; }

    [DisplayName("Thứ tự")]
    public int Order { get; set; }

    [DisplayName("Loại")]
    public string Type { get; set; } = string.Empty;

    [DisplayName("Cập nhật")]
    public string? UpdatedAt { get; set; }
  }
}
