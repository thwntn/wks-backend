namespace Wks.Handlers;

public class CreateCashDescriptionHandler(
    ICreateCashDescription _createCashDescription,
    IUnitOfWork _unitOfWork
) : ICreateCashDescriptionHandler
{
    public async Task<CashDescription> ExecuteAsync(
        Guid id,
        CashDescriptionDataTransformer.Create create
    )
    {
        CashDescription result = _createCashDescription.Execute(id, create);
        await _unitOfWork.SyncAsync();
        return result;
    }
}
