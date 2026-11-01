namespace Wks.DataTransformers;

public class ArtifactDataTransformer
{
    public class Create
    {
        [JsonRequired]
        public string ObjectKey { get; set; }

        [JsonRequired]
        public string RawName { get; set; }

        [JsonRequired]
        public string ObjectName { get; set; }

        [JsonRequired]
        public string FileType { get; set; }
    }
}
