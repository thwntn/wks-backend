namespace Wks.UseCases;

public class UpdateTimeline(
    ITimelineRepository _timelineRepository,
    IUpdateTagTimeline _updateTagTimeline,
    ICache _cache
) : IUpdateTimeline
{
    public async Task<Timeline> ExecuteAsync(
        Guid profileId,
        TimelineDataTransformer.Update update
    )
    {
        Timeline timeline =
            await _timelineRepository.Raw.FirstOrDefaultAsync(timeline =>
                timeline.Id == update.Id && timeline.ProfileId == profileId
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_EXPENSE_CATEGORY
            );

        Timeline merge = timeline.Merge(update);
        _timelineRepository.Update(merge);

        TimelineDataTransformer.UpdateTag updateTag =
            update.MapTo<TimelineDataTransformer.UpdateTag>();
        _updateTagTimeline.Execute(profileId, timeline.Id, updateTag);

        await _cache.RemoveAsync(
            Format.ToCacheName(
                nameof(CacheNames.FINANCIAL_TIMELINE),
                timeline.Id.ToString()
            )
        );

        return merge;
    }
}
