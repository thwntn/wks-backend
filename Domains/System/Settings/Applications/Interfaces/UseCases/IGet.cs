namespace Wks.Interfaces;

///
/// Summary:
///     Contract for retrieving system settings as a dictionary or by identifier.
///     Dictionary lookups are used by internal services; get-by-id is used by the API.
///
public interface IGetSetting
{
    ///
    /// Summary:
    ///     Returns all active settings as a name-value dictionary from cache.
    ///
    /// Returns:
    ///     A dictionary of setting names mapped to their values.
    ///
    Task<Dictionary<string, string>> ExecuteAsync();

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
    Task<Dictionary<string, string>> ExecuteAsync(List<string> configs);

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
    Task<Setting> ExecuteAsync(Guid settingId);
}
