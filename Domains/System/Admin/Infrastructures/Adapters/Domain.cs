namespace Wks.Adapters;

///
/// Summary:
///     Connects administrator domain setup to the profile domain capability.
///
public class DomainAdminAdapter(ICreateDomain _createDomain) : IDomainAdminPort
{
    ///
    /// Summary:
    ///     Delegates domain validation and staging without saving changes.
    /// Parameters:
    ///     profileId: The profile whose records are read or changed.
    ///     create: The fields allowed when creating this resource.
    /// Returns:
    ///     The staged domain returned by the profile module.
    /// Exceptions:
    ///     HttpException when domain creation is rejected.
    ///
    public Task<Domain> ExecuteAsync(
        Guid profileId,
        DomainAdminObject.SetupParams create
    ) =>
        _createDomain.ExecuteAsync(
            profileId,
            create.MapTo<DomainDataTransformer.Create>()
        );
}
