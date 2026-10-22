using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Database.Models
{
  public class DocumentData : BaseModel
  {
    public DateTime? Date { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }

    #region Mapping
    
    #endregion
  }
}
