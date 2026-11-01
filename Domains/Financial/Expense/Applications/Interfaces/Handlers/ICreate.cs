namespace Wks.Interfaces;

public interface ICreateExpenseHandler
{
    Task<Expense> ExecuteAsync(Guid identifier, ExpenseDataTransformer.Create create);
}
