namespace Wks.Interfaces;

public interface ISetupAccountAuth
{
    Task ExecuteAsync(Guid accountId);
}
