namespace Wks.Mains;

///
/// Summary:
///     Provides extension methods for common operations including null checking and environment variable access.
///
public static class Extension
{
    ///
    /// Summary:
    ///     Extension method to check if an object is not null.
    ///     Provides a more readable alternative to null checking.
    ///
    /// Parameters:
    ///     value:
    ///         The object to check for null.
    ///
    /// Returns:
    ///     True if the object is not null, false otherwise.
    ///
    public static bool Justifiable(this object value) => value is not null;

    ///
    /// Summary:
    ///     Extension method to check if a value is not null and execute an action on it.
    ///
    /// Parameters:
    ///     value:
    ///         The value to check for null.
    ///
    ///     action:
    ///         The action to execute if value is not null.
    ///
    /// Returns:
    ///     True if value is not null and action was executed, false otherwise.
    ///
    public static bool Justifiable<T>(this T value, Action<T> action)
    {
        if (value.Justifiable() is false)
            return false;

        action(value);
        return true;
    }

    public static async Task<bool> JustifiableAsync<T>(
        this T value,
        Func<T, Task> action
    )
    {
        if (value.Justifiable() is false)
            return false;

        await action(value);
        return true;
    }

    ///
    /// Summary:
    ///     Gets an environment variable value by runtime configuration key.
    ///
    /// Parameters:
    ///     envConfig:
    ///         The runtime configuration key enum value.
    ///
    /// Returns:
    ///     The environment variable value as a string.
    ///
    public static string Env(this Runtime envConfig) =>
        Environment.GetEnvironmentVariable(envConfig.ToString());
}
