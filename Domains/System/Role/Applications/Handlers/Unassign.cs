namespace Wks.Handlers;

public class UnassignRoleHandler(
    IUnassignRole _unassignRole,
    IUnitOfWork _unitOfWork,
    IPermissionCachePort _permissionCachePort
) : IUnassignRoleHandler
{
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
