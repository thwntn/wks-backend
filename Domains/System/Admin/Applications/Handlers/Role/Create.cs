namespace Wks.Handlers;

///
/// Summary:
///     Coordinates administrator CreateRoleHandler and saves through UnitOfWork.
///
public class CreateRoleAdminHandler(
    ICreateRoleAdminPort _createRole,
    IUnitOfWork _unitOfWork,
    IPermissionCachePort _permissionCachePort
) : ICreateRoleAdminHandler
{
    ///
    /// Summary:
    ///     Stages domain changes, saves once, then completes configured side effects.
    /// Parameters:
    ///     createParams: The allowed fields for the new resource.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public async Task<Role> ExecuteAsync(RoleObject.CreateParams createParams)
    {
        Role role = await _createRole.ExecuteAsync(createParams);
        await _unitOfWork.SyncAsync();
        await _permissionCachePort.RemoveAsync(createParams.AccountId);
        return role;
    }
}
