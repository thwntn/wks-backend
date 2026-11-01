namespace Wks.Handlers;

///
/// Summary:
///     Soft-deletes system settings after admin authorization and cache invalidation.
///     Commits the unit of work before clearing the settings cache.
///
public class RemoveSettingHandler(
    IEnsureAdminSetting _ensureAdminSetting,
    IRemoveSetting _removeSetting,
    IUnitOfWork _unitOfWork,
    ICache _cache
) : IRemoveSettingHandler
{
    ///
    /// Summary:
    ///     Authorizes the caller, soft-deletes settings, then commits and invalidates cache.
    ///
    /// Parameters:
    ///     profileId:
    ///         The authenticated profile identifier used for the admin check and DeletedBy.
    ///
    ///     ids:
    ///         The setting identifiers to soft-delete.
    ///
    /// Returns:
    ///     An empty string after a successful remove.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the caller is not an admin or no matching settings are found.
    ///
    public async Task<string> ExecuteAsync(Guid profileId, List<Guid> ids)
    {
        /// Logic:
        ///     Ensure ADMIN_SETTING on the current account.
        ///     Soft-delete the settings, commit, and drop PROFILE_SETTING cache.
        ///
        await _ensureAdminSetting.ExecuteAsync(profileId);
        await _removeSetting.ExecuteAsync(profileId, ids);
        await _unitOfWork.SyncAsync();
        await _cache.RemoveAsync(nameof(CacheNames.PROFILE_SETTING));
        return string.Empty;
    }
}
