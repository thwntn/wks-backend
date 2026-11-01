namespace Wks.Interfaces;

public interface IRemoveExpenseTransaction
{
    Task<List<ExpenseTransaction>> Execute(Guid profileId, List<Guid> expenseTransactionIds);
}
