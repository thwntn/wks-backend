namespace Wks.Interfaces;

public interface IChangePasswordAuthHandler
{
    Task<string> ExecuteAsync(
        AuthDataTransformer.ChangePassword changePassword
    );
}
