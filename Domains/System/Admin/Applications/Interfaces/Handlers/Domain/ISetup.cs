namespace Wks.Interfaces;

///
/// Summary:
///     Defines administrator domain setup for a selected account.
///
public interface ISetupDomainAdminHandler
{
    ///
    /// Summary:
    ///     Sets up the requested domain for the destination account profile.
    /// Parameters:
    ///     accountId: The destination account identifier.
    ///     create: The host and routing type to register.
    /// Returns:
    ///     The domain owned by the selected account profile.
    /// Exceptions:
    ///     HttpException when the account profile or requested host is invalid.
    ///
    Task<Domain> ExecuteAsync(
        Guid accountId,
        DomainAdminObject.SetupParams create
    );
}
