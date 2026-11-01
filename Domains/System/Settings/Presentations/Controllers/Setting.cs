namespace Wks.Controllers;

///
/// Summary:
///     Provides system settings management endpoints.
///     List and get are available to authenticated users; create, update, and remove require ADMIN_SETTING.
///
[ApiController, Route(RouteConstant.SETTINGS), Tags(SwaggerConstant.SETTINGS)]
public class SettingsController(IGetSetting _getSetting) : ControllerBase
{
    ///
    /// Summary:
    ///     Returns a single active setting by identifier.
    ///
    /// Parameters:
    ///     id:
    ///         The unique identifier of the setting to retrieve.
    ///
    /// Returns:
    ///     An HTTP 200 response containing the setting entity.
    ///
    /// Exceptions:
    ///     T:HttpException:
    ///     Thrown when the setting is missing or soft-deleted.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpGet(RouteConstant.ID)
    ]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        Setting setting = await _getSetting.ExecuteAsync(id);
        return Ok(setting);
    }
}
