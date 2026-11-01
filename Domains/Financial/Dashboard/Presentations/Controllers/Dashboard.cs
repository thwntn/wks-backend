namespace Wks.Controllers;

[Route(RouteConstant.FINANCIAL_DASHBOARD), Tags(SwaggerConstant.DASHBOARD), ApiController]
public class DashboardController(
    IGetExpenseAnalyst _getExpenseAnalyst,
    IGetTopTransaction _getTopTransaction,
    IGetFinancialReport _getFinancialReport
) : ControllerBase
{
    [Permission(nameof(PermissionConstant.FINANCIAL_DASHBOARD)), HttpGet(RouteConstant.EXPENSE_ANALYST)]
    public async Task<IActionResult> GetExpenseAnalyst()
    {
        Guid identifier = await HttpContext.Identifier();
        List<ReportObject.TimelineCounter> information = await _getExpenseAnalyst.ExecuteAsync(
            identifier
        );
        return Ok(information);
    }

    [Permission(nameof(PermissionConstant.FINANCIAL_DASHBOARD)), HttpGet(RouteConstant.FINANCIAL_DASHBOARD_TOP_TRANSACTION)]
    public async Task<IActionResult> GetTopTransaction()
    {
        Guid identifier = await HttpContext.Identifier();
        IEnumerable<ExpenseTransaction> topTransaction = await _getTopTransaction.ExecuteAsync(
            identifier
        );
        return Ok(topTransaction);
    }

    [Permission(nameof(PermissionConstant.FINANCIAL_DASHBOARD)), HttpGet(RouteConstant.FINANCIAL_DASHBOARD_REPORT)]
    public async Task<IActionResult> Report([FromQuery] DateTime from, [FromQuery] DateTime to)
    {
        Guid identifier = await HttpContext.Identifier();
        ReportObject.Statement statement = await _getFinancialReport.ExecuteAsync(
            identifier,
            from,
            to
        );
        return Ok(statement);
    }
}
