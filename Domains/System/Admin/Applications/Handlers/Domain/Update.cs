namespace Wks.Handlers;

///
/// Summary:
///     Coordinates administrator UpdateDomainHandler and saves through UnitOfWork.
///
public class UpdateDomainAdminHandler(
    IUpdateDomainAdminPort _updateDomain,
    IUnitOfWork _unitOfWork,
    ISocket _socket
) : IUpdateDomainAdminHandler
{
    ///
    /// Summary:
    ///     Stages domain changes, saves once, then completes configured side effects.
    /// Parameters:
    ///     profileId: The profile whose records are read or changed.
    ///     update: The fields allowed when updating this resource.
    /// Returns:
    ///     The result supplied by the domain capability.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    public async Task<Domain> ExecuteAsync(
        Guid profileId,
        DomainDataTransformer.Update update
    )
    {
        Domain result = await _updateDomain.ExecuteAsync(profileId, update);
        await _unitOfWork.SyncAsync();
        await _socket.InvokeAsync(profileId, nameof(HubMethodName.DOMAIN));
        return result;
    }
}
