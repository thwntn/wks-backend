namespace Wks.UseCases;

///
/// Summary:
///     Service for generating presigned upload URLs using MinIO client.
///     Provides temporary authenticated URLs for direct file uploads to storage.
///
public class PresignedUploadFile(IMinioClient minioClient)
    : IPresignedUploadFile
{
    private readonly IMinioClient _minioClient = minioClient;

    ///
    /// Summary:
    ///     Generates a presigned URL for uploading a file to object storage.
    ///     The URL allows temporary upload access without requiring storage credentials.
    ///
    /// Parameters:
    ///     bucket:
    ///         The name of the target storage bucket.
    ///     objectKey:
    ///         The unique key for the file to be uploaded.
    ///     expiry:
    ///         The expiration time in seconds for the presigned URL.
    ///
    /// Returns:
    ///     Presigned URL string for uploading the file.
    ///
    public async Task<string> ExecuteAsync(
        string bucket,
        string objectKey,
        int expiry
    ) =>
        await _minioClient.PresignedPutObjectAsync(
            new PresignedPutObjectArgs()
                .WithBucket(bucket)
                .WithObject(objectKey)
                .WithExpiry(expiry)
        );
}
