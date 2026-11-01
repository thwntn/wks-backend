namespace Wks.UseCases;

public class RecentTimeline(DatabaseContext _databaseContext) : IRecentTimeline
{
    public async Task<TimelineObject.Recent> ExecuteAsync(
        Guid profileId,
        QueryOptions queryOptions
    )
    {
        DateTime sevenDaysAgo = DateTime.UtcNow.AddDays(-7);

        IQueryable<ExpenseTransaction> query = _databaseContext
            .ExpenseTransaction.Include(transaction => transaction.Expense)
            .Where(transaction =>
                transaction.Expense.ProfileId == profileId
                && transaction.DateTime >= sevenDaysAgo
                && transaction.DeletedAt == null
            )
            .OrderByDescending(transaction => transaction.DateTime);

        decimal income = await query
            .Where(transaction => transaction.Spend == false)
            .SumAsync(transaction => transaction.Amount);

        decimal outcome = await query
            .Where(transaction => transaction.Spend)
            .SumAsync(transaction => transaction.Amount);

        TimelineObject.RecentSummary summary = new(income, outcome);

        Paged<ExpenseTransaction> transactions = await Pagination.CreateAsync(
            query,
            queryOptions
        );

        return new(summary, transactions);
    }
}
