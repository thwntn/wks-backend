namespace Wks.UseCases;

///
/// Summary:
///     Use case for resolving a session by its presented refresh-token secret.
///
public class ResolveSessionToken(
    IRefreshTokenRepository _refreshTokenRepository
) : IResolveSessionToken
{
    ///
    /// Summary:
    ///     Retrieves the refresh token presented by the client.
    ///
    /// Parameters:
    ///     token:
    ///         The opaque refresh-token secret supplied through the route.
    ///
    /// Returns:
    ///     The stored refresh token containing the original subject identifier.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///         Thrown when the token is blank, deleted or unknown.
    ///
    /// Logic:
    ///     Rejects an empty secret before querying the repository.
    ///     Matches the exact secret and excludes soft-deleted records.
    ///
    public async Task<RefreshToken> ExecuteAsync(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new HttpException(
                HttpStatus.Unauthorized,
                MessageConstant.SESSION_IS_CLOSED_REFRESH_TOKEN
            );

        return await _refreshTokenRepository.Raw.FirstOrDefaultAsync(
                refreshToken =>
                    refreshToken.Token == token
                    && refreshToken.DeletedAt.HasValue == false
            )
            ?? throw new HttpException(
                HttpStatus.Unauthorized,
                MessageConstant.SESSION_IS_CLOSED_REFRESH_TOKEN
            );
    }
}
