namespace Wks.Handlers;

///
/// Summary:
///     Orchestrates user login with email and password authentication.
///     Manages transaction for authentication process and login history.
///
public class UsePasswordAuthHandler(
    ILoginAuth _loginAuth,
    IUnitOfWork _unitOfWork
) : IUsePasswordAuthHandler
{
    ///
    /// Summary:
    ///     Authenticates user with email and password credentials.
    ///     Returns JWT tokens for authenticated users or initiates email confirmation for pending accounts.
    ///
    /// Parameters:
    ///     signin:
    ///         The signin credentials containing email and password.
    ///
    /// Returns:
    ///     Authentication result with access token, refresh token, and account ID.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when credentials are invalid or account not found.
    ///
    public async Task<AuthObject.SigninResult> ExecuteAsync(
        AuthDataTransformer.Signin signin
    )
    {
        /// Logic:
        ///     Execute login use case to validate credentials and generate tokens.
        ///
        AuthObject.SigninResult signinResult = await _loginAuth.ExecuteAsync(
            signin
        );

        /// Logic:
        ///     Commit database transaction to persist login history and updates.
        ///
        await _unitOfWork.SyncAsync();
        return signinResult;
    }
}
