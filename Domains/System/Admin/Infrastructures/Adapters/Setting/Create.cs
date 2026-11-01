namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to CreateSetting.
///
public class CreateSettingAdminAdapter(ICreateSetting _capability)
    : ICreateSettingAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     create: The fields allowed when creating this resource.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task<Setting> ExecuteAsync(SettingDataTransformer.Create create) =>
        _capability.ExecuteAsync(create);
}
