namespace Wks.Interfaces;

///
/// Summary:
///     Interface for generating Google OAuth authorization URLs.
///     Builds the redirect URL from runtime host and caller-supplied client credentials.
///
public interface IGetUrlGoogle
{
    ///
    /// Summary:
    ///     Builds the Google OAuth authorization URL.
    ///
    /// Parameters:
    ///     setting:
    ///         Google OAuth client credentials supplied by the caller.
    ///
    /// Returns:
    ///     Complete Google OAuth authorization URL with encoded parameters.
    ///
    string Execute(GoogleObject.Setting setting);
}
