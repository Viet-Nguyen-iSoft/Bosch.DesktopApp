using ApiSyncData;
using ApiSyncData.Resp;
using iSoft.Database.Models;

var retainedId = Guid.NewGuid();
var missingId = Guid.NewGuid();
var newId = Guid.NewGuid();
var retainedLocalId = Guid.NewGuid();
var retained = new Local { Id = retainedLocalId, IdSrc = retainedId, DeletedFlag = true, Code = "LOCAL", Name = "old" };
var missing = new Local { IdSrc = missingId };
var localOnly = new Local { Name = "local only" };
var emptyId = new Local { IdSrc = Guid.Empty };
var locals = new List<Local> { retained, missing, localOnly, emptyId };
var rows = new List<Source> { new() { Id = retainedId, Name = "updated" }, new() { Id = newId, Name = "new" } };
ServerSnapshot.Validate(rows, 2);
var added = ServerSnapshot.Apply(rows, locals, (s, l) => l.Name = s.Name, default);
Check(added.Count == 1 && added[0].Id == newId && added[0].IdSrc == newId,
  "insert uses server Id as local Id");
Check(retained.Id == retainedLocalId && retained.Code == "LOCAL" && retained.Name == "updated", "update preserves local fields");
Check(!retained.DeletedFlag, "restore active record");
Check(missing.DeletedFlag, "soft delete missing record");
Check(!localOnly.DeletedFlag && !emptyId.DeletedFlag, "preserve unlinked local records");
var matchingPrimaryId = Guid.NewGuid();
var existingByPrimaryId = new Local { Id = matchingPrimaryId, Name = "old primary" };
var primaryIdRows = new List<Source> { new() { Id = matchingPrimaryId, Name = "updated primary" } };
var primaryIdAdded = ServerSnapshot.Apply(primaryIdRows, new List<Local> { existingByPrimaryId },
  (s, l) => l.Name = s.Name, default);
Check(primaryIdAdded.Count == 0 && existingByPrimaryId.IdSrc == matchingPrimaryId &&
  existingByPrimaryId.Name == "updated primary", "update matching primary Id instead of insert");
locals.AddRange(added);
var deletedAt = missing.UpdatedAt;
Check(ServerSnapshot.Apply(rows, locals, (s, l) => l.Name = s.Name, default).Count == 0,
  "repeated sync does not insert duplicates");
Check(missing.UpdatedAt == deletedAt, "repeated delete preserves timestamp");
var skippedId = Guid.NewGuid();
var skipped = new Local { IdSrc = skippedId, Name = "unchanged" };
ServerSnapshot.Apply(new List<Source>(), new List<Local> { skipped },
  (s, l) => l.Name = s.Name, default, new[] { skippedId });
Check(!skipped.DeletedFlag && skipped.Name == "unchanged", "skipped record remains unchanged");
ServerSnapshot.Validate(new List<Source>(), 0);
ServerSnapshot.Apply(new List<Source>(), locals, (s, l) => l.Name = s.Name, default);
Check(retained.DeletedFlag && added[0].DeletedFlag && !localOnly.DeletedFlag, "empty snapshot deletes linked records");
Reject(() => ServerSnapshot.Validate<Source>(null, 0), "null list");
Reject(() => ServerSnapshot.Validate(rows, null), "missing total");
Reject(() => ServerSnapshot.Validate(rows, 3), "incomplete page");
Reject(() => ServerSnapshot.Validate(new List<Source> { rows[0], rows[0] }, 2), "duplicate source id");
Reject(() => ServerSnapshot.Validate(new List<Source> { new() }, 1), "missing id");
Reject(() => ServerSnapshot.Validate(new List<Source> { new() { Id = Guid.Empty } }, 1), "empty id");
Reject(() => ServerSnapshot.Validate(new List<Source> { new() { Id = newId, IsDelete = true } }, 1), "deleted source row");
try
{
  ServerSnapshot.Apply(rows, locals, (s, l) => l.Name = s.Name, new CancellationToken(true));
  throw new Exception("Cancellation was ignored");
}
catch (OperationCanceledException) { }
Console.WriteLine("PASS: insert, update by IdSrc/Id, restore, soft delete, skipped record, local field preservation, repeat sync, empty snapshot, invalid snapshots, cancellation.");

static void Check(bool condition, string name)
{
  if (!condition) throw new Exception("FAIL: " + name);
}
static void Reject(Action action, string name)
{
  try { action(); }
  catch (InvalidOperationException) { return; }
  throw new Exception("FAIL: accepted " + name);
}
sealed class Source : IServerRecord
{
  public Guid? Id { get; set; }
  public bool? IsDelete { get; set; }
  public DateTime? CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
  public string? Name { get; set; }
}
sealed class Local : BaseModel
{
  public string? Code { get; set; }
  public string? Name { get; set; }
}
