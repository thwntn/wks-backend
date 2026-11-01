namespace Wks.Interfaces;

public interface IUnassignRoleHandler
{
    Task<string> ExecuteAsync(Guid roleId, RoleObject.UnassignParams unassignParams);
}
