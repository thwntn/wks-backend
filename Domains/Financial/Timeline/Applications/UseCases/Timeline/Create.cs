namespace Wks.UseCases;

public class CreateTimeline(
    ITimelineRepository _timelineRepository,
    IUpdateTagTimeline _updateTagTimeline
) : ICreateTimeline
{
    public Timeline Execute(Guid profileId, TimelineDataTransformer.Create create)
    {
        Timeline timeline = create.MapTo<Timeline>();
        timeline.ProfileId = profileId;

        _timelineRepository.Create(timeline);
        TimelineDataTransformer.UpdateTag update = create.MapTo<TimelineDataTransformer.UpdateTag>();

        _updateTagTimeline.Execute(profileId, timeline.Id, update);
        return timeline;
    }
}
