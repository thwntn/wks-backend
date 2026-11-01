namespace Wks.Interfaces;

public interface IGetArtifact
{
    Task<Artifact> ExecuteAsync(Guid artifactId);
    Task<Artifact> ExecuteAsync(string token);
}
