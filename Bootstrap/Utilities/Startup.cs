using StackExchange.Redis;

namespace Wks.Mains;

///
/// Summary:
///     Configures application services, middleware, and infrastructure dependencies.
///     Feature: (Centralized service registration orchestrator)
///
/// -> Provides single entry point for configuring all application services
///     including database, authentication, caching, messaging, and documentation.
///
public class Startup
{
    public static async Task Configure(WebApplicationBuilder builder)
    {
        ///
        /// Summary:
        ///     Configures Cross-Origin Resource Sharing (CORS) policy for API access.
        ///
        builder.Services.CorsConfigure();

        ///
        /// Summary:
        ///     Configures Entity Framework Core database context with PostgreSQL provider.
        ///
        builder.Services.DatabaseConfigure();

        ///
        /// Summary:
        ///     Configures MVC controllers with JSON serialization and exception handling.
        ///
        builder.Services.JsonSerializationConfigure();

        ///
        /// Summary:
        ///     Configures JWT Bearer authentication for API security.
        ///
        builder.Services.AuthenticationConfigure();

        ///
        /// Summary:
        ///     Configures ASP.NET Core Data Protection services for encryption.
        ///
        builder.Services.SecurityConfigure();

        ///
        /// Summary:
        ///     Configures MinIO object storage client for file management.
        ///
        builder.Services.StorageConfigure();

        ///
        /// Summary:
        ///     Configures Kestrel web server and form options for large file handling.
        ///
        builder.ServerConfigure();

        ///
        /// Summary:
        ///     Configures logging with warning level filter for Microsoft namespaces
        ///     to reduce log noise while maintaining important warnings.
        ///
        builder.Services.AddLogging(options =>
            options.AddFilter(nameof(Microsoft), LogLevel.Warning)
        );

        ///
        /// Summary:
        ///     Configures SignalR real-time communication with Redis backplane.
        ///
        builder.Services.SignalRConfigure();

        ///
        /// Summary:
        ///     Configures Swagger/OpenAPI documentation for API exploration.
        ///
        builder.Services.SwaggerConfigure();

        ///
        /// Summary:
        ///     Configures Redis distributed cache and connection multiplexer services.
        ///
        builder.Services.RedisConfigure();

        ///
        /// Summary:
        ///     Configures SignalR socket services with Redis pub/sub for real-time communication
        ///     across multiple server instances.
        ///
        builder.Services.SocketConfigure();

        ///
        /// Summary:
        ///     Configures Hangfire background job processing with Redis storage.
        ///
        builder.Services.AddHangfire();

        ///
        /// Summary:
        ///     Adds API explorer services for endpoint discovery and documentation
        ///     generation in ASP.NET Core applications.
        ///
        builder.Services.AddEndpointsApiExplorer();

        ///
        /// Summary:
        ///     Adds MVC controller services for handling HTTP requests and responses
        ///     in the web application.
        ///
        builder.Services.AddControllers(options =>
            options.Conventions.Add(new DuplicateRouteParamConvention())
        );

        ///
        /// Summary:
        ///     Adds HTTP context accessor for accessing current HTTP context
        ///     in dependency injection services and middleware.
        ///
        builder.Services.AddHttpContextAccessor();

        ///
        /// Summary:
        ///     Adds in-memory caching service for temporary data storage
        ///     and performance optimization.
        ///
        builder.Services.AddMemoryCache();

        ///
        /// Summary:
        ///     Registers all repository implementations with dependency injection container
        ///     for data access layer services.
        ///
        Repositories.Injection(builder.Services);

        ///
        /// Summary:
        ///     Registers all business service implementations with dependency injection container
        ///     for application business logic services.
        ///
        Services.Injection(builder.Services);

        ///
        /// Summary:
        ///     Configures RabbitMQ message queue system for asynchronous message processing
        ///     and inter-service communication.
        ///
        builder.Services.AddRabbitMqMassTransit(RabbitMQ.Configure, Runtime.QueueHost.Env());
    }
}
