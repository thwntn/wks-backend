namespace Wks.Interfaces;

public interface IResetPasswordAuthHandler
{
    Task<string> ExecuteAsync(string email);
}
