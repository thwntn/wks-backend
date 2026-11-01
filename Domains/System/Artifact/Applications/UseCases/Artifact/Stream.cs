namespace Wks.UseCases;

public class DataStreamArtifact(
    IArtifactRepository _artifactRepository,
    IGetFile _getFile,
    ICache _cache
) : IDataStreamArtifact
{
    public async Task<ArtifactObject.Stream> ExecuteAsync(string token)
    {
        Artifact artifact =
            await _artifactRepository.Raw.FirstOrDefaultAsync(artifact =>
                artifact.Token == token
            )
            ?? throw new HttpException(
                HttpStatus.NotFound,
                MessageConstant.NOT_FOUND_ARTIFACT
            );

        string cacheName = Format.ToCacheName(
            nameof(CacheNames.MEDIA_FILE),
            token
        );
        string base64 = await _cache.CacheAsync(
            cacheName,
            async () =>
            {
                MemoryStream stream = await _getFile.ExecuteAsync(
                    Runtime.StorageBucketName.Env(),
                    artifact.ObjectKey
                );
                return Convert.ToBase64String(stream.ToArray());
            }
        );
        return new(artifact, ConvertToMemoryStream(base64));
    }

    private static MemoryStream ConvertToMemoryStream(string base64) =>
        new(Convert.FromBase64String(base64));
}
