namespace Wks.Interfaces;

public interface ICreateNotification
{
    Task<Notification> ExecuteAsync(
        Guid profileId,
        Guid fromAccount,
        NotificationNames type,
        object jsonData
    );
}
