namespace Wks.UseCases;

public class CashflowTimeline(ITimelineRepository _timelineRepository)
    : ICashflowTimeline
{
    public async Task<List<TimelineObject.Cashflow>> ExecuteAsync(
        Guid profileId
    )
    {
        List<Timeline> timelines = await _timelineRepository
            .Include(timeline => timeline.Expenses)
                .ThenInclude(expense => expense.ExpenseTransactions)
            .Where(timeline => timeline.ProfileId == profileId)
            .OrderByDescending(timeline => timeline.FromDate)
            .Take(5)
            .ToListAsync();

        List<TimelineObject.Cashflow> cashflows = timelines
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

                return new TimelineObject.Cashflow(
                    timeline.Name,
                    timeline.FromDate,
                    timeline.ToDate,
                    income,
                    outcome
                );
            })
            .ToList();

        return cashflows;
    }
}
