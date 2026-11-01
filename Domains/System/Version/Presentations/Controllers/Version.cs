namespace Wks.Controllers;

[ApiController, Route(RouteConstant.VERSION), Tags(SwaggerConstant.VERSIONS)]
public class VersionController(
    IGetVersion _getVersion,
    ICurrentVersion _currentVersion,
    IListVersion _listVersion
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List()
    {
        List<AppVersion> versions = await _listVersion.ExecuteAsync();
        return Ok(versions);
    }

    ///
    /// Summary:
    ///     Returns the latest version with its features, or empty when none exists.
    ///
    [HttpGet(RouteConstant.VERSION_CURRENT)]
    public async Task<IActionResult> Current()
    {
        AppVersion appVersion = await _currentVersion.ExecuteAsync();
        return Ok(appVersion);
    }

    [HttpGet(RouteConstant.ID)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        AppVersion appVersion = await _getVersion.ExecuteAsync(id);
        return Ok(appVersion);
    }
}
