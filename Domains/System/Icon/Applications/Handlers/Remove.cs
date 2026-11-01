namespace Wks.Handlers;

public class RemoveIconHandler(IRemoveIcon _removeIcon, IUnitOfWork _unitOfWork)
    : IRemoveIconHandler
{
    public async Task<string> ExecuteAsync(Guid id)
    {
        string result = await _removeIcon.ExecuteAsync(id);
        await _unitOfWork.SyncAsync();
        return result;
    }
}
