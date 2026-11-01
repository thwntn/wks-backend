namespace Wks.Interfaces;

///
/// Summary:
///     Handler contract for creating a system setting after admin authorization.
///     Commits the unit of work and invalidates the settings cache.
///
public interface ICreateSettingHandler
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
    Task<Setting> ExecuteAsync(
        Guid profileId,
        SettingDataTransformer.Create create
    );
}
