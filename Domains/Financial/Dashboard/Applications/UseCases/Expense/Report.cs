namespace Wks.UseCases;

///
/// Summary:
///     Builds the financial statement for a date range: totals, category share,
///     monthly income and spending, and the largest outflows.
///
public class GetFinancialReport(IExpenseTransactionRepository _transactionRepository)
    : IGetFinancialReport
{
    private const int LARGEST = 8;

    ///
    /// Summary:
    ///     Aggregates the profile's transactions between the two dates, inclusive.
    ///
    public async Task<ReportObject.Statement> ExecuteAsync(
        Guid profileId,
        DateTime from,
        DateTime to
    )
    {
        DateTime start = from.Date;
        DateTime end = to.Date.AddDays(1);
        if (end <= start)
        {
            start = to.Date;
            end = from.Date.AddDays(1);
        }

        List<ExpenseTransaction> transactions =
            await EntityFrameworkQueryableExtensions.ToListAsync(
                _transactionRepository
                    .Include(transaction => transaction.Expense)
                    .Where(transaction =>
                        transaction.Expense.ProfileId == profileId
                        && transaction.DateTime >= start
                        && transaction.DateTime < end
                    )
            );

        decimal income = transactions
            .Where(transaction => transaction.Spend == false)
            .Sum(transaction => transaction.Amount);
        decimal outcome = transactions
            .Where(transaction => transaction.Spend)
            .Sum(transaction => transaction.Amount);

        List<ReportObject.Category> categories = transactions
            .Where(transaction => transaction.Spend)
            .GroupBy(transaction => transaction.Expense.Name)
            .Select(group => new ReportObject.Category(
                group.Key,
                group.Sum(transaction => transaction.Amount),
                group.Count()
            ))
            .OrderByDescending(category => category.Amount)
            .ToList();

        List<ReportObject.Payment> largest = transactions
            .Where(transaction => transaction.Spend)
            .OrderByDescending(transaction => transaction.Amount)
            .Take(LARGEST)
            .Select(transaction => new ReportObject.Payment(
                transaction.Name,
                transaction.Description,
                transaction.Amount,
                transaction.DateTime,
                transaction.Expense.Name
            ))
            .ToList();

        return new ReportObject.Statement(
            new ReportObject.Summary(income, outcome, income - outcome, transactions.Count),
            categories,
            Months(transactions, start, end),
            largest
        );
    }

    private static List<ReportObject.Month> Months(
        List<ExpenseTransaction> transactions,
        DateTime start,
        DateTime end
    )
    {
        List<ReportObject.Month> months = [];
        DateTime cursor = new(start.Year, start.Month, 1);
        DateTime last = end.AddTicks(-1);
        DateTime limit = new(last.Year, last.Month, 1);

        while (cursor <= limit)
        {
            DateTime next = cursor.AddMonths(1);
            List<ExpenseTransaction> slice = transactions
                .Where(transaction =>
                    transaction.DateTime >= cursor && transaction.DateTime < next
                )
                .ToList();

            months.Add(
                new ReportObject.Month(
                    cursor.ToString(Moment.FORMAT_MONTH_YEAR),
                    slice.Where(transaction => transaction.Spend == false)
                        .Sum(transaction => transaction.Amount),
                    slice.Where(transaction => transaction.Spend)
                        .Sum(transaction => transaction.Amount)
                )
            );
            cursor = next;
        }

        return months;
    }
}
