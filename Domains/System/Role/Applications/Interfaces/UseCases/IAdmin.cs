namespace Wks.Interfaces;

public interface IMakeAdmin
{
    Task<Role> ExecuteAsync(Guid accountId);
}
