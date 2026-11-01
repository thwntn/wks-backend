namespace Wks.UseCases;

///
/// Summary:
///     Lists active system-wide configuration settings.
///     Soft-deleted settings are excluded from the result.
///
public class ListSetting(ISettingRepository _settingRepository) : IListSetting
{
    ///
    /// Summary:
    ///     Returns all settings that have not been soft-deleted.
    ///
    /// Returns:
    ///     Active settings ordered by creation time descending.
    ///
    public async Task<List<Setting>> ExecuteAsync() =>
        await _settingRepository
            .Where(setting => setting.DeletedAt.HasValue == false)
            .OrderByDescending(setting => setting.CreatedAt)
            .ToListAsync();
}
