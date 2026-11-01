namespace Wks.Controllers;

[ApiController, Route(RouteConstant.FEATURE), Tags(SwaggerConstant.FEATURES)]
public class FeaturesController(
    IAddFeatureHandler _addFeatureHandler,
    IListFeature _listFeature,
    IRemoveFeatureHandler _removeFeatureHandler
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> List()
    {
        IEnumerable<FeatureObject.Extension> listResults =
            await _listFeature.ExecuteAsync();
        return Ok(listResults);
    }

    [Permission(nameof(PermissionConstant.ADMIN_SETTING)), HttpPost]
    public async Task<IActionResult> Add(
        [FromBody] FeatureDataTransformers.Add add
    )
    {
        Guid identifier = await HttpContext.Identifier();
        FeatureObject.Extension app = await _addFeatureHandler.ExecuteAsync(
            identifier,
            add.FeatureId
        );
        return Ok(app);
    }

    [
        Permission(nameof(PermissionConstant.ADMIN_SETTING)),
        HttpDelete(RouteConstant.ID)
    ]
    public async Task<IActionResult> Remove([FromRoute] Guid id)
    {
        Guid identifier = await HttpContext.Identifier();
        FeatureObject.Extension app = await _removeFeatureHandler.ExecuteAsync(
            identifier,
            id
        );
        return Ok(app);
    }
}
