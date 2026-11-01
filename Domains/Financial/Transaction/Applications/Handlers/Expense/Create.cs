namespace Wks.Handlers;

public class CreateExpenseTransactionHandler(
    ICreateExpenseTransaction _createExpenseTransaction,
    IRealtimeTransactionPort _realtimeTransactionPort,
    ISocket _socket,
    IUnitOfWork _unitOfWork
) : ICreateExpenseTransactionHandler
{
    public async Task<ExpenseTransaction> ExecuteAsync(
        Guid identifier,
        Guid id,
        ExpenseTransactionDataTransformer.Create create
    )
    {
        ExpenseTransaction transaction =
            await _createExpenseTransaction.ExecuteAsync(
                identifier,
                id,
                create
            );
        await _unitOfWork.SyncAsync();
        await _socket.InvokeAsync(
            identifier,
            nameof(HubMethodName.NOTIFICATION_TRIGGER),
            string.Empty
        );
        await _realtimeTransactionPort.ExecuteAsync(identifier);
        return transaction;
    }
}
