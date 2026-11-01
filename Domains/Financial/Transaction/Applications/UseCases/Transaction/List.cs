namespace Wks.UseCases;

public class ListExpenseTransaction(IExpenseTransactionRepository _expenseTransactionRepository)
    : IListExpenseTransaction
{
    public async Task<Paged<ExpenseTransaction>> ExecuteAsync(
        Guid profileId,
        Guid categoryId,
        QueryOptions queryOptions
    )
    {
        var query = _expenseTransactionRepository
            .Where(expenseTransaction => expenseTransaction.ExpenseId == categoryId)
            .OrderByDescending(expenseTransaction => expenseTransaction.CreatedAt);
        return await Pagination.CreateAsync(query, queryOptions);
    }
}
