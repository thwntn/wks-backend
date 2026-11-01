namespace Wks.Interfaces;

public interface IReadNotificationHandler
{
    Task<Notification> ExecuteAsync(Guid profileId, Guid id);
}
