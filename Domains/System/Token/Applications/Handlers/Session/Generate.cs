namespace Wks.Handlers;

///
/// Summary:
///     Handler for access-token renewal using a validated stored refresh token.
///
public class GenerateSessionTokenHandler(
    IResolveSessionToken _resolveSessionToken,
    IRefreshTokenValidator _refreshTokenValidator,
    IGenerateAccessToken _generateAccessToken
) : IGenerateSessionTokenHandler
{
    ///
    /// Summary:
    ///     Renews access using the original session subject stored by the server.
    ///
    /// Parameters:
    ///     token:
    ///         The refresh-token secret presented by the client.
    ///
    /// Returns:
    ///     The signed access token with the original stored subject identifier.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///         Thrown when the refresh token is missing, inactive or expired.
    ///
    /// Logic:
    ///     Retrieves the exact stored token and validates its activity and expiration.
    ///     Generates access claims from the unchanged stored subject identifier without creating
    ///     another refresh token or reading claims from an expired access token.
    ///
    public async Task<string> ExecuteAsync(string token)
    {
        RefreshToken refreshToken = await _resolveSessionToken.ExecuteAsync(
            token
        );
        RefreshToken validatedToken = await _refreshTokenValidator.ExecuteAsync(
            refreshToken
        );
        return await _generateAccessToken.ExecuteAsync(validatedToken);
    }
}
