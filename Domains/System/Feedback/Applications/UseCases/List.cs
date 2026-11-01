namespace Wks.UseCases;

public class ListFeedback(IFeedbackRepository _feedbackRepository)
    : IListFeedback
{
    public async Task<Paged<Feedback>> ExecuteAsync(
        Guid profileId,
        QueryOptions queryOptions
    ) =>
        await Pagination.CreateAsync(
            _feedbackRepository
                .Where(feedback => feedback.ProfileId == profileId)
                .OrderByDescending(feedback => feedback.IsPinned)
                .ThenByDescending(feedback => feedback.CreatedAt),
            queryOptions
        );
}
