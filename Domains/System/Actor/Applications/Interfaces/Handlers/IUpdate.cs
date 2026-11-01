namespace Wks.Interfaces;

public interface IUpdateActorHandler
{
    Task<Account> ExecuteAsync(
        Guid profileId,
        Guid actorId,
        ActorObject.UpdateParams updateParams
    );
}
