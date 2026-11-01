namespace Wks.Controllers;

[ApiController, Route(RouteConstant.EXPENSE), Tags(SwaggerConstant.EXPENSE)]
public class ExpenseController(
    IListExpense _listExpense,
    ICreateExpenseHandler _createExpenseHandler,
    IUpdateExpenseHandler _updateExpenseHandler,
    IRemoveExpenseHandler _removeExpenseHandler,
    IInfoExpense _infoExpense,
    IFilterExpense _filterExpense,
    IBannerExpense _bannerExpense,
    IPinExpenseHandler _pinExpenseHandler,
    IGetCounterExpense _getCounterExpense,
    IUnpinExpenseHandler _unpinExpenseHandler,
    ISuggestExpense _suggestExpense
) : ControllerBase
{
    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpGet]
    public async Task<IActionResult> List([FromQuery] QueryOptions queryOptions)
    {
        Guid identifier = await HttpContext.Identifier();
        Paged<Expense> list = await _listExpense.ExecuteAsync(identifier, queryOptions);
        return Ok(list);
    }

    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpGet(RouteConstant.EXPENSE_FILTER)]
    public async Task<IActionResult> Filter()
    {
        Guid identifier = await HttpContext.Identifier();
        IEnumerable<Expense> list = await _filterExpense.ExecuteAsync(identifier);
        return Ok(list);
    }

    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpGet(RouteConstant.BANNER + "/" + RouteConstant.ID)]
    public async Task<IActionResult> Banner([FromRoute] Guid id)
    {
        Guid identifier = await HttpContext.Identifier();
        ExpenseObject.Banner banner = await _bannerExpense.ExecuteAsync(identifier, id);
        return Ok(banner);
    }

    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpGet(RouteConstant.FINANCIAL_TRANSACTION_COUNTER)]
    public async Task<IActionResult> Counter([FromRoute] Guid id)
    {
        Guid identifier = await HttpContext.Identifier();
        ExpenseObject.Counter counter = await _getCounterExpense.ExecuteAsync(identifier, id);
        return Ok(counter);
    }

    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpGet(RouteConstant.ID)]
    public async Task<IActionResult> Information([FromRoute] Guid id)
    {
        Guid identifier = await HttpContext.Identifier();
        Expense category = await _infoExpense.ExecuteAsync(identifier, id);
        return Ok(category);
    }

    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpPost]
    public async Task<IActionResult> Create([FromBody] ExpenseDataTransformer.Create create)
    {
        Guid identifier = await HttpContext.Identifier();
        Expense expense = await _createExpenseHandler.ExecuteAsync(identifier, create);
        return Ok(expense);
    }

    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpPatch(RouteConstant.EXPENSE_PIN)]
    public async Task<IActionResult> Pin([FromRoute] Guid id)
    {
        Guid identifier = await HttpContext.Identifier();
        Expense expense = await _pinExpenseHandler.ExecuteAsync(identifier, id);
        return Ok(expense);
    }

    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpPatch(RouteConstant.EXPENSE_UNPIN)]
    public async Task<IActionResult> Unpin([FromRoute] Guid id)
    {
        Guid identifier = await HttpContext.Identifier();
        Expense expense = await _unpinExpenseHandler.ExecuteAsync(identifier, id);
        return Ok(expense);
    }

    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpPut]
    public async Task<IActionResult> Update([FromBody] ExpenseDataTransformer.Update update)
    {
        Guid identifier = await HttpContext.Identifier();
        Expense expense = await _updateExpenseHandler.ExecuteAsync(identifier, update);
        return Ok(expense);
    }

    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpGet(RouteConstant.EXPENSE_SUGGEST)]
    public async Task<IActionResult> Suggest(
        [FromRoute] Guid id,
        [FromQuery] ExpenseDataTransformer.Suggest suggest
    )
    {
        Guid identifier = await HttpContext.Identifier();
        IEnumerable<ExpenseTransaction> expenses = await _suggestExpense.ExecuteAsync(
            identifier,
            id,
            suggest
        );
        return Ok(expenses);
    }

    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpDelete]
    public async Task<IActionResult> Remove([FromBody] ExpenseDataTransformer.Remove remove)
    {
        Guid identifier = await HttpContext.Identifier();
        string message = await _removeExpenseHandler.ExecuteAsync(identifier, remove.Ids);
        return Ok(message);
    }
}
