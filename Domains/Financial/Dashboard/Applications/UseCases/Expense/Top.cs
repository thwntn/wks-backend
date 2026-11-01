namespace Wks.UseCases;

public class GetTopTransaction(
    IExpenseTransactionRepository _expenseTransactionRepository
) : IGetTopTransaction
{
    private readonly int QUANTITY_DEFAULT = 10;

    public async Task<IEnumerable<ExpenseTransaction>> ExecuteAsync(
        Guid profileId
    )
    {
        IQueryable<ExpenseTransaction> transactions =
            _expenseTransactionRepository
                .Where(transaction =>
                    transaction.Expense.ProfileId == profileId
                )
                .OrderByDescending(transaction => transaction.Amount)
                .Take(QUANTITY_DEFAULT);

        return await EntityFrameworkQueryableExtensions.ToListAsync(
            transactions
        );
    }
}
