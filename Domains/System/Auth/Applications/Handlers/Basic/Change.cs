namespace Wks.Handlers;

///
/// Summary:
///     Orchestrates password change operation with transaction management.
///     Coordinates password change use case with database persistence and action logging.
///
public class ChangePasswordAuthHandler(
    IResetPasswordAuth _resetPasswordAuth,
    IUnitOfWork _unitOfWork,
    ICreateActionPublisher _createActionPublisher
) : IChangePasswordAuthHandler
{
    ///
    /// Summary:
    ///     Executes password change operation and publishes change action event.
    ///     Ensures atomic transaction and creates audit log for password change.
    ///
    /// Parameters:
    ///     changePassword:
    ///         The password change data containing email, current password, and new password.
    ///
    /// Returns:
    ///     Empty string indicating successful operation.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when account not found or password validation fails.
    ///
    public async Task<string> ExecuteAsync(
        AuthDataTransformer.ChangePassword changePassword
    )
    {
        /// Logic:
        ///     Execute password change use case to validate and update password.
        ///
        Profile profile = await _resetPasswordAuth.ChangePasswordAsync(
            changePassword
        );

        /// Logic:
        ///     Commit database transaction to persist password change.
        ///
        await _unitOfWork.SyncAsync();

        /// Logic:
        ///     Publish action event for audit logging and tracking.
        ///     Records password change action with profile ID and serialized change data.
        ///
        await _createActionPublisher.Execute(
            new([
                new(
                    profile.Id,
                    nameof(ActionNames.CHANGE_PASSWORD),
                    changePassword.Serialize()
                ),
            ])
        );
        return string.Empty;
    }
}
