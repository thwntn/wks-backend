namespace Wks.Interfaces;

public interface IPinExpense
{
    Task<Expense> ExecuteAsync(Guid profileId, Guid expenseId);
}
