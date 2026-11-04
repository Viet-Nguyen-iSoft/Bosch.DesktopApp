using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HelperManager
{
  public static class EncoderHelper
  {
    public static string Decrypt(string encodedText)
    {
      if (string.IsNullOrEmpty(encodedText))
      {
        return encodedText;
      }

      try
      {
        encodedText = encodedText.Replace("-", "+").Replace("_", "/");
        while (encodedText.Length % 4 != 0)
        {
          encodedText += "=";
        }

        byte[] source = Convert.FromBase64String(encodedText);
        string encryptHandshakeSecretKey = "7cf098ec94a84ea49b8fcc59337b9c28";
        if (Encoding.UTF8.GetBytes(encryptHandshakeSecretKey).Length < 32)
        {
          throw new ArgumentException("Secret key must be at least 256 bits (32 characters)");
        }

        byte[] key = Encoding.UTF8.GetBytes(encryptHandshakeSecretKey).Take(32).ToArray();
        using Aes aes = Aes.Create();
        aes.Key = key;
        byte[] iV = source.Take(16).ToArray();
        byte[] buffer = source.Skip(16).ToArray();
        aes.IV = iV;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        using ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);
        using MemoryStream stream = new MemoryStream(buffer);
        using CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
        using StreamReader streamReader = new StreamReader(stream2);
        return streamReader.ReadToEnd();
      }
      catch (FormatException innerException)
      {
        throw new FormatException("The input is not a valid Base-64 string.", innerException);
      }
      catch (Exception)
      {
        throw new Exception("An error occurred during decryption.");
        //throw new BaseException("An error occurred during decryption.");
      }
    }


    public static bool Verify(
       string username,
       string passwordInput,
       string storedPassword,
       string passwordKey)
    {
      if (string.IsNullOrEmpty(username) ||
          string.IsNullOrEmpty(passwordInput) ||
          string.IsNullOrEmpty(storedPassword) ||
          string.IsNullOrEmpty(passwordKey))
      {
        return false;
      }

      byte[] keyBytes = Encoding.UTF8.GetBytes(passwordKey);
      byte[] inputBytes = Encoding.UTF8.GetBytes(username + passwordInput);

      using var hmac = new HMACMD5(keyBytes);
      string encodedInput = Convert.ToBase64String(
          hmac.ComputeHash(inputBytes)
      );

      return CryptographicOperations.FixedTimeEquals(
          Encoding.UTF8.GetBytes(encodedInput),
          Encoding.UTF8.GetBytes(storedPassword)
      );
    }
  }

}

