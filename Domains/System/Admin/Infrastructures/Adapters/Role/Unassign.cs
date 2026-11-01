namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to UnassignRole.
///
public class UnassignRoleAdminAdapter(IUnassignRole _capability)
    : IUnassignRoleAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     roleId: The role selected for the authorization change.
    ///     unassignParams: The account and role unassignment data.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task<string> ExecuteAsync(
        Guid roleId,
        RoleObject.UnassignParams unassignParams
    ) => _capability.ExecuteAsync(roleId, unassignParams);
}
