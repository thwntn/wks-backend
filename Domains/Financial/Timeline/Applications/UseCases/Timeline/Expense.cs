namespace Wks.UseCases;

public class TimelineUseCase(
    DatabaseContext _databaseContext,
    ICalculatedExpense _calculatedExpense
) : ITimeline
{
    public async Task<Paged<Expense>> ExecuteAsync(
        Guid profileId,
        Guid timelineId,
        QueryOptions queryOptions
    )
    {
        IQueryable<Expense> expenses =
            _databaseContext
                .Expense.Include(expense => expense.ExpenseTransactions)
                .Where(expense =>
                    expense.TimelineId == timelineId
                    && expense.ProfileId == profileId
                )
                .OrderByDescending(expense => expense.IsPinned)
                .ThenByDescending(expense => expense.CreatedAt)
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_EXPENSE_CATEGORY
            );

        var result = await Pagination.CreateAsync(expenses, queryOptions);
        _calculatedExpense.Execute(result.Data);
        return result;
    }
}
