namespace Wks.Interfaces;

///
/// Summary:
///     Clears all namespaces declared by CacheNames.
///
public interface IClearCacheAdmin
{
    ///
    /// Summary:
    ///     Removes every declared cache namespace.
    ///
    Task ExecuteAsync();
}
