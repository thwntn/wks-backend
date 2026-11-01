namespace Wks.Interfaces;

public interface IUpdateTagTimeline
{
    IEnumerable<TimelineTag> Execute(
        Guid profileId,
        Guid timelineId,
        TimelineDataTransformer.UpdateTag updateTag
    );
}
