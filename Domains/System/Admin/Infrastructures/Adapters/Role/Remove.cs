namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to RemoveRole.
///
public class RemoveRoleAdminAdapter(IRemoveRole _capability)
    : IRemoveRoleAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     removeParams: The identifiers and ownership data for removal.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task<IEnumerable<Role>> ExecuteAsync(
        RoleObject.RemoveParams removeParams
    ) => _capability.ExecuteAsync(removeParams);
}
