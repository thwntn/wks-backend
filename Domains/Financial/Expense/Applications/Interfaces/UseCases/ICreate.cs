namespace Wks.Interfaces;

public interface ICreateExpense
{
    Task<Expense> ExecuteAsync(Guid profileId, ExpenseDataTransformer.Create create);
}
