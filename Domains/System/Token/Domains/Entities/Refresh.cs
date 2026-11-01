namespace Wks.Databases;

///
/// Summary:
///     Entity representing refresh tokens for user authentication sessions.
///     Stores refresh token data with expiration tracking and active status management.
///
public class RefreshToken(Guid identifier, string token, DateTime expiredAt)
    : Entity,
        IEntity
{
    ///
    /// Summary:
    ///     The expiration timestamp for the refresh token.
    ///
    /// Returns:
    ///     DateTime value indicating when the refresh token expires.
    ///
    [Required]
    public DateTime ExpiredAt { get; set; } = expiredAt;

    ///
    /// Summary:
    ///     The cryptographically secure refresh token string.
    ///
    /// Returns:
    ///     String value containing the refresh token.
    ///
    [Required]
    public string Token { get; set; } = token;

    ///
    /// Summary:
    ///     Indicates whether the refresh token is currently active.
    ///
    /// Returns:
    ///     Boolean value where true means token is active and valid for use.
    ///
    [Required]
    public bool IsActive { get; set; } = true;

    ///
    /// Summary:
    ///     The unique identifier of the user associated with this refresh token.
    ///
    /// Returns:
    ///     GUID value representing the user identifier.
    ///
    [Required]
    public Guid Identifier { get; set; } = identifier;
}
