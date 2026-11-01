namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to GetProfile.
///
public class GetProfileAdminAdapter(IGetProfile _capability)
    : IGetProfileAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     profileId: The profile whose records are read or changed.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task<Profile> ExecuteAsync(Guid profileId) =>
        _capability.ExecuteAsync(profileId);
}
