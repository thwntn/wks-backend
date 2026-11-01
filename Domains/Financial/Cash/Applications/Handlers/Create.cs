namespace Wks.Handlers;

public class CreateCashHandler(ICreateCash _createCash, IUnitOfWork _unitOfWork)
    : ICreateCashHandler
{
    public async Task<Cash> ExecuteAsync(
        Guid identifier,
        Guid id,
        CashDataTransformer.Create create
    )
    {
        Cash result = await _createCash.ExecuteAsync(identifier, id, create);
        await _unitOfWork.SyncAsync();
        return result;
    }
}
