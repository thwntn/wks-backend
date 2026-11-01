namespace Wks.Interfaces;

public interface IPermissionRole
{
    Task<List<RoleObject.PermissionMaster>> ExecuteAsync();
}
