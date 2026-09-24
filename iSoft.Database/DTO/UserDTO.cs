using iSoft.Database.Models;
using System.ComponentModel;

namespace iSoft.Database.DTO
{
  public class UserDTO
  {
    [Browsable(false)]
    public User? User { get; set; }

    [DisplayName("Stt")]
    public int No { get; set; }

    [DisplayName("Họ và tên")]
    public string? FullName { get; set; }

    [DisplayName("Tên hiển thị")]
    public string? DisplayName { get; set; }

    [DisplayName("Tên đăng nhập")]
    public string? Username { get; set; }

    [DisplayName("Mã nhân viên")]
    public string? EmployeeCode { get; set; }

    [DisplayName("CCCD/CMND")]
    public string? IdCardCode { get; set; }

    [DisplayName("Cập nhật")]
    public string? UpdatedAt { get; set; }
  }
}
