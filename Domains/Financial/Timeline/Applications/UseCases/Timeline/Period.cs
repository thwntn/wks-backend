namespace Wks.UseCases;

public class PeriodTimeline(ITimelineRepository _timelineRepository)
    : IPeriodTimeline
{
    public async Task<List<TimelineObject.Period>> ExecuteAsync(Guid profileId)
    {
        List<Timeline> timelines = await _timelineRepository
            .Include(timeline => timeline.Expenses)
                .ThenInclude(expense => expense.ExpenseTransactions)
            .Where(timeline => timeline.ProfileId == profileId)
            .OrderByDescending(timeline => timeline.CreatedAt)
            .ThenByDescending(timeline => timeline.Id)
            .Take(2)
            .ToListAsync();

        List<TimelineObject.Period> periods = timelines
            .Select(timeline =>
            {
                var transactions = timeline.Expenses.SelectMany(expense =>
                    expense.ExpenseTransactions
                );

                decimal income = transactions
                    .Where(transaction => transaction.Spend == false)
                    .Sum(transaction => transaction.Amount);

                decimal outcome = transactions
                    .Where(transaction => transaction.Spend)
                    .Sum(transaction => transaction.Amount);

                return new TimelineObject.Period(
                    timeline.Name,
                    income,
                    outcome,
                    timeline.Expenses.Sum(expense => (decimal)expense.Budget)
                );
            })
            .ToList();

        return periods;
    }
}
