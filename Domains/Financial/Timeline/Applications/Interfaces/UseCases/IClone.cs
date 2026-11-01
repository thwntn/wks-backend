namespace Wks.Interfaces;

public interface ICloneTimeline
{
    Task<List<Expense>> ExecuteAsync(Guid profileId, Guid fromId, Guid toId);
}
