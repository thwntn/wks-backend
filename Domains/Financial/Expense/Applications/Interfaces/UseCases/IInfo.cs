namespace Wks.Interfaces;

public interface IInfoExpense
{
    Task<Expense> ExecuteAsync(Guid profileId, Guid expenseId);
}
