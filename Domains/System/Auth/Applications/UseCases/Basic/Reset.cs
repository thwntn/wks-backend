namespace Wks.UseCases;

///
/// Summary:
///     Handles password reset and password change operations for user accounts.
///     Provides functionality to reset forgotten passwords and change existing passwords.
///
public class ResetPasswordAuth(
    IAccountRepository _accountRepository,
    ISendCodeAuth _templateConfirmCodeMail
) : IResetPasswordAuth
{
    ///
    /// Summary:
    ///     Initiates password reset process by generating new password.
    ///     Sends the new password to user's email address.
    ///
    /// Parameters:
    ///     email:
    ///         The email address of the account requesting password reset.
    ///
    /// Returns:
    ///     The updated profile information after password reset.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the account with specified email is not found.
    ///
    public async Task<Profile> ExecuteAsync(string email)
    {
        /// Logic:
        ///     Query account with profile information using email.
        ///     Throw exception if account not found.
        ///
        Account account =
            _accountRepository
                .Raw.Include(account => account.Profile)
                .FirstOrDefault(account => account.Email == email)
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ACCOUNT
            );

        /// Logic:
        ///     Generate random password code.
        ///     Hash the new password using MD5 algorithm.
        ///
        string password = Cryptography.RandomCode();
        account.HashPassword = Cryptography.Md5(password);

        /// Logic:
        ///     Send new password to user via email.
        ///     Update account with new hashed password.
        ///
        await _templateConfirmCodeMail.ExecuteAsync(email, password);

        _accountRepository.Update(account);
        return account.Profile;
    }

    ///
    /// Summary:
    ///     Changes password for authenticated user account.
    ///     Validates current password before updating to new password.
    ///
    /// Parameters:
    ///     changePassword:
    ///         The password change data containing email, current password, new password, and confirmation.
    ///
    /// Returns:
    ///     The updated profile information after successful password change.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when account not found, current password is incorrect, or new passwords do not match.
    ///
    public async Task<Profile> ChangePasswordAsync(
        AuthDataTransformer.ChangePassword changePassword
    )
    {
        /// Logic:
        ///     Hash current password using MD5 algorithm.
        ///     Query account with profile where email and hashed password match.
        ///     Throw exception if account not found or password incorrect.
        ///
        string hashPassword = Cryptography.Md5(changePassword.Password);
        Account account =
            await _accountRepository
                .Raw.Include(user => user.Profile)
                .FirstOrDefaultAsync(user =>
                    user.Email == changePassword.Email
                    && user.HashPassword == hashPassword
                )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ACCOUNT
            );

        /// Logic:
        ///     Validate that new password matches confirmation password.
        ///     If match, hash and update password.
        ///     Otherwise, throw password mismatch exception.
        ///
        if (changePassword.NewPassword == changePassword.ConfirmPassword)
            account.HashPassword = Cryptography.Md5(changePassword.NewPassword);
        else
            throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.PASSWORD_NOT_MATCH
            );

        /// Logic:
        ///     Send confirmation email with new password.
        ///     Update account with new password in database.
        ///
        await _templateConfirmCodeMail.ExecuteAsync(
            changePassword.Email,
            changePassword.NewPassword
        );

        _accountRepository.Update(account);
        return account.Profile;
    }
}
