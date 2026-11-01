namespace Wks.Handlers;

///
/// Summary:
///     Persists administrator domain setup and notifies the destination profile.
///
public class SetupDomainAdminHandler(
    ISetupDomainAdmin _setupDomainAdmin,
    IUnitOfWork _unitOfWork,
    ISocket _socket
) : ISetupDomainAdminHandler
{
    ///
    /// Summary:
    ///     Saves the domain once before publishing its update event.
    /// Parameters:
    ///     accountId: The account selected by the administrator.
    ///     create: The domain host and routing type.
    /// Returns:
    ///     The persisted domain belonging to the destination profile.
    /// Exceptions:
    ///     HttpException when setup is invalid; save and socket failures propagate.
    ///
    public async Task<Domain> ExecuteAsync(
        Guid accountId,
        DomainAdminObject.SetupParams create
    )
    {
        Domain domain = await _setupDomainAdmin.ExecuteAsync(accountId, create);
        await _unitOfWork.SyncAsync();
        await _socket.InvokeAsync(
            domain.ProfileId,
            nameof(HubMethodName.DOMAIN)
        );
        return domain;
    }
}
