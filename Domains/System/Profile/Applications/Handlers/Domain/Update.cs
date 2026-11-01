namespace Wks.Handlers;

public class UpdateDomainHandler(
    IUpdateDomain _updateDomain,
    IUnitOfWork _unitOfWork,
    ISocket _socket
) : IUpdateDomainHandler
{
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
