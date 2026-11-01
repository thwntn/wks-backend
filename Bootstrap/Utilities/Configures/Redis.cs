namespace Wks.Mains;

///
/// Summary:
///     Configures Redis integration for distributed caching and connection multiplexing.
///     Feature: (Redis cache and connection multiplexer registration)
///
/// -> Provides Redis-based distributed cache and connection pooling capabilities
///     for high-performance data access across application instances.
///
public static class RedisExtension
{
    private static readonly string CONNECTION = Runtime.Redis.Env();

    ///
    /// Summary:
    ///     Registers Redis distributed cache and connection multiplexer services.
    ///     Feature: (StackExchange.Redis integration for caching and multiplexing)
    ///
    /// -> Configures Redis as the distributed cache provider and establishes
    ///     a singleton connection multiplexer for efficient Redis operations.
    ///
    /// Parameters:
    ///   services:
    ///     The IServiceCollection instance used to register Redis services.
    ///
    public static void RedisConfigure(this IServiceCollection services)
    {
        ///
        /// Summary:
        ///     Register Redis distributed cache provider
        ///     Feature: (StackExchangeRedisCache for IDistributedCache implementation)
        ///
        /// -> Enables distributed caching across multiple application instances
        ///     using Redis as the backing store. InstanceName is derived from Mode
        ///     (e.g. production:) so environments share Redis without key collisions.
        ///
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = CONNECTION;
        });

        ///
        /// Summary:
        ///     Register Redis connection multiplexer as singleton
        ///     Feature: (IConnectionMultiplexer for connection pooling and management)
        ///
        /// -> Provides a single shared connection pool to Redis for efficient
        ///     database operations and pub/sub messaging.
        ///
        services.AddSingleton<StackExchange.Redis.IConnectionMultiplexer>(_ =>
            StackExchange.Redis.ConnectionMultiplexer.Connect(CONNECTION)
        );
    }
}
