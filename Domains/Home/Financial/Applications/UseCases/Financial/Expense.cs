namespace Wks.UseCases;

public class SpendingByExpenseHomeFinancial(
    IExpenseRepository _expenseRepository,
    IExpenseTransactionRepository _transactionRepository
) : ISpendingByExpenseHomeFinancial
{
    public async Task<List<HomeFinancialObject.SpendingByExpense>> ExecuteAsync(
        Guid profileId
    )
    {
        List<Expense> expenses = await _expenseRepository
            .Where(expense => expense.ProfileId == profileId)
            .ToListAsync();

        List<HomeFinancialObject.SpendingByExpense> result = new();

        foreach (Expense expense in expenses)
        {
            decimal total = await _transactionRepository
                .Raw.Where(transaction =>
                    transaction.ExpenseId == expense.Id && transaction.Spend
                )
                .SumAsync(transaction => transaction.Amount);

            if (total > 0)
                result.Add(new(expense.Name, total));
        }

        return result;
    }
}
