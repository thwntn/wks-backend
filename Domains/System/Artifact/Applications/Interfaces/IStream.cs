namespace Wks.Interfaces;

public interface IDataStreamArtifact
{
    Task<ArtifactObject.Stream> ExecuteAsync(string token);
}
