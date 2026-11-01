namespace Wks.UseCases;

public class UpdateTagTimeline(
    ITimelineRepository _expendCategoryRepository,
    IHashTagRepository _tagRepository
) : IUpdateTagTimeline
{
    public IEnumerable<TimelineTag> Execute(
        Guid profileId,
        Guid timelineId,
        TimelineDataTransformer.UpdateTag updateTag
    )
    {
        IEnumerable<TimelineTag> timelineTags = _expendCategoryRepository
            .GetContext()
            .TimelineTag.Where(timelineTag => timelineTag.TimelineId == timelineId);
        _expendCategoryRepository.GetContext().RemoveRange(timelineTags);

        IEnumerable<TimelineTag> update = _tagRepository
            .Where(tag => updateTag.TagIds.Contains(tag.Id))
            .Select(tag => new TimelineTag(timelineId, tag.Id));

        _expendCategoryRepository.GetContext().TimelineTag.AddRange(update);
        // _expendCategoryRepository.GetContext().SaveChanges(); // Handled by Controller Transaction

        return timelineTags;
    }
}
