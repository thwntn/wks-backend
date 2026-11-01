namespace Wks.Objects;

///
/// Summary:
///     Contains data transfer objects for file reading operations.
///     Provides structured objects for handling file data and line reading results.
///
public class ReaderObject
{
    ///
    /// Summary:
    ///     Represents file metadata and information for blob storage operations.
    ///     Used for tracking uploaded files and their properties.
    ///
    /// Parameters:
    ///     key:
    ///         The unique identifier or key for the file.
    ///
    ///     fileName:
    ///         The original name of the uploaded file.
    ///
    ///     path:
    ///         The storage path or location of the file.
    ///
    ///     size:
    ///         The size of the file in bytes.
    ///
    public class Blob(string key, string fileName, string path, long size)
    {
        ///
        /// Summary:
        ///     Gets the unique identifier or key for the file.
        ///
        public string Key { get; } = key;

        ///
        /// Summary:
        ///     Gets the original name of the uploaded file.
        ///
        public string FileName { get; } = fileName;

        ///
        /// Summary:
        ///     Gets the storage path or location of the file.
        ///
        public string Path { get; } = path;

        ///
        /// Summary:
        ///     Gets the size of the file in bytes.
        ///
        public long Size { get; } = size;
    }

    ///
    /// Summary:
    ///     Represents the result of reading lines from a file.
    ///     Contains the lines read and the total count for pagination purposes.
    ///
    /// Parameters:
    ///     lines:
    ///         The collection of lines read from the file.
    ///
    ///     count:
    ///         The total number of lines in the file (default: 0).
    ///
    public class ReadLine(IEnumerable<string> lines, int count = 0)
    {
        ///
        /// Summary:
        ///     Gets or sets the collection of lines read from the file.
        ///
        public IEnumerable<string> Lines { get; set; } = lines;

        ///
        /// Summary:
        ///     Gets or sets the total number of lines in the file.
        ///     Used for pagination and progress tracking.
        ///
        public int Count { get; set; } = count;
    }
}
