using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Database.Models
{
  public class Role : BaseModel
  {
    public int? Type { get; set; } //1 Truck, 2 Goods
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
  }
}
