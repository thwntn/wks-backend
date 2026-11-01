namespace Wks.Mains;

///
/// Summary:
///     Configures MinIO object storage client for file management.
///     Feature: (S3-compatible cloud storage with SSL connection)
///
/// -> Provides scalable object storage service for uploads, downloads,
///     and artifact management with AWS S3-compatible API.
///
public static class StorageExtension
{
    ///
    /// Summary:
    ///     Registers MinIO client as singleton for object storage operations.
    ///     Feature: (MinIO client with endpoint, credentials, and SSL configuration)
    ///
    /// -> Enables file storage operations to MinIO bucket with secure
    ///     SSL connection using access key and secret authentication.
    ///
    /// Parameters:
    ///   services:
    ///     The IServiceCollection instance used to register storage services.
    ///
    public static void StorageConfigure(this IServiceCollection services)
    {
        ///
        /// Summary:
        ///     Add MinIO client singleton with SSL-enabled connection
        ///     Feature: (MinioClient builder with host, credentials, and SSL)
        ///
        /// -> Creates single shared MinIO client instance for efficient
        ///     connection pooling and S3-compatible object storage access.
        ///
        services.AddSingleton(_ =>
            new MinioClient()
                .WithEndpoint(Runtime.StorageHost.Env())
                .WithCredentials(Runtime.StorageAccess.Env(), Runtime.StorageSecret.Env())
                .WithSSL(Runtime.StorageUseSsl.Env().SafeBool())
                .Build()
        );
    }
}
