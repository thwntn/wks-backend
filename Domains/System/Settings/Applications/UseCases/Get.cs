namespace Wks.UseCases;

///
/// Summary:
///     Retrieves system-wide configuration settings with caching support.
///     Dictionary lookups skip soft-deleted settings.
///
public class GetSetting(DatabaseContext _databaseContext, ICache _cache)
    : IGetSetting
{
    ///
    /// Summary:
    ///     Returns all active settings as a name-value dictionary from cache.
    ///
    /// Returns:
    ///     A dictionary of setting names mapped to their values.
    ///
    public async Task<Dictionary<string, string>> ExecuteAsync()
    {
        /// Logic:
        ///     Read settings from cache or load active rows from the database.
        ///     Convert the list into a name-value dictionary.
        ///
        List<Setting> settings = await _cache.CacheAsync(
            nameof(CacheNames.PROFILE_SETTING),
            () =>
                EntityFrameworkQueryableExtensions.ToListAsync(
                    _databaseContext.Setting.Where(setting =>
                        setting.DeletedAt.HasValue == false
                    )
                )
        );

        return settings.ToDictionary(
            key => key.Name,
            value => value.Value.ToString()
        );
    }

    ///
    /// Summary:
    ///     Returns the requested setting names as a name-value dictionary.
    ///
    /// Parameters:
    ///     configs:
    ///         The setting names to load.
    ///
    /// Returns:
    ///     A dictionary of matching active setting names mapped to their values.
    ///
    public async Task<Dictionary<string, string>> ExecuteAsync(
        List<string> configs
    )
    {
        /// Logic:
        ///     Query active settings whose names are in the requested list.
        ///     Convert the list into a name-value dictionary.
        ///
        List<Setting> settings =
            await EntityFrameworkQueryableExtensions.ToListAsync(
                _databaseContext.Setting.Where(setting =>
                    configs.Contains(setting.Name)
                    && setting.DeletedAt.HasValue == false
                )
            );

        return settings.ToDictionary(
            key => key.Name,
            value => value.Value.ToString()
        );
    }

    ///
    /// Summary:
    ///     Returns a single active setting by identifier.
    ///
    /// Parameters:
    ///     settingId:
    ///         The unique identifier of the setting to retrieve.
    ///
    /// Returns:
    ///     The matching setting entity.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the setting is missing or soft-deleted.
    ///
    public async Task<Setting> ExecuteAsync(Guid settingId) =>
        await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(
            _databaseContext.Setting.Where(setting =>
                setting.Id == settingId && setting.DeletedAt.HasValue == false
            )
        )
        ?? throw new HttpException(
            HttpStatus.NotFound,
            MessageConstant.NOT_FOUND_SETTING
        );
}
