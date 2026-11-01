namespace Wks.Interfaces;

public interface ICreateRole
{
    Task<Role> ExecuteAsync(RoleObject.CreateParams createParams);
}
