namespace Wks.Interfaces;

///
/// Summary:
///     Contract for creating a system-wide configuration setting.
///     Enforces unique names among active settings.
///
public interface ICreateSetting
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
    Task<Setting> ExecuteAsync(SettingDataTransformer.Create create);
}
