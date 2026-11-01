namespace Wks.Objects;

public class ArtifactObject
{
    public sealed class Stream(Artifact artifact, MemoryStream memoryStream)
        : IDisposable
    {
        public Artifact Artifact { get; set; } = artifact;
        public MemoryStream MemoryStream { get; set; } = memoryStream;

        public void Dispose() => MemoryStream.Dispose();
    }

    public class Presigned(
        string objectKey,
        string objectName,
        string uploadUrl
    )
    {
        public string ObjectKey { get; set; } = objectKey;
        public string ObjectName { get; set; } = objectName;
        public string UploadUrl { get; set; } = uploadUrl;
    }
}
