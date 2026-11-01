namespace Wks.UseCases;

///
/// Summary:
///     Updates or creates system-wide configuration settings.
///     Public updates target an existing setting by id; upsert is for internal use.
///
public class UpdateSetting(ISettingRepository _settingRepository)
    : IUpdateSetting
{
    ///
    /// Summary:
    ///     Creates a setting by name when missing, otherwise updates its value.
    ///
    /// Parameters:
    ///     create:
    ///         The name and value used to upsert the setting.
    ///
    /// Returns:
    ///     The created or updated setting entity.
    ///
    public async Task<Setting> UpsertAsync(
        SettingDataTransformer.Create create
    )
    {
        /// Logic:
        ///     Find an active setting by name.
        ///     Create a new setting when none exists.
        ///     Otherwise update the value and return the entity.
        ///
        Setting setting = await _settingRepository.Raw.FirstOrDefaultAsync(
            setting =>
                setting.Name == create.Name
                && setting.DeletedAt.HasValue == false
        );
        if (setting.Justifiable() is false)
        {
            Setting created = new(create.Name, create.Value);
            _settingRepository.Create(created);
            return created;
        }

        setting.Value = create.Value;
        _settingRepository.Update(setting);
        return setting;
    }

    ///
    /// Summary:
    ///     Updates an active setting by identifier and keeps the name unique.
    ///
    /// Parameters:
    ///     update:
    ///         The identifier, name, and value to persist.
    ///
    /// Returns:
    ///     The updated setting entity.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the setting is not found or another setting already uses the name.
    ///
    public async Task<Setting> ExecuteAsync(
        SettingDataTransformer.Update update
    )
    {
        /// Logic:
        ///     Load the active setting by id or throw not found.
        ///     Reject the change when another active setting already uses the name.
        ///     Persist the new name and value.
        ///
        Setting current =
            await _settingRepository.FirstOrDefaultAsync(setting =>
                setting.Id == update.Id && setting.DeletedAt.HasValue == false
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_SETTING
            );

        Setting existedName = await _settingRepository.FirstOrDefaultAsync(
            setting =>
                setting.Name == update.Name
                && setting.Id != update.Id
                && setting.DeletedAt.HasValue == false
        );

        if (existedName.Justifiable())
            throw new HttpException(
                HttpStatus.BadRequest,
                MessageConstant.SETTING_NAME_ALREADY_EXISTS
            );

        current.Name = update.Name;
        current.Value = update.Value;
        _settingRepository.Update(current);
        return current;
    }
}
