namespace Wks.UseCases;

public class ListTimeline(DatabaseContext _databaseContext, ICalculatedExpense _calculatedExpense)
    : IListTimeline
{
    public async Task<Paged<Timeline>> ExecuteAsync(Guid profileId, QueryOptions queryOptions)
    {
        IQueryable<Timeline> expenseCategories = _databaseContext
            .Timeline.Include(timeline => timeline.Profile)
            .Include(timeline => timeline.TimelineTags)
                .ThenInclude(timelineTag => timelineTag.Tag)
            .Include(timeline => timeline.Expenses)
                .ThenInclude(expense => expense.ExpenseTransactions)
            .Where(timeline => timeline.ProfileId == profileId)
            .OrderByDescending(timeline => timeline.IsPinned)
            .ThenByDescending(timeline => timeline.CreatedAt);

        Paged<Timeline> result = await Pagination.CreateAsync(expenseCategories, queryOptions);

        result.Data.ForEach(timeline => _calculatedExpense.Execute([.. timeline.Expenses]));

        foreach (Timeline category in result.Data)
        foreach (
            var transaction in category.Expenses.SelectMany(expense => expense.ExpenseTransactions)
        )
            if (transaction.Spend)
                category.Outcome += transaction.Amount;
            else
                category.Income += transaction.Amount;

        foreach (Timeline category in result.Data)
            category.QuantityTransaction = category
                .Expenses.SelectMany(expense => expense.ExpenseTransactions)
                .Count();

        return result;
    }
}
