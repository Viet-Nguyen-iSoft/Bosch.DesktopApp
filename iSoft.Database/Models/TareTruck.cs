using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Database.Models
{
  public class TareTruck : BaseModel
  {
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double? Value { get; set; } = 0.0;

    #region Mapping
    #endregion
  }


}
