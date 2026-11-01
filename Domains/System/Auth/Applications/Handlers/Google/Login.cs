namespace Wks.Handlers;

///
/// Summary:
///     Orchestrates Google OAuth authentication flow.
///     Handles both new user signup and existing user login via Google.
///
public class LoginGoogleAuthHandler(
    IValidateAccountGoogle _validateAccountGoogle,
    IUnitOfWork _unitOfWork,
    ILoginAuth _loginAuth,
    ISignupAuth _signupAuth,
    ISetupAccountAuthPublisher _setupAccountAuthPublisher,
    ICreateJsonWebTokenHandler _createJsonWebTokenHandler,
    ICache _cache
) : ILoginGoogleAuthHandler
{
    ///
    /// Summary:
    ///     Validates Google authorization code and authenticates or registers user.
    ///     Routes to signin flow for existing accounts or signup flow for new users.
    ///
    /// Parameters:
    ///     code:
    ///         The Google OAuth authorization code.
    ///
    /// Returns:
    ///     Authentication result with access token, refresh token, and account ID.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when Google authorization fails or account operations fail.
    ///
    public async Task<AuthObject.SigninResult> ExecuteAsync(string code)
    {
        /// Logic:
        ///     Validate Google authorization code and retrieve user profile.
        ///     Determine if user exists in system.
        ///
        AuthObject.GoogleValidate googleValidate =
            await _validateAccountGoogle.ExecuteAsync(code);

        /// Logic:
        ///     If account exists (SignupRequire is false), execute signin flow.
        ///     Otherwise, execute signup flow for new user.
        ///
        if (googleValidate.SignupRequire is false)
            return await Signin(googleValidate);
        return await Signup(googleValidate);
    }

    ///
    /// Summary:
    ///     Handles signin process for existing Google-authenticated users.
    ///     Generates JWT tokens and records login activity.
    ///
    /// Parameters:
    ///     googleValidate:
    ///         The validated Google account information.
    ///
    /// Returns:
    ///     Authentication result with JWT tokens and account ID.
    ///
    private async Task<AuthObject.SigninResult> Signin(
        AuthObject.GoogleValidate googleValidate
    )
    {
        /// Logic:
        ///     Execute login handler with existing account.
        ///     Return signin result with access and refresh tokens.
        ///
        AuthObject.SigninResult signinResult = await _loginAuth.Handler(
            googleValidate.Account
        );
        return new(
            signinResult.AccessToken,
            signinResult.RefreshToken,
            signinResult.AccountId
        );
    }

    ///
    /// Summary:
    ///     Handles signup process for new Google-authenticated users.
    ///     Creates account, generates tokens, and initiates setup process.
    ///
    /// Parameters:
    ///     googleValidate:
    ///         The validated Google account information containing email and display name.
    ///
    /// Returns:
    ///     Authentication result with JWT tokens and new account ID.
    ///
    private async Task<AuthObject.SigninResult> Signup(
        AuthObject.GoogleValidate googleValidate
    )
    {
        /// Logic:
        ///     Create new account using Google profile data.
        ///     Skip email verification since Google has already verified the email.
        ///
        AuthObject.SignupResult signupResult = await _signupAuth.ExecuteAsync(
            new()
            {
                Name = googleValidate.DisplayName,
                Email = googleValidate.Email,
                Password = googleValidate.Password,
            },
            skipVerify: true
        );

        /// Logic:
        ///     Generate JWT access and refresh tokens for new profile.
        ///
        TokenObject.Generated tokenGenerated =
            await _createJsonWebTokenHandler.ExecuteAsync(
                signupResult.Account.Id
            );

        /// Logic:
        ///     Commit database transaction to persist new account.
        ///
        await _unitOfWork.SyncAsync();

        /// Logic:
        ///     Initiate account setup process for role and domain configuration.
        ///
        await Setup(signupResult);

        return new(
            tokenGenerated.AccessToken,
            tokenGenerated.RefreshToken,
            signupResult.Account.Id
        );
    }

    ///
    /// Summary:
    ///     Initiates account setup process by publishing setup task and setting cache flag.
    ///     Marks account as requiring setup completion.
    ///
    /// Parameters:
    ///     signupResult:
    ///         The signup result containing account ID.
    ///
    private async Task Setup(AuthObject.SignupResult signupResult)
    {
        /// Logic:
        ///     Set the setup flag before the worker can deliver the queued message.
        ///
        string key = Format.ToCacheName(
            nameof(CacheNames.AUTH_USER_SETUP),
            signupResult.AccountId
        );
        await _cache.SetAsync(key, string.Empty);
        try
        {
            await _setupAccountAuthPublisher.Execute(signupResult.AccountId);
        }
        catch
        {
            await _cache.RemoveAsync(key);
            throw;
        }
    }
}
