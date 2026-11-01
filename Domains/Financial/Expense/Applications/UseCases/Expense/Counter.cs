namespace Wks.UseCases;

public class GetCounterExpense(IExpenseRepository _expenseRepository)
    : IGetCounterExpense
{
    public async Task<ExpenseObject.Counter> ExecuteAsync(
        Guid profileId,
        Guid timelineId
    )
    {
        IQueryable<Expense> expenses = _expenseRepository
            .Include(expense => expense.ExpenseTransactions)
            .Where(expense =>
                expense.TimelineId == timelineId
                && expense.ProfileId == profileId
            );

        int quantity = expenses.Count();
        decimal inCome = 0,
            outCome = 0,
            balance = 0;

        foreach (
            ExpenseTransaction expenseTransaction in (
                await EntityFrameworkQueryableExtensions.ToListAsync(expenses)
            ).SelectMany(expense => expense.ExpenseTransactions)
        )
        {
            if (expenseTransaction.Spend)
                outCome += expenseTransaction.Amount;
            else
                inCome += expenseTransaction.Amount;
        }

        balance = inCome - outCome;
        return new ExpenseObject.Counter(inCome, outCome, balance, quantity);
    }
}
