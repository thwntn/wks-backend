namespace Wks.Handlers;

///
/// Summary:
///     Executes RemoveVersion and saves its pending changes through UnitOfWork.
///
public class RemoveVersionHandler(
    IRemoveVersion _removeVersion,
    IUnitOfWork _unitOfWork
) : IRemoveVersionHandler
{
    ///
    /// Summary:
    ///     Stages the requested changes and saves them once.
    /// Parameters:
    ///     versionId:
    ///         The version id supplied to the use case.
    /// Returns:
    ///     A task that completes after pending changes are saved.
    ///
    public async Task ExecuteAsync(Guid versionId)
    {
        await _removeVersion.ExecuteAsync(versionId);
        await _unitOfWork.SyncAsync();
    }
}
