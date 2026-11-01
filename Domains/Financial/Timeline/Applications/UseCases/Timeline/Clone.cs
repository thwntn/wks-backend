namespace Wks.UseCases;

public class CloneTimeline(
    ITimelineRepository _timelineRepository,
    IExpenseRepository _expenseRepository
) : ICloneTimeline
{
    public async Task<List<Expense>> ExecuteAsync(
        Guid profileId,
        Guid fromId,
        Guid toId
    )
    {
        Timeline expenseCategories =
            await _timelineRepository
                .Raw.Include(timeline => timeline.Expenses)
                    .ThenInclude(expense =>
                        expense.ExpenseTransactions.Where(transaction =>
                            transaction.IsLocked
                        )
                    )
                .FirstOrDefaultAsync(expense =>
                    expense.ProfileId == profileId && expense.Id == fromId
                )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_EXPENSE_CATEGORY
            );

        List<Expense> newExpenseCategories = expenseCategories
            .Expenses.Select(expense =>
            {
                List<ExpenseTransaction> budgets = Mapper
                    .MapTo<List<ExpenseTransaction>>(expense.ExpenseTransactions)
                    .Select(expenseTransaction =>
                    {
                        ExpenseTransaction budget =
                            expenseTransaction.MapTo<ExpenseTransaction>();
                        budget.Id = Guid.NewGuid();
                        return budget;
                    })
                    .ToList();

                return new Expense(expense.Name, expense.Description)
                {
                    Id = Guid.NewGuid(),
                    DateTime = Moment.Utc.Now,
                    ProfileId = expense.ProfileId,
                    TimelineId = toId,
                    Budget = expense.Budget,
                    IsPinned = expense.IsPinned,
                    ExpenseTransactions = budgets,
                };
            })
            .ToList();

        _expenseRepository.Create(newExpenseCategories);
        return newExpenseCategories;
    }
}
