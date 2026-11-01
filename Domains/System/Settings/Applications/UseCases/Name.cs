namespace Wks.UseCases;

///
/// Summary:
///     Lists SettingNames from master metadata.
///     The JSON payload is cached like other status endpoints.
///
public class NameSetting(ICache _cache) : INameSetting
{
    ///
    /// Summary:
    ///     Returns setting keys from SettingName.JSON.
    ///
    /// Returns:
    ///     Setting keys with enum, name, and description.
    ///
    public async Task<List<SettingObject.Key>> ExecuteAsync() =>
        await _cache.CacheAsync(
            nameof(NameSetting),
            async () =>
            {
                string define = await File.ReadAllTextAsync(
                    $"{Directory.GetCurrentDirectory()}/Shared/Metadata/Master/System/SettingName.JSON"
                );
                return define.Deserialize<List<SettingObject.Key>>();
            }
        );
}
