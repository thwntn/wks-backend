namespace Wks.Objects;

//
// Summary:
//     Contains cache-related data structures and configuration objects.
//     Provides structured objects for cache operations and configuration.
//
public class CacheObject
{
    //
    // Summary:
    //     Represents a cache item with key, value, and expiration time.
    //     Generic class that can store any type of value in the cache.
    //
    // Type parameters:
    //   T:
    //     The type of value to store in the cache.
    //
    public class CacheItem<T>
    {
        //
        // Summary:
        //     Gets or sets the unique key identifier for the cache item.
        //
        public string Key { get; set; }

        //
        // Summary:
        //     Gets or sets the value stored in the cache item.
        //
        public T Value { get; set; }

        //
        // Summary:
        //     Gets or sets the expiration time when the cache item should be invalidated.
        //
        public DateTime ExpirationTime { get; set; }
    }

    //
    // Summary:
    //     Represents configuration options for cache operations.
    //     Provides settings for cache expiration and enable/disable functionality.
    //
    public class CacheOptions
    {
        //
        // Summary:
        //     Gets or sets the expiration time in minutes for cache items.
        //     Default value is 60 minutes.
        //
        public int ExpirationMinutes { get; set; } = 60;

        //
        // Summary:
        //     Gets or sets whether caching is enabled or disabled.
        //     Default value is true (enabled).
        //
        public bool EnableCache { get; set; } = true;
    }
}
