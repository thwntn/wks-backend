namespace Wks.UseCases;

///
/// Summary:
///     Retrieves the most recent active refresh token for a specified user identifier.
///     Queries the refresh token repository to find valid tokens for authentication.
///
public class GetRefreshToken(IRefreshTokenRepository _refreshTokenRepository)
    : IGetRefreshToken
{
    ///
    /// Summary:
    ///     Retrieves the most recent active refresh token for the specified identifier.
    ///     Orders tokens by creation date descending and filters by identifier and active status.
    ///
    /// Parameters:
    ///     identifier:
    ///         The unique identifier of the user whose refresh token to retrieve.
    ///
    /// Returns:
    ///     A task that represents the asynchronous operation containing the RefreshToken if found, null otherwise.
    ///
    public async Task<RefreshToken> ExecuteAsync(Guid identifier) =>
        await _refreshTokenRepository
            .Raw.OrderByDescending(refreshToken => refreshToken.CreatedAt)
            .FirstOrDefaultAsync(refreshToken =>
                refreshToken.Identifier == identifier && refreshToken.IsActive
            );
}
