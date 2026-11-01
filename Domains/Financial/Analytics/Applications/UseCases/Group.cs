namespace Wks.UseCases;

public class GroupExpenseFinancialAnalytics(
    IExpenseRepository _expenseRepository
) : IGroupExpenseFinancialAnalytics
{
    public async Task<List<Expense>> ExecuteAsync(
        Guid profileId,
        FinancialAnalyticsDataTransformer.GroupExpenseOptions groupExpenseOptions
    )
    {
        List<Expense> expenses =
            await EntityFrameworkQueryableExtensions.ToListAsync(
                _expenseRepository
                    .Include(expense =>
                        expense.ExpenseTransactions.Where(expenseTransaction =>
                            expenseTransaction.DateTime
                                >= groupExpenseOptions.StartTime
                            && expenseTransaction.DateTime
                                <= groupExpenseOptions.EndTime
                        )
                    )
                    .Where(expense =>
                        expense.ProfileId == profileId
                        && expense.TimelineId == groupExpenseOptions.TimelineId
                    )
            );

        expenses.ForEach(expense =>
            expense.TotalCost = expense.ExpenseTransactions.Sum(
                expenseTransaction => (expenseTransaction.Amount)
            )
        );

        return expenses;
    }
}
