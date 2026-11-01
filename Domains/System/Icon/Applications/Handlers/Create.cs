namespace Wks.Handlers;

public class CreateIconHandler(ICreateIcon _createIcon, IUnitOfWork _unitOfWork)
    : ICreateIconHandler
{
    public async Task<Icon> ExecuteAsync(IconDataTransformer.Create create)
    {
        Icon result = _createIcon.Execute(create);
        await _unitOfWork.SyncAsync();
        return result;
    }
}
