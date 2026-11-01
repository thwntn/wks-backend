namespace Wks.Interfaces;

///
/// Summary:
///     Handler contract for updating a system setting after admin authorization.
///     Commits the unit of work and invalidates the settings cache.
///
public interface IUpdateSettingHandler
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
    Task<Setting> ExecuteAsync(
        Guid profileId,
        SettingDataTransformer.Update update
    );
}
