namespace Wks.UseCases;

///
/// Summary:
///     Soft-deletes system-wide configuration settings.
///     Matching rows stay in the database with deletion metadata.
///
public class RemoveSetting(ISettingRepository _settingRepository)
    : IRemoveSetting
{
    ///
    /// Summary:
    ///     Marks the requested active settings as deleted by the current profile.
    ///
    /// Parameters:
    ///     profileId:
    ///         The authenticated profile identifier recorded as DeletedBy.
    ///
    ///     ids:
    ///         The setting identifiers to soft-delete.
    ///
    /// Returns:
    ///     Task representing the asynchronous remove operation.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when none of the requested settings are found or still active.
    ///
    public async Task ExecuteAsync(Guid profileId, List<Guid> ids)
    {
        /// Logic:
        ///     Load active settings matching the requested identifiers.
        ///     Throw when the list is empty.
        ///     Soft-delete the matched settings with the current profile.
        ///
        List<Setting> settings = await _settingRepository.WhereAsync(setting =>
            ids.Contains(setting.Id) && setting.DeletedAt.HasValue == false
        );

        if (settings.Count.Equals(0))
            throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_SETTING
            );

        _settingRepository.SoftRemove(settings, profileId);
    }
}
