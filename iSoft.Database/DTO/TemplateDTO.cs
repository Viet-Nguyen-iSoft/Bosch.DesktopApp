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

    [DisplayName("Tên template")]
    public string? Name { get; set; }

    [DisplayName("File")]
    public string? PathLocal { get; set; }


    [DisplayName("Cập nhật")]
    public string? UpdatedAt { get; set; }
  }
}
