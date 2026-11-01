namespace Wks.UseCases;

///
/// Summary:
///     Checks if account setup process is complete.
///     Verifies setup cache flag to determine if user needs initial configuration.
///
public class StatusSetupGoogleAuth(ICache _cache) : IStatusSetupGoogleAuth
{
    ///
    /// Summary:
    ///     Retrieves account setup status from cache.
    ///     Returns true if setup is complete, false if setup is still required.
    ///
    /// Parameters:
    ///     accountId:
    ///         The unique identifier of the account to check setup status for.
    ///
    /// Returns:
    ///     True if setup is complete, false if setup is pending.
    ///
    public async Task<bool> ExecuteAsync(Guid accountId)
    {
        /// Logic:
        ///     Get setup cache flag for account.
        ///     Return true if cache is empty (setup complete).
        ///     Return false if cache contains value (setup required).
        ///
        string cache = await _cache.GetAsync<string>(
            Format.ToCacheName(nameof(CacheNames.AUTH_USER_SETUP), accountId)
        );
        return cache.Justifiable() is false;
    }
}
