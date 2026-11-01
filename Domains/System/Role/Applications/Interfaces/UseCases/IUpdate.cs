namespace Wks.Interfaces;

public interface IUpdateRole
{
    Task<Role> ExecuteAsync(Guid accountId, Guid roleId, RoleObject.UpdateParams updateParams);
}
