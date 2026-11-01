namespace Wks.Interfaces;

public interface ICreateRoleHandler
{
    Task<Role> ExecuteAsync(RoleObject.CreateParams createParams);
}
