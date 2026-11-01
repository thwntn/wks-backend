namespace Wks.UseCases;

public class ListExpense(DatabaseContext _databaseContext, ICalculatedExpense _calculatedExpense)
    : IListExpense
{
    public async Task<Paged<Expense>> ExecuteAsync(Guid profileId, QueryOptions queryOptions)
    {
        IQueryable<Expense> expenses = _databaseContext
            .Expense.Include(expense => expense.Timeline)
            .Include(expenses => expenses.ExpenseTransactions)
            .Where(expense => expense.ProfileId == profileId)
            .OrderByDescending(expense => expense.CreatedAt);

        Paged<Expense> result = await Pagination.CreateAsync(expenses, queryOptions);
        _calculatedExpense.Execute(result.Data);

        return result;
    }
}
