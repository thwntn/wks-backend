namespace Wks.UseCases;

///
/// Summary:
///     Generates Google OAuth authorization URLs.
///     Uses the runtime authorization host and client credentials supplied by the caller.
///
public class GetUrlGoogle : IGetUrlGoogle
{
    ///
    /// Summary:
    ///     Builds the Google OAuth authorization URL with required parameters.
    ///     Includes openid, email, and profile scopes for user information access.
    ///
    /// Parameters:
    ///     setting:
    ///         Google OAuth client credentials supplied by the caller.
    ///
    /// Returns:
    ///     Complete Google OAuth authorization URL with encoded parameters.
    ///
    public string Execute(GoogleObject.Setting setting) =>
        $"{Runtime.GoogleAuthorizationHost.Env()}?client_id={setting.ClientId}&redirect_uri={Uri.EscapeDataString(setting.RedirectUri)}&response_type=code&scope=openid%20email%20profile";
}
