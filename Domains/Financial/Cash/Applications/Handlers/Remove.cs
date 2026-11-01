namespace Wks.Handlers;

public class RemoveCashHandler(IRemoveCash _removeCash, IUnitOfWork _unitOfWork)
    : IRemoveCashHandler
{
    public async Task<string> ExecuteAsync(Guid identifier, Guid id)
    {
        string result = await _removeCash.ExecuteAsync(identifier, id);
        await _unitOfWork.SyncAsync();
        return result;
    }
}
