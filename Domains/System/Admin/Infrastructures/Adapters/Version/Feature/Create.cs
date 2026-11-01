namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to CreateFeatureVersion.
///
public class CreateFeatureVersionAdminAdapter(ICreateFeatureVersion _capability)
    : ICreateFeatureVersionAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     versionId: The version that owns the requested changelog operation.
    ///     createParams: The allowed fields for the new resource.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task<AppFeature> ExecuteAsync(
        Guid versionId,
        VersionObject.CreateFeatureParams createParams
    ) => _capability.ExecuteAsync(versionId, createParams);
}
