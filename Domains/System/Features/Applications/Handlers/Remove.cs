namespace Wks.Handlers;

public class RemoveFeatureHandler(
    IRemoveFeature _removeFeature,
    IUnitOfWork _unitOfWork
) : IRemoveFeatureHandler
{
    public async Task<FeatureObject.Extension> ExecuteAsync(
        Guid identifier,
        Guid id
    )
    {
        FeatureObject.Extension result = await _removeFeature.ExecuteAsync(
            identifier,
            id
        );
        await _unitOfWork.SyncAsync();
        return result;
    }
}
