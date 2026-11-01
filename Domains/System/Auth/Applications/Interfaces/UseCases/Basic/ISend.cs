namespace Wks.Interfaces;

public interface ISendCodeAuth
{
    Task ExecuteAsync(string toEmail, string code);
}
