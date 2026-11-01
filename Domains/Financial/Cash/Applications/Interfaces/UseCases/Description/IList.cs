namespace Wks.Interfaces;

public interface IListCashDescription
{
    IEnumerable<CashDescription> Execute(Guid profileId);
}
