namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to UpdateVersion.
///
public class UpdateVersionAdminAdapter(IUpdateVersion _capability)
    : IUpdateVersionAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     versionId: The version that owns the requested changelog operation.
    ///     updateParams: The allowed field changes for the selected resource.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task<AppVersion> ExecuteAsync(
        Guid versionId,
        VersionObject.UpdateParams updateParams
    ) => _capability.ExecuteAsync(versionId, updateParams);
}
