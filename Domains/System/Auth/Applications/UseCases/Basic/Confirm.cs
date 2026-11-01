namespace Wks.UseCases;

///
/// Summary:
///     Confirms user account using verification code sent via email.
///     Activates pending accounts and generates authentication tokens upon successful verification.
///
public class ConfirmCodeAuth(
    IAccountRepository _accountRepository,
    IGenerateAccessTokenAuthPort _generateAccessTokenAuthPort,
    IGetAccountWithRoleProfile _getAccountWithRole
) : IConfirmCodeAuth
{
    ///
    /// Summary:
    ///     Verifies confirmation code and activates account.
    ///     Generates JWT tokens for newly confirmed accounts.
    ///
    /// Parameters:
    ///     accountId:
    ///         The unique identifier of the account to confirm.
    ///
    ///     code:
    ///         The verification code sent to the user's email.
    ///
    /// Returns:
    ///     Signup result containing access token, refresh token, account ID, and email.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the confirmation code does not match or is invalid.
    ///
    public async Task<AuthObject.SignupResult> ExecuteAsync(
        Guid accountId,
        string code
    )
    {
        /// Logic:
        ///     Retrieve account with role information using account ID.
        ///
        Account account = _getAccountWithRole.Execute(accountId);

        /// Logic:
        ///     Compare provided code with stored code.
        ///     Throw exception if codes do not match.
        ///
        if (account.Code.CompareTo(code) is not 0)
            throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.CONFIRM_CODE_NOT_SUCCESS
            );

        /// Logic:
        ///     Update account status to Open (activated).
        ///     Persist changes to database.
        ///
        account.AccountStatus = AccountStatus.Open;
        _accountRepository.Update(account);

        /// Logic:
        ///     Generate JWT access and refresh tokens for the confirmed profile.
        ///
        AuthObject.SigninResult signinResponse =
            await _generateAccessTokenAuthPort.Execute(account.Id);

        /// Logic:
        ///     Assign access token to account.
        ///     Return signup result with tokens and account information.
        ///
        account.AccessToken = signinResponse.AccessToken;
        return new(
            signinResponse.AccessToken,
            signinResponse.RefreshToken,
            account.Id,
            account.Email,
            default
        );
    }
}
