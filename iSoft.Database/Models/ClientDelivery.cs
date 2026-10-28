using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iSoft.Database.EnumData;

namespace iSoft.Database.Models
{
  public class ClientDelivery : BaseModel
  {
    public string? Name { get; set; }

    public string? OfficeAddress { get; set; }
    public string? PhoneForOfficeAddress { get; set; }

    public string? AgentAddress { get; set; }
    public string? PhoneForAgentAddress { get; set; }
    public string? Description { get; set; }

    public EnumTypeDataLocation EnumTypeData { get; set; }

    #region Mapping
    public ICollection<RecordTruck> RecordTrucks { get; set; } = new List<RecordTruck>();
    public ICollection<RecordWeight> RecordWeights { get; set; } = new List<RecordWeight>();
    #endregion
  }
}
