namespace Wks.Handlers;

///
/// Summary:
///     Saves the CreateNotification workflow before returning its result.
///
public class CreateNotificationHandler(
    ICreateNotification _createNotification,
    ISocket _socket,
    IUnitOfWork _unitOfWork
) : ICreateNotificationHandler
{
    ///
    /// Summary:
    ///     Executes the use case and saves its pending changes once.
    /// Parameters:
    ///     profileId:
    ///         The profile id supplied to the use case.
    ///     fromAccount:
    ///         The from account supplied to the use case.
    ///     type:
    ///         The type supplied to the use case.
    ///     jsonData:
    ///         The json data supplied to the use case.
    /// Returns:
    ///     The use case result after pending changes are saved.
    ///
    public async Task<Notification> ExecuteAsync(
        Guid profileId,
        Guid fromAccount,
        NotificationNames type,
        object jsonData
    )
    {
        Notification result = await _createNotification.ExecuteAsync(
            profileId,
            fromAccount,
            type,
            jsonData
        );
        await _unitOfWork.SyncAsync();
        await _socket.InvokeAsync(
            profileId,
            nameof(HubMethodName.NOTIFICATION_TRIGGER),
            string.Empty
        );
        return result;
    }
}
