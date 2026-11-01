namespace Wks.Mains;

///
/// Summary:
///     Provides helper methods for application controller routing configuration.
///     Controls whether controllers should be exposed based on runtime mode and consumer settings.
///
public class Api
{
    private static bool IsActive =>
        (
            Runtime.WithConsumer.Env().SafeBool() is false
            && Runtime.WithJob.Env().SafeBool() is false
        )
        || Runtime
            .Mode.Env()
            .Equals(nameof(SystemModeEnum.Development), StringComparison.OrdinalIgnoreCase);

    ///
    /// Summary:
    ///     Maps API controllers to the application route group when the application is active.
    ///     Ensures controllers are only mapped in non-consumer modes or development mode.
    ///
    /// Parameters:
    ///   app:
    ///     The WebApplication instance to map controllers on.
    ///
    public static void MapControllers(WebApplication app)
    {
        /// Summary:
        ///   If the application is configured to run without consumer services, map controllers directly to the root path.
        ///   This allows for a simpler routing structure when consumer-based features are not needed, while still supporting attribute-based routing for all controllers.
        if (IsActive)
            app.MapGroup(Runtime.Prefix.Env()).MapControllers();
    }
}
