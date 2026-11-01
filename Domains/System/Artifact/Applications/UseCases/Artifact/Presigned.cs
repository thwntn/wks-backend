namespace Wks.UseCases;

public class PresignedUploadArtifact(IPresignedUploadFile _presignedUpload)
    : IPresignedUploadArtifact
{
    public async Task<ArtifactObject.Presigned> ExecuteAsync()
    {
        int expiry = int.Parse(Runtime.PresignedExpired.Env());
        string objectName = Cryptography.RandomGuid().ToString(),
            objectKey = Format.ToPathName(
                nameof(FolderNames.Files),
                objectName
            ),
            bucket = Runtime.StorageBucketName.Env();

        string url = await _presignedUpload.ExecuteAsync(
            bucket,
            objectKey,
            expiry
        );
        return new(objectKey, objectName, url);
    }
}
