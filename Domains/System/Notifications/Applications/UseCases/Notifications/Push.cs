using System.Threading.Tasks;

namespace Wks.UseCases;

public class SystemPushNotification(ISocket _socket) : ISystemPushNotification
{
    public async Task ExecuteAsync(
        Guid profileId,
        NotificationDataTransformers.SystemPush systemPush
    ) =>
        await _socket.InvokeAsync(
            profileId,
            nameof(HubMethodName.SYSTEM_PUSH),
            systemPush
        );
}
