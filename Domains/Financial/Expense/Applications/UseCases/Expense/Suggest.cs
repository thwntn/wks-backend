namespace Wks.UseCases;

public class SuggestExpense(
    IExpenseTransactionRepository _expenseTransactionRepository
) : ISuggestExpense
{
    public async Task<IEnumerable<ExpenseTransaction>> ExecuteAsync(
        Guid profileId,
        Guid expenseId,
        ExpenseDataTransformer.Suggest suggest
    )
    {
        if (suggest.Name.Justifiable())
            return await FilterWithNameAsync(
                profileId,
                expenseId,
                suggest.Name
            );

        return await FilterWithoutNameAsync(profileId, expenseId);
    }

    private async Task<IEnumerable<ExpenseTransaction>> FilterWithNameAsync(
        Guid profileId,
        Guid expenseId,
        string name
    )
    {
        return await EntityFrameworkQueryableExtensions.ToListAsync(
            _expenseTransactionRepository
                .Where(expenseTransaction =>
                    expenseTransaction.ExpenseId == expenseId
                    && expenseTransaction.Expense.ProfileId == profileId
                    && expenseTransaction.Name.Contains(name)
                    && expenseTransaction.IsLocked == false
                )
                .OrderByDescending(expenseTransaction =>
                    expenseTransaction.CreatedAt
                )
                .Take(10)
        );
    }

    private async Task<IEnumerable<ExpenseTransaction>> FilterWithoutNameAsync(
        Guid profileId,
        Guid expenseId
    )
    {
        return await EntityFrameworkQueryableExtensions.ToListAsync(
            _expenseTransactionRepository
                .Where(expenseTransaction =>
                    expenseTransaction.ExpenseId == expenseId
                    && expenseTransaction.Expense.ProfileId == profileId
                    && expenseTransaction.IsLocked == false
                )
                .OrderByDescending(expenseTransaction =>
                    expenseTransaction.CreatedAt
                )
                .Take(10)
        );
    }
}
