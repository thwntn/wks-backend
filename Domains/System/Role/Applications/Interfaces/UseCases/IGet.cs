namespace Wks.Interfaces;

public interface IGetRole
{
    Task<Role> ExecuteAsync(Guid roleId);
}
