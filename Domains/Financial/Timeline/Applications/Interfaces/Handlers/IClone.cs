namespace Wks.Interfaces;

public interface ICloneTimelineHandler
{
    Task<List<Expense>> ExecuteAsync(Guid identifier, Guid fromId, Guid toId);
}
