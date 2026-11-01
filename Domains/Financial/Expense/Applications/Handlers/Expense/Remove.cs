namespace Wks.Handlers;

public class RemoveExpenseHandler(
    IRemoveExpense _removeExpense,
    IRealtimeExpensePort _realtimeExpensePort,
    IUnitOfWork _unitOfWork
) : IRemoveExpenseHandler
{
    public async Task<string> ExecuteAsync(Guid identifier, IEnumerable<Guid> ids)
    {
        string message = await _removeExpense.ExecuteAsync(identifier, ids);
        await _unitOfWork.SyncAsync();
        await _realtimeExpensePort.ExecuteAsync(identifier);
        return message;
    }
}
