namespace Wks.UseCases;

public class MonthlyChartHomeFinancial(
    IExpenseTransactionRepository _transactionRepository
) : IMonthlyChartHomeFinancial
{
    public async Task<List<HomeFinancialObject.MonthlySpending>> ExecuteAsync(
        Guid profileId
    )
    {
        List<ExpenseTransaction> transactions = await _transactionRepository
            .Raw.Include(transaction => transaction.Expense)
            .Where(transaction =>
                transaction.Expense.ProfileId == profileId && transaction.Spend
            )
            .ToListAsync();

        List<HomeFinancialObject.MonthlySpending> monthlyData = transactions
            .GroupBy(transaction => new
            {
                transaction.DateTime.Year,
                transaction.DateTime.Month,
            })
            .Select(group => new HomeFinancialObject.MonthlySpending(
                new DateTime(group.Key.Year, group.Key.Month, 1).ToString(
                    Moment.FORMAT_YEAR_MONTH
                ),
                group.Sum(transaction => transaction.Amount)
            ))
            .OrderBy(monthly => monthly.Month)
            .ToList();

        return monthlyData;
    }
}
