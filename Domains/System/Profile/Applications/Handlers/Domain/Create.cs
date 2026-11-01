namespace Wks.Handlers;

public class CreateDomainHandler(
    ICreateDomain _createDomain,
    IUnitOfWork _unitOfWork,
    ISocket _socket
) : ICreateDomainHandler
{
    public async Task<Domain> ExecuteAsync(
        Guid profileId,
        DomainDataTransformer.Create create
    )
    {
        Domain result = await _createDomain.ExecuteAsync(profileId, create);
        await _unitOfWork.SyncAsync();
        await _socket.InvokeAsync(profileId, nameof(HubMethodName.DOMAIN));
        return result;
    }
}
