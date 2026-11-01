namespace Wks.UseCases;

public class RecentTransactionHomeFinancial(
    IExpenseTransactionRepository _transactionRepository
) : IRecentTransactionHomeFinancial
{
    public async Task<List<HomeFinancialObject.RecentTransaction>> ExecuteAsync(
        Guid profileId
    )
    {
        List<ExpenseTransaction> transactions = await _transactionRepository
            .Raw.Include(transaction => transaction.Expense)
            .Where(transaction => transaction.Expense.ProfileId == profileId)
            .OrderByDescending(transaction => transaction.DateTime)
            .Take(10)
            .ToListAsync();

        return transactions
            .Select(transaction => new HomeFinancialObject.RecentTransaction
            {
                Id = transaction.Id,
                Name = transaction.Name,
                Description = transaction.Description,
                Amount = transaction.Amount,
                Spend = transaction.Spend,
                DateTime = transaction.DateTime,
                ExpenseName = transaction.Expense.Name,
            })
            .ToList();
    }
}
