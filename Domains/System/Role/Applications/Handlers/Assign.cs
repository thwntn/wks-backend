namespace Wks.Handlers;

public class AssignRoleHandler(
    IAssignRole _assignRole,
    IUnitOfWork _unitOfWork,
    IPermissionCachePort _permissionCachePort
) : IAssignRoleHandler
{
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
