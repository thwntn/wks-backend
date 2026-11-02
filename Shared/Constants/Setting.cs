namespace Shared;

///
/// Summary:
///     Defines system configuration keys grouped by application responsibility.
///
public enum SettingNames
{
    ///
    /// Summary:
    ///     Application domain configuration.
    ///
    /// Parameters:
    ///     APP_REDIRECT_DOMAIN:
    ///         Default domain configuration for redirect applications.
    ///
    ///     APP_SHOP_DOMAIN:
    ///         Default domain configuration for storefront applications.
    ///
    APP_REDIRECT_DOMAIN,
    APP_SHOP_DOMAIN,

    ///
    /// Summary:
    ///     Default customer, commerce and access settings.
    ///
    /// Parameters:
    ///     DEFAULT_CUSTOMER:
    ///         Default customer configuration used by account setup.
    ///
    ///     DEFAULT_DISCOUNT:
    ///         Default discount configuration used by commerce setup.
    ///
    ///     DEFAULT_PAYMENT:
    ///         Default payment configuration used by commerce setup.
    ///
    ///     DEFAULT_PERMISSION:
    ///         Default permission assignment used by account setup.
    ///
    DEFAULT_CUSTOMER,
    DEFAULT_DISCOUNT,
    DEFAULT_PAYMENT,
    DEFAULT_PERMISSION,

    ///
    /// Summary:
    ///     Subscription package assignments.
    ///
    /// Parameters:
    ///     PACKAGE_FREE:
    ///         Subscription package identifier used for the free tier.
    ///
    ///     PACKAGE_PERSONAL:
    ///         Subscription package identifier used for the personal tier.
    ///
    ///     PACKAGE_BUSINESS:
    ///         Subscription package identifier used for the business tier.
    ///
    PACKAGE_FREE,
    PACKAGE_PERSONAL,
    PACKAGE_BUSINESS,

    ///
    /// Summary:
    ///     Bank transfer recipient details.
    ///
    /// Parameters:
    ///     BANK_ID:
    ///         Bank identifier used for payment recipient details.
    ///
    ///     BANK_ACCOUNT_NAME:
    ///         Account holder name used for bank transfer instructions.
    ///
    ///     BANK_ACCOUNT_NO:
    ///         Recipient bank account number used for transfers.
    ///
    BANK_ID,
    BANK_ACCOUNT_NAME,
    BANK_ACCOUNT_NO,

    ///
    /// Summary:
    ///     Telegram notification configuration.
    ///
    /// Parameters:
    ///     TELEGRAM_TOKEN:
    ///         Bot token used by Telegram notification delivery.
    ///
    ///     TELEGRAM_CHAT_ID:
    ///         Destination chat identifier used by Telegram notifications.
    ///
    TELEGRAM_TOKEN,
    TELEGRAM_CHAT_ID,

    ///
    /// Summary:
    ///     Redirect import, webhook and delivery limits.
    ///
    /// Parameters:
    ///     REDIRECT_TEMPLATE_MAX_ROWS:
    ///         Maximum number of rows accepted by redirect template processing.
    ///
    ///     REDIRECT_WEBHOOK_MAX_RETRY:
    ///         Configured retry limit for webhook delivery attempts.
    ///
    ///     REDIRECT_DELIVERY_RETENTION_DAYS:
    ///         Retention period in days for redirect delivery history.
    ///
    ///     REDIRECT_WEBHOOK_MAX_PER_PROFILE:
    ///         Maximum configured webhook count per profile.
    ///
    REDIRECT_TEMPLATE_MAX_ROWS,
    REDIRECT_WEBHOOK_MAX_RETRY,
    REDIRECT_DELIVERY_RETENTION_DAYS,
    REDIRECT_WEBHOOK_MAX_PER_PROFILE,
}
