namespace Wks.Interfaces;

///
/// Summary:
///     Contract for listing SettingNames master data.
///     Reads metadata the same way status APIs do.
///
public interface INameSetting
{
    ///
    /// Summary:
    ///     Returns every setting key defined in SettingNames.
    ///
    /// Returns:
    ///     Setting keys with enum, name, and description.
    ///
    Task<List<SettingObject.Key>> ExecuteAsync();
}
