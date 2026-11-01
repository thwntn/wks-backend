namespace Wks.Handlers;

///
/// Summary:
///     Persists a setting update after admin authorization and cache invalidation.
///     Commits the unit of work before clearing the settings cache.
///
public class UpdateSettingHandler(
    IEnsureAdminSetting _ensureAdminSetting,
    IUpdateSetting _updateSetting,
    IUnitOfWork _unitOfWork,
    ICache _cache
) : IUpdateSettingHandler
{
    ///
    /// Summary:
    ///     Authorizes the caller, updates the setting, then commits and invalidates cache.
    ///
    /// Parameters:
    ///     profileId:
    ///         The authenticated profile identifier used for the admin check.
    ///
    ///     update:
    ///         The identifier, name, and value to persist.
    ///
    /// Returns:
    ///     The updated setting entity.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the caller is not an admin, the setting is missing, or the name conflicts.
    ///
    public async Task<Setting> ExecuteAsync(
        Guid profileId,
        SettingDataTransformer.Update update
    )
    {
        /// Logic:
        ///     Ensure ADMIN_SETTING on the current account.
        ///     Update the setting, commit, and drop PROFILE_SETTING cache.
        ///
        await _ensureAdminSetting.ExecuteAsync(profileId);
        Setting result = await _updateSetting.ExecuteAsync(update);
        await _unitOfWork.SyncAsync();
        await _cache.RemoveAsync(nameof(CacheNames.PROFILE_SETTING));
        return result;
    }
}
