namespace Wks.Interfaces;

public interface IStatisticExpenseTransaction
{
    Task<IEnumerable<ExpenseTransactionObject.Statistic>> ExecuteAsync(
        Guid profileId,
        Guid categoryId
    );
}
