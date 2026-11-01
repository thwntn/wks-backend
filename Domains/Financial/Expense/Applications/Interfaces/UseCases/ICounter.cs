namespace Wks.Interfaces;

public interface IGetCounterExpense
{
    Task<ExpenseObject.Counter> ExecuteAsync(Guid profileId, Guid expenseId);
}
