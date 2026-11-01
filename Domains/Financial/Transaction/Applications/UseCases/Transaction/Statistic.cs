namespace Wks.UseCases;

public class StatisticExpenseTransaction(IListExpenseTransaction _listExpenseTransaction)
    : IStatisticExpenseTransaction
{
    public async Task<IEnumerable<ExpenseTransactionObject.Statistic>> ExecuteAsync(
        Guid profileId,
        Guid categoryId
    )
    {
        Paged<ExpenseTransaction> expenseTransactions = await _listExpenseTransaction.ExecuteAsync(
            profileId,
            categoryId,
            new QueryOptions()
        );
        return expenseTransactions.Data.Select(
            expenseTransaction => new ExpenseTransactionObject.Statistic(
                expenseTransaction.Name,
                expenseTransaction.Amount
            )
        );
    }
}
