namespace Wks.Handlers;

public class AddFeatureHandler(IAddFeature _addFeature, IUnitOfWork _unitOfWork)
    : IAddFeatureHandler
{
    public async Task<FeatureObject.Extension> ExecuteAsync(
        Guid identifier,
        Guid featureId
    )
    {
        FeatureObject.Extension result = await _addFeature.ExecuteAsync(
            identifier,
            featureId
        );
        await _unitOfWork.SyncAsync();
        return result;
    }
}
