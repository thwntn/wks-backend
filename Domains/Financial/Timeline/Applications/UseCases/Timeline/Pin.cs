namespace Wks.UseCases;

public class PinTimeline(ITimelineRepository _timelineRepository) : IPinTimeline
{
    public async Task<Timeline> ExecuteAsync(Guid profileId, Guid timelineId)
    {
        Timeline timeline = await _timelineRepository.GetByIdAsync(timelineId);

        if (profileId.CompareTo(timeline.ProfileId) is not 0)
            throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_EXPENSE
            );

        // If expense is pinned -> error message
        if (timeline.IsPinned)
            throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.EXPENSE_IS_PINNED
            );

        timeline.IsPinned = true;
        _timelineRepository.Update(timeline);

        return timeline;
    }
}
