namespace Wks.Handlers;

///
/// Summary:
///     Coordinates administrator AssignRoleHandler and saves through UnitOfWork.
///
public class AssignRoleAdminHandler(
    IAssignRoleAdminPort _assignRole,
    IUnitOfWork _unitOfWork,
    IPermissionCachePort _permissionCachePort
) : IAssignRoleAdminHandler
{
    ///
    /// Summary:
    ///     Stages domain changes, saves once, then completes configured side effects.
    /// Parameters:
    ///     roleId: The role selected for the authorization change.
    ///     assignParams: The account and role assignment data.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public async Task<Role> ExecuteAsync(
        Guid roleId,
        RoleObject.AssignParams assignParams
    )
    {
        Role role = await _assignRole.ExecuteAsync(roleId, assignParams);
        await _unitOfWork.SyncAsync();
        await _permissionCachePort.RemoveAsync(assignParams.AccountId);
        return role;
    }
}
