namespace Wks.Controllers;

[ApiController, Route(RouteConstant.CASH), Tags(SwaggerConstant.CASH)]
public class CashController(
    ICreateCashHandler _createCashHandler,
    IRemoveCashHandler _removeCashHandler
) : ControllerBase
{
    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpPost(RouteConstant.ID)]
    public async Task<IActionResult> Create(
        [FromRoute] Guid id,
        [FromBody] CashDataTransformer.Create create
    )
    {
        Guid identifier = await HttpContext.Identifier();
        Cash cash = await _createCashHandler.ExecuteAsync(identifier, id, create);
        return Ok(cash);
    }

    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpDelete(RouteConstant.ID)]
    public async Task<IActionResult> Remove([FromRoute] Guid id)
    {
        Guid identifier = await HttpContext.Identifier();
        string message = await _removeCashHandler.ExecuteAsync(identifier, id);
        return Ok(message);
    }
}
