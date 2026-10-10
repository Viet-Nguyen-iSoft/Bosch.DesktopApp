using HelperManager;
using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;
using System.Text.Json;

namespace iSoft.Database.Service
{
  public class UserService
  {
    public async Task<List<User>> GetAllAsync(bool isContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new UserRepository(context);
      return await repository.GetAllAsync(isContainDelete).ConfigureAwait(false);
    }

    public async Task<User?> GetByIdAsync(
      Guid id,
      bool isContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new UserRepository(context);
      return await repository.GetByIdAsync(id, isContainDelete).ConfigureAwait(false);
    }

    public async Task<User?> GetByUsernameAsync(
      string username,
      bool isContainDelete = false)
    {
      await using var context = new MySqlDbContext();
      var repository = new UserRepository(context);
      return await repository.GetByUsernameAsync(username, isContainDelete)
        .ConfigureAwait(false);
    }

    public async Task<bool> ExistsUsernameAsync(string username)
    {
      await using var context = new MySqlDbContext();
      var repository = new UserRepository(context);
      return await repository.ExistsUsernameAsync(username)
        .ConfigureAwait(false);
    }

    public async Task<User> AddOrUpdateAsync(User user)
    {
      await using var context = new MySqlDbContext();
      var repository = new UserRepository(context);
      return await repository.AddOrUpdateAsync(user).ConfigureAwait(false);
    }

    public async Task<User> DeleteAsync(User user)
    {
      await using var context = new MySqlDbContext();
      var repository = new UserRepository(context);
      return await repository.DeleteAsync(user).ConfigureAwait(false);
    }

    public async Task<LoginResult> CheckLogin(string account, string pass)
    {
      string normalizedAccount = account.Trim();
      await using var context = new MySqlDbContext();
      var repository = new UserRepository(context);
      User? user = await repository.GetByUsernameForLoginAsync(normalizedAccount)
        .ConfigureAwait(false);

      if (user == null || !user.EnableFlag)
        return LoginResult.Invalid();

      if (user.IsLoginLocked)
        return LoginResult.Locked();

      bool passwordMatches = string.Equals(user.PW, pass,
        StringComparison.Ordinal);
      if (!passwordMatches && user.Password != null)
      {
        passwordMatches = SecurityHelper.VerifyPassword(
          user.Username, pass, user.Password);
      }

      if (!passwordMatches)
      {
        user.FailedLoginAttempts++;
        if (user.FailedLoginAttempts >= PasswordPolicy.MaximumFailedLoginAttempts)
          user.IsLoginLocked = true;
        await repository.SaveLoginStateAsync(user).ConfigureAwait(false);
        return user.IsLoginLocked
          ? LoginResult.Locked()
          : LoginResult.Invalid(
            PasswordPolicy.MaximumFailedLoginAttempts - user.FailedLoginAttempts);
      }

      user.FailedLoginAttempts = 0;
      await repository.SaveLoginStateAsync(user).ConfigureAwait(false);

      bool passwordExpired = !user.PasswordChangedAt.HasValue ||
        user.PasswordChangedAt.Value.AddMonths(
          PasswordPolicy.PasswordLifetimeMonths) <= DateTime.UtcNow;
      return LoginResult.Success(user,
        user.MustChangePassword || passwordExpired);
    }

    public static IReadOnlyList<string> GetPasswordHistory(User user)
    {
      if (string.IsNullOrWhiteSpace(user.PasswordHistory))
        return Array.Empty<string>();

      try
      {
        return JsonSerializer.Deserialize<List<string>>(user.PasswordHistory) ??
          new List<string>();
      }
      catch (JsonException)
      {
        return Array.Empty<string>();
      }
    }

    public static void ApplyNewPassword(
      User user,
      string plainPassword,
      bool mustChangePassword)
    {
      var history = GetPasswordHistory(user).ToList();
      if (!string.IsNullOrEmpty(user.PW))
      {
        string currentHash = PasswordPolicy.CreateHistoryHash(
          user.Username, user.PW);
        if (!history.Contains(currentHash, StringComparer.Ordinal))
          history.Insert(0, currentHash);
      }
      history.Insert(0,
        PasswordPolicy.CreateHistoryHash(user.Username, plainPassword));

      user.PasswordHistory = JsonSerializer.Serialize(history
        .Distinct(StringComparer.Ordinal)
        .Take(PasswordPolicy.PasswordHistoryCount));
      user.Password = SecurityHelper.Encrypt(plainPassword);
      user.PW = plainPassword;
      user.PasswordChangedAt = DateTime.UtcNow;
      user.MustChangePassword = mustChangePassword;
      user.FailedLoginAttempts = 0;
      user.IsLoginLocked = false;
    }
  }

  public sealed record LoginResult(
    User? User,
    LoginStatus Status,
    int RemainingAttempts = 0,
    bool MustChangePassword = false)
  {
    public static LoginResult Success(User user, bool mustChangePassword) =>
      new(user, LoginStatus.Success, MustChangePassword: mustChangePassword);
    public static LoginResult Invalid(int remainingAttempts = 0) =>
      new(null, LoginStatus.InvalidCredentials, remainingAttempts);
    public static LoginResult Locked() =>
      new(null, LoginStatus.Locked);
  }

  public enum LoginStatus
  {
    Success,
    InvalidCredentials,
    Locked
  }
}
