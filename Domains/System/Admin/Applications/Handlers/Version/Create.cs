namespace Wks.Handlers;

///
/// Summary:
///     Executes CreateVersion and saves its pending changes through UnitOfWork.
///
public class CreateVersionAdminHandler(
    ICreateVersionAdminPort _createVersion,
    IUnitOfWork _unitOfWork
) : ICreateVersionAdminHandler
{
    ///
    /// Summary:
    ///     Stages the requested changes and saves them once.
    /// Parameters:
    ///     createParams:
    ///         The create params supplied to the use case.
    /// Returns:
    ///     The use case result after pending changes are saved.
    ///
    public async Task<AppVersion> ExecuteAsync(
        VersionObject.CreateParams createParams
    )
    {
        AppVersion result = await _createVersion.ExecuteAsync(createParams);
        await _unitOfWork.SyncAsync();
        return result;
    }
}
