namespace Wks.Interfaces;

public interface ICreateArtifactHandler
{
    Task<Artifact> ExecuteAsync(ArtifactParams.Create createParams);
}
