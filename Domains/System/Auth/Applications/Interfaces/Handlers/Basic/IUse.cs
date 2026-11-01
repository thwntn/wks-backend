namespace Wks.Interfaces;

public interface IUsePasswordAuthHandler
{
    Task<AuthObject.SigninResult> ExecuteAsync(
        AuthDataTransformer.Signin signin
    );
}
