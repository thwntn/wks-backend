namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to AssignRole.
///
public class AssignRoleAdminAdapter(IAssignRole _capability)
    : IAssignRoleAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     roleId: The role selected for the authorization change.
    ///     assignParams: The account and role assignment data.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task<Role> ExecuteAsync(
        Guid roleId,
        RoleObject.AssignParams assignParams
    ) => _capability.ExecuteAsync(roleId, assignParams);
}
