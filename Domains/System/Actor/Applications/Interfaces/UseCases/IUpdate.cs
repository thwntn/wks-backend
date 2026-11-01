namespace Wks.Interfaces;

public interface IUpdateActor
{
    Task<Account> ExecuteAsync(
        Guid profileId,
        Guid actorId,
        ActorObject.UpdateParams updateParams
    );
}
