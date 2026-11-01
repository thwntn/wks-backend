namespace Wks.Interfaces;

///
/// Summary:
///     Contract for updating a setting by id or upserting by name.
///     Upsert is reserved for internal package configuration.
///
public interface IUpdateSetting
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
    Task<Setting> UpsertAsync(SettingDataTransformer.Create create);

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
    Task<Setting> ExecuteAsync(SettingDataTransformer.Update update);
}
