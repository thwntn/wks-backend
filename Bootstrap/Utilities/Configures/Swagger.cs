namespace Wks.Mains;

///
/// Summary:
///     Configures Swagger/OpenAPI documentation for API exploration.
///     Feature: (Swagger UI with JWT authentication and schema customization)
///
/// -> Provides interactive API documentation with authentication support
///     and customized schema naming for better developer experience.
///
public static class SwaggerExtension
{
    ///
    /// Summary:
    ///     Registers Swagger documentation generator with security and schema configuration.
    ///     Feature: (SwaggerGen with JWT Bearer auth and custom schema IDs)
    ///
    /// -> Generates OpenAPI specification with JWT authentication UI
    ///     and simplified type names for clear API documentation.
    ///
    /// Parameters:
    ///   services:
    ///     The IServiceCollection instance used to register Swagger services.
    ///
    public static void SwaggerConfigure(this IServiceCollection services)
    {
        ///
        /// Summary:
        ///     Add Swagger documentation generator with comprehensive configuration
        ///     Feature: (Server URL, API version, security scheme, and schema customization)
        ///
        /// -> Configures Swagger UI with server base URL, JWT Bearer authentication,
        ///     and customized schema naming for improved API documentation clarity.
        ///
        services.AddSwaggerGen(static options =>
        {
            ///
            /// Summary:
            ///     Add server configuration with base URL from environment
            ///     Feature: (Swagger server URL for API endpoint documentation)
            ///
            /// -> Displays correct API endpoint URL in Swagger UI
            ///     based on configured application prefix path.
            ///
            options.AddServer(new() { Url = Runtime.Prefix.Env() });

            ///
            /// Summary:
            ///     Configure Swagger document with version and title
            ///     Feature: (API version and documentation title from environment)
            ///
            /// -> Sets API version identifier and display title
            ///     for Swagger documentation header and metadata.
            ///
            options.SwaggerDoc(
                Runtime.VersionSwagger.Env(),
                new() { Title = Runtime.SwaggerLabel.Env() }
            );

            ///
            /// Summary:
            ///     Customize schema IDs by replacing namespace prefixes
            ///     Feature: (Schema name transformation for cleaner documentation)
            ///
            /// -> Simplifies schema names by removing verbose namespace parts
            ///     for more readable type names in Swagger UI.
            ///
            options.CustomSchemaIds(scheme =>
                scheme.FullName.Replace(
                    Runtime.SwaggerNameReplace.Env(),
                    Runtime.SwaggerNameTo.Env()
                )
            );

            ///
            /// Summary:
            ///     Add JWT Bearer security definition for authentication
            ///     Feature: (Security scheme configuration for JWT tokens)
            ///
            /// -> Enables "Authorize" button in Swagger UI for testing
            ///     authenticated endpoints with Bearer token input.
            ///
            options.AddSecurityDefinition(
                Runtime.TokenScheme.Env(),
                new OpenApiSecurityScheme
                {
                    Name = Runtime.Authorization.Env(),
                    In = ParameterLocation.Header,
                    Scheme = Runtime.TokenScheme.Env(),
                    Type = SecuritySchemeType.Http,
                }
            );

            ///
            /// Summary:
            ///     Add global security requirement for all endpoints
            ///     Feature: (Apply JWT authentication requirement to all operations)
            ///
            /// -> Marks all API endpoints as requiring authentication
            ///     in Swagger documentation with lock icon indicators.
            ///
            options.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference(Runtime.TokenScheme.Env(), doc)] = [],
            });
        });
    }
}
