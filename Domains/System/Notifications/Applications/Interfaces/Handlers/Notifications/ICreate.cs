namespace Wks.Interfaces;

///
/// Summary:
///     Persists the CreateNotification workflow through UnitOfWork.
///
public interface ICreateNotificationHandler
{
    Task<Notification> ExecuteAsync(
        Guid profileId,
        Guid fromAccount,
        NotificationNames type,
        object jsonData
    );
}
