namespace Wks.Interfaces;

public interface IMeProfile
{
    Task<Account> ExecuteAsync(Guid id);
}
