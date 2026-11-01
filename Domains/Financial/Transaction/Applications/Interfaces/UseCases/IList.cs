namespace Wks.Interfaces;

public interface IListExpenseTransaction
{
    Task<Paged<ExpenseTransaction>> ExecuteAsync(
        Guid profileId,
        Guid categoryId,
        QueryOptions queryOptions
    );
}
