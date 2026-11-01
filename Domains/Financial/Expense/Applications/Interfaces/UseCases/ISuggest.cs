namespace Wks.Interfaces;

public interface ISuggestExpense
{
    Task<IEnumerable<ExpenseTransaction>> ExecuteAsync(
        Guid profileId,
        Guid expenseId,
        ExpenseDataTransformer.Suggest suggest
    );
}
