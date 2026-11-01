namespace Wks.Interfaces;

///
/// Summary:
///     Defines session lookup using the refresh-token secret supplied by the client.
///
public interface IResolveSessionToken
{
    ///
    /// Summary:
    ///     Retrieves a stored refresh token without relying on access-token claims.
    ///
    /// Parameters:
    ///     token:
    ///         The opaque refresh-token secret supplied through the route.
    ///
    /// Returns:
    ///     The refresh token containing the original subject identifier.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///         Thrown when the secret is blank, deleted or unknown.
    ///
    /// Logic:
    ///     Matches the exact secret against a non-deleted stored token.
    ///
    Task<RefreshToken> ExecuteAsync(string token);
}
