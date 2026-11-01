namespace Wks.Handlers;

public class UpdateHashTagHandler(
    IUpdateHashTag _updateTag,
    IUnitOfWork _unitOfWork
) : IUpdateHashTagHandler
{
    public async Task<Hashtag> ExecuteAsync(
        Guid identifier,
        HashTagDataTransformers.Update update
    )
    {
        Hashtag result = await _updateTag.ExecuteAsync(identifier, update);
        await _unitOfWork.SyncAsync();
        return result;
    }
}
