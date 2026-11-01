namespace Wks.UseCases;

///
/// Summary:
///     Use case for validating refresh-token activity and expiration.
///     Removes the subject cache entry when a stored token is no longer usable.
///
public class RefreshTokenValidator(ICache _cache) : IRefreshTokenValidator
{
    ///
    /// Summary:
    ///     Requires a refresh token to be both active and unexpired.
    ///
    /// Parameters:
    ///     refreshToken:
    ///         The stored refresh token resolved for the session.
    ///
    /// Returns:
    ///     The supplied refresh token when it is active and still within its lifetime.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///         Thrown with Unauthorized when the token is expired or inactive.
    ///
    /// Logic:
    ///     Returns the token only when activity and expiration checks both pass.
    ///     Removes the subject cache entry before rejecting an invalid token.
    ///
    public async Task<RefreshToken> ExecuteAsync(RefreshToken refreshToken)
    {
        if (
            refreshToken.ExpiredAt > Moment.Utc.Now
            && refreshToken.IsActive == true
        )
            return refreshToken;

        string cacheName = Format.ToCacheName(
            nameof(CacheNames.JWT_REFRESH_TOKEN),
            string.Concat(refreshToken.Identifier)
        );
        await _cache.RemoveAsync(cacheName);
        throw new HttpException(
            HttpStatus.Unauthorized,
            MessageConstant.SESSION_IS_CLOSED_REFRESH_TOKEN
        );
    }
}
