namespace Wks.Adapters;

///
/// Summary:
///     Adapter for generating JWT access and refresh tokens for authentication.
///     Coordinates token generation services to produce authentication token pair.
///
public class GenerateAccessTokenAuthAdapter(
    IGenerateAccessToken _generateAccessToken,
    IGenerateRefreshToken _generateRefreshToken
) : IGenerateAccessTokenAuthPort
{
    ///
    /// Summary:
    ///     Generates both access and refresh tokens for a user account.
    ///     Creates refresh token first, then derives access token from it.
    ///
    /// Parameters:
    ///     accountId:
    ///         The unique identifier of the user account to generate tokens for.
    ///
    /// Returns:
    ///     Signin result containing both access token and refresh token strings.
    ///
    /// Exceptions:
    ///     T:Exception:
    ///     Thrown when token generation fails or token configuration is invalid.
    ///
    public async Task<AuthObject.SigninResult> Execute(Guid accountId)
    {
        /// Logic:
        ///     Generate refresh token entity containing account ID and expiration.
        ///
        RefreshToken refreshToken = await _generateRefreshToken.ExecuteAsync(
            accountId
        );

        /// Logic:
        ///     Generate access token string from refresh token data.
        ///
        string accessToken = await _generateAccessToken.ExecuteAsync(
            refreshToken
        );

        /// Logic:
        ///     Return signin result with both token strings.
        ///
        return new(accessToken, refreshToken.Token);
    }
}
