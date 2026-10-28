using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSyncData.Resp
{
  public class DataCategoryTare
  {
    public int? TotalRecord { get; set; }
    public List<ListDatumCategoryTare>? ListData { get; set; }
  }

  public class ListDatumCategoryTare : IServerRecord
  {
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? SerialCode { get; set; }
    public string? Description { get; set; }
    public double? WeightTare { get; set; }
    public int? TypeTare { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDelete { get; set; }
  }

  public class CategoryTareAPI
  {
    public string? Status { get; set; }
    public DataCategoryTare? Data { get; set; }
  }

}
