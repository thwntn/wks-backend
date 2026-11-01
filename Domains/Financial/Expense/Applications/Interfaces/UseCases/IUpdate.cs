namespace Wks.Interfaces;

public interface IUpdateExpense
{
    Task<Expense> ExecuteAsync(Guid profileId, ExpenseDataTransformer.Update update);
}
