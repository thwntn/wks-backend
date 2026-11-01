using System.Threading.Tasks;

namespace Wks.Controllers;

[
    ApiController,
    Route(RouteConstant.NOTIFICATIONS),
    Tags(SwaggerConstant.NOTIFICATIONS)
]
public class NotificationController(
    IListNotification _listNotification,
    IReadNotificationHandler _readNotificationHandler,
    IHandleNotificationHandler _handleNotificationHandler
) : ControllerBase
{
    [Authorize, HttpGet]
    public async Task<IActionResult> List(
        [FromQuery] NotificationDataTransformers.QueryOptions queryOptions
    )
    {
        Guid profileId = await HttpContext.Identifier(
            personal: true
        );
        Paged<Notification> paged = await _listNotification.ExecuteAsync(
            profileId,
            queryOptions
        );
        return Ok(paged);
    }

    [
        Authorize,
        HttpGet(RouteConstant.NOTIFICATION_READ + "/" + RouteConstant.ID)
    ]
    public async Task<IActionResult> Read([FromRoute] Guid id)
    {
        Guid profileId = await HttpContext.Identifier(
            personal: true
        );
        Notification readResult = await _readNotificationHandler.ExecuteAsync(
            profileId,
            id
        );
        return Ok(readResult);
    }

    [
        Authorize,
        HttpGet(RouteConstant.NOTIFICATION_HANDLE + "/" + RouteConstant.ID)
    ]
    public async Task<IActionResult> Handle([FromRoute] Guid id)
    {
        Guid profileId = await HttpContext.Identifier(
            personal: true
        );
        Notification handleResult =
            await _handleNotificationHandler.ExecuteAsync(profileId, id);
        return Ok(handleResult);
    }
}
