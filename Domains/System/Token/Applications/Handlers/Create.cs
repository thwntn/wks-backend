namespace Wks.Handlers;

///
/// Summary:
///     Orchestrates JSON Web Token creation workflow including refresh token validation.
///     Manages the complete token generation lifecycle with refresh token lifecycle management.
///
public class CreateJsonWebTokenHandler(
    IGetRefreshToken _getRefreshToken,
    IGenerateRefreshToken _createRefreshToken,
    IRefreshTokenValidator _checkRefreshToken,
    IGenerateAccessToken _createJsonWebToken
) : ICreateJsonWebTokenHandler
{
    ///
    /// Summary:
    ///     Creates or refreshes JWT access token and refresh token pair for user authentication.
    ///     Retrieves existing refresh token, validates it, generates new tokens if needed, and creates JWT.
    ///
    /// Parameters:
    ///     identifier:
    ///         The unique identifier of the user for whom to generate tokens.
    ///
    /// Returns:
    ///     A task that represents the asynchronous operation containing TokenObject.Generated with access and refresh tokens.
    ///
    public async Task<TokenObject.Generated> ExecuteAsync(Guid identifier)
    {
        /// Logic:
        ///     Retrieve existing refresh token for the user identifier.
        ///     If no valid refresh token exists, generate a new one.
        ///
        RefreshToken current = await _getRefreshToken.ExecuteAsync(identifier);
        if (current.Justifiable() is false)
            current = await _createRefreshToken.ExecuteAsync(identifier);

        /// Logic:
        ///     Validate the current refresh token for expiration and active status.
        ///     If validation fails, generate a new refresh token.
        ///
        RefreshToken @checked = await _checkRefreshToken.ExecuteAsync(current);
        if (@checked.Justifiable() is false)
            current = await _createRefreshToken.ExecuteAsync(identifier);

        /// Logic:
        ///     Generate JWT access token from the validated refresh token.
        ///     Return token pair containing access token and refresh token.
        ///
        string accessToken = await _createJsonWebToken.ExecuteAsync(current);

        return new(accessToken, current.Token);
    }

    // private List<string> Permission() {
    //     dead code removed - permissions now stored in Role entity as JSON
    // }
}
