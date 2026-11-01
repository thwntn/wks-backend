namespace Wks.Handlers;

public class CreateHashTagHandler(
    ICreateHashTag _createTag,
    IUnitOfWork _unitOfWork
) : ICreateHashTagHandler
{
    public async Task<Hashtag> Execute(
        Guid identifier,
        HashTagDataTransformers.Create create
    )
    {
        Hashtag result = _createTag.Execute(identifier, create);
        await _unitOfWork.SyncAsync();
        return result;
    }
}
