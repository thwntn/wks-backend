namespace Wks.Interfaces;

public interface IListNotification
{
    Task<Paged<Notification>> ExecuteAsync(
        Guid profileId,
        NotificationDataTransformers.QueryOptions queryOptions
    );
}
