using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HelperManager.EnumData;
using static iSoft.Database.EnumData;

namespace iSoft.Database.Models
{
  public class RecordWeight : BaseModel
  {
    public double Net { get; set; }
    public double Tare { get; set; }
    public string? LicensePlate { get; set; }
    public string? NameDriver { get; set; }
    public string? IdCard { get; set; }
    public string? Note { get; set; }


    #region Mapping
    public Guid? StationId { get; set; }
    public Station? Station { get; set; }

    public Guid? UserId { get; set; }
    public User? User { get; set; }

    public Guid? ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public Guid? CategoryTareId { get; set; }
    public CategoryTare CategoryTare { get; set; } = null!;

    public Guid? DeliveryId { get; set; }
    public Delivery Delivery { get; set; } = null!;

    public Guid? RecordTruckId { get; set; }
    public RecordTruck RecordTruck { get; set; } = null!;

    #endregion
  }

}
