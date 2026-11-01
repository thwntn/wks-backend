namespace Wks.Interfaces;

public interface IUpdateExpenseHandler
{
    Task<Expense> ExecuteAsync(Guid identifier, ExpenseDataTransformer.Update update);
}
