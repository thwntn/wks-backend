namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to ListRoles.
///
public class ListRolesAdminAdapter(IListRoles _capability) : IListRolesAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     accountId: The account whose roles or configuration are managed.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task<List<Role>> ExecuteAsync(Guid accountId) =>
        _capability.ExecuteAsync(accountId);
}
