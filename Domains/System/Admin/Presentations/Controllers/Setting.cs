namespace Wks.Controllers;

///
/// Summary:
///     Provides administrator-only setting endpoints.
///
[ApiController, Route(RouteConstant.ADMIN), Tags(SwaggerConstant.ADMIN)]
[Permission(nameof(PermissionConstant.ADMIN_SETTING))]
public class SettingAdminController(
    ICreateSettingAdminHandler _createSettingHandler,
    IListSettingAdminPort _listSetting,
    INameSettingAdminPort _nameSetting,
    IRemoveSettingAdminHandler _removeSettingHandler,
    IUpdateSettingAdminHandler _updateSettingHandler
) : ControllerBase
{
    ///
    /// Summary:
    ///     Lists supported names for setting records through the administrator workflow.
    /// Parameters:
    ///     None.
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpGet(RouteConstant.ADMIN_SETTING_NAME)
    ]
    public async Task<IActionResult> Name()
    {
        List<SettingObject.Key> names = await _nameSetting.ExecuteAsync();
        return Ok(names);
    }

    ///
    /// Summary:
    ///     Lists setting records through the administrator workflow.
    /// Parameters:
    ///     None.
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpGet(RouteConstant.ADMIN_SETTING_LIST)
    ]
    public async Task<IActionResult> List()
    {
        List<Setting> settings = await _listSetting.ExecuteAsync();
        return Ok(settings);
    }

    ///
    /// Summary:
    ///     Creates setting records through the administrator workflow.
    /// Parameters:
    ///     create: The fields allowed when creating this resource.
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpPost(RouteConstant.ADMIN_SETTING_CREATE)
    ]
    public async Task<IActionResult> Create(
        [FromBody] SettingDataTransformer.Create create
    )
    {
        Guid identifier = await HttpContext.Identifier();
        Setting setting = await _createSettingHandler.ExecuteAsync(
            identifier,
            create
        );
        return Ok(setting);
    }

    ///
    /// Summary:
    ///     Updates setting records through the administrator workflow.
    /// Parameters:
    ///     update: The fields allowed when updating this resource.
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpPut(RouteConstant.ADMIN_SETTING_UPDATE)
    ]
    public async Task<IActionResult> Update(
        [FromBody] SettingDataTransformer.Update update
    )
    {
        Guid identifier = await HttpContext.Identifier();
        Setting setting = await _updateSettingHandler.ExecuteAsync(
            identifier,
            update
        );
        return Ok(setting);
    }

    ///
    /// Summary:
    ///     Soft-deletes setting records through the administrator workflow.
    /// Parameters:
    ///     remove: The identifiers selected for soft deletion.
    /// Returns:
    ///     HTTP 200 with the existing domain response after successful completion.
    /// Exceptions:
    ///     Domain validation and infrastructure failures propagate to the caller.
    ///
    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpDelete(RouteConstant.ADMIN_SETTING_REMOVE)
    ]
    public async Task<IActionResult> Remove(
        [FromBody] SettingDataTransformer.Remove remove
    )
    {
        Guid identifier = await HttpContext.Identifier();
        string message = await _removeSettingHandler.ExecuteAsync(
            identifier,
            remove.Ids
        );
        return Ok(message);
    }
}
