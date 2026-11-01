namespace Wks.Interfaces;

public interface IListTimeline
{
    Task<Paged<Timeline>> ExecuteAsync(Guid profileId, QueryOptions queryOptions);
}
