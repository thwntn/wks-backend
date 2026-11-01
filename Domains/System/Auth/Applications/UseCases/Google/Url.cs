namespace Wks.UseCases;

///
/// Summary:
///     Generates Google OAuth authorization URL for user authentication.
///     Passes runtime client credentials into the reusable Google URL builder.
///
public class GetUrlGoogleAuth(IGetUrlGoogle _getUrlGoogle) : IGetUrlGoogleAuth
{
    ///
    /// Summary:
    ///     Builds Google OAuth authorization URL with required parameters.
    ///     Includes openid, email, and profile scopes for user information access.
    ///
    /// Returns:
    ///     Complete Google OAuth authorization URL with encoded parameters.
    ///
    public string Execute()
    {
        /// Logic:
        ///     Pass platform Google client credentials from runtime environment.
        ///     Authorization host is read inside the Google module.
        ///
        GoogleObject.Setting setting = new(
            Runtime.GoogleClientId.Env(),
            Runtime.GoogleSecretId.Env(),
            Runtime.GoogleRedirectUri.Env()
        );
        return _getUrlGoogle.Execute(setting);
    }
}
