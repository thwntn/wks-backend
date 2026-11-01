namespace Wks.Interfaces;

public interface IValidateAccountGoogle
{
    Task<AuthObject.GoogleValidate> ExecuteAsync(string authCode);
}
