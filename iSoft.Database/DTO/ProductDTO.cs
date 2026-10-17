using iSoft.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Database.DTO
{
  public class ProductDTO
  {
    [Browsable(false)]
    public Product? Product { get; set; }


    [DisplayName("Stt")]
    public int? No { get; set; }
    [DisplayName("Nhóm")]
    public string? Group { get; set; }

    [DisplayName("Loại")]
    public string? WasteType { get; set; }

    [DisplayName("Mã")]
    public string? Code { get; set; }

    [DisplayName("Tên sản phẩm")]
    public string? Name { get; set; }

    [DisplayName("Tag mapping")]
    public string? TagMappingReport { get; set; }

    [DisplayName("Mô tả")]
    public string? Description { get; set; }
    [DisplayName("Cập nhật")]
    public string? UpdatedAt { get; set; }
  }
}
