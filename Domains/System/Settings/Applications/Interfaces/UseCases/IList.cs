namespace Wks.Interfaces;

///
/// Summary:
///     Contract for listing active system-wide configuration settings.
///     Soft-deleted settings are excluded from the result.
///
public interface IListSetting
{
    ///
    /// Summary:
    ///     Returns all settings that have not been soft-deleted.
    ///
    /// Returns:
    ///     Active settings ordered by creation time descending.
    ///
    Task<List<Setting>> ExecuteAsync();
}
