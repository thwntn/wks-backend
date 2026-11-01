namespace Wks.Interfaces;

///
/// Summary:
///     Interface for retrieving Google OAuth access tokens.
///     Provides token exchange functionality for Google authentication.
///
public interface IGetAccessTokenGoogle
{
    ///
    /// Summary:
    ///     Exchanges a Google authorization code for an access token.
    ///
    /// Parameters:
    ///     authorizationCode:
    ///         The authorization code received from the Google OAuth callback.
    ///
    ///     setting:
    ///         Google OAuth client credentials supplied by the caller.
    ///
    /// Returns:
    ///     Access token response containing the token and metadata.
    ///
    Task<GoogleObject.AccessTokenResponse> ExecuteAsync(
        string authorizationCode,
        GoogleObject.Setting setting
    );
}
