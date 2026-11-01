namespace Wks.Handlers;

public class UpdateExpenseHandler(
    IUpdateExpense _updateExpense,
    IRealtimeExpensePort _realtimeExpensePort,
    IUnitOfWork _unitOfWork
) : IUpdateExpenseHandler
{
    public async Task<Expense> ExecuteAsync(Guid identifier, ExpenseDataTransformer.Update update)
    {
        Expense expense = await _updateExpense.ExecuteAsync(identifier, update);
        await _unitOfWork.SyncAsync();
        await _realtimeExpensePort.ExecuteAsync(identifier);
        return expense;
    }
}
