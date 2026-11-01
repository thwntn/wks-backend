namespace Wks.Objects;

public class MailObject
{
    public class MailgunSuccessResponse
    {
        public string Id { get; set; }
        public string Message { get; set; }
    }

    public class MailgunErrorResponse
    {
        public string Message { get; set; }
    }

    public class JsonData(string subject, string content)
    {
        public string Subject { get; set; } = subject;
        public string Content { get; set; } = content;
    }
}
