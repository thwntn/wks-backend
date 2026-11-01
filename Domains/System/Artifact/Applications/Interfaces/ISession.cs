namespace Wks.Interfaces;

public interface IPresignedUploadArtifact
{
    Task<ArtifactObject.Presigned> ExecuteAsync();
}
