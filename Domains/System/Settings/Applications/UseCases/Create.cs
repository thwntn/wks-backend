namespace Wks.UseCases;

///
/// Summary:
///     Creates a system-wide configuration setting with a unique name.
///     Only active settings are considered when checking name uniqueness.
///
public class CreateSetting(ISettingRepository _settingRepository)
    : ICreateSetting
{
    ///
    /// Summary:
    ///     Persists a new setting when the name is not already in use.
    ///
    /// Parameters:
    ///     create:
    ///         The name and value of the setting to create.
    ///
    /// Returns:
    ///     The created setting entity.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when an active setting already uses the same name.
    ///
    public async Task<Setting> ExecuteAsync(
        SettingDataTransformer.Create create
    )
    {
        /// Logic:
        ///     Look up an active setting with the same name.
        ///     Throw when the name already exists.
        ///     Create and return the new setting.
        ///
        Setting existing = await _settingRepository.FirstOrDefaultAsync(
            setting =>
                setting.Name == create.Name
                && setting.DeletedAt.HasValue == false
        );

        if (existing.Justifiable())
            throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.SETTING_NAME_ALREADY_EXISTS
            );

        return _settingRepository.Create(new Setting(create.Name, create.Value));
    }
}
