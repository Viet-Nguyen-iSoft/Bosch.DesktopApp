using iSoft.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Database.DTO
{
  public class TypeGoodsDTO
  {
    [Browsable(false)]
    public Product? TypeGoods { get; set; }


    [DisplayName("Stt")]
    public int? No { get; set; }
    [DisplayName("Nhóm")]
    public string? Group { get; set; }

    [DisplayName("Loại chất thải")]
    public string? WasteType { get; set; }

    [DisplayName("Mã")]
    public string? Code { get; set; }

    [DisplayName("Tên")]
    public string? Name { get; set; }

    [DisplayName("Mô tả")]
    public string? Description { get; set; }
    [DisplayName("Cập nhật")]
    public string? UpdatedAt { get; set; }

    [DisplayName("Tag mapping báo cáo")]
    public string? TagMappingReport { get; set; }
  }
}
