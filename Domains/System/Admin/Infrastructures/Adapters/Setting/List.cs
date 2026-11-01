namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to ListSetting.
///
public class ListSettingAdminAdapter(IListSetting _capability)
    : IListSettingAdminPort
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
    public Task<List<Setting>> ExecuteAsync() => _capability.ExecuteAsync();
}
