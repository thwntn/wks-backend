namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to CreateVersion.
///
public class CreateVersionAdminAdapter(ICreateVersion _capability)
    : ICreateVersionAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     createParams: The allowed fields for the new resource.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task<AppVersion> ExecuteAsync(
        VersionObject.CreateParams createParams
    ) => _capability.ExecuteAsync(createParams);
}
