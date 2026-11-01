namespace Wks.Handlers;

public class MakeAdminHandler(
    IMakeAdmin _makeAdmin,
    IUnitOfWork _unitOfWork,
    IPermissionCachePort _permissionCachePort
) : IMakeAdminHandler
{
    public async Task<Role> ExecuteAsync(Guid accountId)
    {
        Role role = await _makeAdmin.ExecuteAsync(accountId);
        await _unitOfWork.SyncAsync();
        await _permissionCachePort.RemoveAsync(accountId);
        return role;
    }
}
