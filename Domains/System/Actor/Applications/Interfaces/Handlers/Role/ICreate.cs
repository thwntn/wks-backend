namespace Wks.Interfaces;

public interface ICreateRoleActorHandler
{
    Task<Role> ExecuteAsync(
        Guid profileId,
        Guid actorId,
        ActorObject.RoleParams roleParams
    );
}
