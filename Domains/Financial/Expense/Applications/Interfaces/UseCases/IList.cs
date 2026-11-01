namespace Wks.Interfaces;

public interface IListExpense
{
    Task<Paged<Expense>> ExecuteAsync(Guid profileId, QueryOptions queryOptions);
}
