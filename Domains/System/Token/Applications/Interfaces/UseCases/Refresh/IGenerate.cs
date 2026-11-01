namespace Wks.Interfaces;

///
/// Summary:
///     Use case interface for generating cryptographically secure refresh tokens.
///     Defines contract for refresh token creation with expiration management.
///
public interface IGenerateRefreshToken
{
    ///
    /// Summary:
    ///     Generates a new refresh token for the specified user identifier.
    ///     Creates secure token string with expiration and persists to repository.
    ///
    /// Parameters:
    ///     identifier:
    ///         The unique identifier of the user for whom to generate the refresh token.
    ///
    /// Returns:
    ///     A task that represents the asynchronous operation containing the newly created RefreshToken.
    ///
    Task<RefreshToken> ExecuteAsync(Guid identifier);
}
