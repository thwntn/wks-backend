using StackExchange.Redis;

namespace Wks.Mains;

///
/// Summary:
///     Configures SignalR real-time communication with Redis backplane.
///     Feature: (SignalR hub with Newtonsoft.Json and Redis scale-out)
///
/// -> Provides WebSocket-based real-time messaging with Redis pub/sub
///     for distributing messages across multiple server instances.
///
public static class SignalRExtension
{
    ///
    /// Summary:
    ///     Registers SignalR services with JSON protocol and Redis backplane.
    ///     Feature: (SignalR with Newtonsoft.Json serialization and Redis scale-out)
    ///
    /// -> Enables real-time bidirectional communication between server and clients
    ///     with Redis-based message distribution for horizontal scaling.
    ///
    /// Parameters:
    ///   services:
    ///     The IServiceCollection instance used to register SignalR services.
    ///
    public static void SignalRConfigure(this IServiceCollection services)
    {
        ///
        /// Summary:
        ///     Add SignalR with Newtonsoft.Json protocol and Redis backplane
        ///     Feature: (SignalR hub registration with JSON serialization and Redis scale-out)
        ///
        /// -> Configures SignalR to use Newtonsoft.Json for message serialization
        ///     and Redis pub/sub with channel prefix for multi-instance messaging.
        ///
        services
            .AddSignalR()
            .AddNewtonsoftJsonProtocol()
            .AddStackExchangeRedis(
                Runtime.Redis.Env(),
                options =>
                    options.Configuration.ChannelPrefix = RedisChannel.Literal(
                        Runtime.HubPath.Env()
                    )
            );
    }
}
