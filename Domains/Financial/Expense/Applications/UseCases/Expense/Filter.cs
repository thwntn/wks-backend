namespace Wks.UseCases;

public class FilterExpense(
    DatabaseContext _databaseContext,
    ICache _cacheService
) : IFilterExpense
{
    public async Task<IEnumerable<Expense>> ExecuteAsync(Guid profileId)
    {
        string cacheName = Format.ToCacheName(
            nameof(CacheNames.FINANCIAL_EXPENSE),
            profileId
        );
        IEnumerable<Expense> expenses = await _cacheService.GetAsync<
            IEnumerable<Expense>
        >(cacheName);

        expenses ??= await EntityFrameworkQueryableExtensions.ToListAsync(
            _databaseContext
                .Expense.Include(expense => expense.Timeline)
                .Where(expense => expense.ProfileId == profileId)
        );

        return expenses;
    }
}
