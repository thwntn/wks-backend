namespace Wks.Interfaces;

public interface ICounterTimeline
{
    Task<TimelineObject.Counter> ExecuteAsync(Guid profileId);
}
