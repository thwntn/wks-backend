using System.Globalization;
using System.Text.RegularExpressions;
using Slugify;

namespace Wks.Mains;

///
/// Summary:
///     Provides string formatting, conversion, and utility methods for data manipulation.
///     Includes currency formatting, text transformation, path building, and collection operations.
///
public static class Format
{
    public const string SPLIT_CHARACTER = ":";
    public const string SPACE_CHARACTER = " ";

    ///
    /// Summary:
    ///     Converts the first character of a string to uppercase while keeping the rest unchanged.
    ///     Returns the original string if it's empty.
    ///
    /// Parameters:
    ///     input:
    ///         The input string to capitalize.
    ///
    /// Returns:
    ///     The string with the first letter capitalized, or the original string if empty.
    ///
    public static string UpperFirstLetter(string input)
    {
        if (input.Length is 0)
            return input;

        return string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1));
    }

    ///
    /// Summary:
    ///     Splits a collection into smaller chunks of specified size.
    ///     Useful for processing large datasets in batches.
    ///
    /// Parameters:
    ///     source:
    ///         The source collection to split into chunks.
    ///
    ///     size:
    ///         The size of each chunk.
    ///
    /// Returns:
    ///     An enumerable of chunks, where each chunk is an enumerable of the specified size.
    ///
    public static IEnumerable<IEnumerable<T>> Patch<T>(
        IEnumerable<T> source,
        int size
    )
    {
        return source
            .Select((value, index) => new { value, index })
            .GroupBy(select => select.index / size)
            .Select(group => group.Select(select => select.value));
    }

    ///
    /// Summary:
    ///     Provides a default value if the input is null, otherwise returns the input value.
    ///     Useful for null-coalescing operations with generic types.
    ///
    /// Parameters:
    ///     verify:
    ///         The value to check for null.
    ///
    ///     replace:
    ///         The default value to return if verify is null.
    ///
    /// Returns:
    ///     The verify value if not null, otherwise the replace value.
    ///
    public static TFormat Default<TFormat>(TFormat? verify, TFormat replace) =>
        verify ?? replace;

    ///
    /// Summary:
    ///     Formats a number as Vietnamese currency with thousand separators and "đ" suffix.
    ///     Uses Vietnamese culture settings for proper number formatting.
    ///
    /// Parameters:
    ///     quantity:
    ///         The number to format as currency.
    ///
    /// Returns:
    ///     A formatted currency string in Vietnamese format (e.g., "1,000,000 đ").
    ///
    public static string CurrencyDefault(long quantity)
    {
        CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN"); // try with "en-US"
        string str =
            double.Parse(quantity.ToString())
                .ToString("#,###", cul.NumberFormat) + " đ";
        return str;
    }

    ///
    /// Summary:
    ///     Converts a camelCase or PascalCase string to a constant-style format with underscores.
    ///     Example: "userName" becomes "USER_NAME".
    ///
    /// Parameters:
    ///     text:
    ///         The text to convert to constant format.
    ///
    /// Returns:
    ///     The text converted to uppercase with underscores before capital letters.
    ///
    public static string ToConstant(string text)
    {
        string convert = string.Empty;
        for (int index = 0; index < text.Length; index++)
        {
            char character = text[index];
            if (char.IsUpper(character) && index > 0)
                convert += "_" + character;
            else
                convert += character;
        }
        return convert.ToUpper();
    }

    ///
    /// Summary:
    ///     Converts a string to lowercase.
    ///
    /// Parameters:
    ///     value:
    ///         The string to convert to lowercase.
    ///
    /// Returns:
    ///     The lowercase version of the input string.
    ///
    public static string ToLower(string value) => value.ToLower();

    ///
    /// Summary:
    ///     Concatenates multiple strings with spaces between them.
    ///     Adds leading and trailing spaces to the result.
    ///
    /// Parameters:
    ///     values:
    ///         The strings to concatenate with spaces.
    ///
    /// Returns:
    ///     A string with all values joined by spaces, with leading and trailing spaces.
    ///
    public static string ConcatSpace(params string[] values) =>
        $" {string.Join(" ", values)} ";

    ///
    /// Summary:
    ///     Concatenates multiple strings without separators.
    ///
    /// Parameters:
    ///     values:
    ///         The strings to concatenate.
    ///
    /// Returns:
    ///     A string with all values joined directly.
    ///
    public static string Concat(params string[] values) =>
        string.Join(string.Empty, values);

    ///
    /// Summary:
    ///     Concatenates multiple strings as a file path using forward slashes.
    ///     Useful for building file system paths.
    ///
    /// Parameters:
    ///     values:
    ///         The path components to concatenate.
    ///
    /// Returns:
    ///     A path string with components joined by forward slashes.
    ///
    public static string ToPathName(params string[] values) =>
        string.Join("/", values);

    ///
    /// Summary:
    ///     Builds an HTTPS URL from a domain host without a protocol and a path.
    ///
    public static string ToPublicUrl(string host, string path) =>
        UrlConstant.DEFAULT_PROTOCOL + ToPathName(host, path);

    ///
    /// Summary:
    ///     Concatenates integers as a date string using forward slashes.
    ///     Useful for formatting date components (day/month/year).
    ///
    /// Parameters:
    ///     values:
    ///         The date components to concatenate.
    ///
    /// Returns:
    ///     A date string with components joined by forward slashes.
    ///
    public static string ToStringDate(params int[] values) =>
        string.Join("/", values);

    ///
    /// Summary:
    ///     Concatenates multiple strings as a cache key using colons.
    ///     Useful for building Redis cache keys.
    ///
    /// Parameters:
    ///     values:
    ///         The cache key components to concatenate.
    ///
    /// Returns:
    ///     A cache key string with components joined by colons.
    ///
    public static string ToCacheName(params object[] values) =>
        string.Join(
            SPLIT_CHARACTER,
            values
                .Where(value => value.Justifiable())
                .Select(value => value.ToString())
        );

    ///
    /// Summary:
    ///     Converts text to a URL-friendly slug format.
    ///
    /// Parameters:
    ///     text:
    ///         The text to convert to a slug.
    ///
    /// Returns:
    ///     A slugified string safe for URLs.
    ///
    public static string ToSlugify(string text)
    {
        SlugHelper helper = new();
        return helper.GenerateSlug(text);
    }

    ///
    /// Summary:
    ///     Maps dictionary keys to slugified column names while keeping values unchanged.
    ///
    /// Parameters:
    ///     source:
    ///         The dictionary whose keys should be converted to slugs.
    ///
    /// Returns:
    ///     A new dictionary keyed by slugified column names.
    ///
    public static Dictionary<string, TValue> ToSlugKeys<TValue>(
        IDictionary<string, TValue> source
    )
    {
        Dictionary<string, TValue> mapped = new();
        foreach (KeyValuePair<string, TValue> entry in source)
            mapped.TryAdd(ToSlugify(entry.Key).ToLower(), entry.Value);

        return mapped;
    }

    ///
    /// Summary:
    ///     Ensures a number is not negative by returning 0 if the input is less than 0.
    ///     Useful for preventing negative values in calculations.
    ///
    /// Parameters:
    ///     number:
    ///         The number to limit to non-negative values.
    ///
    /// Returns:
    ///     The original number if non-negative, otherwise 0.
    ///
    public static long LimitZero(long number) => number >= 0 ? number : 0;

    ///
    /// Summary:
    ///     Removes line breaks and carriage returns from a text string.
    ///     Useful for cleaning text input or converting multiline text to single line.
    ///
    /// Parameters:
    ///     text:
    ///         The text to remove line breaks from.
    ///
    /// Returns:
    ///     The text with all line breaks and carriage returns removed.
    ///
    public static string ToSimpleText(string text) =>
        Regex.Replace(text, @"\r?\n", string.Empty);

    ///
    /// Summary:
    ///     Converts a string representation of a boolean value to its boolean equivalent.
    ///     If the conversion fails, it returns false by default.
    ///     Useful for safely parsing user input or configuration values that may not be valid booleans.
    ///
    /// Parameters:
    ///     text:
    ///         The string representation of the boolean value to convert.
    ///
    /// Returns:
    ///     The boolean equivalent of the input string, or false if the conversion fails.
    ///
    public static bool SafeBool(this string text)
    {
        _ = bool.TryParse(text, out bool result);
        return result;
    }
}
