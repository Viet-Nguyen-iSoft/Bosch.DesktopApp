using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iSoft.Communication.EnumCommunication;

namespace iSoft.Communication.Interface
{
  public class MessageDataInput
  {
    public string? Source { get; set; }
    public Guid? MachineId { get; set; }
    public string? NameDevice { get; set; }

    public byte[]? DataAsBytes { get; set; }

    public string? DataAsString { get; set; }

    public eDataType eDataType { get; }
    public EnumModeCommunication eModeCommunication { get; set; }

    public EnumValueWeightType eValueWeightType { get; set; }

    public DateTime SourceDateTime { get; set; }
  }
}
