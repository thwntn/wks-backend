namespace Wks.Handlers;

public class ReadNotificationHandler(
    IReadNotification _readNotification,
    IUnitOfWork _unitOfWork
) : IReadNotificationHandler
{
    public async Task<Notification> ExecuteAsync(Guid profileId, Guid id)
    {
        Notification result = await _readNotification.ExecuteAsync(
            profileId,
            id
        );
        await _unitOfWork.SyncAsync();
        return result;
    }
}
