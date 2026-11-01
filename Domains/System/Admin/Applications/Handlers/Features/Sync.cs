namespace Wks.Handlers;

///
/// Summary:
///     Atomically synchronizes default permissions and clears affected authorization caches.
///
public class SyncFeaturesAdminHandler(
    ISyncFeaturesAdmin _syncFeaturesAdmin,
    IUnitOfWork _unitOfWork,
    IPermissionCachePort _permissionCachePort
) : ISyncFeaturesAdminHandler
{
    ///
    /// Summary:
    ///     Commits all changes together before invalidating permission caches.
    /// Returns:
    ///     The number of synchronized non-admin accounts.
    ///
    public async Task<int> ExecuteAsync()
    {
        List<Guid> accountIds = await _syncFeaturesAdmin.ExecuteAsync();
        await _unitOfWork.SyncAsync();

        foreach (Guid accountId in accountIds)
            await _permissionCachePort.RemoveAsync(accountId);

        return accountIds.Count;
    }
}
