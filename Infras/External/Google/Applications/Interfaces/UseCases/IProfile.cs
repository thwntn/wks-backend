namespace Wks.Interfaces;

///
/// Summary:
///     Interface for retrieving Google user profile information.
///     Provides user data access using OAuth access tokens.
///
public interface IGetProfileGoogle
{
    ///
    /// Summary:
    ///     Retrieves Google user profile information using an access token.
    ///
    /// Parameters:
    ///     accessToken:
    ///         The access token obtained from Google OAuth.
    ///
    /// Returns:
    ///     Profile response containing user information.
    ///
    Task<GoogleObject.ProfileResponse> ExecuteAsync(string accessToken);
}
