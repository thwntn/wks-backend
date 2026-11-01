namespace Wks.Interfaces;

public interface IUpdateRoleActor
{
    Task<Role> ExecuteAsync(
        Guid profileId,
        Guid roleId,
        ActorObject.RoleParams roleParams
    );
}
