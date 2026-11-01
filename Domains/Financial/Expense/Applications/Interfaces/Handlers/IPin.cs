namespace Wks.Interfaces;

public interface IPinExpenseHandler
{
    Task<Expense> ExecuteAsync(Guid identifier, Guid expenseId);
}
