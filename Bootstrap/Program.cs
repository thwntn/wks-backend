namespace Wks.Mains;

///
/// Summary:
///      Entry point of the application.
///      Responsible for bootstrapping the system, configuring services,
///      building middleware pipeline, and running the web host.
///
public class Program
{
    ///
    /// Summary:
    ///      Main method to initialize and run the application.
    ///
    /// Parameters:
    ///      args:
    ///          Command-line arguments used for runtime configuration.
    ///
    /// Returns:
    ///      A Task representing the asynchronous execution.
    ///
    public static async Task Main(string[] args)
    {
        /// Logic:
        ///      Initialize the WebApplicationBuilder which acts as the foundation
        ///      for configuring services, dependency injection, logging, and app settings.
        ///      It also loads default configurations such as appsettings.json,
        ///      environment variables, and command-line arguments.
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        /// Logic:
        ///      Execute pre-start configuration logic.
        ///      Reader.ConfigureAsync(): Load and prepare external resources or environment-based configurations.
        ///      Startup.Configure(builder): Register services, dependencies, and core application modules into DI container.
        await Reader.ConfigureAsync();
        await Startup.Configure(builder);

        /// Logic:
        ///      Build the WebApplication instance from the configured builder.
        ///      This step finalizes the service container and prepares the middleware pipeline for execution.
        WebApplication app = builder.Build();

        /// Logic:
        ///      Register request logging outside the exception handler.
        ///      Records the final response status and retrieves handled exception details.
        app.UseMiddleware<GrafanaLogging>();

        /// Logic:
        ///      Configure centralized error handling for unhandled exceptions.
        ///      Converts failures into consistent error responses before request logging completes.
        Error.Configure(app);

        /// Logic:
        ///      Register default controller routing without a predefined template.
        ///      Allows attribute-based routing to take full control over endpoint resolution.
        app.MapControllerRoute(string.Empty, string.Empty);

        /// Logic:
        ///      Enable Cross-Origin Resource Sharing (CORS) using predefined policy.
        ///      This allows the API to be accessed securely from different domains as configured in PolicyNames.Cors.
        app.UseCors(nameof(PolicyNames.Cors));

        /// Logic:
        ///      Map SignalR hub endpoint for real-time communication.
        ///      Clients can connect to this hub path to receive live updates or push messages.
        app.MapHub<SignalrHub>(Runtime.HubPath.Env());

        /// Logic:
        ///      Enable Swagger middleware for API documentation and testing.
        ///      SwaggerEndpoint: Defines the JSON endpoint for API schema.
        ///      RoutePrefix: Controls the URL path where Swagger UI is exposed.
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint(Runtime.Swagger.Env(), string.Empty);
            options.RoutePrefix = Runtime.RouterPrefix.Env();
        });

        /// Logic:
        ///      Enable authentication and authorization middleware.
        ///      Authentication verifies the identity of incoming requests.
        ///      Authorization ensures the authenticated user has permission to access specific resources.
        app.UseAuthentication();
        app.UseAuthorization();

        /// Summary:
        ///      If the application is configured to run without consumer services
        app.UseHangfire();

        /// Logic:
        ///      Map controller endpoints under a dynamic prefix.
        ///      This is useful for versioning, multi-tenant routing, or environment-based API grouping.
        Api.MapControllers(app);
        Jobs.Injectable();

        /// Logic:
        ///      Output host and environment information for monitoring and debugging purposes.
        Logger.Worker();
        Host.Information();

        /// Logic:
        ///      Start the web server and listen on the configured host/port.
        ///      Runtime.Host.Env() allows dynamic binding (e.g., http://*:5000, specific domain, etc.).
        app.Run(Runtime.Host.Env());
    }
}
