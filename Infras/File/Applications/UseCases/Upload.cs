namespace Wks.UseCases;

///
/// Summary:
///     Service for uploading files to object storage using MinIO client.
///     Handles file upload from memory stream with error handling and logging.
///
public class UploadFile(IMinioClient _minioClient) : IUploadFile
{
    private const int RESET_STREAM = 0;

    ///
    /// Summary:
    ///     Uploads a file from memory stream to object storage.
    ///     Resets stream position before upload to ensure complete data transfer.
    ///
    /// Parameters:
    ///     memoryStream:
    ///         The memory stream containing the file data to upload.
    ///     bucket:
    ///         The name of the target storage bucket.
    ///     objectKey:
    ///         The unique key to assign to the uploaded file.
    ///
    /// Returns:
    ///     The object key of the successfully uploaded file.
    ///
    /// Exceptions:
    ///     T:Exception:
    ///     Rethrows any exception that occurs during upload after logging.
    ///
    public async Task<string> ExecuteAsync(
        MemoryStream memoryStream,
        string bucket,
        string objectKey
    )
    {
        try
        {
            /// Logic:
            ///     Reset stream position to beginning before upload.
            ///
            memoryStream.Position = RESET_STREAM;
            await _minioClient.PutObjectAsync(
                new PutObjectArgs()
                    .WithBucket(bucket)
                    .WithStreamData(memoryStream)
                    .WithObject(objectKey)
                    .WithObjectSize(memoryStream.Length)
            );

            return objectKey;
        }
        catch (Exception exception)
        {
            Logger.Write(exception);
            throw;
        }
    }
}
