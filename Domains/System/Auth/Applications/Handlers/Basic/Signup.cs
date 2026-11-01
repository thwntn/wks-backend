namespace Wks.Handlers;

///
/// Summary:
///     Orchestrates user signup operation with transaction management.
///     Coordinates account creation, email sending, and background setup tasks.
///
public class SignupAuthHandler(
    ISignupAuth _signupAuth,
    IUnitOfWork _unitOfWork,
    ISendCodeAuth _sendCodeAuth,
    ISetupAccountAuthPublisher _setupAccountAuthPublisher
) : ISignupAuthHandler
{
    ///
    /// Summary:
    ///     Creates new user account, sends verification code, and initiates setup process.
    ///     Publishes setup task to background queue for asynchronous processing.
    ///
    /// Parameters:
    ///     signup:
    ///         The signup data containing email, password, and name.
    ///
    /// Returns:
    ///     Signup result containing account ID, email, and verification code.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when email already exists or validation fails.
    ///
    public async Task<AuthObject.SignupResult> ExecuteAsync(
        AuthDataTransformer.Signup signup
    )
    {
        /// Logic:
        ///     Execute signup use case to create account and profile.
        ///
        AuthObject.SignupResult signupResult = await _signupAuth.ExecuteAsync(
            signup
        );

        /// Logic:
        ///     Commit database transaction to persist new account.
        ///
        await _unitOfWork.SyncAsync();

        /// Logic:
        ///     Publish account setup task to background queue for role and domain configuration.
        ///
        await _setupAccountAuthPublisher.Execute(signupResult.AccountId);

        /// Logic:
        ///     Send verification code email to user.
        ///
        await _sendCodeAuth.ExecuteAsync(signupResult.Email, signupResult.Code);

        return signupResult;
    }
}
