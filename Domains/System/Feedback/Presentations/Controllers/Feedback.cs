namespace Wks.Controllers;

[ApiController, Route(RouteConstant.FEEDBACK), Tags(SwaggerConstant.FEEDBACK)]
public class FeedbackController(
    ICreateFeedbackHandler _createFeedbackHandler,
    IListFeedback _listFeedback,
    IReplyFeedbackHandler _replyFeedbackHandler,
    IListStatusFeedback _listFeedbackStatus,
    IPinFeedbackHandler _pinFeedbackHandler,
    IUnpinFeedbackHandler _unpinFeedbackHandler
) : ControllerBase
{
    [Authorize, HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] FeedbackDataTransformer.Create createParams
    )
    {
        Guid identifier = await HttpContext.Identifier();
        Feedback feedback = await _createFeedbackHandler.ExecuteAsync(
            identifier,
            createParams.MapTo<FeedbackObject.Create>()
        );
        return Ok(feedback);
    }

    [Authorize, HttpGet]
    public async Task<IActionResult> List([FromQuery] QueryOptions queryOptions)
    {
        Guid identifier = await HttpContext.Identifier();
        Paged<Feedback> paged = await _listFeedback.ExecuteAsync(
            identifier,
            queryOptions
        );
        return Ok(paged);
    }

    [Authorize, HttpPost(RouteConstant.FEEDBACK_REPLY)]
    public async Task<IActionResult> Reply(
        [FromRoute] Guid id,
        [FromBody] FeedbackDataTransformer.Reply replyParams
    )
    {
        Guid identifier = await HttpContext.Identifier();
        FeedbackMessage feedbackMessage =
            await _replyFeedbackHandler.ExecuteAsync(
                identifier,
                id,
                replyParams.MapTo<FeedbackObject.Reply>()
            );
        return Ok(feedbackMessage);
    }

    [Authorize, HttpGet(RouteConstant.FEEDBACK_STATUS)]
    public async Task<IActionResult> ListStatus()
    {
        List<FeedbackObject.Status> statuses =
            await _listFeedbackStatus.ExecuteAsync();
        return Ok(statuses);
    }

    [Authorize, HttpPatch(RouteConstant.FEEDBACK_PIN)]
    public async Task<IActionResult> Pin([FromRoute] Guid id)
    {
        Guid identifier = await HttpContext.Identifier();
        Feedback feedback = await _pinFeedbackHandler.ExecuteAsync(
            identifier,
            id
        );
        return Ok(feedback);
    }

    [Authorize, HttpPatch(RouteConstant.FEEDBACK_UNPIN)]
    public async Task<IActionResult> Unpin([FromRoute] Guid id)
    {
        Guid identifier = await HttpContext.Identifier();
        Feedback feedback = await _unpinFeedbackHandler.ExecuteAsync(
            identifier,
            id
        );
        return Ok(feedback);
    }
}
