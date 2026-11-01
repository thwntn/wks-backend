namespace Wks.Interfaces;

public interface IRecentTimeline
{
    Task<TimelineObject.Recent> ExecuteAsync(
        Guid profileId,
        QueryOptions queryOptions
    );
}
