namespace Wks.Handlers;

public class PinFeedbackHandler(
    IPinFeedback _pinFeedback,
    IUnitOfWork _unitOfWork
) : IPinFeedbackHandler
{
    ///
    /// Summary:
    ///     Handles the pin feedback request by executing the use case and committing changes.
    ///
    /// Parameters:
    ///     profileId:
    ///         The identifier of the profile owning the feedback.
    ///
    ///     id:
    ///         The unique identifier of the feedback to pin.
    ///
    /// Returns:
    ///     The updated Feedback entity with pinned status.
    ///
    /// Exceptions:
    ///     HttpException (BadRequest):
    ///         Thrown when the feedback is not found or does not belong to the profile.
    ///
    /// Logic:
    ///     1. Executes the pin feedback use case
    ///     2. Commits the transaction to persist changes
    ///     3. Returns the updated feedback
    ///
    public async Task<Feedback> ExecuteAsync(Guid profileId, Guid id)
    {
        Feedback feedback = await _pinFeedback.ExecuteAsync(profileId, id);
        await _unitOfWork.SyncAsync();
        return feedback;
    }
}
