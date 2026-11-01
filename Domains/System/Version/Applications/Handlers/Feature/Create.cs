namespace Wks.Handlers;

///
/// Summary:
///     Executes CreateFeatureVersion and saves its pending changes through UnitOfWork.
///
public class CreateFeatureVersionHandler(
    ICreateFeatureVersion _createFeatureVersion,
    IUnitOfWork _unitOfWork
) : ICreateFeatureVersionHandler
{
    ///
    /// Summary:
    ///     Stages the requested changes and saves them once.
    /// Parameters:
    ///     versionId:
    ///         The version id supplied to the use case.
    ///     createParams:
    ///         The create params supplied to the use case.
    /// Returns:
    ///     The use case result after pending changes are saved.
    ///
    public async Task<AppFeature> ExecuteAsync(
        Guid versionId,
        VersionObject.CreateFeatureParams createParams
    )
    {
        AppFeature result = await _createFeatureVersion.ExecuteAsync(
            versionId,
            createParams
        );
        await _unitOfWork.SyncAsync();
        return result;
    }
}
