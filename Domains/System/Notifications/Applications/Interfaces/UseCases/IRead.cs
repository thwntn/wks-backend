namespace Wks.Interfaces;

public interface IReadNotification
{
    Task<Notification> ExecuteAsync(Guid profileId, Guid notificationId);
}
