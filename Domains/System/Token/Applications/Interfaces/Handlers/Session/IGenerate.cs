namespace Wks.Interfaces;

///
/// Summary:
///     Defines access-token renewal using a stored refresh-token secret.
///
public interface IGenerateSessionTokenHandler
{
    ///
    /// Summary:
    ///     Renews access while preserving the original session subject.
    ///
    /// Parameters:
    ///     token:
    ///         The refresh-token secret supplied by the client.
    ///
    /// Returns:
    ///     The signed JWT containing the stored subject identifier.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///         Thrown when the token is invalid or its subject is unavailable.
    ///
    /// Logic:
    ///     Resolves and validates the refresh token before generating access claims.
    ///
    Task<string> ExecuteAsync(string token);
}
