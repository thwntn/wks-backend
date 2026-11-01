namespace Wks.UseCases;

///
/// Summary:
///     Verifies email addresses exist in the system.
///     Validates account existence before operations like password reset.
///
public class VerifyAuth(IAccountRepository _accountRepository) : IVerifyAuth
{
    ///
    /// Summary:
    ///     Checks if email address has associated account in the system.
    ///     Retrieves account with profile information for validation.
    ///
    /// Parameters:
    ///     verifyEmail:
    ///         The email verification data containing the email to verify.
    ///
    /// Returns:
    ///     True if email exists and account is found.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the email address is not found in the system.
    ///
    public async Task<bool> ExecuteAsync(
        AuthDataTransformer.VerifyEmail verifyEmail
    )
    {
        /// Logic:
        ///     Query account with profile using provided email.
        ///     Throw exception if no account found with the email.
        ///
        Account account =
            await _accountRepository
                .Raw.Include(account => account.Profile)
                .FirstOrDefaultAsync(account =>
                    account.Email == verifyEmail.Email
                )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_EMAIL
            );
        return true;
    }
}
