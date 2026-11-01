namespace Wks.Mains;

///
/// Summary:
///     Configures MVC controllers with JSON serialization and exception handling.
///     Feature: (Newtonsoft.Json with reference loop handling and global exception filter)
///
/// -> Provides JSON serialization with circular reference handling
///     and centralized exception management for API responses.
///
public static class JsonSerializationExtension
{
    ///
    /// Summary:
    ///     Registers MVC controllers with Newtonsoft.Json and exception filter.
    ///     Feature: (Controller services with JSON settings and ExceptionFilter)
    ///
    /// -> Configures API controllers with JSON serialization settings
    ///     to handle reference loops, null values, and UTC timestamps.
    ///
    /// Parameters:
    ///   services:
    ///     The IServiceCollection instance used to register MVC services.
    ///
    public static void JsonSerializationConfigure(this IServiceCollection services)
    {
        ///
        /// Summary:
        ///     Add MVC controllers with maximum buffer limit and exception filter
        ///     Feature: (Controller registration with IAsyncEnumerable buffer and error handling)
        ///
        /// -> Enables MVC controllers with unlimited async enumerable buffering
        ///     and global exception filter for consistent error responses.
        ///
        services.AddControllers(options =>
        {
            options.MaxIAsyncEnumerableBufferLimit = int.MaxValue;
            options.Filters.Add<ExceptionFilter>();
        });

        ///
        /// Summary:
        ///     Add Newtonsoft.Json serialization with reference loop handling
        ///     Feature: (JSON serializer settings for UTC time, reference loops, and nulls)
        ///
        /// -> Configures JSON responses to use UTC timestamps, ignore circular
        ///     references in object graphs, and omit null values from output.
        ///
        services
            .AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
    }
}
