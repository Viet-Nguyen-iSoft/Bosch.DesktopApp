using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperManager
{
  public static class KeyHelper
  {
    public static string CreateLabel(DateTime date, int sequenceNumber)
    {
      if (sequenceNumber <= 0 || sequenceNumber > 9999)
        throw new ArgumentOutOfRangeException(nameof(sequenceNumber),
          "Số thứ tự tem phải nằm trong khoảng từ 1 đến 9999.");

      return $"{date:yyMMdd}{sequenceNumber:D4}";
    }
  }
}
