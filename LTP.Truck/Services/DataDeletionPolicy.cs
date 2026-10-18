namespace LTP.Truck.Services
{
  internal static class DataDeletionPolicy
  {
    internal static bool CanDelete(DateTime? createdAtUtc, int? permittedDays,
      DateTime nowUtc, int utcOffsetHours)
    {
      var days = permittedDays ?? 1;
      if (!createdAtUtc.HasValue || days <= 0) return false;
      var today = nowUtc.AddHours(utcOffsetHours).Date;
      var recordDate = createdAtUtc.Value.AddHours(utcOffsetHours).Date;
      var ageInDays = (today - recordDate).Days;
      return ageInDays >= 0 && ageInDays < days;
    }
  }
}
