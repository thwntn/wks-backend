namespace Wks.Interfaces;

///
/// Summary:
///     Interface for uploading files to object storage.
///     Provides file upload functionality to storage buckets.
///
public interface IUploadFile
{
    ///
    /// Summary:
    ///     Uploads a file from memory stream to object storage.
    ///     Stores the file in the specified bucket with the given key.
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
    ///     The object key of the uploaded file.
    ///
    Task<string> ExecuteAsync(
        MemoryStream memoryStream,
        string bucket,
        string objectKey
    );
}
