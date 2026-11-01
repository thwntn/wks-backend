namespace Wks.Interfaces;

///
/// Summary:
///     Use case interface for validating refresh tokens.
///     Defines contract for refresh token expiration and active status verification.
///
public interface IRefreshTokenValidator
{
    ///
    /// Summary:
    ///     Validates the provided refresh token for authentication.
    ///     Verifies token expiration time and active status, managing cache invalidation for expired tokens.
    ///
    /// Parameters:
    ///     refreshToken:
    ///         The refresh token object to validate.
    ///
    /// Returns:
    ///     A task that represents the asynchronous operation containing the validated RefreshToken.
    ///
    Task<RefreshToken> ExecuteAsync(RefreshToken refreshToken);
}
