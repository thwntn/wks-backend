namespace Wks.UseCases;

public class GetArtifact(IArtifactRepository _artifactRepository) : IGetArtifact
{
    public async Task<Artifact> ExecuteAsync(Guid artifactId) =>
        await _artifactRepository.Raw.FirstOrDefaultAsync(artifact =>
            artifact.Id == artifactId
        )
        ?? throw new HttpException(
            HttpStatus.NotFound,
            MessageConstant.NOT_FOUND_ARTIFACT
        );

    public async Task<Artifact> ExecuteAsync(string token) =>
        await _artifactRepository.Raw.FirstOrDefaultAsync(artifact =>
            artifact.Token == token
        )
        ?? throw new HttpException(
            HttpStatus.NotFound,
            MessageConstant.NOT_FOUND_ARTIFACT
        );
}
