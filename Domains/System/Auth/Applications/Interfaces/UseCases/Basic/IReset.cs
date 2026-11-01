namespace Wks.Interfaces;

public interface IResetPasswordAuth
{
    Task<Profile> ExecuteAsync(string email);
    Task<Profile> ChangePasswordAsync(
        AuthDataTransformer.ChangePassword changePassword
    );
}
