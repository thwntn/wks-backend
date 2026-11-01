namespace Wks.Interfaces;

public interface IUpdateRoleHandler
{
    Task<Role> ExecuteAsync(Guid accountId, Guid roleId, RoleObject.UpdateParams updateParams);
}
