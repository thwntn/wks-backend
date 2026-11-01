namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to RemoveVersion.
///
public class RemoveVersionAdminAdapter(IRemoveVersion _capability)
    : IRemoveVersionAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     versionId: The version that owns the requested changelog operation.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task ExecuteAsync(Guid versionId) =>
        _capability.ExecuteAsync(versionId);
}
