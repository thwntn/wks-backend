namespace Wks.UseCases;

///
/// Summary:
///     Generates secure refresh tokens for user authentication sessions.
///     Creates cryptographically strong refresh tokens with expiration tracking and persistence.
///
public class GenerateRefreshToken(
    IRefreshTokenRepository _refreshTokenRepository
) : IGenerateRefreshToken
{
    ///
    /// Summary:
    ///     The length of the random hexadecimal component of the refresh token.
    ///
    /// Returns:
    ///     Integer constant value of 20 characters.
    ///
    private const int TOKEN_LENGTH = 20;

    ///
    /// Summary:
    ///     Generates a new refresh token for the specified user identifier.
    ///     Creates a cryptographically secure token string combining random hex and MD5 hash.
    ///
    /// Parameters:
    ///     identifier:
    ///         The unique identifier of the user for whom to generate the refresh token.
    ///
    /// Returns:
    ///     A task that represents the asynchronous operation containing the newly created RefreshToken object.
    ///
    public async Task<RefreshToken> ExecuteAsync(Guid identifier)
    {
        /// Logic:
        ///     Calculate token expiration time by adding configured hours to current UTC time.
        ///
        DateTime expired = Moment.Utc.Now.AddHours(
            int.Parse(Runtime.RefreshTokenExpired.Env())
        );

        /// Logic:
        ///     Generate secure token string by combining random hex string and MD5 hash of current timestamp.
        ///     Convert result to lowercase for consistency.
        ///
        string token = Format.ToLower(
            Cryptography.RandomHexString(TOKEN_LENGTH)
                + Cryptography.Md5(string.Concat(Moment.Utc.Now.Ticks))
        );

        /// Logic:
        ///     Create refresh token entity with identifier, token string, and expiration.
        ///     Persist token to repository for future validation.
        ///
        RefreshToken refreshToken = new(identifier, token, expired);
        _refreshTokenRepository.Create(refreshToken);

        await Task.CompletedTask;
        return refreshToken;
    }
}
