namespace Wks.Interfaces;

public interface IHandleNotification
{
    Task<Notification> ExecuteAsync(Guid profileId, Guid notificationId);
}
