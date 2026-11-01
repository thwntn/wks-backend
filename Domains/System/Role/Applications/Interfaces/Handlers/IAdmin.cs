namespace Wks.Interfaces;

public interface IMakeAdminHandler
{
    Task<Role> ExecuteAsync(Guid accountId);
}
