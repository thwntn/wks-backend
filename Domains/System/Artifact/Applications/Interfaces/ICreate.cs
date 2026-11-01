namespace Wks.Interfaces;

public interface ICreateArtifact
{
    Task<Artifact> ExecuteAsync(ArtifactParams.Create createParams);
}
