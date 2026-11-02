namespace Shared;

///
/// Summary:
///     Defines cache namespaces for application data and metadata.
///
public enum CacheNames
{
    ///
    /// Summary:
    ///     Authentication and delegated access.
    ///
    /// Parameters:
    ///     AUTH_CONFIRM_COUNT_MESSAGE:
    ///         Confirmation-message send counters keyed by recipient.
    ///
    ///     AUTH_CONFIRM_PREVIOUS_TIME:
    ///         Previous confirmation-message timestamps used for send throttling.
    ///
    ///     AUTH_TEMPLATE_CONFIRM:
    ///         Email template content used to send account confirmation codes.
    ///
    ///     AUTH_USER_SETUP:
    ///         Pending account setup flags keyed by account identifier.
    ///
    ///     AUTH_PERMISSION:
    ///         Resolved permission codes keyed by the authenticated account identifier.
    ///
    ///     AUTH_ACTOR_ACCOUNT:
    ///         Namespace reserved for delegated actor account snapshots.
    ///
    AUTH_CONFIRM_COUNT_MESSAGE,
    AUTH_CONFIRM_PREVIOUS_TIME,
    AUTH_TEMPLATE_CONFIRM,
    AUTH_USER_SETUP,
    AUTH_PERMISSION,
    AUTH_ACTOR_ACCOUNT,

    ///
    /// Summary:
    ///     Profile settings metadata.
    ///
    /// Parameters:
    ///     PROFILE_SETTING:
    ///         Cached collection of system setting values.
    ///
    PROFILE_SETTING,

    ///
    /// Summary:
    ///     Media content.
    ///
    /// Parameters:
    ///     MEDIA_FILE:
    ///         Encoded media file content keyed by the requested file identifier.
    ///
    MEDIA_FILE,

    ///
    /// Summary:
    ///     Token session cache.
    ///
    /// Parameters:
    ///     JWT_REFRESH_TOKEN:
    ///         Namespace reserved for refresh-token session entries.
    ///
    JWT_REFRESH_TOKEN,

    ///
    /// Summary:
    ///     Subscription quota counters.
    ///
    /// Parameters:
    ///     QUOTAS_COUNTER:
    ///         Usage counters keyed by profile ID and quota code.
    ///
    QUOTAS_COUNTER,

    ///
    /// Summary:
    ///     Broadcast email processing.
    ///
    /// Parameters:
    ///     BROADCAST_EMAIL:
    ///         Shared list of email broadcasts currently marked for sending.
    ///
    BROADCAST_EMAIL,

    ///
    /// Summary:
    ///     Customer profiles and activity metadata.
    ///
    /// Parameters:
    ///     CUSTOMER_PROFILE:
    ///         Customer details keyed by customer identifier.
    ///
    ///     CUSTOMER_LOGIN:
    ///         Namespace isolating cached customer login data from other application features.
    ///
    ///     CUSTOMER_AUDIT_ACTION:
    ///         Namespace isolating cached customer audit action data from other application features.
    ///
    CUSTOMER_PROFILE,
    CUSTOMER_LOGIN,
    CUSTOMER_AUDIT_ACTION,

    ///
    /// Summary:
    ///     Storefront snapshots and checkout processing.
    ///
    /// Parameters:
    ///     SHOP_ORDER_GENERATE:
    ///         Checkout processing state keyed by checkout session.
    ///
    ///     SHOP_BANNER:
    ///         Storefront banners keyed by owning profile identifier.
    ///
    ///     SHOP_SETUP:
    ///         Storefront setup resolved for the incoming host.
    ///
    ///     SHOP_DOMAIN:
    ///         Storefront domain resolution keyed by incoming host.
    ///
    SHOP_ORDER_GENERATE,
    SHOP_BANNER,
    SHOP_SETUP,
    SHOP_DOMAIN,

    ///
    /// Summary:
    ///     Redirect links, campaigns and delivery metadata.
    ///
    /// Parameters:
    ///     REDIRECT_DOMAIN:
    ///         Namespace isolating cached redirect domain data from other application features.
    ///
    ///     REDIRECT_LINK:
    ///         Namespace isolating cached redirect link data from other application features.
    ///
    ///     REDIRECT_ANALYST_ACCESS:
    ///         Namespace isolating cached redirect analyst access data from other application features.
    ///
    ///     REDIRECT_ANALYST_BROWSER:
    ///         Namespace isolating cached redirect analyst browser data from other application features.
    ///
    ///     REDIRECT_STATUS_CAMPAIGN:
    ///         Reference metadata for redirect status campaign, shared by the corresponding lookup endpoints.
    ///
    ///     REDIRECT_STATUS_TEMPLATE:
    ///         Reference metadata for redirect status template, shared by the corresponding lookup endpoints.
    ///
    ///     REDIRECT_CAMPAIGN_EXPORTING:
    ///         Export-in-progress flags keyed by redirect campaign identifier.
    ///
    ///     REDIRECT_CAMPAIGN_STATUS:
    ///         Reference metadata for redirect campaign status, shared by the corresponding lookup endpoints.
    ///
    ///     REDIRECT_WEBHOOK_STATUS:
    ///         Reference metadata for redirect webhook status, shared by the corresponding lookup endpoints.
    ///
    ///     REDIRECT_WEBHOOK_EVENT:
    ///         Reference metadata for redirect webhook event, shared by the corresponding lookup endpoints.
    ///
    ///     REDIRECT_DELIVERY_STATUS:
    ///         Reference metadata for redirect delivery status, shared by the corresponding lookup endpoints.
    ///
    ///     REDIRECT_WEBHOOK_ENDPOINT:
    ///         Delivery endpoint snapshots keyed by profile and webhook event.
    ///
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

    ///
    /// Summary:
    ///     Marketing records, credentials and workflow metadata.
    ///
    /// Parameters:
    ///     MARKETING_DATA_TYPE:
    ///         Reference metadata for marketing data type, shared by the corresponding lookup endpoints.
    ///
    ///     MARKETING_DATA_NAME:
    ///         Namespace isolating cached marketing data name data from other application features.
    ///
    ///     MARKETING_CONTENT_PUBLISH_STATUS:
    ///         Reference metadata for marketing content publish status, shared by the corresponding lookup endpoints.
    ///
    ///     MARKETING_CONTACT_UNIQUE:
    ///         Atomic delivery flags scoped to broadcast, execution and contact identity.
    ///
    ///     MARKETING_TELEGRAM_CREDENTIAL:
    ///         Namespace isolating cached marketing telegram credential data from other application features.
    ///
    ///     MARKETING_MAIL_CREDENTIAL:
    ///         Namespace isolating cached marketing mail credential data from other application features.
    ///
    ///     MARKETING_AUTOMATION_STATUS:
    ///         Reference metadata for marketing automation status, shared by the corresponding lookup endpoints.
    ///
    ///     MARKETING_AUTOMATION_TYPE:
    ///         Reference metadata for marketing automation type, shared by the corresponding lookup endpoints.
    ///
    ///     MARKETING_AUTOMATION_OPERATOR:
    ///         Reference metadata for marketing automation operator, shared by the corresponding lookup endpoints.
    ///
    ///     MARKETING_AUTOMATION_LOOP:
    ///         Reference metadata for marketing automation loop, shared by the corresponding lookup endpoints.
    ///
    ///     MARKETING_AUTOMATION_HTTP_METHOD:
    ///         Reference metadata for marketing automation http method, shared by the corresponding lookup endpoints.
    ///
    ///     MARKETING_ZALO_CREDENTIAL:
    ///         Namespace isolating cached marketing zalo credential data from other application features.
    ///
    ///     MARKETING_WEB_PUSH_CREDENTIAL:
    ///         Namespace isolating cached marketing web push credential data from other application features.
    ///
    ///     MARKETING_BROADCAST:
    ///         Namespace isolating cached marketing broadcast data from other application features.
    ///
    ///     MARKETING_BROADCAST_FORMAT:
    ///         Reference metadata for marketing broadcast format, shared by the corresponding lookup endpoints.
    ///
    ///     MARKETING_CONTENT:
    ///         Namespace isolating cached marketing content data from other application features.
    ///
    ///     MARKETING_AUTOMATION:
    ///         Namespace isolating cached marketing automation data from other application features.
    ///
    ///     MARKETING_FILTER:
    ///         Namespace isolating cached marketing filter data from other application features.
    ///
    ///     MARKETING_DATA:
    ///         Namespace isolating cached marketing data data from other application features.
    ///
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

    ///
    /// Summary:
    ///     Redirect links, campaigns and delivery metadata.
    ///
    /// Parameters:
    ///     REDIRECT_WEBHOOK:
    ///         Namespace isolating cached redirect webhook data from other application features.
    ///
    ///     REDIRECT_TEMPLATE:
    ///         Namespace isolating cached redirect template data from other application features.
    ///
    ///     REDIRECT_DELIVERY:
    ///         Namespace isolating cached redirect delivery data from other application features.
    ///
    REDIRECT_WEBHOOK,
    REDIRECT_TEMPLATE,
    REDIRECT_DELIVERY,

    ///
    /// Summary:
    ///     Commerce records and catalog metadata.
    ///
    /// Parameters:
    ///     BUSINESS_PRODUCT:
    ///         Namespace isolating cached business product data from other application features.
    ///
    ///     BUSINESS_BRAND:
    ///         Namespace isolating cached business brand data from other application features.
    ///
    ///     BUSINESS_CATEGORY:
    ///         Namespace isolating cached business category data from other application features.
    ///
    ///     BUSINESS_ORDER:
    ///         Namespace isolating cached business order data from other application features.
    ///
    ///     BUSINESS_BANNER:
    ///         Namespace isolating cached business banner data from other application features.
    ///
    BUSINESS_PRODUCT,
    BUSINESS_BRAND,
    BUSINESS_CATEGORY,
    BUSINESS_ORDER,
    BUSINESS_BANNER,

    ///
    /// Summary:
    ///     Financial timeline.
    ///
    /// Parameters:
    ///     FINANCIAL_TIMELINE:
    ///         Namespace isolating cached financial timeline data from other application features.
    ///
    FINANCIAL_TIMELINE,

    ///
    /// Summary:
    ///     Audience membership and consent.
    ///
    /// Parameters:
    ///     AUDIENCE_MEMBERSHIP:
    ///         Namespace isolating cached audience membership data from other application features.
    ///
    ///     AUDIENCE_CONSENT:
    ///         Namespace isolating cached audience consent data from other application features.
    ///
    ///     AUDIENCE_CONSENT_TYPE:
    ///         Reference metadata for audience consent type, shared by the corresponding lookup endpoints.
    ///
    AUDIENCE_MEMBERSHIP,
    AUDIENCE_CONSENT,
    AUDIENCE_CONSENT_TYPE,

    ///
    /// Summary:
    ///     Domain metadata.
    ///
    /// Parameters:
    ///     DOMAIN_TYPE:
    ///         Reference metadata for domain type, shared by the corresponding lookup endpoints.
    ///
    DOMAIN_TYPE,

    ///
    /// Summary:
    ///     Telegram webhook identities.
    ///
    /// Parameters:
    ///     WEBHOOK_TELEGRAM_USER:
    ///         Namespace isolating cached webhook telegram user data from other application features.
    ///
    WEBHOOK_TELEGRAM_USER,

    ///
    /// Summary:
    ///     System reference metadata.
    ///
    /// Parameters:
    ///     SYSTEM_ROLE_PERMISSION:
    ///         Namespace isolating cached system role permission data from other application features.
    ///
    SYSTEM_ROLE_PERMISSION,

    ///
    /// Summary:
    ///     System feedback and setting reference metadata.
    ///
    /// Parameters:
    ///     SYSTEM_FEEDBACK_STATUS:
    ///         Reference metadata for feedback status, shared by the corresponding lookup endpoint.
    ///
    ///     PROFILE_SETTING_NAME:
    ///         Reference metadata for setting names, shared by the corresponding lookup endpoint.
    ///
    SYSTEM_FEEDBACK_STATUS,
    PROFILE_SETTING_NAME,

    ///
    /// Summary:
    ///     Financial expenses and expense transactions.
    ///
    /// Parameters:
    ///     FINANCIAL_EXPENSE:
    ///         Expense collections keyed by the owning profile identifier.
    ///
    ///     FINANCIAL_EXPENSE_TRANSACTION:
    ///         Expense transaction data keyed by the owning profile identifier.
    ///
    FINANCIAL_EXPENSE,
    FINANCIAL_EXPENSE_TRANSACTION,
}
