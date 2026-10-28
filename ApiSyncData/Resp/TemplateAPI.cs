namespace ApiSyncData.Resp
{
  public class TemplateAPI
  {
    public string? Status { get; set; }
    public DataTemplate? Data { get; set; }
  }

  public class DataTemplate
  {
    public int? TotalRecord { get; set; }
    public List<ListDatumTemplate>? ListData { get; set; }
  }

  public class ListDatumTemplate : IServerRecord
  {
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public string? SerialCode { get; set; }
    public string? TemplateFile { get; set; }
    public int Order { get; set; }
    public bool IsBBGN { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool? IsDelete { get; set; }
  }
}
