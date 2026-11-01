namespace Wks.Handlers;

public class RemoveDomainHandler(
    IRemoveDomain _removeDomain,
    IUnitOfWork _unitOfWork,
    ISocket _socket
) : IRemoveDomainHandler
{
    public async Task<string> ExecuteAsync(Guid profileId, List<Guid> ids)
    {
        await _removeDomain.ExecuteAsync(profileId, ids);
        await _unitOfWork.SyncAsync();
        await _socket.InvokeAsync(profileId, nameof(HubMethodName.DOMAIN));
        return string.Empty;
    }
}
