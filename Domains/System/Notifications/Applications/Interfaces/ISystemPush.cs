namespace Wks.Interfaces;

public interface ISystemPushNotification
{
    Task ExecuteAsync(
        Guid profileId,
        NotificationDataTransformers.SystemPush systemPush
    );
}
