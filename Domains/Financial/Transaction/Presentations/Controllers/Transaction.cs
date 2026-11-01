using System.Threading.Tasks;

namespace Wks.Controllers;

[
    ApiController,
    Route(RouteConstant.FINANCIAL_TRANSACTION),
    Tags(SwaggerConstant.EXPENSE_TRANSACTION)
]
public class ExpenseTransactionController(
    IListExpenseTransaction _listExpenseTransaction,
    ICreateExpenseTransactionHandler _createExpenseTransactionHandler,
    IUpdateExpenseTransactionHandler _updateExpenseTransactionHandler,
    IRemoveExpenseTransactionHandler _removeExpenseTransactionHandler,
    IWithCategoryExpenseTransaction _withCategoryExpenseTransaction,
    ICounterExpenseTransaction _counterExpenseTransaction,
    IStatisticExpenseTransaction _statisticExpenseTransaction,
    IExpenditureExpenseTransaction _expenditureExpenseTransaction
) : ControllerBase
{
    [HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] ExpenseTransactionDataTransformer.Update update
    )
    {
        Guid identifier = await HttpContext.Identifier();
        ExpenseTransaction timelineTransaction =
            await _updateExpenseTransactionHandler.ExecuteAsync(
                update.Id,
                update,
                identifier
            );
        return Ok(timelineTransaction);
    }

    [HttpGet(RouteConstant.ID)]
    public async Task<IActionResult> List(
        [FromRoute] Guid id,
        [FromQuery] QueryOptions queryOptions
    )
    {
        Guid identifier = await HttpContext.Identifier();
        Paged<ExpenseTransaction> timelineTransactions =
            await _listExpenseTransaction.ExecuteAsync(
                identifier,
                id,
                queryOptions
            );
        return Ok(timelineTransactions);
    }

    [HttpGet(
        RouteConstant.FINANCIAL_TRANSACTION_STATISTIC + "/" + RouteConstant.ID
    )]
    public async Task<IActionResult> Statistic([FromRoute] Guid id)
    {
        Guid identifier = await HttpContext.Identifier();
        IEnumerable<ExpenseTransactionObject.Statistic> statistics =
            await _statisticExpenseTransaction.ExecuteAsync(identifier, id);
        return Ok(statistics);
    }

    [HttpPost(RouteConstant.ID)]
    public async Task<IActionResult> Create(
        [FromRoute] Guid id,
        [FromBody] ExpenseTransactionDataTransformer.Create create
    )
    {
        Guid identifier = await HttpContext.Identifier();
        ExpenseTransaction timelineTransaction =
            await _createExpenseTransactionHandler.ExecuteAsync(
                identifier,
                id,
                create
            );
        return Ok(timelineTransaction);
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(
        [FromBody] ExpenseTransactionDataTransformer.Remove remove
    )
    {
        Guid identifier = await HttpContext.Identifier();
        string message = await _removeExpenseTransactionHandler.ExecuteAsync(
            identifier,
            remove.Ids
        );
        return Ok(message);
    }

    [HttpGet(
        RouteConstant.FINANCIAL_TRANSACTION_CATEGORY + "/" + RouteConstant.ID
    )]
    public IActionResult WithCategory([FromRoute] Guid id)
    {
        IEnumerable<ExpenseTransaction> timelineTransactions =
            _withCategoryExpenseTransaction.Execute(id);
        return Ok(timelineTransactions);
    }

    [HttpGet(RouteConstant.BASE_COUNTER)]
    public async Task<IActionResult> Counter()
    {
        Guid identifier = await HttpContext.Identifier();
        IEnumerable<ExpenseTransactionObject.Counter> counter =
            _counterExpenseTransaction.Execute(identifier);
        return Ok(counter);
    }

    [HttpGet(
        RouteConstant.FINANCIAL_TRANSACTION_EXPENDITURE + "/" + RouteConstant.ID
    )]
    public async Task<IActionResult> Expenditure([FromRoute] Guid id)
    {
        Guid identifier = await HttpContext.Identifier();
        IEnumerable<DashboardObject.Value> expenditureResult =
            await _expenditureExpenseTransaction.ExecuteAsync(identifier, id);
        return Ok(expenditureResult);
    }
}
