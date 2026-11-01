namespace Wks.UseCases;

///
/// Summary:
///     Implements cache operations backed by Redis via IDistributedCache, with
///     pub/sub and atomic set-if-not-exists through IConnectionMultiplexer.
///     Provides storing, retrieving, updating, and removing cached data, plus a
///     get-or-compute helper guarded against cache stampede.
///
/// Parameters:
///   _distributedCache:
///     The IDistributedCache instance used for key/value cache operations.
///   _connectionMultiplexer:
///     The Redis connection used for atomic flags and pub/sub messaging.
///
public class Cache(
    IDistributedCache _distributedCache,
    StackExchange.Redis.IConnectionMultiplexer _connectionMultiplexer
) : ICache
{
    ///
    /// Summary:
    ///     The default expiration for cached items in minutes (10080 = 7 days).
    ///
    public const int EXPIRE_TIME_MINUTES = 10080;
    private const string KEY_PATTERN_WILDCARD = "*";

    ///
    /// Summary:
    ///     Serializes and stores a value under the given key, overwriting any existing value.
    ///
    /// Parameters:
    ///   name:
    ///     The cache key to store the value under.
    ///   data:
    ///     The value to serialize and cache.
    ///   expireTime:
    ///     The expiration in minutes; defaults to EXPIRE_TIME_MINUTES.
    ///
    public async Task SetAsync(
        string name,
        object data,
        int expireTime = EXPIRE_TIME_MINUTES
    ) =>
        await ExecuteLoggedAsync(
            nameof(LogEventEnum.Redis),
            nameof(LogEventEnum.Set),
            name,
            () =>
                _distributedCache.SetStringAsync(
                    name,
                    data.Serialize(),
                    BuildOptions(expireTime)
                )
        );

    ///
    /// Summary:
    ///     Atomically stores a value only when the key does not already exist.
    ///     Uses an env-prefixed Redis key via IConnectionMultiplexer, so it is
    ///     suited to distributed flags/locks rather than values read back with GetAsync.
    ///
    /// Parameters:
    ///   name:
    ///     The cache key to store the value under (env prefix is applied).
    ///   data:
    ///     The value to serialize and cache.
    ///   expireTime:
    ///     The expiration in minutes; defaults to EXPIRE_TIME_MINUTES.
    ///
    /// Returns:
    ///     True when the value was set (key was absent); false when the key already existed.
    ///
    public async Task<bool> SetIfNotExistsAsync(
        string name,
        object data,
        int expireTime = EXPIRE_TIME_MINUTES
    ) =>
        await ExecuteLoggedAsync(
            nameof(LogEventEnum.Redis),
            nameof(LogEventEnum.SetIfNotExists),
            name,
            () =>
                _connectionMultiplexer
                    .GetDatabase()
                    .StringSetAsync(
                        $"{Runtime.Mode.Env().ToLower()}:{name}",
                        data.Serialize(),
                        TimeSpan.FromMinutes(expireTime),
                        StackExchange.Redis.When.NotExists
                    )
        );

    ///
    /// Summary:
    ///     Retrieves a cached item by key and deserializes it to the requested type.
    ///     Logs a Hit, Miss, or Failed outcome; swallows errors so cache faults do
    ///     not break the calling flow.
    ///
    /// Type parameters:
    ///   TFormat:
    ///     The type to deserialize the cached value into.
    ///
    /// Parameters:
    ///   name:
    ///     The cache key to read.
    ///
    /// Returns:
    ///     The cached value, or default(TFormat) when the key is missing or an error occurs.
    ///
    public async Task<TFormat> GetAsync<TFormat>(string name)
    {
        try
        {
            /// Logic:
            ///     Read + deserialize inside the shared logger, reporting Hit/Miss
            ///     via the status resolver (Failed is logged by the helper on error).
            return await ExecuteLoggedAsync(
                nameof(LogEventEnum.Redis),
                nameof(LogEventEnum.Get),
                name,
                async () =>
                {
                    string json = await _distributedCache.GetStringAsync(name);
                    if (json.Justifiable() is false)
                        return default;

                    return json.Deserialize<TFormat>();
                },
                result =>
                {
                    if (result.Justifiable() is false)
                        return nameof(LogEventEnum.Miss);

                    return nameof(LogEventEnum.Hit);
                }
            );
        }
        catch
        {
            /// Logic:
            ///     Swallow cache faults so a read failure never breaks the caller.
            return default;
        }
    }

    ///
    /// Summary:
    ///     Returns a cached value, or computes it via the handler and caches the result.
    ///     Guards against cache stampede with a per-key lock and double-checked read.
    ///
    /// Type parameters:
    ///   TFormat:
    ///     The type of the value to cache.
    ///
    /// Parameters:
    ///   name:
    ///     The cache key for the value.
    ///   handler:
    ///     The function invoked to produce the value on a cache miss.
    ///   expireTime:
    ///     The expiration in minutes; defaults to EXPIRE_TIME_MINUTES.
    ///
    /// Returns:
    ///     The cached value, or the freshly computed value stored under the key.
    ///
    public async Task<TFormat> CacheAsync<TFormat>(
        string name,
        Func<Task<TFormat>> handler,
        int expireTime = EXPIRE_TIME_MINUTES
    )
    {
        /// Logic:
        ///     Fast path: return the cached value when present.
        TFormat cache = await GetAsync<TFormat>(name);
        if (cache.Justifiable())
            return cache;

        return await Mains.Lock.Run(
            name,
            async () =>
            {
                /// Logic:
                ///     Re-check under the lock so only one caller computes the value.
                cache = await GetAsync<TFormat>(name);
                if (cache.Justifiable() is true)
                    return cache;

                /// Logic:
                ///     Compute the value, store it, then return it.
                TFormat data = await handler();
                await Update(name, data, expireTime);
                return data;
            }
        );
    }

    ///
    /// Summary:
    ///     Serializes and writes a value to the cache under the given key.
    ///
    /// Type parameters:
    ///   TFormat:
    ///     The type of the value to store.
    ///
    /// Parameters:
    ///   name:
    ///     The cache key to write.
    ///   data:
    ///     The value to serialize and store.
    ///   expireTime:
    ///     The expiration in minutes; defaults to EXPIRE_TIME_MINUTES.
    ///
    public async Task Update<TFormat>(
        string name,
        TFormat data,
        int expireTime = EXPIRE_TIME_MINUTES
    )
    {
        /// Logic:
        ///     Serialize the value to JSON and store it with the given expiration.
        await ExecuteLoggedAsync(
            nameof(LogEventEnum.Redis),
            nameof(LogEventEnum.Set),
            name,
            () =>
                _distributedCache.SetStringAsync(
                    name,
                    data.Serialize(),
                    BuildOptions(expireTime)
                )
        );
    }

    ///
    /// Summary:
    ///     Scans primary Redis servers and deletes only the selected namespace.
    /// Parameters:
    ///     name: The root namespace; descendants must be separated by a colon.
    /// Logic:
    ///     Uses the configured database and never flushes unrelated Redis data.
    ///     Deletes keys individually so Redis Cluster hash slots may differ.
    ///
    public async Task RemoveNamespaceAsync(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        StackExchange.Redis.IDatabase database =
            _connectionMultiplexer.GetDatabase();
        string[] roots =
        [
            name,
            Format.ToCacheName(Runtime.Mode.Env().ToLower(), name),
        ];

        foreach (
            System.Net.EndPoint endpoint in _connectionMultiplexer.GetEndPoints()
        )
        {
            StackExchange.Redis.IServer server =
                _connectionMultiplexer.GetServer(endpoint);
            if (server.IsReplica)
                continue;

            foreach (string root in roots.Distinct())
            await foreach (
                StackExchange.Redis.RedisKey key in server.KeysAsync(
                    database.Database,
                    pattern: root + KEY_PATTERN_WILDCARD
                )
            )
            {
                string value = key.ToString();
                if (
                    value != root
                    && value.StartsWith(
                        root + Format.SPLIT_CHARACTER,
                        StringComparison.Ordinal
                    ) == false
                )
                    continue;

                await database.KeyDeleteAsync(key);
            }
        }
    }

    ///
    /// Summary:
    ///     Removes a cached item by its key.
    ///
    /// Parameters:
    ///   name:
    ///     The cache key to remove.
    ///
    public async Task RemoveAsync(string name) =>
        /// Logic:
        ///     Delete the key from Redis and log the outcome.
        await ExecuteLoggedAsync(
            nameof(LogEventEnum.Redis),
            nameof(LogEventEnum.Delete),
            name,
            () => _distributedCache.RemoveAsync(name)
        );

    ///
    /// Summary:
    ///     Subscribes to a Redis channel and invokes the handler for each message,
    ///     deserializing the payload into TData.
    ///
    /// Type parameters:
    ///   TData:
    ///     The type the message payload is deserialized into.
    ///
    /// Parameters:
    ///   channel:
    ///     The Redis channel to subscribe to.
    ///   handler:
    ///     The callback invoked with the channel and the deserialized payload.
    ///
    public async Task SignSubscriber<TData>(
        StackExchange.Redis.RedisChannel channel,
        Action<StackExchange.Redis.RedisChannel, TData> handler
    ) =>
        await _connectionMultiplexer
            .GetSubscriber()
            .SubscribeAsync(
                channel,
                (redisChannel, value) =>
                {
                    TData data = default;
                    value.Justifiable(_ =>
                        data = _.ToString().Deserialize<TData>()
                    );
                    handler(redisChannel, data);
                }
            );

    ///
    /// Summary:
    ///     Publishes a message to a Redis channel, serializing the payload to JSON.
    ///
    /// Parameters:
    ///   channel:
    ///     The Redis channel to publish to.
    ///   data:
    ///     The payload to serialize and publish; null publishes an empty message.
    ///
    public async Task PublisherAsync(
        StackExchange.Redis.RedisChannel channel,
        object data = default
    )
    {
        string json = default;
        data.Justifiable((data) => json = data.Serialize());
        await ExecuteLoggedAsync(
            nameof(LogEventEnum.Redis),
            nameof(LogEventEnum.Publish),
            channel.ToString(),
            () =>
                _connectionMultiplexer
                    .GetSubscriber()
                    .PublishAsync(channel, json)
        );
    }

    ///
    /// Summary:
    ///     Executes a Redis operation with the ambient trace and logs its outcome.
    ///
    /// Logic:
    ///     Awaits the supplied action and returns its result without transformation.
    ///     Logs the success event, using the status resolver when provided
    ///     (e.g. Hit/Miss) or Completed by default, with the key and elapsed ms.
    ///     Logs Failed with the exception type and rethrows the original exception.
    ///     Restores the previous trace when the operation scope is disposed.
    ///
    /// Type parameters:
    ///   T:
    ///     The result type of the asynchronous Redis operation.
    ///
    /// Parameters:
    ///   source:
    ///     The infrastructure source name produced from LogEventEnum.
    ///   operation:
    ///     The operation name produced from LogEventEnum.
    ///   key:
    ///     The logical cache key or publication channel included in the event.
    ///   action:
    ///     The asynchronous operation whose result and exception behavior are preserved.
    ///   status:
    ///     Optional resolver that maps the result to a success status, overriding
    ///     the default Completed (e.g. Hit/Miss for reads). When null, Completed is used.
    ///
    /// Returns:
    ///     The original result returned by the supplied action.
    ///
    private static async Task<T> ExecuteLoggedAsync<T>(
        string source,
        string operation,
        string key,
        Func<Task<T>> action,
        Func<T, string> status = default
    )
    {
        using IDisposable scope = Logger.BeginScope(Logger.TraceId);
        Stopwatch watch = Stopwatch.StartNew();

        LoggingSchema.Redis logging = new()
        {
            LogIndexName = Runtime.LogIndexName.Env(),
            Source = source,
            Operation = operation,
            Key = key,
        };

        try
        {
            T result = await action();

            logging.Status = status.Justifiable()
                ? status(result)
                : nameof(LogEventEnum.Completed);
            logging.DurationMs = watch.Elapsed.TotalMilliseconds;

            Logger.Write(logging);
            return result;
        }
        catch (Exception error)
        {
            logging.Status = nameof(LogEventEnum.Failed);
            logging.Error = error.GetType().Name;

            Logger.Write(logging, nameof(LogLevel.Error));
            throw;
        }
    }

    ///
    /// Summary:
    ///     Applies the same trace and outcome logging to Redis operations that do not return a value.
    ///
    /// Logic:
    ///     Wraps the action in the generic logging helper and awaits it before completing.
    ///
    /// Parameters:
    ///   source:
    ///     The infrastructure source name produced from LogEventEnum.
    ///   operation:
    ///     The operation name produced from LogEventEnum.
    ///   key:
    ///     The logical cache key included in the event.
    ///   action:
    ///     The asynchronous Redis operation to execute.
    ///
    /// Returns:
    ///     A task that completes with the supplied operation and propagates its failures.
    ///
    private static Task ExecuteLoggedAsync(
        string source,
        string operation,
        string key,
        Func<Task> action
    ) =>
        ExecuteLoggedAsync(
            source,
            operation,
            key,
            async () =>
            {
                await action();
                return true;
            }
        );

    ///
    /// Summary:
    ///     Builds cache entry options with a sliding-from-now absolute expiration.
    ///
    /// Parameters:
    ///   expireTime:
    ///     The expiration in minutes; defaults to EXPIRE_TIME_MINUTES.
    ///
    /// Returns:
    ///     Distributed cache options with AbsoluteExpirationRelativeToNow set.
    ///
    private static DistributedCacheEntryOptions BuildOptions(
        int expireTime = EXPIRE_TIME_MINUTES
    ) =>
        new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expireTime),
        };
}
