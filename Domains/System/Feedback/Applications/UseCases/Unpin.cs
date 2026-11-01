namespace Wks.UseCases;

public class UnpinFeedback(IFeedbackRepository _feedbackRepository)
    : IUnpinFeedback
{
    ///
    /// Summary:
    ///     Unpins a feedback by removing its highlight status.
    ///     Sets the IsPinned flag to false for the specified feedback.
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
    ///     1. Retrieves the feedback by ID and validates profile ownership
    ///     2. Sets IsPinned property to false
    ///     3. Updates the feedback in repository
    ///     4. Returns the updated feedback
    ///
    public async Task<Feedback> ExecuteAsync(Guid profileId, Guid id)
    {
        Feedback feedback =
            await _feedbackRepository.FirstOrDefaultAsync(feedback =>
                feedback.Id == id && feedback.ProfileId == profileId
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_FEEDBACK
            );

        feedback.IsPinned = false;
        _feedbackRepository.Update(feedback);
        return feedback;
    }
}
