namespace Wks.Interfaces;

///
/// Summary:
///     Defines JWT signing without subject identity resolution.
///
public interface IGenerateAccessToken
{
    ///
    /// Summary:
    ///     Signs the subject identifier contained in the refresh token.
    ///
    /// Parameters:
    ///     refreshToken:
    ///         The stored token whose Identifier is copied unchanged.
    ///
    /// Returns:
    ///     The signed JWT string.
    ///
    /// Logic:
    ///     Copies Identifier and signs the token without querying account data.
    ///
    Task<string> ExecuteAsync(RefreshToken refreshToken);
}
