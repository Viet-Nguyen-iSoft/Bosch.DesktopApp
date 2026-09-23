using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HelperManager.EnumData;

namespace iSoft.Database.Models
{
  public class ApiJobs : BaseModel
  {
    public string? Json { get; set; }
    public string? Description { get; set; }
    public EnumTypeAPI? EnumTypeAPI { get; set; }
    public EnumStatusAPI? EnumStatusAPI { get; set; }
    public int? Retry { get; set; } = 0;
  }
}
