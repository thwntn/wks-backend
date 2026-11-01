namespace Wks.Controllers;

[ApiController, Route(RouteConstant.ACTION), Tags(SwaggerConstant.ACTION)]
public class ActionController(IListAction _listAction) : ControllerBase
{
    [HttpGet, Authorize]
    public async Task<IActionResult> List([FromQuery] QueryOptions queryOptions)
    {
        Guid identifier = await HttpContext.Identifier(
            personal: true
        );
        Paged<Databases.Action> activities = await _listAction.ExecuteAsync(
            identifier,
            queryOptions
        );
        PaginationResponse<ActionObject.ActionResponse> response =
            activities.MapTo<PaginationResponse<ActionObject.ActionResponse>>();
        return Ok(response);
    }
}
