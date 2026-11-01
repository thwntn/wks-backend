namespace Wks.UseCases;

public class CounterHomeFinancial(IExpenseTransactionRepository _transactionRepository)
    : ICounterHomeFinancial
{
    public async Task<HomeFinancialObject.Counter> ExecuteAsync(Guid profileId)
    {
        IEnumerable<ExpenseTransaction> transactions = _transactionRepository
            .Raw.Include(transaction => transaction.Expense)
            .Where(transaction => transaction.Expense.ProfileId == profileId);

        decimal income = await _transactionRepository
            .Where(transaction => transaction.Spend == false)
            .SumAsync(transaction => transaction.Amount);

        decimal spending = await _transactionRepository
            .Where(transaction => transaction.Spend)
            .SumAsync(transaction => transaction.Amount);

        decimal transactionCount = await _transactionRepository.Raw.CountAsync();

        return new(income, spending, transactionCount);
    }
}
