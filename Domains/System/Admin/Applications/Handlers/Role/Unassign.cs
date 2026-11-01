namespace Wks.Handlers;

///
/// Summary:
///     Coordinates administrator UnassignRoleHandler and saves through UnitOfWork.
///
public class UnassignRoleAdminHandler(
    IUnassignRoleAdminPort _unassignRole,
    IUnitOfWork _unitOfWork,
    IPermissionCachePort _permissionCachePort
) : IUnassignRoleAdminHandler
{
    ///
    /// Summary:
    ///     Stages domain changes, saves once, then completes configured side effects.
    /// Parameters:
    ///     roleId: The role selected for the authorization change.
    ///     unassignParams: The account and role unassignment data.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public async Task<string> ExecuteAsync(
        Guid roleId,
        RoleObject.UnassignParams unassignParams
    )
    {
        string result = await _unassignRole.ExecuteAsync(
            roleId,
            unassignParams
        );
        await _unitOfWork.SyncAsync();
        await _permissionCachePort.RemoveAsync(unassignParams.AccountId);
        return result;
    }
}
