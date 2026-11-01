namespace Wks.Handlers;

public class CreateExpenseHandler(
    ICreateExpense _createExpense,
    IRealtimeExpensePort _realtimeExpensePort,
    IUnitOfWork _unitOfWork
) : ICreateExpenseHandler
{
    public async Task<Expense> ExecuteAsync(Guid identifier, ExpenseDataTransformer.Create create)
    {
        Expense expense = await _createExpense.ExecuteAsync(identifier, create);
        await _unitOfWork.SyncAsync();
        await _realtimeExpensePort.ExecuteAsync(identifier);
        return expense;
    }
}
