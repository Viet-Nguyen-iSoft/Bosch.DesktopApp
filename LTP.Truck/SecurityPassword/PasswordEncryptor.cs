using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LTP.Truck.SecurityPassword
{
  public static class PasswordEncryptor
  {
    // Khóa bí mật đồng bộ với hệ thống Web/Backend
    //private const string SecretKey = "7cf098ec94a84ea49b8fcc59337b9c28";
    private const string SecretKey = "3e7da5e8-9540-40ba-bd75-21fe19686e16";

    /// <summary>
    /// Mã hóa mật khẩu từ plainText (VD: "Admin@1234") 
    /// thành chuỗi URL-Safe Base64 (VD: "BNNgTHgnv02Dgr0pBcfK7LqWnrKQDX1fLF1ibepRw3E")
    /// </summary>
    public static string Encrypt(string plainText)
    {
      if (string.IsNullOrEmpty(plainText))
      {
        return plainText;
      }

      // 1. Chuẩn hóa SecretKey đúng 32 bytes (256-bit)
      byte[] keyBytes = Encoding.UTF8.GetBytes(SecretKey);
      byte[] key = new byte[32];
      Array.Copy(keyBytes, key, Math.Min(keyBytes.Length, 32));

      using (Aes aes = Aes.Create())
      {
        aes.Key = key;
        aes.GenerateIV(); // Tự động sinh ngẫu nhiên 16 bytes IV
        byte[] iv = aes.IV;

        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using (var ms = new MemoryStream())
        {
          // 2. Ghi 16 bytes IV vào đầu stream
          ms.Write(iv, 0, iv.Length);

          // 3. Mã hóa dữ liệu mật khẩu và ghi tiếp vào stream
          using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
          using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
          using (StreamWriter sw = new StreamWriter(cs, Encoding.UTF8))
          {
            sw.Write(plainText);
          }

          byte[] encryptedBytes = ms.ToArray();

          // 4. Chuyển sang Base64 chuẩn URL-Safe (giống Web FE)
          return Convert.ToBase64String(encryptedBytes)
              .Replace("+", "-")
              .Replace("/", "_")
              .TrimEnd('=');
        }
      }
    }
  }
}
