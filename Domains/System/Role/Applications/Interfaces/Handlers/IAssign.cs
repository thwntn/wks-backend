namespace Wks.Interfaces;

public interface IAssignRoleHandler
{
    Task<Role> ExecuteAsync(Guid roleId, RoleObject.AssignParams assignParams);
}
