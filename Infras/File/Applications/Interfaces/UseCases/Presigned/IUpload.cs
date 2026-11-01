namespace Wks.Interfaces;

///
/// Summary:
///     Interface for generating presigned upload URLs for object storage.
///     Enables secure temporary file upload access without requiring credentials.
///
public interface IPresignedUploadFile
{
    ///
    /// Summary:
    ///     Generates a presigned URL for uploading a file to object storage.
    ///     The URL grants temporary upload access for the specified duration.
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
    Task<string> ExecuteAsync(string bucket, string objectKey, int expiry);
}
