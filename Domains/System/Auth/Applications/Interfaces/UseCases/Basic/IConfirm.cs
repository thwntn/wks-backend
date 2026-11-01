namespace Wks.Interfaces;

public interface IConfirmCodeAuth
{
    Task<AuthObject.SignupResult> ExecuteAsync(Guid accountId, string code);
}
