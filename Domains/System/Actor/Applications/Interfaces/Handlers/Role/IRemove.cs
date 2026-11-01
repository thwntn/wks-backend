namespace Wks.Interfaces;

public interface IRemoveRoleActorHandler
{
    Task<IEnumerable<Role>> ExecuteAsync(Guid profileId, Guid roleId);
}
