namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to PermissionRole.
///
public class PermissionRoleAdminAdapter(IPermissionRole _capability)
    : IPermissionRoleAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     None.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task<List<RoleObject.PermissionMaster>> ExecuteAsync() =>
        _capability.ExecuteAsync();
}
