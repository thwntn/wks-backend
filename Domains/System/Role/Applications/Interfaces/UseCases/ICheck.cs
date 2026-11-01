namespace Wks.Interfaces;

///
/// Summary:
///     Defines permission evaluation for root and actor account identifiers.
///
public interface ICheckPermission
{
    ///
    /// Summary:
    ///     Checks whether the account scope owns any required permission code.
    ///
    /// Parameters:
    ///     accountId:
    ///         The raw account ID stored in the JWT Identifier claim.
    ///
    ///     permissions:
    ///         The codes accepted by the authorization policy.
    ///
    /// Returns:
    ///     True when the subject or its linked actors own a required code.
    ///
    /// Logic:
    ///     Matches role ownership by Account.Id or Account.Identifier.
    ///
    Task<bool> ExecuteAsync(Guid accountId, List<string> permissions);
}
