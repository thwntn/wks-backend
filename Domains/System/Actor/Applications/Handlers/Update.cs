namespace Wks.Handlers;

///
/// Summary:
///     Handler for updating a sub-account (actor) and committing the transaction.
///
public class UpdateActorHandler(
    IUpdateActor _updateActor,
    IUnitOfWork _unitOfWork
) : IUpdateActorHandler
{
    ///
    /// Summary:
    ///     Updates an actor profile and persists the staged changes.
    ///
    /// Parameters:
    ///     profileId:
    ///         The profile identifier of the parent account.
    ///
    ///     actorId:
    ///         The actor account identifier to update.
    ///
    ///     updateParams:
    ///         The update parameters carrying name and description.
    ///
    /// Returns:
    ///     The updated actor account.
    ///
    public async Task<Account> ExecuteAsync(
        Guid profileId,
        Guid actorId,
        ActorObject.UpdateParams updateParams
    )
    {
        Account actor = await _updateActor.ExecuteAsync(
            profileId,
            actorId,
            updateParams
        );
        await _unitOfWork.SyncAsync();
        return actor;
    }
}
