namespace Wks.Interfaces;

public interface ICounterExpenseTransaction
{
    IEnumerable<ExpenseTransactionObject.Counter> Execute(Guid profileId);
}
