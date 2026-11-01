namespace Wks.Interfaces;

///
/// Summary:
///     Provides domain creation capability to administrator workflows.
///
public interface IDomainAdminPort
{
    ///
    /// Summary:
    ///     Stages a domain for the resolved destination profile.
    /// Parameters:
    ///     profileId: The trusted destination profile identifier.
    ///     create: The requested host and routing type.
    /// Returns:
    ///     The staged domain without committing changes.
    /// Exceptions:
    ///     HttpException when domain rules reject the requested host.
    ///
    Task<Domain> ExecuteAsync(
        Guid profileId,
        DomainAdminObject.SetupParams create
    );
}
