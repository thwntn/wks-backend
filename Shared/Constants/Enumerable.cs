namespace Shared;

public static class PrintJobConstant
{
    public const HttpStatus CONFLICT = (HttpStatus)409;
}

public enum PrintJobTypeEnum
{
    WAREHOUSE = 0,
    ORDER = 1,
    INVOICE = 2,
}

public enum PrintJobStatusEnum
{
    PENDING = 0,
    PROCESSING = 1,
    COMPLETED = 2,
    FAILED = 3,
    CANCELLED = 4,
}

///
/// Summary:
///     Defines the application runtime environment mode.
///
public enum SystemModeEnum
{
    ///
    /// Summary:
    ///     Development environment for local testing and debugging.
    ///
    Development,

    ///
    /// Summary:
    ///     Production environment for live deployment.
    ///
    Production,
}

///
/// Summary:
///     Defines WebSocket subscriber channel types for real-time communication.
///
public enum SocketSubscriberChannelEnum { }

///
/// Summary:
///     Defines runtime configuration keys for environment variables and application settings.
///     Used to access configuration values throughout the application.
///
public enum Runtime
{
    Host,
    Mode,
    BackendApi,
    Database,
    Prefix,
    Timezone,
    HubPath,
    EnableLegacyTimestampBehavior,
    Origins,
    LogIndexName,
    MaxRequestBodyLogSize,
    MaxRequestSize,
    ApmHost,
    ApmServiceName,
    ApmCenterConfig,
    ApmSecretToken,
    ApmVersion,
    QueueHost,
    QueuePublisherCapacity,
    QueuePublisherConcurrency,
    QueuePublisherTimeoutSeconds,
    QueuePublisherMaxRetryDelaySeconds,
    WithConsumer,
    WithJob,
    PrefixJob,
    HangfirePrefix,
    WithSocket,
    SocketChannel,
    Redis,
    SendingPathSize,
    Api,
    StorageHost,
    StorageUseSsl,
    StorageBucketName,
    StorageAccess,
    PresignedExpired,
    StorageSecret,
    Issuer,
    Audience,
    JwtKey,
    JwtExpire,
    RefreshTokenExpired,
    Media,
    Static,
    Migration,
    Swagger,
    SwaggerLabel,
    RouterPrefix,
    VersionSwagger,
    SwaggerNameReplace,
    SwaggerNameTo,
    TokenScheme,
    AddressProvince,
    AddressDistrict,
    AddressWard,
    Authorization,
    GoogleAuthorizationHost,
    GoogleOauthHost,
    GoogleClientId,
    GoogleSecretId,
    GoogleApiHost,
    GoogleRedirectUri,
    GoogleGrandType,
    GatewayConnector,
    HistoryConnect,
    HistoryDirectory,
    SizeStorage,
    MaiUserName,
    MailApiKey,
    MailFrom,
    MailDomain,
    MailgunApi,
    MailUserName,
    TelegramBotUri,
    ZaloApi,
    ZaloAuthorizationHost,
    ZaloOauthHost,
    ZaloApiHost,
    ZaloAppId,
    ZaloSecretKey,
    ZaloRedirectUri,
    ZaloAuthorizationCodeGrantType,
    ZaloRefreshTokenGrantType,
    IntelligenceApi,
    IntelligenceApiKey,
    IntelligenceModel,
    IntelligenceRole,
    AdminRequiredPass,
    HangfireUsername,
    HangfirePassword,
    HangfireScheme,
}

///
/// Summary:
///     Defines keys for temporary logging state stored in a job execution context.
///     Use nameof when storing and retrieving each value so both lifecycle callbacks share the same key.
///     These keys identify per-execution objects and are not persisted Hangfire job parameters.
///
public enum LogContextEnum
{
    ///
    /// Summary:
    ///     Identifies the disposable scope created before job execution.
    ///     The completion callback disposes this scope to restore the previous asynchronous trace.
    ///
    TraceScope,

    ///
    /// Summary:
    ///     Identifies the stopwatch started before job execution.
    ///     The completion callback reads its elapsed time for the execution outcome log.
    ///
    Stopwatch,
}

///
/// Summary:
///     Defines the shared vocabulary for infrastructure logging sources, operations, and outcomes.
///     Use nameof for event fields and diagnostic filter names instead of inline string literals.
///     Member names are the emitted log values and should remain consistent across producers and consumers.
///
public enum LogEventEnum
{
    ///
    /// Summary:
    ///     Outgoing HTTP operations performed through the shared IRequest service.
    ///
    Http,

    ///
    /// Summary:
    ///     RabbitMQ message transport events.
    ///
    RabbitMQ,

    ///
    /// Summary:
    ///     Redis cache and publication events.
    ///
    Redis,

    ///
    /// Summary:
    ///     Hangfire job creation and execution events.
    ///
    Hangfire,

    ///
    /// Summary:
    ///     Quartz scheduled job execution events.
    ///
    Quartz,

    ///
    /// Summary:
    ///     Processing an incoming message through the consumer pipeline.
    ///
    Consume,

    ///
    /// Summary:
    ///     Publishing a message through the broker pipeline or Redis channel.
    ///
    Publish,

    ///
    /// Summary:
    ///     Sending a message through the MassTransit send pipeline.
    ///
    Send,

    ///
    /// Summary:
    ///     Creating a persisted background job.
    ///
    Enqueue,

    ///
    /// Summary:
    ///     Reading an entry from Redis.
    ///
    Get,

    ///
    /// Summary:
    ///     Writing or replacing an entry in Redis.
    ///
    Set,

    ///
    /// Summary:
    ///     Writing a Redis entry only when its key does not already exist.
    ///
    SetIfNotExists,

    ///
    /// Summary:
    ///     Removing a Redis entry.
    ///
    Delete,

    ///
    /// Summary:
    ///     The consumer pipeline has received a message before downstream processing.
    ///
    Received,

    ///
    /// Summary:
    ///     The wrapped operation finished without an exception.
    ///
    Completed,

    ///
    /// Summary:
    ///     The wrapped operation raised an exception.
    ///
    Failed,

    ///
    /// Summary:
    ///     Hangfire successfully persisted the background job.
    ///
    Created,

    ///
    /// Summary:
    ///     Execution has begun before the job business work runs.
    ///
    Started,

    ///
    /// Summary:
    ///     A cache read returned no usable value.
    ///
    Miss,

    ///
    /// Summary:
    ///     A cache read returned a value that was successfully deserialized.
    ///
    Hit,
}

///
/// Summary:
///     Defines background job names for scheduled tasks and recurring operations.
///
public enum JobNames
{
    RedirectDailyTraffic,
    RedirectTemplateStatistic,
    AudienceMemberUpgrade,
    BroadcastDailyStatistic,
    RedirectCampaignStatus,
    AutomationClearNode,
    RedirectDeliveryClear,
    BannerExpiredStatus,
    ZaloCredentialRefreshToken,
    GoodsReceiptAutoComplete,
}

///
/// Summary:
///     Defines folder names for file storage organization.
///
public enum FolderNames
{
    Files,
    Anonymous,
    Static,
    Avatar,
    Broadcast,
    Products,
}

///
/// Summary:
///     Defines SignalR hub method names for real-time client-server communication.
///     Used for WebSocket message routing and event broadcasting.
///
public enum HubMethodName
{
    INITIALIZE,
    NOTIFICATION_TRIGGER,
    PING,
    PONG,
    ERROR,

    EXPENSE_TRANSACTION,
    EXPENSE,
    FUND,
    CASH,

    SYSTEM_PUSH,

    SIGN_LISTEN,
    REMOVE_LISTEN,
    UPDATE_MESSAGE,
    UPDATE_LIST_FILE,

    FEEDBACK,

    DATA,
    DATA_INFORMATION,
    TEMPLATE,
    MARKETING_BROADCAST,
    PROFILE_CONFIG,

    MARKETING_FILTER,
    MARKETING_CONTENT,
    MARKETING_AUTOMATION,

    PRODUCT,
    UPDATE_GROUP,
    SCANNER_PRODUCT,
    KANBAN_CATEGORY,

    CATEGORIES = 28,

    BRAND,
    BANNER,

    INVOICE_OPEN_TRANSACTION = 32,
    INVOICE_CLOSE_TRANSACTION,
    INVOICE_FINISH_TRANSACTION,

    SALE_PROGRAM,
    PROMOTION,
    DISCOUNT,
    CUSTOMER,
    MEMBERSHIP,
    AUDIENCE_CONSENT,
    AUDIENCE_CREDENTIAL,

    MAIL_CREDENTIAL,
    ZALO_CREDENTIAL,
    TELEGRAM_CREDENTIAL,
    WEB_PUSH_CREDENTIAL,

    FINANCIAL_TIMELINE,
    ORDER,
    ORDER_COMPLETED,

    DOMAIN,
    REDIRECT_URL,
    REDIRECT_CAMPAIGN,
    REDIRECT_TEMPLATE,
    REDIRECT_WEBHOOK,

    WAREHOUSE,
    WAREHOUSE_GOODS_RECEIPT,
    WAREHOUSE_GOODS_ISSUE,
    WAREHOUSE_STOCK_COUNT,
    WAREHOUSE_INVENTORY_ADJUSTMENT,
}

///
/// Summary:
///     Defines cache key names for Redis caching operations.
///     Used to store and retrieve cached data across the application.
///
public enum CacheNames
{
    AUTH_CONFIRM_COUNT_MESSAGE,
    AUTH_CONFIRM_PREVIOUS_TIME,
    AUTH_TEMPLATE_CONFIRM,
    AUTH_USER_SETUP,
    AUTH_PERMISSION,
    AUTH_ACTOR_ACCOUNT,
    PROFILE_SETTING,
    MEDIA_FILE,
    JWT_REFRESH_TOKEN,
    QUOTAS_COUNTER,
    BROADCAST_EMAIL,
    CUSTOMER_PROFILE,
    CUSTOMER_LOGIN,
    CUSTOMER_AUDIT_ACTION,
    SHOP_ORDER_GENERATE,
    SHOP_BANNER,
    SHOP_SETUP,
    SHOP_DOMAIN,
    REDIRECT_DOMAIN,
    REDIRECT_LINK,
    REDIRECT_ANALYST_ACCESS,
    REDIRECT_ANALYST_BROWSER,
    REDIRECT_STATUS_CAMPAIGN,
    REDIRECT_STATUS_TEMPLATE,
    REDIRECT_CAMPAIGN_EXPORTING,
    REDIRECT_CAMPAIGN_STATUS,
    REDIRECT_WEBHOOK_STATUS,
    REDIRECT_WEBHOOK_EVENT,
    REDIRECT_DELIVERY_STATUS,
    REDIRECT_WEBHOOK_ENDPOINT,
    MARKETING_DATA_TYPE,
    MARKETING_DATA_NAME,
    MARKETING_CONTENT_PUBLISH_STATUS,
    MARKETING_CONTACT_UNIQUE,
    MARKETING_TELEGRAM_CREDENTIAL,
    MARKETING_MAIL_CREDENTIAL,
    MARKETING_AUTOMATION_STATUS,
    MARKETING_AUTOMATION_TYPE,
    MARKETING_AUTOMATION_OPERATOR,
    MARKETING_AUTOMATION_LOOP,
    MARKETING_AUTOMATION_HTTP_METHOD,
    MARKETING_ZALO_CREDENTIAL,
    MARKETING_WEB_PUSH_CREDENTIAL,
    MARKETING_BROADCAST,
    MARKETING_BROADCAST_FORMAT,
    MARKETING_CONTENT,
    MARKETING_AUTOMATION,
    MARKETING_FILTER,
    MARKETING_DATA,
    REDIRECT_WEBHOOK,
    REDIRECT_TEMPLATE,
    REDIRECT_DELIVERY,
    BUSINESS_PRODUCT,
    BUSINESS_BRAND,
    BUSINESS_CATEGORY,
    BUSINESS_ORDER,
    BUSINESS_BANNER,
    FINANCIAL_TIMELINE,
    AUDIENCE_MEMBERSHIP,
    AUDIENCE_CONSENT,
    AUDIENCE_CONSENT_TYPE,
    DOMAIN_TYPE,
    WEBHOOK_TELEGRAM_USER,
    SYSTEM_ROLE_PERMISSION,
}

///
/// Summary:
///     Defines CORS policy names for cross-origin resource sharing configuration.
///
public enum PolicyNames
{
    Cors,
    AllowAll,
}

///
/// Summary:
///     Defines authentication account types supported by the system.
///
public enum AccountNames
{
    Basic = 1,
    Google = 2,
    Actor = 3,
}

///
/// Summary:
///     Defines user account status states for access control and verification.
///
public enum AccountStatus
{
    Open = 0,
    ValidateRequire = 1,
    Lock = 2,
}

///
/// Summary:
///     Defines user action types for audit logging and activity tracking.
///
public enum ActionNames
{
    LOGIN_WITH_PASSWORD,
    CHANGE_PASSWORD,
    SIGN_UP,
    INVOICE_CREATE,
    INVOICE_UPDATE,
    INVOICE_DELETE,
    EXPENSE_CREATE,
    EXPENSE_UPDATE,
    EXPENSE_DELETE,
    EXPENSE_TRANSACTION_CREATE,
    EXPENSE_TRANSACTION_DELETE,
    TIMELINE_CREATE,
    TIMELINE_UPDATE,
    TIMELINE_DELETE,
}

///
/// Summary:
///     Defines feedback status states for customer support ticket lifecycle.
///
public enum FeedbackStatusEnum
{
    Initial = 0,
    Read = 1,
    InProgress = 2,
    Resolve = 3,
}

///
/// Summary:
///     Defines storage item types for file system organization.
///
public enum StorageNames
{
    File,
    Folder,
}

///
/// Summary:
///     Defines storage item status for trash and normal states.
///
public enum StorageStatus
{
    Trash,
    Normal,
}

///
/// Summary:
///     Defines special folder identifiers for default and headless folders.
///
public enum DefaultFolder
{
    DefaultFolder = -1,
    HeadlessFolder = -2,
}

///
/// Summary:
///     Defines notification types for system-wide user notifications.
///
public enum NotificationNames
{
    DEFAULT = 1,
    REMOVE_FROM_GROUP = 2,
    INVITE_TO_GROUP = 3,
    UPLOADABLE = 4,
    EXPIRE_PLAN = 5,
    CREATED_EXPENSE_TRANSACTION = 6,
    EXPENSE_TRANSACTION_CREATE = 7,
    PLAN_REMINDER = 8,

    REDIRECT_EXPORT_CAMPAIGN = 9,
    REDIRECT_EXPORT_CAMPAIGN_FAILED = 10,
}

///
/// Summary:
///     Defines group invitation status for collaboration features.
///
public enum GroupInviteStatus
{
    Invited,
    Accept,
}

///
/// Summary:
///     Defines gender types for user profiles.
///
public enum GenderEnum
{
    ORTHER = 1,
    MALE = 2,
    FEMALE = 3,
}

///
/// Summary:
///     Defines payment methods for financial transactions.
///
public enum PaymentMethod
{
    Cash = 0,
    Credit = 1,
}

///
/// Summary:
///     Defines monetary units for currency representation.
///
public enum MonetaryUnit
{
    Vnd = 0,
    Usd = 1,
}

///
/// Summary:
///     Defines note status states for note management.
///
public enum StatusNote
{
    Default = 0,
    Archive = 1,
    Remove = 2,
}

///
/// Summary:
///     Defines discount status for promotion availability.
///
public enum DiscountStatus
{
    Active = 0,
    Suspend = 1,
}

///
/// Summary:
///     Defines cash transaction types for accounting operations.
///
public enum CashNames
{
    None = 0,
    Abstract = 1,
    Subtract = 2,
}

///
/// Summary:
///     Defines system-wide configuration setting keys.
///
public enum SettingNames
{
    APP_REDIRECT_DOMAIN,
    APP_SHOP_DOMAIN,
    DEFAULT_CUSTOMER,
    DEFAULT_DISCOUNT,
    DEFAULT_PAYMENT,
    DEFAULT_PERMISSION,

    PACKAGE_FREE,
    PACKAGE_PERSONAL,
    PACKAGE_BUSINESS,

    BANK_ID,
    BANK_ACCOUNT_NAME,
    BANK_ACCOUNT_NO,

    TELEGRAM_TOKEN,
    TELEGRAM_CHAT_ID,

    REDIRECT_TEMPLATE_MAX_ROWS,
    REDIRECT_WEBHOOK_MAX_RETRY,
    REDIRECT_DELIVERY_RETENTION_DAYS,
    REDIRECT_WEBHOOK_MAX_PER_PROFILE,
}

///
/// Summary:
///     Defines user-specific account configuration setting keys.
///
public enum AccountSettingNames
{
    // REDIRECT_DOMAIN,
}

///
/// Summary:
///     Defines expense transaction counter time periods.
///
public enum ExpenseTransactionCounterNames
{
    TODAY = 0,
    WEEK = 1,
    MONTH = 2,
}

///
/// Summary:
///     Defines invoice lifecycle status states.
///
public enum InvoiceStatus
{
    DRAFT = 0,
    WAITING = 1,
    ACTIVE = 2,
    DONE = 3,
    OWE_MONEY = 4,
}

///
/// Summary:
///     Defines sale program activation status.
///
public enum SaleProgramStatus
{
    NOT_ACTIVE = 0,
    ACTIVE = 1,
    OUT_OF_TIME_RANGE = 2,
}

///
/// Summary:
///     Defines transaction validation status for financial operations.
///
public enum StatusTransaction
{
    ACCEPT = 0,
    ERROR = 1,
    WARNING = 2,
}

///
/// Summary:
///     Defines discount time application types.
///
public enum DiscountTimeFrameType
{
    NORMAL = 0,
    TIME_FRAME = 1,
}

///
/// Summary:
///     Defines discount calculation methods.
///
public enum DiscountCode
{
    AMOUNT = 0,
    PERCENT = 1,
}

///
/// Summary:
///     Defines discount application scope.
///
public enum DiscountType
{
    NONE = -1,
    PRODUCT = 0,
    INVOICE = 1,
}

///
/// Summary:
///     Defines discount quantity-based rules.
///
public enum DiscountQuantityType
{
    NONE = 0,
    WITH_QUANTITY = 1,
}

///
/// Summary:
///     Defines promotion types for marketing campaigns.
///
public enum PromotionTypeEnum
{
    BUY_ONE_GET_ONE = 1,
    DISCOUNT_PERCENTAGE = 2,
    DISCOUNT_AMOUNT = 3,
    FREE_GIFT = 4,
    FREE_SHIPPING = 5,
    COMBO_PRODUCT_DISCOUNT = 6,
}

///
/// Summary:
///     Defines promotion application conditions.
///
public enum PromotionConditionEnum
{
    APPLY_TO_ALL = 1,
    AMOUNT_ORDER = 2,
    QUANTITY_PRODUCT_ORDER = 3,
    WEIGHT_ORDER = 4,
    SHIPPING_METHOD = 5,
}

///
/// Summary:
///     Defines promotion application scope.
///
public enum PromotionScopeEnum
{
    ALL = 1,
    CATEGORY = 2,
    PRODUCT = 3,
}

///
/// Summary:
///     Defines broadcast data source types.
///
public enum BroadcastDataTypeEnum
{
    ///
    /// Summary:
    ///     Import data from Excel file with automatic structure parsing.
    ///
    IMPORT_FROM_EXCEL = 0,

    ///
    /// Summary:
    ///     Synchronize data from existing customer records in CRM system.
    ///
    FROM_CUSTOMER = 1,

    ///
    /// Summary:
    ///     Synchronize data from Telegram webhook user interactions.
    ///
    FROM_TELEGRAM = 3,

    ///
    /// Summary:
    ///     Initial empty data source awaiting manual record entry.
    ///
    INITIAL = 4,

    ///
    /// Summary:
    ///     Synchronize data from existing order records of the current profile.
    ///
    FROM_ORDER = 5,

    ///
    /// Summary:
    ///     Synchronize data from browser Web Push subscriptions.
    ///
    FROM_WEB_PUSH = 6,
}

///
/// Summary:
///     Defines promotion product limit types.
///
public enum PromotionProductTypeEnum
{
    NO_LIMIT = 0,
    LIMIT = 1,
}

///
/// Summary:
///     Defines customer account status states.
///
public enum CustomerStatusEnum
{
    LOCK = 0,
    ACTIVE = 1,
    PENDING = 2,
}

///
/// Summary:
///     Defines the category of a consent/terms document presented to
///     customers. A shop may publish multiple consents under the same
///     category (e.g. several Sales Terms revisions).
///     Values:
///     OTHER:
///     Miscellaneous consent/terms not tied to a specific storefront
///     flow (e.g. general policies).
///     TERMS_OF_SERVICE:
///     Customer-facing terms of service, shown during account
///     registration.
///     SALES_TERMS:
///     Sales/purchase terms, shown during checkout.
///     POLICY:
///     General shop policies (e.g. privacy, warranty, shipping/return
///     policy documents).
///
public enum ConsentCategoryEnum
{
    OTHER = 0,
    TERMS_OF_SERVICE = 1,
    SALES_TERMS = 2,
    POLICY = 3,
}

///
/// Summary:
///     Defines whether a customer has granted or revoked a consent.
///
public enum ConsentStatusEnum
{
    GRANTED = 0,
    REVOKED = 1,
}

///
/// Summary:
///     Defines customer audit action types.
///
public enum CustomerAuditTypeEnum
{
    INVOICE_CREATED = 0,
    MESSAGE_RECEIVED = 1,
    LINK_ACCESSED = 2,
    PROFILE_UPDATED = 3,
    CREATED = 4,
}

///
/// Summary:
///     Defines product lifecycle states.
///
public enum ListingStatusEnum
{
    DRAFT = 0,
    PENDING = 1,
    ARCHIVED = 2,
    ACTIVE = 3,
    DISABLE = 4,
}

///
/// Summary:
///     Defines inventory status categories for the storefront product breakdown.
///
public enum InventoryStatusEnum
{
    SELLING = 0,
    LOW_STOCK = 1,
    OUT_OF_STOCK = 2,
    DRAFT = 3,
}

///
/// Summary:
///     Defines inventory warning severity levels for products on the storefront.
///
public enum InventoryWarningEnum
{
    LOW_STOCK = 0,
    OUT_OF_STOCK = 1,
}

///
/// Summary:
///     Defines broadcast communication channels.
///
public enum BroadcastChannelEnum
{
    EMAIl = 0,
    TELEGRAM = 1,
    ZALO = 2,
    WEB_PUSH = 3,
    SMS = 4,
    DISCORD = 5,
}

///
/// Summary:
///     Defines value format transforms applied when mapping broadcast fields.
///
public enum BroadcastMapFormatEnum
{
    TRIM = 0,
    UPPERCASE = 1,
    LOWERCASE = 2,
    CAPITALIZE = 3,
    REMOVE_EXTRA_SPACES = 4,
    LOCAL_PHONE = 5,
    CURRENCY = 6,
    PERCENTAGE = 7,
    DATE = 8,
}

public enum BroadcastAutomationTypeEnum
{
    DATA_CREATED = 0,
    DATA_UPDATED = 1,
    ON_ACTIVE = 2,
}

///
/// Summary:
///     Defines automation status states.
///
public enum StatusAutomationEnum
{
    DRAFT = 0,
    ACTIVE = 1,
    STOPPED = 2,
}

public enum TelegramContentEnum
{
    PHOTO = 0,
    VIDEO = 2,
    VOICE = 3,
    DOCUMENT = 1,
}

///
/// Summary:
///     Defines broadcast execution process states.
///
public enum BroadcastProcessEnum
{
    DRAFT = 0,
    SENDING = 1,
    DONE = 2,
    CANCEL = 3,
    WAITING = 4,
    QUEUE = 5,
    TRIGGER = 6,
}

///
/// Summary:
///     Defines content lifecycle status states.
///
public enum ContentStatusEnum
{
    DRAFT = 0,
    ACTIVE = 1,
    ARCHIVED = 2,
}

///
/// Summary:
///     Defines broadcast delivery status codes.
///
public enum BroadcastHistoryStatusEnum
{
    FAILED = 0,
    SUCCESS = 1,
    EMAIL_NOT_VALID_FORMAT = 100,
    CONTACT_DUPLICATE_SEND = 101,
    PHONE_NOT_VALID_FORMAT = 102,
    ERROR_WHEN_REQUEST_TO_MAILGUN = 200,
    PACKAGE_QUOTA_EXCEEDED = 201,
}

///
/// Summary:
///     Defines broadcast filter condition operators.
///
public enum BroadcastConditionNameEnum
{
    EQUAL = 0,
    NOT_EQUAL = 1,
    GREATER = 2,
    LESS = 3,
    IN = 4,
    NOT_IN = 5,
    LIKE = 6,
}

///
/// Summary:
///     Distinguishes group nodes from leaf condition nodes in the filter tree.
///
public enum ConditionNodeTypeEnum
{
    GROUP = 0,
    CONDITION = 1,
}

///
/// Summary:
///     Boolean combinator applied to children of a filter condition group.
///
public enum ConditionLogicEnum
{
    AND = 0,
    OR = 1,
}

///
/// Summary:
///     Defines data processing status states.
///
public enum DataStatusEnum
{
    PROCESSING = 0,
    COMPLETE = 1,
    FAILED = 2,
}

///
/// Summary:
///     Defines external integration availability status.
///
public enum OpenIntegrateStatus
{
    CLOSE = 0,
    OPEN = 1,
}

///
/// Summary:
///     Defines external integration types.
///
public enum OpenIntegrationType
{
    BROADCAST = 0,
}

///
/// Summary:
///     Defines order fulfillment status states.
///
public enum OrderStatusEnum
{
    NONE = 0,
    WAITING_ACCEPT = 1,
    CONFIRMED = 2,
    DELIVERING = 3,
    FINISHED = 4,
    CANCELLED = 5,
}

///
/// Summary:
///     Defines order status transition actions.
///
public enum ActionUpdateOrderStateEnum
{
    NEXT = 1,
    PREVIOUS = 2,
}

///
/// Summary:
///     Defines how an order was created.
///
public enum OrderSourceEnum
{
    CUSTOMER_CHECKOUT = 0,
    MANUAL_STAFF = 1,
}

///
/// Summary:
///     Defines the display status of a banner, derived from IsActive and ExpiredAt.
///
public enum BannerStatusEnum
{
    ACTIVE = 0,
    INACTIVE = 1,
    EXPIRED = 2,
}

///
/// Summary:
///     Defines the monetary component types that make up an order price breakdown.
///
public enum OrderPriceTypeEnum
{
    SUBTOTAL = 0,
    DISCOUNT = 1,
    TAX = 2,
    SHIPPING = 3,
    TOTAL = 4,
}

///
/// Summary:
///     Defines brand status states.
///
public enum BrandStatusEnum
{
    DRAFT = 0,
    ACTIVE = 1,
}

///
/// Summary:
///     Defines category hierarchy levels.
///
public enum CategoryLevelEnum
{
    PARENT = 1,
    SUB_TITLE = 2,
    MAIN = 3,
}

///
/// Summary:
///     Defines domain usage types.
///
public enum DomainTypeEnum
{
    SHOP = 0,
    REDIRECT = 1,
}

///
/// Summary:
///     Defines URL shortening link types.
///
public enum LinkTypeEnum
{
    MANUAL = 0,
    CAMPAIGN = 1,
}

///
/// Summary:
///     Defines redirect campaign status states.
///
public enum StatusCampaignRedirectEnum
{
    DRAFT = 0,
    WAIT = 1,
    ACTIVE = 2,
    STOPPED = 3,
    CANCELLED = 4,
}

///
/// Summary:
///     Defines webhook event types for redirect service notifications.
///
public enum EventWebhookRedirectEnum
{
    ACCESS_LINK_SUCCESS = 0,
    ACCESS_LINK_EXPIRED = 1,

    LINK_CREATED = 2,
    LINK_UPDATED = 3,
    LINK_DELETED = 4,

    CAMPAIGN_CREATED = 5,
    CAMPAIGN_UPDATED = 6,
    CAMPAIGN_DELETED = 7,

    TEMPLATE_CREATED = 8,
    TEMPLATE_UPDATED = 9,
    TEMPLATE_DELETED = 10,
}

///
/// Summary:
///     Defines webhook delivery attempt status.
///
public enum StatusWebhookDeliveryEnum
{
    PENDING = 0,
    SUCCESS = 1,
    FAILED = 3,
}

///
/// Summary:
///     Defines URL template processing status.
///
public enum TemplateStatusEnum
{
    DRAFT = 0,
    PROCESSING = 1,
    COMPLETED = 3,
    FAILED = 4,
    DISABLED = 5,
}

///
/// Summary:
///     Defines mobile device operating systems for user agent detection.
///
public enum MobileDeviceOsEnum
{
    IOS,
    ANDROID,
    PHONE,
    HARMONY,
    BLACKBERRY,
    TIZEN,
    SYMBIAN,
}

///
/// Summary:
///     Defines AI template types for intelligent content generation.
///
public enum TemplateIntelligenceEnum
{
    FINANCIAL_TRANSACTION_DESCRIPTION = 0,
    REDIRECT_SHORT_LINK_DESCRIPTION = 1,
    REDIRECT_CAMPAIGN_DESCRIPTION = 2,
    REDIRECT_TEMPLATE_URL_DESCRIPTION = 3,
    REDIRECT_WEBHOOK_DESCRIPTION = 4,
}

public enum WorkflowNodeEnum
{
    DATA,
    BROADCAST,
    END,
    START,
    WAIT,
    CONDITION,
    POINT,
    HTTP_REQUEST,
    SWITCH,
    RANDOM_SPLIT,
}

///
/// Summary:
///     Defines HTTP methods supported by automation HTTP_REQUEST nodes.
///
public enum WorkflowHttpMethodEnum
{
    GET = 0,
    POST = 1,
    PUT = 2,
    PATCH = 3,
    DELETE = 4,
}

///
/// Summary:
///     Defines how an automation workflow repeats after it finishes.
///
public enum WorkflowLoopTypeEnum
{
    NONE = 0,
    INTERVAL = 1,
    WEEKDAY = 2,
    MONTH_DAY = 3,
    YEAR_MONTH = 4,
}

///
/// Summary:
///     Defines validation error codes for automation workflow nodes.
///
public enum AutomationValidationCode
{
    DATA_NOT_SETUP,
    BROADCAST_NOT_SETUP,
    START_NOT_SETUP,
    WAIT_TIME_REQUIRED,
    WAIT_TIME_TOO_SHORT,
    CONDITION_COLUMN_REQUIRED,
    CONDITION_VALUE_REQUIRED,
    START_SETUP_NOT_ALLOWED,
    HTTP_METHOD_REQUIRED,
    HTTP_URL_REQUIRED,
    HTTP_URL_INVALID,
    HTTP_TIMEOUT_REQUIRED,
    SWITCH_COLUMN_REQUIRED,
    SWITCH_CASES_REQUIRED,
    RANDOM_BRANCHES_REQUIRED,
    RANDOM_PERCENT_INVALID,
}

public enum WorkflowEdgeEnum
{
    DEFAULT,
    ADD,
    SLEEP,
}

public enum WorkflowOperatorEnum
{
    Equals = 1,
    NotEquals = 2,
    GreaterThan = 3,
    GreaterThanOrEqual = 4,
    LessThan = 5,
    LessThanOrEqual = 6,
    Contains = 7,
    NotContains = 8,
    StartsWith = 9,
    EndsWith = 10,
}

///
/// Summary:
///     Defines warehouse classification types for inventory operations.
///
public enum WarehouseTypeEnum
{
    NORMAL = 0,
    TRANSIT = 1,
    RETURN = 2,
    DAMAGED = 3,
}

///
/// Summary:
///     Defines warehouse lifecycle status states.
///
public enum WarehouseStatusEnum
{
    ACTIVE = 0,
    INACTIVE = 1,
}

///
/// Summary:
///     Defines warehouse location functional types for bin-level storage.
///
public enum WarehouseLocationTypeEnum
{
    RECEIVING = 0,
    STORAGE = 1,
    PICKING = 2,
    PACKING = 3,
    SHIPPING = 4,
    RETURN = 5,
    DAMAGED = 6,
    QUARANTINE = 7,
    TRANSIT = 8,
}

///
/// Summary:
///     Defines warehouse location availability and capacity status states.
///
public enum WarehouseLocationStatusEnum
{
    ACTIVE = 0,
    INACTIVE = 1,
    BLOCKED = 2,
    FULL = 3,
}

///
/// Summary:
///     Defines warehouse lot lifecycle status states.
///
public enum InventoryLotStatusEnum
{
    DRAFT = 0,
    ACTIVE = 1,
    EXPIRED = 2,
    BLOCKED = 3,
    CONSUMED = 4,
}

///
/// Summary:
///     Defines warehouse product catalog lifecycle status states.
///
public enum WarehouseProductStatusEnum
{
    DRAFT = 0,
    ACTIVE = 1,
    INACTIVE = 2,
}

///
/// Summary:
///     Defines source document types for goods receipt operations.
///
public enum GoodsReceiptSourceTypeEnum
{
    PURCHASE_ORDER = 0,
    RETURN_ORDER = 1,
    WAREHOUSE_TRANSFER = 2,
    MANUAL = 3,
    PRODUCTION = 4,
    OTHER = 5,
}

///
/// Summary:
///     Defines goods receipt document workflow status states.
///
public enum GoodsReceiptStatusEnum
{
    PENDING = 0,
    RECEIVING = 1,
    LABELING = 2,
    PUTAWAY = 3,
    COMPLETED = 4,
    CANCELLED = 5,
    WAITING_LABELING = 6,
    WAITING_PUTAWAY = 7,
}

///
/// Summary:
///     Defines the origin of a variant sellable stock change recorded in history.
///
public enum StockHistoryTypeEnum
{
    ///
    /// Summary:
    ///     A manual adjustment performed directly by a user.
    ///
    MANUAL = 0,

    ///
    /// Summary:
    ///     An increase applied when completing an inbound goods receipt.
    ///
    GOODS_RECEIPT = 1,

    ///
    /// Summary:
    ///     Any other inbound or outbound movement not covered by the specific types.
    ///
    OTHER = 2,
}

///
/// Summary:
///     Defines source document types for goods issue operations.
///
public enum GoodsIssueSourceTypeEnum
{
    SALES_ORDER = 0,
    WAREHOUSE_TRANSFER = 1,
    RETURN_TO_SUPPLIER = 2,
    MANUAL = 3,
    PRODUCTION = 4,
    OTHER = 5,
}

///
/// Summary:
///     Defines goods issue document workflow status states.
///
public enum GoodsIssueStatusEnum
{
    WAITING = 0,
    PICKING = 1,
    WAITING_PACKING = 2,
    PACKING = 3,
    WAITING_LABELING = 4,
    LABELING = 5,
    STAGING = 6,
    DISPATCHED = 7,
    COMPLETED = 8,
    CANCELLED = 9,
    HANDING_OVER = 10,
}

///
/// Summary:
///     Defines internal transfer document workflow status states.
///
public enum InternalTransferStatusEnum
{
    DRAFT = 0,
    APPROVED = 1,
    MOVING = 2,
    COMPLETED = 3,
    CANCELLED = 4,
}

///
/// Summary:
///     Defines warehouse transfer document workflow status states.
///
public enum WarehouseTransferStatusEnum
{
    DRAFT = 0,
    APPROVED = 1,
    ALLOCATED = 2,
    PICKING = 3,
    SHIPPED = 4,
    IN_TRANSIT = 5,
    PARTIALLY_RECEIVED = 6,
    RECEIVED = 7,
    COMPLETED = 8,
    CANCELLED = 9,
}

///
/// Summary:
///     Defines stock count document classification types.
///
public enum StockCountTypeEnum
{
    FULL_COUNT = 0,
    CYCLE_COUNT = 1,
    LOCATION_COUNT = 2,
    SKU_COUNT = 3,
}

///
/// Summary:
///     Defines stock count document workflow status states.
///
public enum StockCountStatusEnum
{
    DRAFT = 0,
    COUNTING = 1,
    REVIEWING = 2,
    APPROVED = 3,
    COMPLETED = 4,
    CANCELLED = 5,
}

///
/// Summary:
///     Defines inventory adjustment document workflow status states.
///
public enum InventoryAdjustmentStatusEnum
{
    DRAFT = 0,
    WAITING_APPROVAL = 1,
    APPROVED = 2,
    COMPLETED = 3,
    CANCELLED = 4,
}

///
/// Summary:
///     Defines the selectable reasons for an inventory adjustment.
///
public enum InventoryAdjustmentReasonEnum
{
    STOCKTAKE_DIFF = 0,
    DAMAGED = 1,
    LOST = 2,
    EXPIRED = 3,
    RETURN = 4,
    CORRECTION = 5,
    OTHER = 6,
}

///
/// Summary:
///     Defines inventory ledger transaction movement types.
///
public enum InventoryTransactionTypeEnum
{
    RECEIPT = 0,
    ISSUE = 1,
    TRANSFER_OUT = 2,
    TRANSFER_IN = 3,
    INTERNAL_MOVE = 4,
    ADJUSTMENT_INCREASE = 5,
    ADJUSTMENT_DECREASE = 6,
    STOCK_COUNT_ADJUSTMENT = 7,
    RETURN_IN = 8,
    RETURN_OUT = 9,
    RESERVE = 10,
    RELEASE_RESERVATION = 11,
}

///
/// Summary:
///     Defines reference document types linked to inventory transactions.
///
public enum InventoryReferenceTypeEnum
{
    GOODS_RECEIPT = 0,
    GOODS_ISSUE = 1,
    INTERNAL_TRANSFER = 2,
    WAREHOUSE_TRANSFER = 3,
    STOCK_COUNT = 4,
    INVENTORY_ADJUSTMENT = 5,
    OTHER_RECEIPT = 6,
    OTHER_ISSUE = 7,
    SALES_ORDER = 8,
    PURCHASE_ORDER = 9,
    RETURN_ORDER = 10,
    PRODUCTION_ORDER = 11,
    RESERVATION = 12,
    MANUAL = 13,
}

///
/// Summary:
///     Defines billing invoice lifecycle status states.
///
public enum BillingInvoiceStatusEnum
{
    DRAFT = 0,
    UNPAID = 1,
    PARTIALLY_PAID = 2,
    PAID = 3,
    READY_TO_ISSUE = 4,
    ISSUED = 5,
    PARTIALLY_REFUNDED = 6,
    REFUNDED = 7,
    CANCELLED = 8,
    VOIDED = 9,
}

///
/// Summary:
///     Defines billing invoice source document types.
///
public enum BillingSourceTypeEnum
{
    POS_SALE = 0,
    ONLINE_ORDER = 1,
    MANUAL = 2,
}

///
/// Summary:
///     Defines billing invoice line item types.
///
public enum BillingLineTypeEnum
{
    PRODUCT = 0,
    SHIPPING_FEE = 1,
    SERVICE_FEE = 2,
    SURCHARGE = 3,
    OTHER = 4,
}

///
/// Summary:
///     Defines billing tax calculation modes.
///
public enum BillingTaxModeEnum
{
    TAX_EXCLUSIVE = 0,
    TAX_INCLUSIVE = 1,
}

///
/// Summary:
///     Defines billing tax category types.
///
public enum BillingTaxCategoryEnum
{
    STANDARD = 0,
    REDUCED = 1,
    ZERO = 2,
    EXEMPT = 3,
}

///
/// Summary:
///     Defines billing discount application types.
///
public enum BillingDiscountTypeEnum
{
    LINE_DISCOUNT = 0,
    INVOICE_DISCOUNT = 1,
    PROMOTION = 2,
    VOUCHER = 3,
    MANUAL = 4,
}

///
/// Summary:
///     Defines billing payment method types.
///
public enum BillingPaymentMethodEnum
{
    CASH = 0,
    BANK_TRANSFER = 1,
    CARD = 2,
    E_WALLET = 3,
    COD = 4,
    OTHER = 5,
}

///
/// Summary:
///     Defines billing payment status states.
///
public enum BillingPaymentStatusEnum
{
    PENDING = 0,
    CONFIRMED = 1,
    FAILED = 2,
    CANCELLED = 3,
    PROCESSING = 4,
}

///
/// Summary:
///     Defines billing refund status states.
///
public enum BillingRefundStatusEnum
{
    PENDING = 0,
    CONFIRMED = 1,
    FAILED = 2,
    CANCELLED = 3,
}

///
/// Summary:
///     Defines POS merchant availability status states.
///
public enum PosMerchantStatusEnum
{
    ACTIVE = 0,
    INACTIVE = 1,
}

///
/// Summary:
///     Defines POS store availability status states.
///
public enum PosStoreStatusEnum
{
    ACTIVE = 0,
    INACTIVE = 1,
}

///
/// Summary:
///     Defines billing document sequence types for number generation.
///
public enum BillingDocumentSequenceTypeEnum
{
    INVOICE = 0,
    CREDIT_NOTE = 1,
    DEBIT_NOTE = 2,
}

///
/// Summary:
///     Defines billing amount rounding modes.
///
public enum BillingRoundingModeEnum
{
    NONE = 0,
    NEAREST_1 = 1,
    NEAREST_10 = 2,
    NEAREST_100 = 3,
}

///
/// Summary:
///     Defines the billing cycle of a subscription package.
///
public enum PackageCycleEnum
{
    MONTHLY = 0,
    YEARLY = 1,
}

///
/// Summary:
///     Defines the lifecycle status of a subscription invoice.
///
public enum SubscriptionInvoiceStatusEnum
{
    PENDING = 0,
    PAID = 1,
    CANCELLED = 2,
    REFUNDED = 3,
}

///
/// Summary:
///     Defines the quota types that can be metered per profile subscription.
///
public enum QuotaDefinitionEnum
{
    MARKETING_DATA = 0,
    MARKETING_FILTER = 1,
    MARKETING_WORKFLOW = 2,
    MARKETING_BROADCAST = 3,
    MARKETING_SEND_MESSAGE = 4,
    MARKETING_CONTENT = 5,
    REDIRECT_LINK = 6,
    REDIRECT_CAMPAIGN = 7,
    REDIRECT_TEMPLATE = 8,
    REDIRECT_WEBHOOK = 9,
}

///
/// Summary:
///     Defines the media asset type in the signage system.
///
public enum SignageMediaTypeEnum
{
    IMAGE = 0,
    VIDEO = 1,
}

///
/// Summary:
///     Defines the connection status of a signage screen.
///
public enum SignageScreenStatusEnum
{
    OFFLINE = 0,
    ONLINE = 1,
}

///
/// Summary:
///     Identifies supported wallet top-up presets; amounts are defined in metadata.
///
public enum WalletTopupEnum
{
    HUNDRED_THOUSAND = 0,
    TWO_HUNDRED_THOUSAND = 1,
    FIVE_HUNDRED_THOUSAND = 2,
    ONE_MILLION = 3,
    TWO_MILLION = 4,
    FIVE_MILLION = 5,
}

///
/// Summary:
///     Identifies the code-selected business operation for goods receipt approval.
///
public enum GoodsReceiptApprovalActionEnum
{
    NONE = 0,
    TRANSITION = 1,
    RECEIVING = 2,
    COMPLETE = 3,
    CANCEL = 4,
}

///
/// Summary:
///     Identifies the code-selected business operation for goods issue approval.
///
public enum GoodsIssueApprovalActionEnum
{
    NONE = 0,
    TRANSITION = 1,
    SHIP = 2,
    CANCEL = 4,
}

///
/// Summary:
///     Defines the sort options available for the storefront product listing.
///
public enum ShopProductSortEnum
{
    ///
    /// Summary:
    ///     Newest products first by creation time.
    ///
    NEWEST = 0,

    ///
    /// Summary:
    ///     Oldest products first by creation time.
    ///
    OLDEST = 1,

    ///
    /// Summary:
    ///     Best-selling products first by total sold quantity.
    ///
    BEST_SELLING = 2,

    ///
    /// Summary:
    ///     Lowest variant price first.
    ///
    PRICE_ASC = 3,

    ///
    /// Summary:
    ///     Highest variant price first.
    ///
    PRICE_DESC = 4,

    ///
    /// Summary:
    ///     Highest average rating first.
    ///
    RATE = 5,
}
