namespace Wks.Interfaces;

public interface IVerifyAuth
{
    Task<bool> ExecuteAsync(AuthDataTransformer.VerifyEmail verifyEmail);
}
