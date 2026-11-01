namespace Wks.Interfaces;

public interface ICreateActorHandler
{
    Task<Account> ExecuteAsync(
        Guid profileId,
        ActorObject.CreateParams createParams
    );
}
