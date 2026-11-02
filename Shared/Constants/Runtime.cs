namespace Shared;

///
/// Summary:
///     Defines environment configuration keys grouped by service responsibility.
///
public enum Runtime
{
    ///
    /// Summary:
    ///     Application hosting and request configuration.
    ///
    /// Parameters:
    ///     Host:
    ///         HTTP host binding used when starting the application server.
    ///
    ///     Mode:
    ///         Deployment mode used to select environment-specific behavior and prefixes.
    ///
    ///     BackendApi:
    ///         Public backend API address used when constructing service URLs.
    ///
    Host,
    Mode,
    BackendApi,

    ///
    /// Summary:
    ///     Database configuration.
    ///
    /// Parameters:
    ///     Database:
    ///         Database connection configuration consumed by the persistence provider.
    ///
    Database,

    ///
    /// Summary:
    ///     Application hosting and request configuration.
    ///
    /// Parameters:
    ///     Prefix:
    ///         Application prefix used by configured infrastructure integrations.
    ///
    ///     Timezone:
    ///         IANA business timezone used by date and time calculations.
    ///
    Prefix,
    Timezone,

    ///
    /// Summary:
    ///     Real-time transport.
    ///
    /// Parameters:
    ///     HubPath:
    ///         HTTP route where the SignalR hub accepts connections.
    ///
    HubPath,

    ///
    /// Summary:
    ///     Database configuration.
    ///
    /// Parameters:
    ///     EnableLegacyTimestampBehavior:
    ///         Npgsql compatibility switch name used when configuring timestamp handling.
    ///
    EnableLegacyTimestampBehavior,

    ///
    /// Summary:
    ///     Application hosting and request configuration.
    ///
    /// Parameters:
    ///     Origins:
    ///         Allowed origins supplied to the CORS configuration.
    ///
    Origins,

    ///
    /// Summary:
    ///     Request logging.
    ///
    /// Parameters:
    ///     LogIndexName:
    ///         Index name identifying application records in the logging service.
    ///
    ///     MaxRequestBodyLogSize:
    ///         Maximum request-body size captured by request logging.
    ///
    LogIndexName,
    MaxRequestBodyLogSize,

    ///
    /// Summary:
    ///     Application hosting and request configuration.
    ///
    /// Parameters:
    ///     MaxRequestSize:
    ///         Maximum request size accepted by the configured HTTP server.
    ///
    MaxRequestSize,

    ///
    /// Summary:
    ///     Application performance monitoring.
    ///
    /// Parameters:
    ///     ApmHost:
    ///         Endpoint of the application performance monitoring service.
    ///
    ///     ApmServiceName:
    ///         Service identifier attached to performance monitoring records.
    ///
    ///     ApmCenterConfig:
    ///         Configuration for retrieving centralized performance monitoring settings.
    ///
    ///     ApmSecretToken:
    ///         Credential used to authenticate performance monitoring requests.
    ///
    ///     ApmVersion:
    ///         Application version reported to the performance monitoring service.
    ///
    ApmHost,
    ApmServiceName,
    ApmCenterConfig,
    ApmSecretToken,
    ApmVersion,

    ///
    /// Summary:
    ///     Message transport and publisher limits.
    ///
    /// Parameters:
    ///     QueueHost:
    ///         Message broker connection configuration.
    ///
    ///     QueuePublisherCapacity:
    ///         Capacity limit for pending publisher work.
    ///
    ///     QueuePublisherConcurrency:
    ///         Maximum concurrent publisher operations.
    ///
    ///     QueuePublisherTimeoutSeconds:
    ///         Publisher operation timeout expressed in seconds.
    ///
    ///     QueuePublisherMaxRetryDelaySeconds:
    ///         Upper retry-delay bound for publisher operations, expressed in seconds.
    ///
    QueueHost,
    QueuePublisherCapacity,
    QueuePublisherConcurrency,
    QueuePublisherTimeoutSeconds,
    QueuePublisherMaxRetryDelaySeconds,

    ///
    /// Summary:
    ///     Background processing switches.
    ///
    /// Parameters:
    ///     WithConsumer:
    ///         Switch controlling whether message consumers are hosted.
    ///
    ///     WithJob:
    ///         Switch controlling whether background job processing is hosted.
    ///
    ///     PrefixJob:
    ///         Prefix identifying application background jobs.
    ///
    WithConsumer,
    WithJob,
    PrefixJob,

    ///
    /// Summary:
    ///     Background job hosting and access.
    ///
    /// Parameters:
    ///     HangfirePrefix:
    ///         Storage namespace used by Hangfire background processing.
    ///
    HangfirePrefix,

    ///
    /// Summary:
    ///     Real-time transport.
    ///
    /// Parameters:
    ///     WithSocket:
    ///         Switch controlling the socket integration.
    ///
    ///     SocketChannel:
    ///         Pub/sub channel used to distribute socket messages.
    ///
    WithSocket,
    SocketChannel,

    ///
    /// Summary:
    ///     Cache connection.
    ///
    /// Parameters:
    ///     Redis:
    ///         Redis connection configuration for caching and related integrations.
    ///
    Redis,

    ///
    /// Summary:
    ///     Media storage and transfer limits.
    ///
    /// Parameters:
    ///     SendingPathSize:
    ///         Configured transfer size used by the sending pipeline.
    ///
    SendingPathSize,

    ///
    /// Summary:
    ///     Application hosting and request configuration.
    ///
    /// Parameters:
    ///     Api:
    ///         API configuration value consumed by service integrations.
    ///
    Api,

    ///
    /// Summary:
    ///     Object storage configuration.
    ///
    /// Parameters:
    ///     StorageHost:
    ///         Object storage service endpoint.
    ///
    ///     StorageUseSsl:
    ///         Storage use ssl configuration read from the environment variable with this exact name.
    ///
    ///     StorageBucketName:
    ///         Bucket containing application objects.
    ///
    ///     StorageAccess:
    ///         Access identifier used to authenticate object storage operations.
    ///
    StorageHost,
    StorageUseSsl,
    StorageBucketName,
    StorageAccess,

    ///
    /// Summary:
    ///     Media storage and transfer limits.
    ///
    /// Parameters:
    ///     PresignedExpired:
    ///         Configured lifetime of generated presigned storage URLs.
    ///
    PresignedExpired,

    ///
    /// Summary:
    ///     Object storage configuration.
    ///
    /// Parameters:
    ///     StorageSecret:
    ///         Secret credential used to authenticate object storage operations.
    ///
    StorageSecret,

    ///
    /// Summary:
    ///     Authentication and token configuration.
    ///
    /// Parameters:
    ///     Issuer:
    ///         Expected issuer used when signing and validating access tokens.
    ///
    ///     Audience:
    ///         Expected audience used when signing and validating access tokens.
    ///
    ///     JwtKey:
    ///         Symmetric signing key used for access-token signatures.
    ///
    ///     JwtExpire:
    ///         Access-token lifetime expressed in minutes.
    ///
    ///     RefreshTokenExpired:
    ///         Refresh-token lifetime expressed in hours.
    ///
    Issuer,
    Audience,
    JwtKey,
    JwtExpire,
    RefreshTokenExpired,

    ///
    /// Summary:
    ///     Media storage and transfer limits.
    ///
    /// Parameters:
    ///     Media:
    ///         Media path configuration used to construct asset addresses.
    ///
    ///     Static:
    ///         Static asset path configuration.
    ///
    Media,
    Static,

    ///
    /// Summary:
    ///     Database configuration.
    ///
    /// Parameters:
    ///     Migration:
    ///         Configuration used by the database migration workflow.
    ///
    Migration,

    ///
    /// Summary:
    ///     API documentation.
    ///
    /// Parameters:
    ///     Swagger:
    ///         Swagger document endpoint supplied to the API documentation UI.
    ///
    ///     SwaggerLabel:
    ///         Display label used by API documentation.
    ///
    ///     RouterPrefix:
    ///         Route prefix hosting the API documentation UI.
    ///
    ///     VersionSwagger:
    ///         Version identifier used by API documentation.
    ///
    ///     SwaggerNameReplace:
    ///         Source text matched by documentation name replacement.
    ///
    ///     SwaggerNameTo:
    ///         Replacement text used for documentation names.
    ///
    Swagger,
    SwaggerLabel,
    RouterPrefix,
    VersionSwagger,
    SwaggerNameReplace,
    SwaggerNameTo,

    ///
    /// Summary:
    ///     Authentication and token configuration.
    ///
    /// Parameters:
    ///     TokenScheme:
    ///         Authentication scheme identifier shown by API documentation.
    ///
    TokenScheme,

    ///
    /// Summary:
    ///     Address metadata sources.
    ///
    /// Parameters:
    ///     AddressProvince:
    ///         Source configuration for province reference data.
    ///
    ///     AddressDistrict:
    ///         Source configuration for district reference data.
    ///
    ///     AddressWard:
    ///         Source configuration for ward reference data.
    ///
    AddressProvince,
    AddressDistrict,
    AddressWard,

    ///
    /// Summary:
    ///     Authentication and token configuration.
    ///
    /// Parameters:
    ///     Authorization:
    ///         Authorization configuration consumed by authentication integrations.
    ///
    Authorization,

    ///
    /// Summary:
    ///     Google authorization and API integration.
    ///
    /// Parameters:
    ///     GoogleAuthorizationHost:
    ///         Google authorization endpoint used to initiate sign-in.
    ///
    ///     GoogleOauthHost:
    ///         Google OAuth endpoint used during credential exchange.
    ///
    ///     GoogleClientId:
    ///         Client identifier registered for Google OAuth.
    ///
    ///     GoogleSecretId:
    ///         Client secret registered for Google OAuth.
    ///
    ///     GoogleApiHost:
    ///         Base endpoint of the Google API integration.
    ///
    ///     GoogleRedirectUri:
    ///         Callback URI registered for Google authorization.
    ///
    ///     GoogleGrandType:
    ///         Grant type supplied by the Google OAuth exchange.
    ///
    GoogleAuthorizationHost,
    GoogleOauthHost,
    GoogleClientId,
    GoogleSecretId,
    GoogleApiHost,
    GoogleRedirectUri,
    GoogleGrandType,

    ///
    /// Summary:
    ///     Gateway integration.
    ///
    /// Parameters:
    ///     GatewayConnector:
    ///         Gateway connection configuration used by gateway adapters.
    ///
    GatewayConnector,

    ///
    /// Summary:
    ///     Connection history storage.
    ///
    /// Parameters:
    ///     HistoryConnect:
    ///         Connection configuration used by history storage.
    ///
    ///     HistoryDirectory:
    ///         Directory configuration used for stored history.
    ///
    HistoryConnect,
    HistoryDirectory,

    ///
    /// Summary:
    ///     Media storage and transfer limits.
    ///
    /// Parameters:
    ///     SizeStorage:
    ///         Configured storage-size allowance or limit.
    ///
    SizeStorage,

    ///
    /// Summary:
    ///     Email delivery.
    ///
    /// Parameters:
    ///     MaiUserName:
    ///         Legacy email username configuration key retained for existing consumers.
    ///
    ///     MailApiKey:
    ///         Credential used by the email delivery API.
    ///
    ///     MailFrom:
    ///         Sender address used for outgoing email.
    ///
    ///     MailDomain:
    ///         Domain configured for email delivery.
    ///
    ///     MailgunApi:
    ///         Endpoint of the Mailgun email delivery integration.
    ///
    ///     MailUserName:
    ///         Username configured for the email delivery integration.
    ///
    MaiUserName,
    MailApiKey,
    MailFrom,
    MailDomain,
    MailgunApi,
    MailUserName,

    ///
    /// Summary:
    ///     Telegram integration.
    ///
    /// Parameters:
    ///     TelegramBotUri:
    ///         Base URI used for Telegram bot API requests.
    ///
    TelegramBotUri,

    ///
    /// Summary:
    ///     Zalo authorization and API integration.
    ///
    /// Parameters:
    ///     ZaloApi:
    ///         API address configured for the Zalo integration.
    ///
    ///     ZaloAuthorizationHost:
    ///         Endpoint used to initiate Zalo authorization.
    ///
    ///     ZaloOauthHost:
    ///         Endpoint used for Zalo OAuth credential exchange.
    ///
    ///     ZaloApiHost:
    ///         Base address used for Zalo API requests.
    ///
    ///     ZaloAppId:
    ///         Application identifier registered with Zalo.
    ///
    ///     ZaloSecretKey:
    ///         Secret credential registered with Zalo.
    ///
    ///     ZaloRedirectUri:
    ///         Callback URI registered for Zalo authorization.
    ///
    ///     ZaloAuthorizationCodeGrantType:
    ///         Grant type used to exchange a Zalo authorization code.
    ///
    ///     ZaloRefreshTokenGrantType:
    ///         Grant type used to renew Zalo credentials.
    ///
    ZaloApi,
    ZaloAuthorizationHost,
    ZaloOauthHost,
    ZaloApiHost,
    ZaloAppId,
    ZaloSecretKey,
    ZaloRedirectUri,
    ZaloAuthorizationCodeGrantType,
    ZaloRefreshTokenGrantType,

    ///
    /// Summary:
    ///     Intelligence service configuration.
    ///
    /// Parameters:
    ///     IntelligenceApi:
    ///         Endpoint of the intelligence service.
    ///
    ///     IntelligenceApiKey:
    ///         Credential used for intelligence service requests.
    ///
    ///     IntelligenceModel:
    ///         Model identifier supplied to the intelligence service.
    ///
    ///     IntelligenceRole:
    ///         Role configuration supplied to intelligence requests.
    ///
    IntelligenceApi,
    IntelligenceApiKey,
    IntelligenceModel,
    IntelligenceRole,

    ///
    /// Summary:
    ///     Administration access.
    ///
    /// Parameters:
    ///     AdminRequiredPass:
    ///         Required password configuration for protected administrator operations.
    ///
    AdminRequiredPass,

    ///
    /// Summary:
    ///     Background job hosting and access.
    ///
    /// Parameters:
    ///     HangfireUsername:
    ///         Username accepted by the Hangfire dashboard authentication.
    ///
    ///     HangfirePassword:
    ///         Password accepted by the Hangfire dashboard authentication.
    ///
    ///     HangfireScheme:
    ///         Authentication scheme configured for the Hangfire dashboard.
    ///
    HangfireUsername,
    HangfirePassword,
    HangfireScheme,
}
