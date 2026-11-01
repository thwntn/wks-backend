namespace Wks.Handlers;

public class UnpinFeedbackHandler(
    IUnpinFeedback _unpinFeedback,
    IUnitOfWork _unitOfWork
) : IUnpinFeedbackHandler
{
    ///
    /// Summary:
    ///     Handles the unpin feedback request by executing the use case and committing changes.
    ///
    /// Parameters:
    ///     profileId:
    ///         The identifier of the profile owning the feedback.
    ///
    ///     id:
    ///         The unique identifier of the feedback to unpin.
    ///
    /// Returns:
    ///     The updated Feedback entity with unpinned status.
    ///
    /// Exceptions:
    ///     HttpException (BadRequest):
    ///         Thrown when the feedback is not found or does not belong to the profile.
    ///
    /// Logic:
    ///     1. Executes the unpin feedback use case
    ///     2. Commits the transaction to persist changes
    ///     3. Returns the updated feedback
    ///
    public async Task<Feedback> ExecuteAsync(Guid profileId, Guid id)
    {
        Feedback feedback = await _unpinFeedback.ExecuteAsync(profileId, id);
        await _unitOfWork.SyncAsync();
        return feedback;
    }
}
