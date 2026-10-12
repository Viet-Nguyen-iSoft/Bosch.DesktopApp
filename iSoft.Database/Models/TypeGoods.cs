using static iSoft.Database.EnumData;

namespace iSoft.Database.Models
{
  public class TypeGoods : BaseModel
  {
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public EnumWasteType EnumWasteType { get; set; }

    #region Mapping
    public Guid? ProductGroupId { get; set; }
    public ProductGroup? ProductGroup { get; set; }
    public ICollection<RecordTruck> RecordTrucks { get; set; } = new List<RecordTruck>();
    #endregion
  }
}
