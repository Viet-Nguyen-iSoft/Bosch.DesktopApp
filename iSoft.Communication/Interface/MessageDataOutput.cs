using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iSoft.Communication.EnumCommunication;

namespace iSoft.Communication.Interface
{
  public class MessageDataOutput
  {
    public string? Source { get; set; }
    public Guid? MachineId { get; set; }
    public string? NameDevice { get; set; }

    public double Net { get; set; } = 0;
    public double Tare { get; set; } = 0;

    public eValueWeightType eValueWeight { get; set; }

    public UnitOfWeight unitOfWeight { get; set; }

    public ActiveWeighingStatus ActiveWeighingStatus { get; set; }

    public DateTime SourceDateTime { get; set; }


    public byte[]? DataAsBytes { get; set; }

    public string? DataAsString { get; set; }
  }
}
