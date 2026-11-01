namespace Wks.Interfaces;

public interface IUnpinExpenseHandler
{
    Task<Expense> ExecuteAsync(Guid identifier, Guid expenseId);
}
