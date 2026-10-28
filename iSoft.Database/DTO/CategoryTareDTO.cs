using iSoft.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Database.DTO
{
  public class CategoryTareDTO
  {
    [Browsable(false)]
    public Tare? CategoryTare { get; set; }


    [DisplayName("Stt")]
    public int? No { get; set; }

    [DisplayName("Mã")]
    public string? Code { get; set; }

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
