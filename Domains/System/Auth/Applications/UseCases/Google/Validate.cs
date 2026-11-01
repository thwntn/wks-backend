namespace Wks.UseCases;

///
/// Summary:
///     Validates Google OAuth authorization code and retrieves user account.
///     Exchanges authorization code for access token and fetches Google profile information.
///
public class ValidateAccountGoogle(
    IGetAccessTokenGoogle _getAccessTokenGoogle,
    IGetProfileGoogle _getProfileGoogle,
    IAccountRepository _accountRepository
) : IValidateAccountGoogle
{
    ///
    /// Summary:
    ///     Validates Google authorization code and retrieves or identifies user account.
    ///     Determines if account exists in system based on Google email.
    ///
    /// Parameters:
    ///     authCode:
    ///         The authorization code received from Google OAuth callback.
    ///
    /// Returns:
    ///     Google validation result containing account existence status, email, profile ID, name, and account entity.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when Google API requests fail or authorization code is invalid.
    ///
    public async Task<AuthObject.GoogleValidate> ExecuteAsync(string authCode)
    {
        /// Logic:
        ///     Build Google client credentials from runtime environment.
        ///     Exchange authorization code for Google access token.
        ///
        GoogleObject.AccessTokenResponse accessTokenResponse =
            await _getAccessTokenGoogle.ExecuteAsync(authCode, new(
            Runtime.GoogleClientId.Env(),
            Runtime.GoogleSecretId.Env(),
            Runtime.GoogleRedirectUri.Env()
        ));

        /// Logic:
        ///     Retrieve Google user profile using access token.
        ///
        GoogleObject.ProfileResponse profileResponse =
            await _getProfileGoogle.ExecuteAsync(
                accessTokenResponse.access_token
            );

        /// Logic:
        ///     Query local account by Google email with profile information.
        ///     Account will be null if user doesn't exist in system.
        ///
        Account account = await _accountRepository
            .Include(account => account.Profile)
            .FirstOrDefaultAsync(account =>
                account.Email == profileResponse.email
            );

        /// Logic:
        ///     Return validation result with new user flag (true if account is null).
        ///     Include Google profile data and existing account if found.
        ///
        return new(
            account.Justifiable() == false,
            profileResponse.email,
            profileResponse.id,
            profileResponse.name,
            account
        );
    }
}
