namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to UpdateRole.
///
public class UpdateRoleAdminAdapter(IUpdateRole _capability)
    : IUpdateRoleAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     accountId: The account whose roles or configuration are managed.
    ///     roleId: The role selected for the authorization change.
    ///     updateParams: The allowed field changes for the selected resource.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task<Role> ExecuteAsync(
        Guid accountId,
        Guid roleId,
        RoleObject.UpdateParams updateParams
    ) => _capability.ExecuteAsync(accountId, roleId, updateParams);
}
