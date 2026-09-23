using iSoft.Database.DbContexts;
using iSoft.Database.Models;
using iSoft.Database.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Database.Service
{
  public class WarehouseService
  {
    public async Task<List<Warehouse>> GetAllAsync(bool IsContainDelete = false)
    {
      // Mỗi lần gọi dùng context riêng và giải phóng sau khi đọc xong.
      await using var context = new MySqlDbContext();
      var repository = new WarehouseRepository(context);
      return await repository.GetAllAsync(IsContainDelete).ConfigureAwait(false);
    }

    public async Task<Warehouse> AddOrUpdateAsync(Warehouse warehouse)
    {
      await using var context = new MySqlDbContext();
      return await MasterDataUpsertHelper.AddOrUpdateAsync(context, warehouse).ConfigureAwait(false);
    }
  }
}
