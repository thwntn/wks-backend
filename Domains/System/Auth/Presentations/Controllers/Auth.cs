namespace Wks.Controllers;

///
/// Summary:
///     Provides HTTP API endpoints for user authentication and account management.
///     Handles signup, login, password operations, and Google OAuth integration.
///
[ApiController, Route(RouteConstant.AUTH), Tags(SwaggerConstant.AUTH)]
public class AuthController(
    IResetPasswordAuthHandler _resetPasswordHandler,
    IUsePasswordAuthHandler _usePasswordHandler,
    IChangePasswordAuthHandler _changePasswordHandler,
    ILoginGoogleAuthHandler _loginGoogleHandler,
    IGetUrlGoogleAuth _getUrlGoogleAuth,
    IStatusSetupGoogleAuth _statusSetupGoogleAuth,
    IConfirmCodeAuthHandler _confirmCodeHandler,
    ISignupAuthHandler _signupHandler,
    IVerifyAuth _verifyAuth
) : ControllerBase
{
    ///
    /// Summary:
    ///     Registers new user account with email and password.
    ///     Sends verification code email and initiates account setup.
    ///
    /// Parameters:
    ///     signup:
    ///         The signup data containing email, password, and name.
    ///
    /// Returns:
    ///     HTTP 200 OK with signup response containing account ID and verification code info.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///         Thrown when email already exists or validation fails.
    ///
    /// Logic:
    ///     Maps the allowed request fields to the action contract.
    ///     Invokes _signupHandler.ExecuteAsync.
    ///
    [HttpPost(RouteConstant.AUTH_SIGNUP)]
    public async Task<IActionResult> Signup(AuthDataTransformer.Signup signup)
    {
        AuthObject.SignupResult signupResponse =
            await _signupHandler.ExecuteAsync(signup);
        AuthObject.SignupResponse signupPayload =
            signupResponse.MapTo<AuthObject.SignupResponse>();
        return Ok(signupPayload);
    }

    ///
    /// Summary:
    ///     Verifies if email address exists in the system.
    ///     Used for pre-validation before password reset or other operations.
    ///
    /// Parameters:
    ///     verifyEmail:
    ///         The email verification data containing the email to verify.
    ///
    /// Returns:
    ///     HTTP 200 OK with boolean indicating email exists.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///         Thrown when email is not found in the system.
    ///
    /// Logic:
    ///     Invokes _verifyAuth.ExecuteAsync.
    ///
    [HttpPost(RouteConstant.AUTH_VERIFY_EMAIL)]
    public async Task<IActionResult> VerifyEmail(
        AuthDataTransformer.VerifyEmail verifyEmail
    )
    {
        bool status = await _verifyAuth.ExecuteAsync(verifyEmail);
        return Ok(status);
    }

    ///
    /// Summary:
    ///     Authenticates user with email and password credentials.
    ///     Returns JWT tokens for valid credentials or initiates confirmation for pending accounts.
    ///
    /// Parameters:
    ///     signin:
    ///         The signin credentials containing email and password.
    ///
    /// Returns:
    ///     HTTP 200 OK with signin result containing access token, refresh token, and account ID.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///         Thrown when credentials are invalid or account not found.
    ///
    /// Logic:
    ///     Maps the allowed request fields to the action contract.
    ///     Invokes _usePasswordHandler.ExecuteAsync.
    ///
    [HttpPost(RouteConstant.AUTH_USE_PASSWORD)]
    public async Task<IActionResult> UsePassword(
        [FromBody] AuthDataTransformer.Signin signin
    )
    {
        AuthObject.SigninResult signinResult =
            await _usePasswordHandler.ExecuteAsync(signin);
        AuthObject.SigninResult signinResponse =
            signinResult.MapTo<AuthObject.SigninResult>();
        return Ok(signinResponse);
    }

    ///
    /// Summary:
    ///     Confirms account using verification code sent to email.
    ///     Activates account and generates authentication tokens.
    ///
    /// Parameters:
    ///     confirmCode:
    ///         The confirmation data containing account ID and verification code.
    ///
    /// Returns:
    ///     HTTP 200 OK with signup response containing access token and account information.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///         Thrown when verification code is invalid or account not found.
    ///
    /// Logic:
    ///     Maps the allowed request fields to the action contract.
    ///     Invokes _confirmCodeHandler.ExecuteAsync.
    ///
    [HttpPost(RouteConstant.AUTH_CONFIRM_CODE)]
    public async Task<IActionResult> ConfirmCode(
        AuthDataTransformer.ConfirmCode confirmCode
    )
    {
        AuthObject.SignupResult signupResult =
            await _confirmCodeHandler.ExecuteAsync(
                confirmCode.AccountId,
                confirmCode.Code
            );
        AuthObject.SignupResponse signupResponse =
            signupResult.MapTo<AuthObject.SignupResponse>();
        return Ok(signupResponse);
    }

    ///
    /// Summary:
    ///     Initiates password reset by generating new password and sending via email.
    ///     Used when user has forgotten their password.
    ///
    /// Parameters:
    ///     resetPassword:
    ///         The reset data containing email address.
    ///
    /// Returns:
    ///     HTTP 200 OK with empty message indicating successful reset email sent.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///         Thrown when account with specified email is not found.
    ///
    /// Logic:
    ///     Invokes _resetPasswordHandler.ExecuteAsync.
    ///
    [HttpPatch(RouteConstant.AUTH_RESET_PASSWORD)]
    public async Task<IActionResult> ResetPassword(
        [FromBody] AuthDataTransformer.ResetPassword resetPassword
    )
    {
        string message = await _resetPasswordHandler.ExecuteAsync(
            resetPassword.Email
        );
        return Ok(message);
    }

    ///
    /// Summary:
    ///     Changes password for authenticated user.
    ///     Validates current password before updating to new password.
    ///
    /// Parameters:
    ///     changePassword:
    ///         The password change data containing email, current password, and new password.
    ///
    /// Returns:
    ///     HTTP 200 OK with empty message indicating successful password change.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///         Thrown when account not found or password validation fails.
    ///
    /// Logic:
    ///     Invokes _changePasswordHandler.ExecuteAsync.
    ///
    [HttpPatch(RouteConstant.AUTH_CHANGE_PASSWORD)]
    public async Task<IActionResult> ChangePassword(
        [FromBody] AuthDataTransformer.ChangePassword changePassword
    )
    {
        string message = await _changePasswordHandler.ExecuteAsync(
            changePassword
        );
        return Ok(message);
    }

    ///
    /// Summary:
    ///     Authenticates user using Google OAuth authorization code.
    ///     Handles both new user registration and existing user login.
    ///
    /// Parameters:
    ///     signinWithGoogleTransformer:
    ///         The Google signin data containing authorization code.
    ///
    /// Returns:
    ///     HTTP 200 OK with signin result containing JWT tokens and account ID.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///         Thrown when Google authorization fails or account operations fail.
    ///
    /// Logic:
    ///     Invokes _loginGoogleHandler.ExecuteAsync.
    ///
    [HttpPost(RouteConstant.AUTH_GOOGLE)]
    public async Task<IActionResult> Google(
        [FromBody]
            AuthDataTransformer.SigninWithGoogle signinWithGoogleTransformer
    )
    {
        AuthObject.SigninResult googleSignin =
            await _loginGoogleHandler.ExecuteAsync(
                signinWithGoogleTransformer.Code
            );
        return Ok(googleSignin);
    }

    ///
    /// Summary:
    ///     Generates Google OAuth authorization URL for user authentication.
    ///     Used to redirect user to Google login page.
    ///
    /// Returns:
    ///     HTTP 200 OK with Google OAuth authorization URL.
    ///
    /// Logic:
    ///     Invokes _getUrlGoogleAuth.Execute.
    ///
    [HttpGet(RouteConstant.AUTH_GOOGLE_PRESIGNED)]
    public IActionResult GetUrl()
    {
        string url = _getUrlGoogleAuth.Execute();
        return Ok(url);
    }

    ///
    /// Summary:
    ///     Checks if authenticated user's account setup is complete.
    ///     Used to determine if additional setup steps are required.
    ///
    /// Returns:
    ///     HTTP 200 OK with boolean indicating setup completion status.
    ///
    /// Logic:
    ///     Extract account ID from JWT claims in HttpContext.
    ///     Check setup status from cache.
    ///     Return true if setup complete, false if pending.
    ///
    [Authorize, HttpGet(RouteConstant.AUTH_GOOGLE_STATUS)]
    public async Task<IActionResult> StatusSetup()
    {
        ///
        /// Logic:
        ///     Extract account ID from JWT claims in HttpContext.
        ///
        Guid accountId = HttpContext.User().Identifier;

        ///
        /// Logic:
        ///     Check setup status from cache.
        ///     Return true if setup complete, false if pending.
        ///
        bool isSetupComplete = await _statusSetupGoogleAuth.ExecuteAsync(
            accountId
        );
        return Ok(isSetupComplete);
    }
}
