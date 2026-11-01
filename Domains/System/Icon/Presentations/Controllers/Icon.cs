namespace Wks.Controllers;

[ApiController, Route(RouteConstant.ICONS), Tags(SwaggerConstant.ICONS)]
public class IconController(
    IListIcon _listIcon,
    ICreateIconHandler _createIconHandler,
    IRemoveIconHandler _removeIconHandler
) : ControllerBase
{
    [Permission(nameof(PermissionConstant.ADMIN_SETTING)), HttpGet]
    public async Task<IActionResult> List()
    {
        IEnumerable<Icon> icons = await _listIcon.ExecuteAsync();
        return Ok(icons);
    }

    [Permission(nameof(PermissionConstant.ADMIN_SETTING)), HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] IconDataTransformer.Create create
    )
    {
        Icon icon = await _createIconHandler.ExecuteAsync(create);
        return Ok(icon);
    }

    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpDelete(RouteConstant.ID)
    ]
    public async Task<IActionResult> Remove([FromRoute] Guid id)
    {
        string message = await _removeIconHandler.ExecuteAsync(id);
        return Ok(message);
    }
}
