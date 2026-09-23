using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Database.Models
{
  public class LicensePlate : BaseModel
  {
    public string? Plate { get; set; }
    public string? Description { get; set; }
  }
}
