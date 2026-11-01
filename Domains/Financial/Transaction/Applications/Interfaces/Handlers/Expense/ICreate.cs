namespace Wks.Interfaces;

public interface ICreateExpenseTransactionHandler
{
    Task<ExpenseTransaction> ExecuteAsync(
        Guid identifier,
        Guid id,
        ExpenseTransactionDataTransformer.Create create
    );
}
