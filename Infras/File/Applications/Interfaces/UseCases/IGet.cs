namespace Wks.Interfaces;

///
/// Summary:
///     Interface for retrieving files from object storage.
///     Provides file download functionality from storage buckets.
///
public interface IGetFile
{
    ///
    /// Summary:
    ///     Downloads a file from the specified storage bucket.
    ///     Retrieves file data and returns it as a memory stream.
    ///
    /// Parameters:
    ///     bucket:
    ///         The name of the bucket containing the file.
    ///     objectKey:
    ///         The unique key identifying the file in the bucket.
    ///
    /// Returns:
    ///     Memory stream containing the file data, or null if file not found.
    ///
    Task<MemoryStream> ExecuteAsync(string bucket, string objectKey);

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
    Task<long?> SizeAsync(string bucket, string objectKey);
}
