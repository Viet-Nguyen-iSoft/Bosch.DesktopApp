using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace iSoft.Database.Repositorys
{
  public class UserRepository : GenericRepository<User, CommonDbContext>
  {
    public UserRepository(CommonDbContext context) : base(context)
    {
    }

    public Task<List<User>> GetAllAsync(bool isContainDelete = false)
    {
      var query = Context.Set<User>()
        .AsNoTracking()
        .AsQueryable();
      if (!isContainDelete)
        query = query.Where(user => user.DeletedFlag != true);

      return query
        .OrderBy(user => user.DisplayName ?? user.FullName)
        .ThenBy(user => user.Username)
        .ToListAsync();
    }

    public Task<User?> GetByIdAsync(Guid id, bool isContainDelete = false)
    {
      var query = Context.Set<User>()
        .AsNoTracking()
        .AsQueryable();
      if (!isContainDelete)
        query = query.Where(user => user.DeletedFlag != true);

      return query.FirstOrDefaultAsync(user => user.Id == id);
    }

    public Task<User?> GetByUsernameAsync(
      string username,
      bool isContainDelete = false)
    {
      if (string.IsNullOrWhiteSpace(username))
        throw new ArgumentException("Username is required.", nameof(username));

      var query = Context.Set<User>()
        .AsNoTracking()
        .AsQueryable();
      if (!isContainDelete)
        query = query.Where(user => user.DeletedFlag != true);

      return query.FirstOrDefaultAsync(user => user.Username == username);
    }

    public async Task<User> AddOrUpdateAsync(User user)
    {
      ArgumentNullException.ThrowIfNull(user);

      user.SyncFlag = false;
      await Context.Database.EnsureCreatedAsync();

      var users = Context.Set<User>();
      var existingUser = user.Id == Guid.Empty
        ? null
        : await users.FindAsync(user.Id);

      if (existingUser == null)
      {
        if (user.Id == Guid.Empty)
          user.Id = Guid.NewGuid();

        user.CreatedAt ??= DateTime.UtcNow;
        await users.AddAsync(user);
      }
      else
      {
        user.UpdatedAt = DateTime.UtcNow;
        Context.Entry(existingUser).CurrentValues.SetValues(user);
      }

      await Context.SaveChangesAsync();
      return existingUser ?? user;
    }

    public async Task<User> DeleteAsync(User user)
    {
      ArgumentNullException.ThrowIfNull(user);

      user.DeletedFlag = true;
      user.SyncFlag = false;
      user.UpdatedAt = DateTime.UtcNow;
      return await AddOrUpdateAsync(user);
    }

    public Task<User?> GetAccountAsync(string account, string pass)
    {
      return Context.Set<User>()
        .Where(x=>x.Username == account && x.Password == pass && x.DeletedFlag == false)
        .AsNoTracking()
        .AsQueryable()
        .FirstOrDefaultAsync();
    }
  }
}
