namespace Wks.Handlers;

public class PublishFeatureHandler(
    IPublishFeature _publishFeature,
    IUnitOfWork _unitOfWork
) : IPublishFeatureHandler
{
    public async Task ExecuteAsync(string code)
    {
        await _publishFeature.ExecuteAsync(code);
        await _unitOfWork.SyncAsync();
    }
}
