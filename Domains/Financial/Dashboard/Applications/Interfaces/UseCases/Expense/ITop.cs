namespace Wks.Interfaces;

public interface IGetTopTransaction
{
    Task<IEnumerable<ExpenseTransaction>> ExecuteAsync(Guid profileId);
}
