namespace Wks.UseCases;

public class RemoveTimeline(
    ITimelineRepository _timelineRepository,
    ICache _cache
) : IRemoveTimeline
{
    public string Execute(Guid profileId, Guid timelineId)
    {
        Timeline timeline =
            _timelineRepository.Raw.FirstOrDefault(timeline =>
                timeline.Id == timelineId && timeline.ProfileId == profileId
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_EXPENSE_CATEGORY
            );

        _timelineRepository.Remove(timeline);

        _cache.RemoveAsync(
            Format.ToCacheName(
                nameof(CacheNames.FINANCIAL_TIMELINE),
                timeline.Id.ToString()
            )
        );

        return string.Empty;
    }
}
