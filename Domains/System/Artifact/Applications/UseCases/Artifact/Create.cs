namespace Wks.UseCases;

public class CreateArtifact(IArtifactRepository _artifactRepository)
    : ICreateArtifact
{
    private readonly int TOKEN_LENGTH = 20;

    public async Task<Artifact> ExecuteAsync(ArtifactParams.Create createParams)
    {
        Artifact artifact = createParams.MapTo<Artifact>();
        artifact.Extension = Path.GetExtension(createParams.RawName);

        artifact.Token =
            Cryptography.RandomHexString(TOKEN_LENGTH) + artifact.Extension;
        _artifactRepository.Create(artifact);

        return artifact;
    }
}
