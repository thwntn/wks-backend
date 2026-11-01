namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to NameSetting.
///
public class NameSettingAdminAdapter(INameSetting _capability)
    : INameSettingAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     None.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task<List<SettingObject.Key>> ExecuteAsync() =>
        _capability.ExecuteAsync();
}
