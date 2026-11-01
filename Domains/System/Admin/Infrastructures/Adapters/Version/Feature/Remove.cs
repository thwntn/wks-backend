namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to RemoveFeatureVersion.
///
public class RemoveFeatureVersionAdminAdapter(IRemoveFeatureVersion _capability)
    : IRemoveFeatureVersionAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     featureId: The changelog entry to remove.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task ExecuteAsync(Guid featureId) =>
        _capability.ExecuteAsync(featureId);
}
