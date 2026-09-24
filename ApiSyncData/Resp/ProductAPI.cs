using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSyncData.Resp
{
  public class DataProduct
  {
    public int? TotalRecord { get; set; }
    public List<ListDatumProduct>? ListData { get; set; }
  }

  public class ItemProductGroup
  {
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDelete { get; set; }
  }

  public class ListDatumProduct : IServerRecord
  {
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? SerialCode { get; set; }
    public string? Description { get; set; }
    public string? ProductGroupId { get; set; }
    public int? WasteType { get; set; }
    public ItemProductGroup? ItemProductGroup { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDelete { get; set; }
  }

  public class ProductAPI
  {
    public string? Status { get; set; }
    public DataProduct? Data { get; set; }
  }


}
