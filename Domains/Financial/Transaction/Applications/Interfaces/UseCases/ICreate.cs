namespace Wks.Interfaces;

public interface ICreateExpenseTransaction
{
    Task<ExpenseTransaction> ExecuteAsync(
        Guid profileId,
        Guid expenseId,
        ExpenseTransactionDataTransformer.Create create
    );
}
