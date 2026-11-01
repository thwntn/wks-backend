namespace Wks.Handlers;

public class UpdateExpenseTransactionHandler(
    IUpdateExpenseTransaction _updateExpenseTransaction,
    IRealtimeTransactionPort _realtimeTransactionPort,
    IUnitOfWork _unitOfWork
) : IUpdateExpenseTransactionHandler
{
    public async Task<ExpenseTransaction> ExecuteAsync(
        Guid id,
        ExpenseTransactionDataTransformer.Update update,
        Guid identifier
    )
    {
        ExpenseTransaction transaction = _updateExpenseTransaction.Execute(update);
        await _unitOfWork.SyncAsync();
        await _realtimeTransactionPort.ExecuteAsync(identifier);
        return transaction;
    }
}
