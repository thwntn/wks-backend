using System.Threading.Tasks;

namespace Wks.Controllers;

[ApiController, Route(RouteConstant.TIMELINE), Tags(SwaggerConstant.TIMELINE)]
public class TimelineController(
    IListTimeline _listTimeline,
    ICreateTimelineHandler _createTimelineHandler,
    IUpdateTimelineHandler _updateTimelineHandler,
    IRemoveTimelineHandler _removeTimelineHandler,
    IInfoTimeline _infoTimeline,
    ICounterTimeline _counterTimeline,
    ITimeline _timeline,
    ICloneTimelineHandler _cloneTimelineHandler,
    IUpdateTagTimelineHandler _updateTagTimelineHandler,
    IPinTimelineHandler _pinTimelineHandler,
    IUnpinTimelineHandler _unpinTimelineHandler,
    IChartTimeline _chartTimeline,
    IRecentTimeline _recentTimeline,
    IPeriodTimeline _periodTimeline,
    ICashflowTimeline _cashflowTimeline
) : ControllerBase
{
    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] TimelineDataTransformer.Create create
    )
    {
        Guid identifier = await HttpContext.Identifier();
        Timeline timeline = await _createTimelineHandler.ExecuteAsync(
            identifier,
            create
        );
        return Ok(timeline);
    }

    [
        Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)),
        HttpPost(RouteConstant.TIMELINE_CLONE)
    ]
    public async Task<IActionResult> Clone(
        [FromBody] TimelineDataTransformer.Clone clone
    )
    {
        Guid identifier = await HttpContext.Identifier();
        IEnumerable<Expense> expenses =
            await _cloneTimelineHandler.ExecuteAsync(
                identifier,
                clone.FromId,
                clone.ToId
            );
        return Ok(expenses);
    }

    [
        Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)),
        HttpPatch(RouteConstant.TIMELINE_HASHTAG)
    ]
    public async Task<IActionResult> UpdateTag(
        [FromRoute] Guid id,
        [FromBody] TimelineDataTransformer.UpdateTag updateTag
    )
    {
        Guid identifier = await HttpContext.Identifier();
        IEnumerable<TimelineTag> timelineTags =
            await _updateTagTimelineHandler.ExecuteAsync(
                identifier,
                id,
                updateTag
            );
        return Ok(timelineTags);
    }

    [
        Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)),
        HttpPatch(RouteConstant.TIMELINE_PIN)
    ]
    public async Task<IActionResult> Pin([FromRoute] Guid id)
    {
        Guid identifier = await HttpContext.Identifier();
        Timeline timeline = await _pinTimelineHandler.ExecuteAsync(
            identifier,
            id
        );
        return Ok(timeline);
    }

    [
        Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)),
        HttpPatch(RouteConstant.TIMELINE_UNPIN)
    ]
    public async Task<IActionResult> Unpin([FromRoute] Guid id)
    {
        Guid identifier = await HttpContext.Identifier();
        Timeline timeline = await _unpinTimelineHandler.ExecuteAsync(
            identifier,
            id
        );
        return Ok(timeline);
    }

    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] TimelineDataTransformer.Update create
    )
    {
        Guid identifier = await HttpContext.Identifier();
        Timeline timeline = await _updateTimelineHandler.ExecuteAsync(
            identifier,
            create
        );
        return Ok(timeline);
    }

    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpGet]
    public async Task<IActionResult> List([FromQuery] QueryOptions queryOptions)
    {
        Guid identifier = await HttpContext.Identifier();
        Paged<Timeline> expenseCategories = await _listTimeline.ExecuteAsync(
            identifier,
            queryOptions
        );
        PaginationResponse<TimelineDataTransformer.TimelineResponse> response =
            expenseCategories.MapTo<
                PaginationResponse<TimelineDataTransformer.TimelineResponse>
            >();
        return Ok(response);
    }

    [
        Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)),
        HttpGet(RouteConstant.BASE_COUNTER)
    ]
    public async Task<IActionResult> Counter()
    {
        Guid identifier = await HttpContext.Identifier();
        TimelineObject.Counter counter = await _counterTimeline.ExecuteAsync(
            identifier
        );
        return Ok(counter);
    }

    [
        Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)),
        HttpGet(RouteConstant.TIMELINE_CHART)
    ]
    public async Task<IActionResult> Chart()
    {
        Guid identifier = await HttpContext.Identifier();
        List<TimelineObject.Chart> summary = await _chartTimeline.ExecuteAsync(
            identifier
        );
        return Ok(summary);
    }

    [
        Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)),
        HttpGet(RouteConstant.TIMELINE_GET_EXPENSE)
    ]
    public async Task<IActionResult> Expense(
        [FromRoute] Guid id,
        [FromQuery] QueryOptions queryOptions
    )
    {
        Guid identifier = await HttpContext.Identifier();
        Paged<Expense> expense = await _timeline.ExecuteAsync(
            identifier,
            id,
            queryOptions
        );
        return Ok(expense);
    }

    [
        Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)),
        HttpGet(RouteConstant.ID)
    ]
    public async Task<IActionResult> Information([FromRoute] Guid id)
    {
        Guid identifier = await HttpContext.Identifier();
        Timeline timeline = _infoTimeline.Execute(identifier, id);
        return Ok(timeline);
    }

    [
        Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)),
        HttpDelete(RouteConstant.ID)
    ]
    public async Task<IActionResult> Remove([FromRoute] Guid id)
    {
        Guid identifier = await HttpContext.Identifier();
        string message = await _removeTimelineHandler.ExecuteAsync(
            identifier,
            id
        );
        return Ok(message);
    }

    [
        Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)),
        HttpGet(RouteConstant.TIMELINE_RECENT)
    ]
    public async Task<IActionResult> Recent(
        [FromQuery] QueryOptions queryOptions
    )
    {
        Guid identifier = await HttpContext.Identifier();
        TimelineObject.Recent recent = await _recentTimeline.ExecuteAsync(
            identifier,
            queryOptions
        );
        return Ok(recent);
    }

    [
        Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)),
        HttpGet(RouteConstant.TIMELINE_PERIOD)
    ]
    public async Task<IActionResult> Period()
    {
        Guid identifier = await HttpContext.Identifier();
        List<TimelineObject.Period> periods =
            await _periodTimeline.ExecuteAsync(identifier);
        return Ok(periods);
    }

    [
        Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)),
        HttpGet(RouteConstant.TIMELINE_CASHFLOW)
    ]
    public async Task<IActionResult> Cashflow()
    {
        Guid identifier = await HttpContext.Identifier();
        List<TimelineObject.Cashflow> cashflows =
            await _cashflowTimeline.ExecuteAsync(identifier);
        return Ok(cashflows);
    }
}
