namespace Wks.Interfaces;

public interface IUnassignRole
{
    Task<string> ExecuteAsync(
        Guid roleId,
        RoleObject.UnassignParams unassignParams
    );
}
