namespace Wks.Handlers;

public class UnpinExpenseHandler(
    IUnpinExpense _unpinExpense,
    IRealtimeExpensePort _realtimeExpensePort,
    IUnitOfWork _unitOfWork
) : IUnpinExpenseHandler
{
    public async Task<Expense> ExecuteAsync(Guid identifier, Guid id)
    {
        Expense expense = await _unpinExpense.ExecuteAsync(identifier, id);
        await _unitOfWork.SyncAsync();
        await _realtimeExpensePort.ExecuteAsync(identifier);
        return expense;
    }
}
