namespace Wks.UseCases;

public class RecentCashDescription(DatabaseContext _databaseContext) : IRecentCashDescription
{
    private readonly int QuantityRecent = 6;

    public IEnumerable<CashDescription> Execute(Guid profileId)
    {
        IEnumerable<CashDescription> cashDescriptions = _databaseContext
            .Cash.Include(cash => cash.CashDescriptions)
            .Where(cash => cash.ProfileId == profileId)
            .SelectMany(cash => cash.CashDescriptions)
            .OrderByDescending(cash => cash.CreatedAt)
            .Take(QuantityRecent);
        return cashDescriptions;
    }
}
