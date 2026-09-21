using iSoft.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSyncData.Record
{
  public class RecordWeightSync
  {
    public Guid Id { get; set; }
    public double Net { get; set; }
    public double Tare { get; set; }
    public string? LicensePlate { get; set; }
    public Guid? StationId { get; set; }
    public Guid? EmployeeId { get; set; }
    public Guid? ProductId { get; set; }
    public Guid? CategoryTareId { get; set; }
    public Guid? RecordTruckId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
  }
}
