namespace Wks.Handlers;

///
/// Summary:
///     Persists a new system setting after admin authorization and cache invalidation.
///     Commits the unit of work before clearing the settings cache.
///
public class CreateSettingHandler(
    IEnsureAdminSetting _ensureAdminSetting,
    ICreateSetting _createSetting,
    IUnitOfWork _unitOfWork,
    ICache _cache
) : ICreateSettingHandler
{
    ///
    /// Summary:
    ///     Authorizes the caller, creates the setting, then commits and invalidates cache.
    ///
    /// Parameters:
    ///     profileId:
    ///         The authenticated profile identifier used for the admin check.
    ///
    ///     create:
    ///         The name and value of the setting to create.
    ///
    /// Returns:
    ///     The created setting entity.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the caller is not an admin or the name already exists.
    ///
    public async Task<Setting> ExecuteAsync(
        Guid profileId,
        SettingDataTransformer.Create create
    )
    {
        /// Logic:
        ///     Ensure ADMIN_SETTING on the current account.
        ///     Create the setting, commit, and drop PROFILE_SETTING cache.
        ///
        await _ensureAdminSetting.ExecuteAsync(profileId);
        Setting result = await _createSetting.ExecuteAsync(create);
        await _unitOfWork.SyncAsync();
        await _cache.RemoveAsync(nameof(CacheNames.PROFILE_SETTING));
        return result;
    }
}
