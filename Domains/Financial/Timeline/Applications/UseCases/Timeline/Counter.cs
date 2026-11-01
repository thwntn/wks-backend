using System.Threading.Tasks;

namespace Wks.UseCases;

public class CounterTimeline(
    ITimelineRepository _expendCategoryRepository,
    IExpenseTransactionRepository _expenseTransactionRepository
) : ICounterTimeline
{
    public async Task<TimelineObject.Counter> ExecuteAsync(Guid profileId)
    {
        IQueryable<ExpenseTransaction> context = _expenseTransactionRepository.Where(
            expenseTransaction => expenseTransaction.Expense.ProfileId == profileId
        );

        decimal inCome = await context
            .Where(expenseTransaction => expenseTransaction.Spend == false)
            .SumAsync(expense => expense.Amount);

        decimal outCome = await context
            .Where(expenseTransaction => expenseTransaction.Spend)
            .SumAsync(expense => expense.Amount);

        int quantityCategory = await _expendCategoryRepository.Raw.CountAsync(expense =>
            expense.ProfileId == profileId
        );

        int quantityTransaction = await _expenseTransactionRepository.Raw.CountAsync(
            expenseTransaction =>
                expenseTransaction.Expense.ProfileId == profileId
                && expenseTransaction.DeletedAt == null
        );

        return new(inCome, outCome, quantityCategory, quantityTransaction);
    }
}
