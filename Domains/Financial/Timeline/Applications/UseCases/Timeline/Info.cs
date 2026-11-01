namespace Wks.UseCases;

public class InfoTimeline(
    DatabaseContext _databaseContext,
    ICalculatedExpense _calculatedExpense
) : IInfoTimeline
{
    public Timeline Execute(Guid profileId, Guid timelineId)
    {
        Timeline timeline =
            _databaseContext
                .Timeline.Include(timeline => timeline.Expenses)
                    .ThenInclude(expense => expense.ExpenseTransactions)
                .Include(timeline => timeline.TimelineTags)
                    .ThenInclude(timelineTag => timelineTag.Tag)
                .FirstOrDefault(timeline =>
                    timeline.Id == timelineId && timeline.ProfileId == profileId
                )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_EXPENSE_CATEGORY
            );

        _calculatedExpense.Execute([.. timeline.Expenses]);
        return timeline;
    }
}
