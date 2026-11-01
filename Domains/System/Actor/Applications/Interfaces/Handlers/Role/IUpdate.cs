namespace Wks.Interfaces;

public interface IUpdateRoleActorHandler
{
    Task<Role> ExecuteAsync(
        Guid profileId,
        Guid roleId,
        ActorObject.RoleParams roleParams
    );
}
