namespace Wks.UseCases;

public class SendMail(IRequest _requestService) : ISendMail
{
    public async Task<HttpObject.Response<MailObject.MailgunSuccessResponse>> ExecuteAsync(
        string domain,
        string username,
        string apiKey,
        string from,
        string to,
        string subject,
        string html
    )
    {
        string[] keys =
        [
            nameof(subject),
            nameof(from),
            nameof(to),
            nameof(html),
        ];
        string[] values = [subject, from, to, html];

        string authorization = Convert.ToBase64String(
            Encoding.ASCII.GetBytes($"{username}:{apiKey}")
        );

        FormUrlEncodedContent content = new(
            keys.Zip(values, (key, value) => new { key, value })
                .ToDictionary(key => key.key, value => value.value)
        );

        string Basic,
            messages;
        HttpObject.Response<MailObject.MailgunSuccessResponse> response =
            await _requestService.FormUrlEncoded<MailObject.MailgunSuccessResponse>(
                Format.ToPathName(
                    Runtime.MailgunApi.Env(),
                    domain,
                    nameof(messages)
                ),
                content,
                new { Authorization = $"{nameof(Basic)} {authorization}" }.MapTo<
                    Dictionary<string, string>
                >()
            );

        return response;
    }
}
