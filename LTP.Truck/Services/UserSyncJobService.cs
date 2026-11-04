using ApiSyncData.Req;
using HelperManager;
using iSoft.Database.Models;
using LTP.Truck.Controls;
using static HelperManager.EnumData;

namespace LTP.Truck.Services
{
  internal static class UserSyncJobService
  {
    public static async Task QueueUpsertAsync(User user)
    {
      ArgumentNullException.ThrowIfNull(user);

      // ISOFT (hiển thị i-Soft) là tài khoản quản trị chỉ tồn tại ở local.
      if (PasswordPolicy.IsBuiltInAccount(user.Username))
        return;

      var userUpsertRequest = new UserUpsertRequest
      {
        // Dùng chung một Id giữa local và server.
        Id = user.Id,
        TranslateFormDatas = new List<UserTranslateFormData>
        {
          new() { Lang = "VI", FullName = user.FullName },
          new() { Lang = "EN", FullName = user.FullName },
        },
        Username = user.Username,
        Password = user.Password,
        EmployeeCode = user.EmployeeCode,
        IdCardCode = user.IdCardCode,
        PasswordChangedAt = user.PasswordChangedAt,
        MustChangePassword = user.MustChangePassword,
        EnableFlag = user.EnableFlag,
        SyncFlag = user.SyncFlag,
        DeletedFlag = user.DeletedFlag,
      };

      var apiJob = new ApiJobs
      {
        Id = Guid.NewGuid(),
        Json = JsonHelper.ToJson(userUpsertRequest),
        Description = $"Đồng bộ tài khoản {user.Username}",
        EnumTypeAPI = EnumTypeAPI.MD_User,
        EnumStatusAPI = EnumStatusAPI.Created,
        CreatedAt = DateTime.UtcNow,
      };

      await AppCore.Ins._apiJobsService.AddOrUpdateAsync(apiJob);
    }
  }
}
