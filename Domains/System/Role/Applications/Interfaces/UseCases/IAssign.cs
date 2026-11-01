namespace Wks.Interfaces;

public interface IAssignRole
{
    Task<Role> ExecuteAsync(Guid roleId, RoleObject.AssignParams assignParams);
}
