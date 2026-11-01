namespace Wks.UseCases;

///
/// Summary:
///     Service for downloading files from object storage using MinIO client.
///     Implements file retrieval functionality with error handling.
///
public class GetFile(IMinioClient minioClient) : IGetFile
{
    private readonly IMinioClient _minioClient = minioClient;

    ///
    /// Summary:
    ///     Downloads a file from the specified storage bucket.
    ///     Retrieves file data and returns it as a memory stream for further processing.
    ///
    /// Parameters:
    ///     bucket:
    ///         The name of the bucket containing the file.
    ///     objectKey:
    ///         The unique key identifying the file in the bucket.
    ///
    /// Returns:
    ///     Memory stream containing the file data, or null if download fails.
    ///
    public async Task<MemoryStream> ExecuteAsync(
        string bucket,
        string objectKey
    )
    {
        try
        {
            /// Logic:
            ///     Create memory stream and retrieve file from MinIO bucket using callback.
            ///
            MemoryStream file = new();
            await _minioClient.GetObjectAsync(
                new GetObjectArgs()
                    .WithBucket(bucket)
                    .WithObject(objectKey)
                    .WithCallbackStream(stream => stream.CopyTo(file))
            );

            /// Logic:
            ///     Reset stream position to beginning for reading.
            ///
            file.Position = 0;
            return file;
        }
        catch (Exception exception)
        {
            Logger.Write(exception);
            return default;
        }
    }

    ///
    /// Summary:
    ///     Returns the object size when the file exists in the bucket.
    ///
    /// Parameters:
    ///     bucket:
    ///         The name of the bucket containing the file.
    ///
    ///     objectKey:
    ///         The unique key identifying the file in the bucket.
    ///
    /// Returns:
    ///     The object size in bytes, or null when the file is missing.
    ///
    public async Task<long?> SizeAsync(string bucket, string objectKey)
    {
        try
        {
            ObjectStat stat = await _minioClient.StatObjectAsync(
                new StatObjectArgs().WithBucket(bucket).WithObject(objectKey)
            );
            return stat.Size;
        }
        catch (Exception exception)
        {
            Logger.Write(exception);
            return default;
        }
    }
}
