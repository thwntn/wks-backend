namespace Wks.Mains;

///
/// Summary:
///     Configures JWT Bearer authentication for API security.
///     Feature: (JWT token validation with issuer, audience, and signing key verification)
///
/// -> Provides secure token-based authentication for API endpoints
///     with comprehensive validation rules and zero clock skew.
///
public static class AuthenticationExtension
{
    ///
    /// Summary:
    ///     Registers JWT Bearer authentication services with validation parameters.
    ///     Feature: (JwtBearer scheme with symmetric key validation)
    ///
    /// -> Enables API security through JWT tokens with strict validation
    ///     of issuer, audience, lifetime, and cryptographic signature.
    ///
    /// Parameters:
    ///   services:
    ///     The IServiceCollection instance used to register authentication services.
    ///
    public static void AuthenticationConfigure(this IServiceCollection services)
    {
        ///
        /// Summary:
        ///     Add JWT Bearer authentication with default scheme configuration
        ///     Feature: (Authentication, challenge, and default scheme setup)
        ///
        /// -> Sets JwtBearer as the default authentication mechanism
        ///     for all protected API endpoints in the application.
        ///
        services
            .AddAuthentication(configs =>
            {
                configs.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                configs.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                configs.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(configs =>
            {
                ///
                /// Summary:
                ///     Configure JWT token validation parameters
                ///     Feature: (Issuer, audience, signing key, and lifetime validation)
                ///
                /// -> Validates all JWT token claims including issuer and audience
                ///     using symmetric HMAC key with zero clock skew tolerance.
                ///
                configs.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidIssuer = Runtime.Issuer.Env(),
                        ValidAudience = Runtime.Audience.Env(),
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Runtime.JwtKey.Env())
                        ),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                    };
            });

        ///
        /// Summary:
        ///     Register permission-based authorization services.
        ///     Feature: (Dynamic policy provider and permission requirement handler)
        ///
        /// -> Resolves PermissionAttribute policies into PermissionRequirement
        ///     and evaluates them through PermissionHandler against account permissions.
        ///
        services.AddAuthorization();
        services.AddSingleton<
            IAuthorizationPolicyProvider,
            PermissionPolicyProvider
        >();
        services.AddScoped<IAuthorizationHandler, PermissionHandler>();
    }
}
