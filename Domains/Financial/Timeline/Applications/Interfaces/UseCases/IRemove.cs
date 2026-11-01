namespace Wks.Interfaces;

public interface IRemoveTimeline
{
    string Execute(Guid profileId, Guid timelineId);
}
