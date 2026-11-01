namespace Wks.Interfaces;

public interface IRemoveRole
{
    Task<IEnumerable<Role>> ExecuteAsync(RoleObject.RemoveParams removeParams);
}
