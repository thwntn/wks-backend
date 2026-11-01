namespace Wks.Mains;

///
/// Summary:
///     Provides file reading, writing, and configuration management utilities.
///     Handles environment variable loading, file operations, image processing,
///     and URL generation for storage services.
///
public class Reader
{
    ///
    /// Summary:
    ///     Storage host URL for file storage service configuration.
    ///
    private static string @storageHost;

    ///
    /// Summary:
    ///     Media prefix path for file storage organization.
    ///
    private static string @prefix;

    ///
    /// Summary:
    ///     Environment file name for configuration loading.
    ///
    private static readonly string @env = "Shared/Variables/.env";

    ///
    /// Summary:
    ///     String delimiter character for environment variable parsing.
    ///

    ///
    /// Summary:
    ///     Key-value separator character for environment variable parsing.
    ///

    ///
    /// Summary:
    ///     Token placeholder for URL parameter replacement.
    ///
    private static readonly string @params = "{token}/{**fileName}";

    ///
    /// Summary:
    ///     File name placeholder for static URL generation.
    ///
    private static readonly string @static = "{fileName}";

    ///
    /// Summary:
    ///     Comment character for environment variable file parsing.
    ///

    ///
    /// Summary:
    ///     Parameter name constant for file name references.
    ///
    public static readonly string @paramName = "fileName";

    ///
    /// Summary:
    ///     Counts the total number of lines in a text file asynchronously.
    ///
    /// Parameters:
    ///   path:
    ///     The file path to count lines from.
    ///
    /// Returns:
    ///     The total number of lines in the file.
    ///
    public static async Task<int> CountLineAsync(string path)
    {
        StreamReader reader = new(path);
        int count = 0;
        while ((await reader.ReadLineAsync()).Justifiable())
            count++;

        reader.Close();
        return count;
    }

    ///
    /// Summary:
    ///     Reads a specific range of lines from a text file asynchronously.
    ///
    /// Parameters:
    ///   path:
    ///     The file path to read from.
    ///
    ///   start:
    ///     The starting line index (zero-based).
    ///
    ///   loop:
    ///     The number of lines to read from the start position.
    ///
    /// Returns:
    ///     A ReadLine object containing the requested lines and total line count.
    ///
    public static async Task<ReaderObject.ReadLine> ReadLineAsync(
        string path,
        int start,
        int loop
    )
    {
        try
        {
            /// Logic:
            ///     Initializes an empty list to store the read lines.
            ///
            var records = Enumerable
                .Range(0, 0)
                .Select(_ => string.Empty)
                .ToList();
            StreamReader reader = new(path);
            int count = await CountLineAsync(path);

            for (int index = 0; index < count; index++)
            {
                if (index >= (start + loop))
                    break;

                /// Logic:
                ///     Skips lines before the start position and reads lines within the range.
                ///
                if (index < start)
                    continue;
                else
                    records.Add(await reader.ReadLineAsync());
            }

            return new(records, count);
        }
        catch
        {
            return new([]);
        }
    }

    ///
    /// Summary:
    ///     Loads optional Shared/Variables/.env values without overriding existing environment variables.
    ///     Skips blank/comment lines and initializes file URL settings from the resulting environment.
    ///     Deployments may provide configuration entirely through environment variables.
    ///
    public static async Task ConfigureAsync()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), @env);
        if (File.Exists(path))
        {
            foreach (string line in await File.ReadAllLinesAsync(path))
            {
                string content = line.Trim();
                if (content.Length == 0 || content.StartsWith('#')) continue;
                int separator = content.IndexOf('=');
                if (separator <= 0) continue;
                string key = content[..separator].Trim();
                string value = content[(separator + 1)..].Trim().Trim('"');
                if (Environment.GetEnvironmentVariable(key) is null)
                    Environment.SetEnvironmentVariable(key, value);
            }
        }
        @storageHost = Runtime.BackendApi.Env();
        @prefix = Runtime.Media.Env();
    }

    ///
    /// Summary:
    ///     Opens a file for reading with specified path and filename.
    ///
    /// Parameters:
    ///   fileName:
    ///     The name of the file to read.
    ///
    ///   path:
    ///     Optional subdirectory path within the media directory.
    ///
    /// Returns:
    ///     A FileStream for reading the file, or null if the file cannot be opened.
    ///
    public static FileStream ReadFile(string fileName, string path = default)
    {
        string location = string.Empty;
        if (path.Justifiable())
            location = path;

        try
        {
            /// Logic:
            ///     Creates a file stream for reading the specified file.
            ///
            FileStream stream = new(
                Format.ToPathName(
                    Directory.GetCurrentDirectory(),
                    @prefix,
                    location,
                    fileName
                ),
                FileMode.Open,
                FileAccess.Read
            );
            return stream;
        }
        catch (Exception)
        {
            return default;
        }
    }

    ///
    /// Summary:
    ///     Saves an uploaded file to the server with a unique filename.
    ///
    /// Parameters:
    ///   file:
    ///     The uploaded file from HTTP request.
    ///
    ///   path:
    ///     The subdirectory path where the file should be saved.
    ///
    /// Returns:
    ///     A Blob object containing file information and metadata.
    ///
    public static async Task<ReaderObject.Blob> SaveAsync(
        IFormFile file,
        string path
    )
    {
        /// Logic:
        ///     Generates a unique filename using GUID and preserves original extension.
        ///
        string fileName =
            Cryptography.RandomGuid() + Path.GetExtension(file.FileName);

        /// Logic:
        ///     Creates the target directory if it doesn't exist.
        ///
        string directory =
            Directory.GetCurrentDirectory() + $"/{@prefix}/{path}/";
        Directory.CreateDirectory(directory);

        /// Logic:
        ///     Copies the uploaded file to the target location.
        ///
        FileStream stream = File.Create(directory + fileName);
        await file.CopyToAsync(stream);

        stream.Close();
        return new ReaderObject.Blob(
            file.Name,
            fileName,
            fileName,
            file.Length
        );
    }

    ///
    /// Summary:
    ///     Gets file size information from an uploaded file without saving it.
    ///
    /// Parameters:
    ///   file:
    ///     The uploaded file to get size information from.
    ///
    /// Returns:
    ///     A Blob object containing file size and metadata.
    ///
    public static ReaderObject.Blob GetSize(IFormFile file)
    {
        ReaderObject.Blob info = new(
            file.Name,
            string.Empty,
            string.Empty,
            file.Length
        );
        return info;
    }

    ///
    /// Summary:
    ///     Creates a public URL for accessing a file through the storage service using a token.
    ///
    /// Parameters:
    ///   token:
    ///     The token identifier for the file.
    ///
    /// Returns:
    ///     A complete URL for accessing the file, or null if token is invalid.
    ///
    public static string CreateURL(string token)
    {
        if (token.Justifiable() is false)
            return default;

        if (@storageHost.Justifiable() is false)
            Console.WriteLine(nameof(@storageHost));

        return $"{@storageHost}/{RouteConstant.ARTIFACT}/{RouteConstant.ARTIFACT_TOKEN.Replace(@params, token)}";
    }

    ///
    /// Summary:
    ///     Creates a public URL for accessing a static file through the storage service.
    ///
    /// Parameters:
    ///   fileName:
    ///     The file name to create URL for.
    ///
    /// Returns:
    ///     A complete URL for accessing the static file, or null if fileName is invalid.
    ///
    public static string CreatePublicURL(string fileName)
    {
        if (fileName.Justifiable() is false)
            return default;

        if (@storageHost.Justifiable() is false)
            Console.WriteLine(nameof(@storageHost));

        return $"{@storageHost}/{RouteConstant.ARTIFACT}/{RouteConstant.ARTIFACT_STATIC.Replace(@static, fileName)}";
    }

    ///
    /// Summary:
    ///     Creates a thumbnail image from a file stream by resizing it.
    ///
    /// Parameters:
    ///   file:
    ///     The file stream containing the image to resize.
    ///
    ///   downSize:
    ///     The factor by which to reduce the image size (default: 10).
    ///
    /// Returns:
    ///     The filename of the saved thumbnail image.
    ///
    public static string Thumbnail(FileStream file, int downSize = 10)
    {
        /// Logic:
        ///     Generates a unique filename for the thumbnail with .jpg extension.
        ///
        string fileName = Cryptography.RandomGuid() + ".jpg";

        /// Logic:
        ///     Loads the image and resizes it according to the downSize factor.
        ///
        Image image = Image.Load(file);
        image.Mutate(x =>
            x.Resize(image.Width / downSize, image.Height / downSize)
        );

        /// Logic:
        ///     Saves the resized image as JPEG format.
        ///
        image.SaveAsJpeg(
            Directory.GetCurrentDirectory() + $"/{@prefix}/{fileName}"
        );
        return fileName;
    }

    ///
    /// Summary:
    ///     Converts an uploaded file to a memory stream for processing.
    ///
    /// Parameters:
    ///   formFile:
    ///     The uploaded file to convert to memory stream.
    ///
    /// Returns:
    ///     A MemoryStream containing the file data with position reset to beginning.
    ///
    public static async Task<MemoryStream> ToMemoryStreamAsync(
        IFormFile formFile
    )
    {
        MemoryStream stream = new();
        await formFile.CopyToAsync(stream);
        stream.Position = 0;
        return stream;
    }
}
