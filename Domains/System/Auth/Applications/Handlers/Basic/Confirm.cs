namespace Wks.Handlers;

///
/// Summary:
///     Orchestrates account confirmation with verification code.
///     Manages transaction for account activation and token generation.
///
public class ConfirmCodeAuthHandler(
    IConfirmCodeAuth _confirmCodeAuth,
    IUnitOfWork _unitOfWork
) : IConfirmCodeAuthHandler
{
    ///
    /// Summary:
    ///     Confirms account using verification code and activates account.
    ///     Returns authentication tokens for newly confirmed account.
    ///
    /// Parameters:
    ///     accountId:
    ///         The unique identifier of the account to confirm.
    ///
    ///     code:
    ///         The verification code sent to user's email.
    ///
    /// Returns:
    ///     Signup result containing access token, refresh token, and account information.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when verification code is invalid or account not found.
    ///
    public async Task<AuthObject.SignupResult> ExecuteAsync(
        Guid accountId,
        string code
    )
    {
        /// Logic:
        ///     Execute confirmation use case to validate code and activate account.
        ///
        AuthObject.SignupResult signupResult =
            await _confirmCodeAuth.ExecuteAsync(accountId, code);

        /// Logic:
        ///     Commit database transaction to persist account activation.
        ///
        await _unitOfWork.SyncAsync();
        return signupResult;
    }
}
