namespace Wks.Interfaces;

public interface IPermissionActor
{
    Task<List<RoleObject.PermissionMaster>> ExecuteAsync();
}
