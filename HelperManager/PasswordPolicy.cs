using System.Security.Cryptography;
using System.Text;

namespace HelperManager
{
  public static class PasswordPolicy
  {
    public const string BuiltInAccount = "ISOFT";
    public const int MinimumLength = 12;
    public const int PasswordHistoryCount = 5;
    public const int MaximumFailedLoginAttempts = 8;
    public const int PasswordLifetimeMonths = 6;

    public static bool IsBuiltInAccount(string? username)
    {
      if (string.IsNullOrWhiteSpace(username))
        return false;

      var normalizedUsername = new string(username
        .Where(char.IsLetterOrDigit)
        .ToArray());
      return string.Equals(
        normalizedUsername,
        BuiltInAccount,
        StringComparison.OrdinalIgnoreCase);
    }

    private static readonly string[] ConsecutiveSequences =
    {
      "0123456789", "9876543210",
      "abcdefghijklmnopqrstuvwxyz", "zyxwvutsrqponmlkjihgfedcba",
      "qwertyuiop", "poiuytrewq",
      "asdfghjkl", "lkjhgfdsa",
      "zxcvbnm", "mnbvcxz"
    };

    public static string? Validate(
      string password,
      params string?[] guessableValues) =>
      ValidateAll(password, guessableValues).FirstOrDefault();

    public static IReadOnlyList<string> ValidateAll(
      string password,
      params string?[] guessableValues)
    {
      var errors = new List<string>();

      if (password.Length < MinimumLength)
        errors.Add($"Mật khẩu phải có ít nhất {MinimumLength} ký tự.");
      if (!password.Any(char.IsUpper))
        errors.Add("Mật khẩu phải có ít nhất một chữ cái viết hoa.");
      if (!password.Any(char.IsLower))
        errors.Add("Mật khẩu phải có ít nhất một chữ cái viết thường.");
      if (!password.Any(char.IsDigit))
        errors.Add("Mật khẩu phải có ít nhất một chữ số.");
      if (!password.Any(character => !char.IsLetterOrDigit(character)))
        errors.Add("Mật khẩu phải có ít nhất một ký tự đặc biệt.");

      string normalizedPassword = password.ToLowerInvariant();
      if (ConsecutiveSequences.Any(sequence =>
        ContainsSequence(normalizedPassword, sequence, 4)))
      {
        errors.Add("Mật khẩu không được chứa chuỗi ký tự hoặc chữ số liên tiếp (ví dụ: 1234, abcd, qwerty).");
      }

      bool containsGuessableValue = guessableValues
        .Where(value => !string.IsNullOrWhiteSpace(value))
        .SelectMany(SplitGuessableValue)
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .Any(value => value.Length >= 3 && normalizedPassword.Contains(
          value.ToLowerInvariant(), StringComparison.Ordinal));

      if (containsGuessableValue)
        errors.Add("Mật khẩu không được chứa tên đăng nhập, mã nhân viên hoặc thông tin cá nhân dễ đoán.");

      return errors;
    }

    public static string CreateHistoryHash(string username, string password) =>
      SecurityHelper.EncodePassword(username.Trim(), password);

    public static bool HistoryHashMatches(
      string username,
      string password,
      string storedHash)
    {
      try
      {
        byte[] candidate = Convert.FromBase64String(
          CreateHistoryHash(username, password));
        byte[] stored = Convert.FromBase64String(storedHash);
        return candidate.Length == stored.Length &&
          CryptographicOperations.FixedTimeEquals(candidate, stored);
      }
      catch (FormatException)
      {
        return false;
      }
    }

    private static IEnumerable<string> SplitGuessableValue(string? value) =>
      value!
        .Split(new[] { ' ', '-', '_', '.', '@', '/', '\\' },
          StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Append(value.Trim());

    private static bool ContainsSequence(
      string password,
      string sequence,
      int minimumSequenceLength)
    {
      for (int index = 0;
        index <= sequence.Length - minimumSequenceLength;
        index++)
      {
        if (password.Contains(
          sequence.Substring(index, minimumSequenceLength),
          StringComparison.Ordinal))
          return true;
      }

      return false;
    }





    //public static string Decrypt(string encodedText)
    //{
    //  if (string.IsNullOrEmpty(encodedText))
    //  {
    //    return encodedText;
    //  }

    //  try
    //  {
    //    encodedText = encodedText.Replace("-", "+").Replace("_", "/");
    //    while (encodedText.Length % 4 != 0)
    //    {
    //      encodedText += "=";
    //    }

    //    byte[] source = Convert.FromBase64String(encodedText);
    //    string encryptHandshakeSecretKey = CommonConfig.EncryptHandshakeSecretKey;
    //    if (Encoding.UTF8.GetBytes(encryptHandshakeSecretKey).Length < 32)
    //    {
    //      throw new ArgumentException("Secret key must be at least 256 bits (32 characters)");
    //    }

    //    byte[] key = Encoding.UTF8.GetBytes(encryptHandshakeSecretKey).Take(32).ToArray();
    //    using Aes aes = Aes.Create();
    //    aes.Key = key;
    //    byte[] iV = source.Take(16).ToArray();
    //    byte[] buffer = source.Skip(16).ToArray();
    //    aes.IV = iV;
    //    aes.Mode = CipherMode.CBC;
    //    aes.Padding = PaddingMode.PKCS7;
    //    using ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);
    //    using MemoryStream stream = new MemoryStream(buffer);
    //    using CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
    //    using StreamReader streamReader = new StreamReader(stream2);
    //    return streamReader.ReadToEnd();
    //  }
    //  catch (FormatException innerException)
    //  {
    //    throw new FormatException("The input is not a valid Base-64 string.", innerException);
    //  }
    //  catch (Exception)
    //  {
    //    throw new BaseException("An error occurred during decryption.");
    //  }
    //}

  }
}
