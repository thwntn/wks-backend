namespace Wks.Interfaces;

/// Summary:
///     Defines the contract for cache operations.
///     Provides methods for getting, setting, updating, and resetting cached data.
///
public interface ICache
{
    /// Summary:
    ///     Retrieves a cached item by its name.
    ///
    /// Type parameters:
    ///   TFormat:
    ///     The type of the cached item to retrieve.
    ///
    /// Parameters:
    ///   name:
    ///     The name/key of the cached item to retrieve.
    ///
    /// Returns:
    ///     The cached item of type TFormat, or default value if not found.
    Task<TFormat> GetAsync<TFormat>(string name);

    /// Summary:
    ///     Caches the result of a function if it's not already cached, or returns the cached value.
    ///     Uses lazy evaluation to only execute the function when needed.
    ///
    /// Type parameters:
    ///   TFormat:
    ///     The type of the value to cache.
    ///
    /// Parameters:
    ///   name:
    ///     The name/key for the cached item.
    ///   func:
    ///     The function to execute if the item is not cached.
    ///
    /// Returns:
    ///     The cached or newly computed value of type TFormat.
    Task<TFormat> CacheAsync<TFormat>(
        string name,
        Func<Task<TFormat>> func,
        int expire = Cache.EXPIRE_TIME_MINUTES
    );

    /// Summary:
    ///     Updates an existing cached item with new data.
    ///
    /// Type parameters:
    ///   TFormat:
    ///     The type of the value to update in the cache.
    ///
    /// Parameters:
    ///   name:
    ///     The name/key of the cached item to update.
    ///   data:
    ///     The new data to store in the cache.
    Task Update<TFormat>(
        string name,
        TFormat data,
        int expireTime = Cache.EXPIRE_TIME_MINUTES
    );

    /// Summary:
    ///     Sets a value in the cache.
    ///
    /// Parameters:
    ///   name:
    ///     The name/key of the cached item to set.
    ///   data:
    ///     The value to store in the cache.
    ///   expire:
    ///     The expiration time in minutes for the cached item.
    Task SetAsync(
        string name,
        object data,
        int expire = Cache.EXPIRE_TIME_MINUTES
    );

    Task<bool> SetIfNotExistsAsync(
        string name,
        object data,
        int expireTime = Cache.EXPIRE_TIME_MINUTES
    );

    /// Summary:
    ///     Removes a cached item from the cache by its name.
    ///
    /// Parameters:
    ///   name:
    ///     The name/key of the cached item to remove.
    Task RemoveAsync(string name);

    ///
    /// Summary:
    ///     Removes a namespace root and descendant keys, including current-environment flags.
    /// Parameters:
    ///     name: The cache namespace to clear.
    ///
    Task RemoveNamespaceAsync(string name);

    Task SignSubscriber<TData>(
        StackExchange.Redis.RedisChannel channel,
        Action<StackExchange.Redis.RedisChannel, TData> handler
    );

    Task PublisherAsync(
        StackExchange.Redis.RedisChannel channel,
        object data = default
    );
}
