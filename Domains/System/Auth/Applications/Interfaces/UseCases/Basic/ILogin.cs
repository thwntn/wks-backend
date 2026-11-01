namespace Wks.Interfaces;

public interface ILoginAuth
{
    Task<AuthObject.SigninResult> ExecuteAsync(
        AuthDataTransformer.Signin signin
    );
    Task<AuthObject.SigninResult> Handler(Account account);
}
