namespace Wks.Interfaces;

public interface IRemoveRoleActor
{
    Task<IEnumerable<Role>> ExecuteAsync(Guid profileId, Guid roleId);
}
