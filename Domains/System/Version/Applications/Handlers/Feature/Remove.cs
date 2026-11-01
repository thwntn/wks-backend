namespace Wks.Handlers;

///
/// Summary:
///     Executes RemoveFeatureVersion and saves its pending changes through UnitOfWork.
///
public class RemoveFeatureVersionHandler(
    IRemoveFeatureVersion _removeFeatureVersion,
    IUnitOfWork _unitOfWork
) : IRemoveFeatureVersionHandler
{
    ///
    /// Summary:
    ///     Stages the requested changes and saves them once.
    /// Parameters:
    ///     featureId:
    ///         The feature id supplied to the use case.
    /// Returns:
    ///     A task that completes after pending changes are saved.
    ///
    public async Task ExecuteAsync(Guid featureId)
    {
        await _removeFeatureVersion.ExecuteAsync(featureId);
        await _unitOfWork.SyncAsync();
    }
}
