[ApiController, Route(RouteConstant.HASHTAG), Tags(SwaggerConstant.TAGS)]
public class TagController(
    IListHashTag _listTag,
    IGetHashTag _getTag,
    ICreateHashTagHandler _createTagHandler,
    IUpdateHashTagHandler _updateTagHandler
) : ControllerBase
{

    [Authorize, HttpPost]
    public async Task<IActionResult> Create([FromBody] HashTagDataTransformers.Create create)
    {
        Guid identifier = await HttpContext.Identifier();
        Hashtag tag = await _createTagHandler.Execute(identifier, create);
        return Ok(tag);
    }

    [Authorize, HttpPut]
    public async Task<IActionResult> Update([FromBody] HashTagDataTransformers.Update update)
    {
        Guid identifier = await HttpContext.Identifier();
        Hashtag tag = await _updateTagHandler.ExecuteAsync(identifier, update);
        return Ok(tag);
    }

    [Authorize, HttpGet(RouteConstant.HASHTAG_FILTER)]
    public async Task<IActionResult> List(
        [FromQuery] QueryOptions queryOptions,
        [FromRoute] int filter
    )
    {
        Guid identifier = await HttpContext.Identifier();
        Paged<Hashtag> tags = await _listTag.ExecuteAsync(identifier, filter, queryOptions);
        return Ok(tags);
    }

    [Authorize, HttpGet(RouteConstant.ID)]
    public async Task<IActionResult> Information([FromRoute] Guid id)
    {
        Guid identifier = await HttpContext.Identifier();
        Hashtag tag = await _getTag.ExecuteAsync(identifier, id);
        return Ok(tag);
    }
}
