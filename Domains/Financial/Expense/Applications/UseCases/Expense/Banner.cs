namespace Wks.UseCases;

public class BannerExpense(DatabaseContext _databaseContext) : IBannerExpense
{
    public async Task<ExpenseObject.Banner> ExecuteAsync(
        Guid profileId,
        Guid timelineId
    )
    {
        long fundCost = 0;

        IEnumerable<ExpenseTransaction> expenseTransactions =
            _databaseContext.ExpenseTransaction.Where(timelineTransaction =>
                timelineTransaction.Expense.ProfileId == profileId
                && timelineTransaction.Spend
            );
        decimal timelineTransactionCost = expenseTransactions.Sum(cash =>
            cash.Amount
        );

        decimal costInMoth = (
            await EntityFrameworkQueryableExtensions.ToListAsync(
                _databaseContext.ExpenseTransaction.Where(timelineTransaction =>
                    timelineTransaction.Expense.ProfileId == profileId
                    && timelineTransaction.Expense.TimelineId == timelineId
                    && timelineTransaction.Spend
                )
            )
        ).Sum(timelineTransaction => (timelineTransaction.Amount));

        decimal budget = (
            _databaseContext
                .ExpenseTransaction.Where(timelineTransaction =>
                    timelineTransaction.Expense.ProfileId == profileId
                    && timelineTransaction.Expense.TimelineId == timelineId
                    && timelineTransaction.Spend == false
                )
                .MapTo<List<ExpenseTransaction>>()
        ).Sum(expenseTransaction => (expenseTransaction.Amount));

        ExpenseObject.Banner banner = new(
            costInMoth,
            fundCost,
            fundCost - timelineTransactionCost,
            budget
        );

        return banner;
    }
}
