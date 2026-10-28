namespace iSoft.Database.Models
{
  public class Template : BaseModel
  {
    public string? Name { get; set; }
    public string? SerialCode { get; set; }
    public string? TemplateFile { get; set; }
    public int Order { get; set; }
    public bool IsBBGN { get; set; }
  }
}
