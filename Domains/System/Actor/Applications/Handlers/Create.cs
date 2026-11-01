namespace Wks.Handlers;

///
/// Summary:
///     Handler for creating a sub-account (actor) and committing the transaction.
///
public class CreateActorHandler(
    ICreateActor _createActor,
    IUnitOfWork _unitOfWork
) : ICreateActorHandler
{
    ///
    /// Summary:
    ///     Creates an actor account and persists the staged changes.
    ///
    /// Parameters:
    ///     profileId:
    ///         The profile identifier of the parent account.
    ///
    ///     createParams:
    ///         The actor creation parameters.
    ///
    /// Returns:
    ///     The created actor account.
    ///
    public async Task<Account> ExecuteAsync(
        Guid profileId,
        ActorObject.CreateParams createParams
    )
    {
        Account actor = await _createActor.ExecuteAsync(profileId, createParams);
        await _unitOfWork.SyncAsync();
        return actor;
    }
}
