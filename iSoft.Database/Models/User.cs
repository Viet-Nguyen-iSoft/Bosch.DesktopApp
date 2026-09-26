using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iSoft.Database.Models
{
  public class User : BaseModel
  {
    [Required]
    [MaxLength(255)]
    public string FullName { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? EmployeeCode { get; set; }

    [MaxLength(255)]
    public string? IdCardCode { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Password { get; set; }

    [MaxLength(50)]
    public string? DisplayName { get; set; }

    [Required]
    [MaxLength(10)]
    public string Role { get; set; } = string.Empty;

    public ICollection<RecordTruck>? RecordTrucks { get; set; }
    public ICollection<RecordWeight>? RecordWeights { get; set; }
  }
}
