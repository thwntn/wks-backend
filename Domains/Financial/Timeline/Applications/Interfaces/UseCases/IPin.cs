namespace Wks.Interfaces;

public interface IPinTimeline
{
    Task<Timeline> ExecuteAsync(Guid profileId, Guid timelineId);
}
