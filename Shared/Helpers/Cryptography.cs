namespace Wks.Mains;

///
/// Summary:
///     Provides cryptographic utility methods for hashing, encoding, and generating secure random values.
///     Includes MD5, SHA512, HMAC-SHA256, PBKDF2 hashing, and Base64URL encoding operations.
///
public static class Cryptography
{
    ///
    /// Summary:
    ///     Base character set for generating random strings.
    ///     Contains digits, uppercase and lowercase letters.
    ///
    private static readonly string BASE_CHARS =
        "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    ///
    /// Summary:
    ///     Generates a new globally unique identifier (GUID).
    ///     Useful for creating unique identifiers for files, records, or sessions.
    ///
    /// Returns:
    ///     A new GUID as a string representation.
    ///
    public static Guid RandomGuid() => Guid.NewGuid();

    ///
    /// Summary:
    ///     Computes SHA-512 hash of a string input.
    ///
    /// Parameters:
    ///     input:
    ///         The input string to hash.
    ///
    /// Returns:
    ///     A Base64 encoded string representation of the SHA-512 hash.
    ///
    public static string Sha512(string input) =>
        Convert.ToBase64String(SHA512.HashData(Encoding.UTF8.GetBytes(input)));

    ///
    /// Summary:
    ///     Computes MD5 hash of a string input.
    ///     Note: MD5 is not cryptographically secure, use for non-security purposes only.
    ///
    /// Parameters:
    ///     str:
    ///         The input string to hash.
    ///
    /// Returns:
    ///     A hexadecimal string representation of the MD5 hash without dashes.
    ///
    public static string Md5(string str) =>
        Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(str)));

    ///
    /// Summary:
    ///     Encodes a string to Base64URL format for safe transmission in URLs.
    ///     Replaces unsafe characters (+ and /) with URL-safe alternatives (- and _).
    ///
    /// Parameters:
    ///     input:
    ///         The string to encode to Base64URL format.
    ///
    /// Returns:
    ///     A Base64URL encoded string safe for use in URLs and filenames.
    ///
    public static string Base64UrlEncode(this string input) =>
        Base64UrlEncoder.Encode(Encoding.UTF8.GetBytes(input));

    ///
    /// Summary:
    ///     Decodes a Base64URL encoded string back to original text.
    ///
    /// Parameters:
    ///     input:
    ///         The Base64URL encoded string to decode.
    ///
    /// Returns:
    ///     The decoded original string.
    ///
    public static string Base64UrlDecode(this string input) =>
        Encoding.UTF8.GetString(Base64UrlEncoder.DecodeBytes(input));

    ///
    /// Summary:
    ///     Generates a cryptographically secure random hexadecimal string.
    ///
    /// Parameters:
    ///     length:
    ///         The desired length of the random string.
    ///
    /// Returns:
    ///     A random string composed of characters from BASE_CHARS.
    ///
    public static string RandomHexString(int length) =>
        string.Concat(
            RandomNumberGenerator
                .GetBytes(length)
                .Select(b => BASE_CHARS[b % BASE_CHARS.Length])
        );

    ///
    /// Summary:
    ///     Generates a random code based on the current UTC time hash code.
    ///     Useful for creating temporary codes or identifiers.
    ///
    /// Returns:
    ///     A hexadecimal string representation of the time-based hash code.
    ///
    public static string RandomCode() =>
        string.Format("{0:X}", Moment.Utc.Now.GetHashCode());

    ///
    /// Summary:
    ///     Creates a secure hash of content using PBKDF2 with HMACSHA256.
    ///     Uses a random salt and 100,000 iterations for security.
    ///
    /// Parameters:
    ///     content:
    ///         The content to hash securely.
    ///
    /// Returns:
    ///     A Base64 encoded string containing the hashed content with salt.
    ///
    public static string Hash(string content)
    {
        /// Logic:
        ///     Generate a cryptographically secure random salt.
        ///     Hash content using PBKDF2 with HMACSHA256, 100,000 iterations.
        ///     Return Base64 encoded hash result.
        ///
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

        string hashed = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                password: content,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8
            )
        );
        return hashed;
    }

    ///
    /// Summary:
    ///     Computes HMAC-SHA256 hash of content using a private key.
    ///
    /// Parameters:
    ///     content:
    ///         The content to hash with HMAC.
    ///
    ///     privateKey:
    ///         The private key to use for HMAC computation.
    ///
    /// Returns:
    ///     A hexadecimal string representation of the HMAC-SHA256 hash, or empty string on error.
    ///
    /// Exceptions:
    ///     T:System.IO.IOException:
    ///     Logs error and returns empty string if HMAC computation fails.
    ///
    public static string HmacSha256(string content, string privateKey)
    {
        try
        {
            using HMACSHA256 hmac = new(Encoding.UTF8.GetBytes(privateKey));
            byte[] computeHash = hmac.ComputeHash(
                Encoding.UTF8.GetBytes(content)
            );

            string hash = Convert.ToHexString(computeHash);
            return hash;
        }
        catch (IOException exception)
        {
            Logger.Write(exception.Message);
            return string.Empty;
        }
    }
}
