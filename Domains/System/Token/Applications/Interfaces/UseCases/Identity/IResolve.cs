namespace Wks.Interfaces;

///
/// Summary:
///     Defines request-time account identity resolution into a profile scope.
///
public interface IResolveIdentityToken
{
    ///
    /// Summary:
    ///     Resolves the personal or effective business profile for an account.
    ///
    /// Parameters:
    ///     accountId:
    ///         The unchanged account ID from the authenticated token.
    ///
    ///     personal:
    ///         True for the signed-in account profile; false for the business scope.
    ///
    /// Returns:
    ///     The personal profile ID or the parent profile ID for an actor business request.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///         Thrown when the account or required profile is unavailable.
    ///
    /// Logic:
    ///     Reads the account on each request and resolves an actor parent only for business scope.
    ///
    Task<Guid> ExecuteAsync(Guid accountId, bool personal = false);
}
