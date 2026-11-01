namespace Wks.Interfaces;

public interface IUnpinExpense
{
    Task<Expense> ExecuteAsync(Guid profileId, Guid expenseId);
}
