namespace Wks.UseCases;

public class PinFeedback(IFeedbackRepository _feedbackRepository) : IPinFeedback
{
    ///
    /// Summary:
    ///     Pins a feedback to highlight it as important.
    ///     Sets the IsPinned flag to true for the specified feedback.
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
    ///     1. Retrieves the feedback by ID and validates profile ownership
    ///     2. Sets IsPinned property to true
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

        feedback.IsPinned = true;
        _feedbackRepository.Update(feedback);
        return feedback;
    }
}
