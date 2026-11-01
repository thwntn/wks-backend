using System.Text.RegularExpressions;

namespace Wks.Mains;

public partial class Validate
{
    ///
    /// Summary:
    ///     Validates an email address format using regular expression pattern matching.
    ///
    /// Parameters:
    ///   email:
    ///     The email address string to validate.
    ///
    /// Returns:
    ///     True if the email format is valid, false otherwise.
    ///
    public static bool Email(string email)
    {
        if (email.Justifiable() is false)
            return false;

        return RegexValidateEmail().IsMatch(email);
    }

    ///
    /// Summary:
    ///     Validates a Vietnamese phone number format using regular expression pattern matching.
    ///     Supports both local (0) and international (+84) formats.
    ///
    /// Parameters:
    ///   phone:
    ///     The phone number string to validate.
    ///
    /// Returns:
    ///     True if the phone number format is valid, false otherwise.
    ///
    public static bool Phone(string phone)
    {
        if (phone.Justifiable() is false)
            return false;

        return RegexValidatePhone().IsMatch(phone);
    }

    ///
    /// Summary:
    ///     Generates a compiled regular expression for Vietnamese phone number validation.
    ///     Pattern supports: 0 or +84 prefix, followed by valid mobile prefixes (3[2-9], 5[2-9], 7[0-9], 8[1-9], 9[0-9]),
    ///     and 7 digits for the phone number.
    ///
    /// Returns:
    ///     A compiled Regex object for phone number validation.
    ///
    [GeneratedRegex(@"^(0|\+84)(3[2-9]|5[2-9]|7[0-9]|8[1-9]|9[0-9])[0-9]{7}$")]
    private static partial Regex RegexValidatePhone();

    ///
    /// Summary:
    ///     Generates a compiled regular expression for email address validation.
    ///     Pattern supports: alphanumeric characters, dots, underscores, percent signs, plus signs, hyphens
    ///     in local part, followed by @ symbol, domain name with dots, and TLD with 2 or more characters.
    ///
    /// Returns:
    ///     A compiled Regex object for email validation.
    ///
    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
    private static partial Regex RegexValidateEmail();
}
