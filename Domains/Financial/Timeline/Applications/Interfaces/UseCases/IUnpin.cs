namespace Wks.Interfaces;

public interface IUnpinTimeline
{
    Task<Timeline> ExecuteAsync(Guid profileId, Guid timelineId);
}
