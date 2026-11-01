namespace Wks.Interfaces;

public interface ISignupAuth
{
    Task<AuthObject.SignupResult> ExecuteAsync(
        AuthDataTransformer.Signup signup,
        bool skipVerify = false
    );
}
