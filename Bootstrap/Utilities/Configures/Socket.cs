namespace Wks.Mains;

///
/// Summary:
///     Configures SignalR WebSocket services with Redis backplane for real-time communication.
///     Feature: (SignalR hub and Redis pub/sub subscriber registration)
///
/// -> Provides real-time bidirectional communication between server and clients
///     using SignalR over WebSocket protocol with Redis message distribution.
///
public static class SocketExtension
{
    private static readonly bool IsActive = Runtime.WithSocket.Env().SafeBool();

    ///
    /// Summary:
    ///     Registers SignalR socket services and Redis subscriber for real-time communication.
    ///     Feature: (ISocket hub implementation and hosted Redis subscriber service)
    ///
    /// -> Enables WebSocket connections through SignalR hub and Redis pub/sub
    ///     for broadcasting messages across multiple server instances.
    ///
    /// Parameters:
    ///   services:
    ///     The IServiceCollection instance used to register socket services.
    ///
    public static void SocketConfigure(this IServiceCollection services)
    {
        ///
        /// Summary:
        ///     Skip socket configuration if disabled in environment settings
        ///     Feature: (Conditional socket service registration)
        ///
        /// -> Allows disabling real-time features when not needed
        ///     to reduce resource usage in non-interactive environments.
        ///
        services.AddScoped<ISocket, Socket>();

        if (IsActive is false)
            return;

        ///
        /// Summary:
        ///     Register SignalR connection hub for WebSocket communication
        ///     Feature: (ISocket service implementation for real-time messaging)
        ///
        /// -> Provides centralized SignalR hub service for sending real-time
        ///     notifications and updates to connected clients.
        ///
        services.AddScoped<ISocket, Socket>();

        ///
        /// Summary:
        ///     Register Redis subscriber as background hosted service
        ///     Feature: (RedisSocketSubscriber for Redis pub/sub message distribution)
        ///
        /// -> Enables message broadcasting across multiple server instances
        ///     by subscribing to Redis channels and forwarding to SignalR clients.
        ///
        services.AddHostedService<RedisSocketSubscriber>();
    }
}
