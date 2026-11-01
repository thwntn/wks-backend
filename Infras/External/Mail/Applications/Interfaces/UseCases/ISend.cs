namespace Wks.Interfaces;

public interface ISendMail
{
    Task<HttpObject.Response<MailObject.MailgunSuccessResponse>> ExecuteAsync(
        string domain,
        string username,
        string apiKey,
        string from,
        string to,
        string subject,
        string html
    );
}
