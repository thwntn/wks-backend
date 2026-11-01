namespace Wks.Interfaces;

public interface IConfirmCodeAuthHandler
{
    Task<AuthObject.SignupResult> ExecuteAsync(Guid accountId, string code);
}
