namespace Wks.Interfaces;

public interface IInfoTimeline
{
    Timeline Execute(Guid profileId, Guid timelineId);
}
