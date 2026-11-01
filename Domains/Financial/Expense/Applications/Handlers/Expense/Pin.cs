namespace Wks.Handlers;

public class PinExpenseHandler(
    IPinExpense _pinExpense,
    IRealtimeExpensePort _realtimeExpensePort,
    IUnitOfWork _unitOfWork
) : IPinExpenseHandler
{
    public async Task<Expense> ExecuteAsync(Guid identifier, Guid id)
    {
        Expense expense = await _pinExpense.ExecuteAsync(identifier, id);
        await _unitOfWork.SyncAsync();
        await _realtimeExpensePort.ExecuteAsync(identifier);
        return expense;
    }
}
