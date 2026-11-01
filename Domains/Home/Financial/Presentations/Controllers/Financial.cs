namespace Wks.Controllers;

[ApiController, Route(RouteConstant.HOME_FINANCIAL), Tags(SwaggerConstant.HOME_FINANCIAL)]
public class HomeFinancialController(
    ICounterHomeFinancial _counterHomeFinancial,
    IMonthlyChartHomeFinancial _monthlyChartHomeFinancial,
    ISpendingByExpenseHomeFinancial _spendingByExpenseHomeFinancial,
    IRecentTransactionHomeFinancial _recentTransactionHomeFinancial
) : ControllerBase
{
    [Permission(nameof(PermissionConstant.HOME_OVERVIEW)), HttpGet(RouteConstant.HOME_FINANCIAL_COUNTER)]
    public async Task<IActionResult> Counter()
    {
        Guid identifier = await HttpContext.Identifier();
        HomeFinancialObject.Counter counter = await _counterHomeFinancial.ExecuteAsync(identifier);
        return Ok(counter);
    }

    [Permission(nameof(PermissionConstant.HOME_OVERVIEW)), HttpGet(RouteConstant.HOME_FINANCIAL_MONTHLY)]
    public async Task<IActionResult> MonthlyChart()
    {
        Guid identifier = await HttpContext.Identifier();
        List<HomeFinancialObject.MonthlySpending> monthlyData =
            await _monthlyChartHomeFinancial.ExecuteAsync(identifier);
        return Ok(monthlyData);
    }

    [Permission(nameof(PermissionConstant.HOME_OVERVIEW)), HttpGet(RouteConstant.HOME_FINANCIAL_EXPENSE)]
    public async Task<IActionResult> SpendingByExpense()
    {
        Guid identifier = await HttpContext.Identifier();
        List<HomeFinancialObject.SpendingByExpense> spendingData =
            await _spendingByExpenseHomeFinancial.ExecuteAsync(identifier);
        return Ok(spendingData);
    }

    [Permission(nameof(PermissionConstant.HOME_OVERVIEW)), HttpGet(RouteConstant.HOME_FINANCIAL_RECENT)]
    public async Task<IActionResult> RecentTransactions()
    {
        Guid identifier = await HttpContext.Identifier();
        List<HomeFinancialObject.RecentTransaction> transactions =
            await _recentTransactionHomeFinancial.ExecuteAsync(identifier);
        return Ok(transactions);
    }
}
