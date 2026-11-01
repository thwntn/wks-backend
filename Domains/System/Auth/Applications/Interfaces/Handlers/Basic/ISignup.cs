namespace Wks.Interfaces;

public interface ISignupAuthHandler
{
    Task<AuthObject.SignupResult> ExecuteAsync(
        AuthDataTransformer.Signup signup
    );
}
