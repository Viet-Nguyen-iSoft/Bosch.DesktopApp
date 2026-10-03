using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HelperManager
{
  public static class SecurityHelper
  {
    private const string PasswordHashKey =
        "3e7da5e8-9540-40ba-bd75-21fe19686e16";
    //private const string PasswordHashKey =
    //    "7cf098ec94a84ea49b8fcc59337b9c28";

    /// <summary>
    /// Mã hóa giống EncodeUtil.EncodePassword của iSoft.Auth.
    /// </summary>
    public static string EncodePassword(
        string username,
        string plainPassword)
    {
      byte[] keyBytes =
          Encoding.UTF8.GetBytes(PasswordHashKey);

      // Phải đúng thứ tự: username + password.
      byte[] inputBytes =
          Encoding.UTF8.GetBytes(username + plainPassword);

      using var hmac = new HMACMD5(keyBytes);
      byte[] hashBytes = hmac.ComputeHash(inputBytes);

      return Convert.ToBase64String(hashBytes);
    }

    public static string Encrypt(string source)
    {
      if (string.IsNullOrEmpty(source))
      {
        return source;
      }

      string text = PasswordHashKey;


      //if (MemCached.Instance.IsTimeToRun("_5m_ check_secretKey", 300000L))
      //{
      //  Log.Information("Encrypt, secretKey: " + text.SubstringSafe(0, 4) + "***");
      //}

      //if (Encoding.UTF8.GetBytes(text).Length < 32)
      //{
      //  throw new ArgumentException("Secret key must be at least 256 bits (32 characters)");
      //}
      text = "7cf098ec94a84ea49b8fcc59337b9c28";
      byte[] key = Encoding.UTF8.GetBytes(text).Take(32).ToArray();
      using Aes aes = Aes.Create();
      aes.Key = key;
      aes.GenerateIV();
      byte[] iV = aes.IV;
      using MemoryStream memoryStream = new MemoryStream();
      memoryStream.Write(iV, 0, iV.Length);
      using (ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV))
      {
        using CryptoStream stream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
        using StreamWriter streamWriter = new StreamWriter(stream);
        streamWriter.Write(source);
      }

      return Convert.ToBase64String(memoryStream.ToArray()).Replace("+", "-").Replace("/", "_")
          .Replace("=", "");
    }

    /// <summary>
    /// So sánh password nhập vào với password hash trả về từ API.
    /// </summary>
    public static bool VerifyPassword(
        string username,
        string plainPassword,
        string? passwordHashFromApi)
    {
      if (string.IsNullOrWhiteSpace(username) ||
          string.IsNullOrEmpty(plainPassword) ||
          string.IsNullOrWhiteSpace(passwordHashFromApi))
      {
        return false;
      }

      string candidateHash =
          EncodePassword(username, plainPassword);

      try
      {
        byte[] candidateBytes =
            Convert.FromBase64String(candidateHash);

        byte[] storedBytes =
            Convert.FromBase64String(passwordHashFromApi);

        return candidateBytes.Length == storedBytes.Length &&
               CryptographicOperations.FixedTimeEquals(
                   candidateBytes,
                   storedBytes);
      }
      catch (FormatException)
      {
        return false;
      }
    }

    /// <summary>
    /// Gọi get-list-simplify và kiểm tra username/password.
    /// HttpClient phải có Authorization header nếu API yêu cầu token.
    /// </summary>
    public static async Task<UserSimplifyDto?> CheckUserAsync(
        HttpClient httpClient,
        string enteredUsername,
        string enteredPassword,
        CancellationToken cancellationToken = default)
    {
      string username = enteredUsername.Trim();

      string url =
          "api/v1/User/get-list-simplify" +
          "?page=1" +
          "&pageSize=100" +
          $"&searchStr={Uri.EscapeDataString(username)}";

      using HttpResponseMessage response =
          await httpClient.GetAsync(url, cancellationToken);

      response.EnsureSuccessStatusCode();

      UserSimplifyEnvelope? result =
          await response.Content.ReadFromJsonAsync<UserSimplifyEnvelope>(
              cancellationToken: cancellationToken);

      IEnumerable<UserSimplifyDto> matchingUsers =
          result?.Data?.ListData?
              .Where(user =>
                  string.Equals(
                    user.Username,
                        username,
                        StringComparison.Ordinal));

      // Dùng Any, không dùng FirstOrDefault vì API hiện có nhiều
      // record trùng Username "admin".
      return matchingUsers.FirstOrDefault(user =>
          VerifyPassword(
              user.Username!,
              enteredPassword,
              user.Password));
    }
  }

  public sealed class UserSimplifyEnvelope
  {
    public string? Status { get; set; }
    public UserSimplifyData? Data { get; set; }
  }

  public sealed class UserSimplifyData
  {
    public long TotalRecord { get; set; }
    public List<UserSimplifyDto>? ListData { get; set; }
  }

  public sealed class UserSimplifyDto
  {
    public Guid Id { get; set; }
    public string? DisplayName { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? IdCardCode { get; set; }
    public string? EmployeeCode { get; set; }
  }
}
