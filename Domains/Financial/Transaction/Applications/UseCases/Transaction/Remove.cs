namespace Wks.UseCases;

public class RemoveExpenseTransaction(
    IExpenseTransactionRepository _expenseTransactionRepository
) : IRemoveExpenseTransaction
{
    public async Task<List<ExpenseTransaction>> Execute(
        Guid profileId,
        List<Guid> expenseTransactionIds
    )
    {
        List<ExpenseTransaction> expenseTransactions =
            await _expenseTransactionRepository
                .Where(expenseTransaction =>
                    expenseTransactionIds.Any(item =>
                        item == expenseTransaction.Id
                        && expenseTransaction.Expense.ProfileId == profileId
                    )
                )
                .ToListAsync();

        _expenseTransactionRepository.Remove(expenseTransactions);
        return expenseTransactions;
    }
}
