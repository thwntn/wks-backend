namespace Wks.Interfaces;

public interface ICreateRoleActor
{
    Task<Role> ExecuteAsync(
        Guid profileId,
        Guid actorId,
        ActorObject.RoleParams roleParams
    );
}
