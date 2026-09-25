using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Database.Models
{
  public class AppConfig: BaseModel
  {
    public string? IpServer { get; set; }
    public int? PortServer { get; set; }
    public int? TimeoutConnectServer { get; set; }
    public Guid? StationId { get; set; }
    public string? Version { get; set; }

    public string? Key { get; set; }
    public string? NamePrintA4 { get; set; }
    public string? NamePrintLabel { get; set; }
    public bool PermitCheckWeight { get; set; } = false;
    public double ValueCheckWeight { get; set; } = 0;

    public string? Company { get; set; }
    public string? OfficeAddress { get; set; }
    public string? PhoneForOfficeAddress { get; set; }

    public string? AgentAddress { get; set; }
    public string? PhoneForAgentAddress { get; set; }


    public double? ValueWeightGoodsCheckPermitConfirm { get; set; } = 0.0;
  }
}
