namespace Wks.UseCases;

public class FilterExpense(
    DatabaseContext _databaseContext,
    ICache _cacheService
) : IFilterExpense
{
    public async Task<IEnumerable<Expense>> ExecuteAsync(Guid profileId)
    {
        string nameofCache = nameof(Expense) + profileId;
        IEnumerable<Expense> expenses = await _cacheService.GetAsync<
            IEnumerable<Expense>
        >(nameofCache);

        expenses ??= await EntityFrameworkQueryableExtensions.ToListAsync(
            _databaseContext
                .Expense.Include(expense => expense.Timeline)
                .Where(expense => expense.ProfileId == profileId)
        );

        return expenses;
    }
}
