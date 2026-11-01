namespace Wks.Interfaces;

public interface IRecentCashDescription
{
    IEnumerable<CashDescription> Execute(Guid profileId);
}
