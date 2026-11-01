namespace Wks.Handlers;

public class UpdateRoleHandler(
    IUpdateRole _updateRole,
    IUnitOfWork _unitOfWork,
    IPermissionCachePort _permissionCachePort
) : IUpdateRoleHandler
{
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
