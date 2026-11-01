namespace Wks.Interfaces;

public interface IStaticArtifact
{
    Task<MemoryStream> ExecuteAsync(string fileName);
}
