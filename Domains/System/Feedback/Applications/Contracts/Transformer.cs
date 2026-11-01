namespace Wks.DataTransformers;

public class FeedbackDataTransformer
{
    public class Create
    {
        [JsonRequired]
        public string Name { get; set; }

        [JsonRequired]
        public string Contact { get; set; }

        [JsonRequired]
        public string Content { get; set; }

        public string File { get; set; }
    }

    public class Reply
    {
        [JsonRequired]
        public string Message { get; set; }

        public string File { get; set; }
    }
}
