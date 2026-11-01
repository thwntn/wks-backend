namespace Wks.Interfaces;

public interface IHandleNotificationHandler
{
    Task<Notification> ExecuteAsync(Guid profileId, Guid id);
}
