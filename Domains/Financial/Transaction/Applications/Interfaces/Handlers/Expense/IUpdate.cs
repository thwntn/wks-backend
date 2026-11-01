namespace Wks.Interfaces;

public interface IUpdateExpenseTransactionHandler
{
    Task<ExpenseTransaction> ExecuteAsync(
        Guid id,
        ExpenseTransactionDataTransformer.Update update,
        Guid identifier
    );
}
