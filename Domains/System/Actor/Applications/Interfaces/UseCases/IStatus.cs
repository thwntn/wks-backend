namespace Wks.Interfaces;

public interface IStatusActor
{
    Task<Account> ExecuteAsync(Guid profileId, Guid actorId);
}
