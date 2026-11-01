namespace Wks.Interfaces;

public interface ICreateActor
{
    Task<Account> ExecuteAsync(
        Guid profileId,
        ActorObject.CreateParams createParams
    );
}
