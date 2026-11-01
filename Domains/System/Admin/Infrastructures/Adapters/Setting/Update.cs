namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to UpdateSetting.
///
public class UpdateSettingAdminAdapter(IUpdateSetting _capability)
    : IUpdateSettingAdminPort
{
    ///
    /// Summary:
    ///     Delegates UpsertAsync without committing changes.
    /// Parameters:
    ///     create: The fields allowed when creating this resource.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task<Setting> UpsertAsync(SettingDataTransformer.Create create) =>
        _capability.UpsertAsync(create);

    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     update: The fields allowed when updating this resource.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task<Setting> ExecuteAsync(SettingDataTransformer.Update update) =>
        _capability.ExecuteAsync(update);
}
