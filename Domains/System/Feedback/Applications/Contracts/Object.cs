namespace Wks.Objects;

public class FeedbackObject
{
    #region Params
    public class Create(
        string name,
        string contact,
        string content,
        string file
    )
    {
        public string Name { get; set; } = name;
        public string Contact { get; set; } = contact;
        public string Content { get; set; } = content;
        public string File { get; set; } = file;
    }

    public class Reply(string message, string file)
    {
        public string Message { get; set; } = message;
        public string File { get; set; } = file;
    }
    #endregion

    #region Object
    public class Status
    {
        public FeedbackStatusEnum Enum { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string BackgroundColor { get; set; }
    }
    #endregion
}
