namespace Wks.Handlers;

public class RemoveRoleHandler(
    IRemoveRole _removeRole,
    IUnitOfWork _unitOfWork,
    IPermissionCachePort _permissionCachePort
) : IRemoveRoleHandler
{
    public async Task<IEnumerable<Role>> ExecuteAsync(
        RoleObject.RemoveParams removeParams
    )
    {
        IEnumerable<Role> roles = await _removeRole.ExecuteAsync(removeParams);
        await _unitOfWork.SyncAsync();

        foreach (
            Guid accountId in roles.Select(role => role.AccountId).Distinct()
        )
            await _permissionCachePort.RemoveAsync(accountId);

        return roles;
    }
}
