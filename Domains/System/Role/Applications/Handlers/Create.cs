namespace Wks.Handlers;

public class CreateRoleHandler(
    ICreateRole _createRole,
    IUnitOfWork _unitOfWork,
    IPermissionCachePort _permissionCachePort
) : ICreateRoleHandler
{
    public async Task<Role> ExecuteAsync(RoleObject.CreateParams createParams)
    {
        Role role = await _createRole.ExecuteAsync(createParams);
        await _unitOfWork.SyncAsync();
        await _permissionCachePort.RemoveAsync(createParams.AccountId);
        return role;
    }
}
