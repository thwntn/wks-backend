namespace Wks.Handlers;

///
/// Summary:
///     Orchestrates password reset operation with transaction management.
///     Coordinates password reset use case with database persistence and action logging.
///
public class ResetPasswordAuthHandler(
    IResetPasswordAuth _resetPasswordAuth,
    IUnitOfWork _unitOfWork,
    ICreateActionPublisher _createActionPublisher
) : IResetPasswordAuthHandler
{
    ///
    /// Summary:
    ///     Initiates password reset by generating new password and sending via email.
    ///     Publishes password change action event for audit logging.
    ///
    /// Parameters:
    ///     email:
    ///         The email address of the account to reset password for.
    ///
    /// Returns:
    ///     Empty string indicating successful operation.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when account with specified email is not found.
    ///
    public async Task<string> ExecuteAsync(string email)
    {
        /// Logic:
        ///     Execute password reset use case to generate and send new password.
        ///
        Profile profile = await _resetPasswordAuth.ExecuteAsync(email);

        /// Logic:
        ///     Commit database transaction to persist new password.
        ///
        await _unitOfWork.SyncAsync();

        /// Logic:
        ///     Publish action event for audit logging.
        ///     Records password reset action with profile ID and email.
        ///
        await _createActionPublisher.Execute(
            new([new(profile.Id, nameof(ActionNames.CHANGE_PASSWORD), email)])
        );

        return string.Empty;
    }
}
