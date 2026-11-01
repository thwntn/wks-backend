namespace Wks.Interfaces;

public interface IFilterExpense
{
    Task<IEnumerable<Expense>> ExecuteAsync(Guid profileId);
}
