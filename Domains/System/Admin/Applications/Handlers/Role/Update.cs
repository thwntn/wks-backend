namespace Wks.Handlers;

///
/// Summary:
///     Coordinates administrator UpdateRoleHandler and saves through UnitOfWork.
///
public class UpdateRoleAdminHandler(
    IUpdateRoleAdminPort _updateRole,
    IUnitOfWork _unitOfWork,
    IPermissionCachePort _permissionCachePort
) : IUpdateRoleAdminHandler
{
    ///
    /// Summary:
    ///     Stages domain changes, saves once, then completes configured side effects.
    /// Parameters:
    ///     accountId: The account whose roles or configuration are managed.
    ///     roleId: The role selected for the authorization change.
    ///     updateParams: The allowed field changes for the selected resource.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public async Task<Role> ExecuteAsync(
        Guid accountId,
        Guid roleId,
        RoleObject.UpdateParams updateParams
    )
    {
        Role role = await _updateRole.ExecuteAsync(
            accountId,
            roleId,
            updateParams
        );
        await _unitOfWork.SyncAsync();
        await _permissionCachePort.RemoveAsync(role.AccountId);
        return role;
    }
}
