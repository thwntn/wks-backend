namespace Wks.Handlers;

///
/// Summary:
///     Coordinates administrator RemoveDomainHandler and saves through UnitOfWork.
///
public class RemoveDomainAdminHandler(
    IRemoveDomainAdminPort _removeDomain,
    IUnitOfWork _unitOfWork,
    ISocket _socket
) : IRemoveDomainAdminHandler
{
    ///
    /// Summary:
    ///     Stages domain changes, saves once, then completes configured side effects.
    /// Parameters:
    ///     profileId: The profile whose records are read or changed.
    ///     ids: The identifiers selected for removal.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public async Task<string> ExecuteAsync(Guid profileId, List<Guid> ids)
    {
        await _removeDomain.ExecuteAsync(profileId, ids);
        await _unitOfWork.SyncAsync();
        await _socket.InvokeAsync(profileId, nameof(HubMethodName.DOMAIN));
        return string.Empty;
    }
}
