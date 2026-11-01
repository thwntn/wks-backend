namespace Wks.Handlers;

public class HandleNotificationHandler(
    IHandleNotification _handleNotification,
    IUnitOfWork _unitOfWork
) : IHandleNotificationHandler
{
    public async Task<Notification> ExecuteAsync(Guid profileId, Guid id)
    {
        Notification result = await _handleNotification.ExecuteAsync(
            profileId,
            id
        );
        await _unitOfWork.SyncAsync();
        return result;
    }
}
