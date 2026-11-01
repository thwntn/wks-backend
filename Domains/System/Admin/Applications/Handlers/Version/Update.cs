namespace Wks.Handlers;

///
/// Summary:
///     Executes UpdateVersion and saves its pending changes through UnitOfWork.
///
public class UpdateVersionAdminHandler(
    IUpdateVersionAdminPort _updateVersion,
    IUnitOfWork _unitOfWork
) : IUpdateVersionAdminHandler
{
    ///
    /// Summary:
    ///     Stages the requested changes and saves them once.
    /// Parameters:
    ///     versionId:
    ///         The version id supplied to the use case.
    ///     updateParams:
    ///         The update params supplied to the use case.
    /// Returns:
    ///     The use case result after pending changes are saved.
    ///
    public async Task<AppVersion> ExecuteAsync(
        Guid versionId,
        VersionObject.UpdateParams updateParams
    )
    {
        AppVersion result = await _updateVersion.ExecuteAsync(
            versionId,
            updateParams
        );
        await _unitOfWork.SyncAsync();
        return result;
    }
}
