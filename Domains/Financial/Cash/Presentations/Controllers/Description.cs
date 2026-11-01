namespace Wks.Controllers;

[Route(RouteConstant.CASH_DESCRIPTION), Tags(SwaggerConstant.CASH_DESCRIPTION), ApiController]
public class CashDescriptionController(
    IListCashDescription _listCashDescription,
    IRecentCashDescription _recentCashDescription,
    ICreateCashDescriptionHandler _createCashDescriptionHandler
) : ControllerBase
{
    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpGet]
    public async Task<IActionResult> List()
    {
        Guid identifier = await HttpContext.Identifier();
        IEnumerable<CashDescription> cashDescriptions = _listCashDescription.Execute(identifier);
        return Ok(cashDescriptions);
    }

    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpGet(RouteConstant.RECENT)]
    public async Task<IActionResult> Recent()
    {
        Guid identifier = await HttpContext.Identifier();
        IEnumerable<CashDescription> cashDescription = _recentCashDescription.Execute(identifier);
        return Ok(cashDescription);
    }

    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpPost(RouteConstant.ID)]
    public async Task<IActionResult> Create(
        [FromRoute] Guid id,
        [FromBody] CashDescriptionDataTransformer.Create create
    )
    {
        CashDescription cashDescription = await _createCashDescriptionHandler.ExecuteAsync(
            id,
            create
        );
        return Ok(cashDescription);
    }
}
