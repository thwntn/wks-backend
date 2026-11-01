namespace Wks.Controllers;

[
    ApiController,
    Route(RouteConstant.FINANCIAL_ANALYTICS),
    Tags(SwaggerConstant.FINANCIAL_ANALYTIC)
]
public class FinancialReportController(
    IGetActivitiesFinancialAnalytics _getActivitiesFinancialAnalytics,
    IGroupExpenseFinancialAnalytics _groupExpenseFinancialAnalytics
) : ControllerBase
{
    [
        Permission(nameof(PermissionConstant.FINANCIAL_DASHBOARD)),
        HttpGet(RouteConstant.FinancialAnalyticsActivities)
    ]
    public async Task<IActionResult> Activities()
    {
        Guid identifier = await HttpContext.Identifier();
        List<Databases.Action> activities =
            await _getActivitiesFinancialAnalytics.ExecuteAsync(identifier);
        return Ok(activities);
    }

    [
        Permission(nameof(PermissionConstant.FINANCIAL_DASHBOARD)),
        HttpGet(RouteConstant.FinancialAnalyticsExpenses)
    ]
    public async Task<IActionResult> Expense(
        [FromQuery]
            FinancialAnalyticsDataTransformer.GroupExpenseOptions groupExpenseOptions
    )
    {
        Guid identifier = await HttpContext.Identifier();
        List<Expense> expenses =
            await _groupExpenseFinancialAnalytics.ExecuteAsync(
                identifier,
                groupExpenseOptions
            );
        return Ok(expenses);
    }
}
