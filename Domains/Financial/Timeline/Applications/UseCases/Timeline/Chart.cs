namespace Wks.UseCases;

public class ChartTimeline(ITimelineRepository _timelineRepository)
    : IChartTimeline
{
    public async Task<List<TimelineObject.Chart>> ExecuteAsync(Guid profileId)
    {
        var categories = await EntityFrameworkQueryableExtensions.ToListAsync(
            _timelineRepository
                .Include(timeline => timeline.Expenses)
                    .ThenInclude(expense => expense.ExpenseTransactions)
                .Where(timeline => timeline.ProfileId == profileId)
        );

        var summary = categories.Select(category =>
        {
            decimal count = category
                .Expenses.SelectMany(expense => expense.ExpenseTransactions)
                .Where(expenseTransaction => expenseTransaction.Spend)
                .Sum(expenseTransaction => expenseTransaction.Amount);

            return new TimelineObject.Chart(category.Name, count);
        });

        return [.. summary];
    }
}
