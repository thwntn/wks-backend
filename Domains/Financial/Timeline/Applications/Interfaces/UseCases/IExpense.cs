namespace Wks.Interfaces;

public interface ITimeline
{
    Task<Paged<Expense>> ExecuteAsync(Guid profileId, Guid timelineId, QueryOptions queryOptions);
}
