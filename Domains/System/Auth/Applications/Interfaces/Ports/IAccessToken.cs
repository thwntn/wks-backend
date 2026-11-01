namespace Wks.Ports;

public interface IGenerateAccessTokenAuthPort
{
    Task<AuthObject.SigninResult> Execute(Guid accountId);
}
