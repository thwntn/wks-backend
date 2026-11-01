namespace Wks.UseCases;

public class ReadNotification(
    INotificationRepository _notificationRepository,
    ISocket _socketService
) : IReadNotification
{
    public async Task<Notification> ExecuteAsync(
        Guid profileId,
        Guid notificationId
    )
    {
        Notification notification =
            _notificationRepository.Raw.FirstOrDefault(notification =>
                notification.Id == notificationId
                && notification.ProfileId == profileId
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_NOTIFICATION
            );

        notification.IsRead = true;
        _notificationRepository.Update(notification);

        await RealtimeUpdateAsync(profileId);
        return notification;
    }

    private async Task RealtimeUpdateAsync(Guid profileId) =>
        await _socketService.InvokeAsync(
            profileId,
            nameof(HubMethodName.NOTIFICATION_TRIGGER),
            string.Empty
        );
}
