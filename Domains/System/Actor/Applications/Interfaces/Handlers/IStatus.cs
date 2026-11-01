namespace Wks.Interfaces;

public interface IStatusActorHandler
{
    Task<Account> ExecuteAsync(Guid profileId, Guid actorId);
}
