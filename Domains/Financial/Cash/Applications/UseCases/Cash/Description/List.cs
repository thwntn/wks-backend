namespace Wks.UseCases;

public class ListCashDescription(DatabaseContext _databaseContext) : IListCashDescription
{
    public IEnumerable<CashDescription> Execute(Guid profileId)
    {
        IEnumerable<CashDescription> cashDescriptions = _databaseContext
            .Cash.Include(cash => cash.CashDescriptions)
            .Where(cash => cash.ProfileId == profileId)
            .SelectMany(cash => cash.CashDescriptions);
        return cashDescriptions;
    }
}
