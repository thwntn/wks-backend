namespace Wks.UseCases;

///
/// Summary:
///     Authenticates users using email and password credentials.
///     Handles both open accounts and accounts requiring email confirmation.
///
public class LoginAuth(
    IAccountRepository _accountRepository,
    ILoginAccountRepository _loginAccountRepository,
    ISendCodeAuth _templateConfirmCodeMail,
    ICreateJsonWebTokenHandler _createJsonWebTokenHandler
) : ILoginAuth
{
    ///
    /// Summary:
    ///     Authenticates a user with email and password.
    ///     Returns JWT tokens for authenticated users or sends confirmation code for pending accounts.
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
    ///     Thrown when account is not found or credentials are invalid.
    ///
    public async Task<AuthObject.SigninResult> ExecuteAsync(
        AuthDataTransformer.Signin signin
    )
    {
        /// Logic:
        ///     Hash password using MD5 algorithm for comparison.
        ///
        string hashPassword = Cryptography.Md5(signin.Password);

        /// Logic:
        ///     Query account with profile data using email and hashed password.
        ///     Throw exception if account not found.
        ///
        Account account =
            _accountRepository
                .Include(account => account.Profile)
                .FirstOrDefault(account =>
                    account.Email == signin.Email
                    && account.HashPassword == hashPassword
                )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ACCOUNT,
                ErrorCodeConstant.AUTH_INVALID_IDENTIFIER
            );
        return await Handler(account);
    }

    ///
    /// Summary:
    ///     Processes account authentication and generates JWT tokens.
    ///     Records login activity and updates last login timestamp.
    ///
    /// Parameters:
    ///     account:
    ///         The authenticated account entity with profile data.
    ///
    /// Returns:
    ///     Authentication result containing JWT tokens and account information.
    ///
    public async Task<AuthObject.SigninResult> Handler(Account account)
    {
        /// Logic:
        ///     Generate JWT access and refresh tokens for the profile.
        ///
        TokenObject.Generated tokenGenerated =
            await _createJsonWebTokenHandler.ExecuteAsync(account.Id);

        /// Logic:
        ///     For open accounts, assign access token directly.
        ///     For pending accounts, generate and send confirmation code via email.
        ///
        if (account.AccountStatus == AccountStatus.Open)
            account.AccessToken = tokenGenerated.AccessToken;
        else
        {
            string code = Cryptography.RandomCode();
            account.Code = code;
            _accountRepository.Update(account);

            await _templateConfirmCodeMail.ExecuteAsync(account.Email, code);
        }

        /// Logic:
        ///     Record login activity with current UTC timestamp.
        ///     Create login history entry for audit purposes.
        ///
        DateTime loginTime = Moment.Utc.Now;
        LoginAccount accountLogin = new()
        {
            CreatedAt = loginTime,
            AccountId = account.Id,
        };
        _loginAccountRepository.Create(accountLogin);

        /// Logic:
        ///     Update profile last login timestamp.
        ///     Return authentication result with tokens and account ID.
        ///
        account.Profile.LastLogin = loginTime;
        return new(
            tokenGenerated.AccessToken,
            tokenGenerated.RefreshToken,
            account.Id
        );
    }
}
