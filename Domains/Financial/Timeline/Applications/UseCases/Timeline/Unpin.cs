namespace Wks.UseCases;

public class UnpinTimeline(ITimelineRepository _timelineRepository)
    : IUnpinTimeline
{
    public async Task<Timeline> ExecuteAsync(Guid profileId, Guid expenseId)
    {
        Timeline timeline = await _timelineRepository.GetByIdAsync(expenseId);

        if (profileId.CompareTo(timeline.ProfileId) is not 0)
            throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_EXPENSE
            );

        // Summary:
        //     If expense is not pinned -> error message
        if (timeline.IsPinned is false)
            throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.EXPENSE_IS_NOT_PINNED
            );

        timeline.IsPinned = false;
        _timelineRepository.Update(timeline);

        return timeline;
    }
}
