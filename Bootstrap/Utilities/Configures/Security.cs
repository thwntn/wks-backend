namespace Wks.Mains;

///
/// Summary:
///     Configures ASP.NET Core Data Protection services for encryption.
///     Feature: (AES-256-CBC encryption with HMACSHA256 validation)
///
/// -> Provides secure data encryption and integrity validation
///     for cookies, tokens, and sensitive application data.
///
public static class SecurityExtension
{
    ///
    /// Summary:
    ///     Registers Data Protection services with strong cryptographic algorithms.
    ///     Feature: (AES-256 encryption and HMAC-SHA256 message authentication)
    ///
    /// -> Ensures data confidentiality through AES-256 encryption
    ///     and data integrity through HMAC-SHA256 signatures.
    ///
    /// Parameters:
    ///   services:
    ///     The IServiceCollection instance used to register data protection services.
    ///
    public static void SecurityConfigure(this IServiceCollection services)
    {
        ///
        /// Summary:
        ///     Add Data Protection with AES-256-CBC and HMACSHA256 algorithms
        ///     Feature: (Cryptographic algorithm configuration for data protection)
        ///
        /// -> Protects sensitive data using industry-standard encryption
        ///     with 256-bit key strength and authenticated encryption mode.
        ///
        services
            .AddDataProtection()
            .UseCryptographicAlgorithms(
                new()
                {
                    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256,
                }
            );
    }
}
