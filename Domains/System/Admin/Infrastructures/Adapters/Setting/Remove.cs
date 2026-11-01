namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to RemoveSetting.
///
public class RemoveSettingAdminAdapter(IRemoveSetting _capability)
    : IRemoveSettingAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     profileId: The profile whose records are read or changed.
    ///     ids: The identifiers selected for removal.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task ExecuteAsync(Guid profileId, List<Guid> ids) =>
        _capability.ExecuteAsync(profileId, ids);
}
