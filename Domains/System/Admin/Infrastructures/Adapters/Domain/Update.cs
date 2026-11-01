namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator workflows to UpdateDomain.
///
public class UpdateDomainAdminAdapter(IUpdateDomain _capability)
    : IUpdateDomainAdminPort
{
    ///
    /// Summary:
    ///     Delegates ExecuteAsync without committing changes.
    /// Parameters:
    ///     profileId: The profile whose records are read or changed.
    ///     update: The fields allowed when updating this resource.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public Task<Domain> ExecuteAsync(
        Guid profileId,
        DomainDataTransformer.Update update
    ) => _capability.ExecuteAsync(profileId, update);
}
