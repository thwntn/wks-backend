namespace Wks.Interfaces;

public interface ISetupAccountAuthHandler
{
    Task ExecuteAsync(Guid accountId);
}
