namespace Wks.Interfaces;

public interface ILoginGoogleAuthHandler
{
    Task<AuthObject.SigninResult> ExecuteAsync(string code);
}
