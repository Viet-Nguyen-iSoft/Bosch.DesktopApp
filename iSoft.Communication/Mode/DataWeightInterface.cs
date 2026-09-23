using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iSoft.Communication.EnumCommunication;

namespace iSoft.Communication.Mode
{
  public class DataWeightInterface
  {
    public double IndicatedWeight { get; set; } = 0.0;
    public double TareWeight { get; set; } = 0.0;
    public UnitOfWeight Unit;
    public ActiveWeighingStatus ActiveWeighingStatus { get; set; }
  }
}
