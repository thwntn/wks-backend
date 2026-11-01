namespace Wks.Interfaces;

public interface IRemoveRoleHandler
{
    Task<IEnumerable<Role>> ExecuteAsync(RoleObject.RemoveParams removeParams);
}
