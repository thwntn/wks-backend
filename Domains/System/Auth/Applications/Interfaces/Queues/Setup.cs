namespace Wks.Interfaces;

public interface ISetupAccountAuthPublisher
{
    Task Execute(Guid accountId);
}
