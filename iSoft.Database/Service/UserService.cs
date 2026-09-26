using HelperManager;
using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;

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

    public async Task<User?> CheckLogin(string account, string pass)
    {
      try
      {
        string passS = SecurityHelper.EncodePassword(account, pass);
        await using var context = new MySqlDbContext();
        var repository = new UserRepository(context);
        return await repository.GetAccountAsync(account, passS).ConfigureAwait(false);
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}
