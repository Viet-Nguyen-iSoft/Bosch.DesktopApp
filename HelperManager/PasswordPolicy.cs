using System.Security.Cryptography;

namespace HelperManager
{
  public static class PasswordPolicy
  {
    public const int MinimumLength = 12;
    public const int PasswordHistoryCount = 5;
    public const int MaximumFailedLoginAttempts = 8;
    public const int PasswordLifetimeMonths = 6;

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
      params string?[] guessableValues)
    {
      if (password.Length < MinimumLength)
        return $"Mật khẩu phải có ít nhất {MinimumLength} ký tự.";
      if (!password.Any(char.IsUpper))
        return "Mật khẩu phải có ít nhất một chữ cái viết hoa.";
      if (!password.Any(char.IsLower))
        return "Mật khẩu phải có ít nhất một chữ cái viết thường.";
      if (!password.Any(char.IsDigit))
        return "Mật khẩu phải có ít nhất một chữ số.";
      if (!password.Any(character => !char.IsLetterOrDigit(character)))
        return "Mật khẩu phải có ít nhất một ký tự đặc biệt.";

      string normalizedPassword = password.ToLowerInvariant();
      if (ConsecutiveSequences.Any(sequence =>
        ContainsSequence(normalizedPassword, sequence, 4)))
      {
        return "Mật khẩu không được chứa chuỗi ký tự hoặc chữ số liên tiếp (ví dụ: 1234, abcd, qwerty).";
      }

      foreach (string value in guessableValues
        .Where(value => !string.IsNullOrWhiteSpace(value))
        .SelectMany(SplitGuessableValue)
        .Distinct(StringComparer.OrdinalIgnoreCase))
      {
        if (value.Length >= 3 && normalizedPassword.Contains(
          value.ToLowerInvariant(), StringComparison.Ordinal))
        {
          return "Mật khẩu không được chứa tên đăng nhập, mã nhân viên hoặc thông tin cá nhân dễ đoán.";
        }
      }

      return null;
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
  }
}
