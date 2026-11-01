namespace Wks.Interfaces;

public interface IPeriodTimeline
{
    Task<List<TimelineObject.Period>> ExecuteAsync(Guid profileId);
}
