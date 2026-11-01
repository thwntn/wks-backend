namespace Wks.Handlers;

///
/// Summary:
///     Handler for toggling a sub-account (actor) lock status and committing the transaction.
///
public class StatusActorHandler(
    IStatusActor _statusActor,
    IUnitOfWork _unitOfWork
) : IStatusActorHandler
{
    ///
    /// Summary:
    ///     Toggles an actor account status and persists the staged changes.
    ///
    /// Parameters:
    ///     profileId:
    ///         The profile identifier of the parent account.
    ///
    ///     actorId:
    ///         The actor account identifier to toggle.
    ///
    /// Returns:
    ///     The actor account with the updated status.
    ///
    public async Task<Account> ExecuteAsync(Guid profileId, Guid actorId)
    {
        Account actor = await _statusActor.ExecuteAsync(profileId, actorId);
        await _unitOfWork.SyncAsync();
        return actor;
    }
}
