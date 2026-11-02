namespace Shared;

///
/// Summary:
///     Defines real-time notification methods grouped by application responsibility.
///
public enum HubMethodName
{
    ///
    /// Summary:
    ///     Connection lifecycle and system notifications.
    ///
    /// Parameters:
    ///     INITIALIZE:
    ///         Real-time method identifying initialize messages; publishers and subscribed clients use this exact name.
    ///
    ///     NOTIFICATION_TRIGGER:
    ///         Real-time method identifying notification trigger messages; publishers and subscribed clients use this exact name.
    ///
    ///     PING:
    ///         Real-time method identifying ping messages; publishers and subscribed clients use this exact name.
    ///
    ///     PONG:
    ///         Real-time method identifying pong messages; publishers and subscribed clients use this exact name.
    ///
    ///     ERROR:
    ///         Real-time method identifying error messages; publishers and subscribed clients use this exact name.
    ///
    INITIALIZE,
    NOTIFICATION_TRIGGER,
    PING,
    PONG,
    ERROR,

    ///
    /// Summary:
    ///     Financial activity notifications.
    ///
    /// Parameters:
    ///     EXPENSE_TRANSACTION:
    ///         Real-time method identifying expense transaction messages; publishers and subscribed clients use this exact name.
    ///
    ///     EXPENSE:
    ///         Real-time method identifying expense messages; publishers and subscribed clients use this exact name.
    ///
    ///     FUND:
    ///         Real-time method identifying fund messages; publishers and subscribed clients use this exact name.
    ///
    ///     CASH:
    ///         Real-time method identifying cash messages; publishers and subscribed clients use this exact name.
    ///
    EXPENSE_TRANSACTION,
    EXPENSE,
    FUND,
    CASH,

    ///
    /// Summary:
    ///     Connection lifecycle and system notifications.
    ///
    /// Parameters:
    ///     SYSTEM_PUSH:
    ///         Real-time method identifying system push messages; publishers and subscribed clients use this exact name.
    ///
    SYSTEM_PUSH,

    ///
    /// Summary:
    ///     Subscriptions and file activity.
    ///
    /// Parameters:
    ///     SIGN_LISTEN:
    ///         Real-time method identifying sign listen messages; publishers and subscribed clients use this exact name.
    ///
    ///     REMOVE_LISTEN:
    ///         Real-time method identifying remove listen messages; publishers and subscribed clients use this exact name.
    ///
    ///     UPDATE_MESSAGE:
    ///         Real-time method identifying update message messages; publishers and subscribed clients use this exact name.
    ///
    ///     UPDATE_LIST_FILE:
    ///         Real-time method identifying update list file messages; publishers and subscribed clients use this exact name.
    ///
    SIGN_LISTEN,
    REMOVE_LISTEN,
    UPDATE_MESSAGE,
    UPDATE_LIST_FILE,

    ///
    /// Summary:
    ///     Support feedback.
    ///
    /// Parameters:
    ///     FEEDBACK:
    ///         Real-time method identifying feedback messages; publishers and subscribed clients use this exact name.
    ///
    FEEDBACK,

    ///
    /// Summary:
    ///     Marketing data and profile configuration.
    ///
    /// Parameters:
    ///     DATA:
    ///         Real-time method identifying data messages; publishers and subscribed clients use this exact name.
    ///
    ///     DATA_INFORMATION:
    ///         Real-time method identifying data information messages; publishers and subscribed clients use this exact name.
    ///
    ///     TEMPLATE:
    ///         Real-time method identifying template messages; publishers and subscribed clients use this exact name.
    ///
    DATA,
    DATA_INFORMATION,
    TEMPLATE,

    ///
    /// Summary:
    ///     Marketing delivery and automation.
    ///
    /// Parameters:
    ///     MARKETING_BROADCAST:
    ///         Real-time method identifying marketing broadcast messages; publishers and subscribed clients use this exact name.
    ///
    MARKETING_BROADCAST,

    ///
    /// Summary:
    ///     Marketing data and profile configuration.
    ///
    /// Parameters:
    ///     PROFILE_CONFIG:
    ///         Real-time method identifying profile config messages; publishers and subscribed clients use this exact name.
    ///
    PROFILE_CONFIG,

    ///
    /// Summary:
    ///     Marketing delivery and automation.
    ///
    /// Parameters:
    ///     MARKETING_FILTER:
    ///         Real-time method identifying marketing filter messages; publishers and subscribed clients use this exact name.
    ///
    ///     MARKETING_CONTENT:
    ///         Real-time method identifying marketing content messages; publishers and subscribed clients use this exact name.
    ///
    ///     MARKETING_AUTOMATION:
    ///         Real-time method identifying marketing automation messages; publishers and subscribed clients use this exact name.
    ///
    MARKETING_FILTER,
    MARKETING_CONTENT,
    MARKETING_AUTOMATION,

    ///
    /// Summary:
    ///     Catalog and storefront updates.
    ///
    /// Parameters:
    ///     PRODUCT:
    ///         Real-time method identifying product messages; publishers and subscribed clients use this exact name.
    ///
    ///     UPDATE_GROUP:
    ///         Real-time method identifying update group messages; publishers and subscribed clients use this exact name.
    ///
    ///     SCANNER_PRODUCT:
    ///         Real-time method identifying scanner product messages; publishers and subscribed clients use this exact name.
    ///
    ///     KANBAN_CATEGORY:
    ///         Real-time method identifying kanban category messages; publishers and subscribed clients use this exact name.
    ///
    ///     CATEGORIES:
    ///         Real-time method identifying categories messages; publishers and subscribed clients use this exact name.
    ///
    ///     BRAND:
    ///         Real-time method identifying brand messages; publishers and subscribed clients use this exact name.
    ///
    ///     BANNER:
    ///         Real-time method identifying banner messages; publishers and subscribed clients use this exact name.
    ///
    PRODUCT,
    UPDATE_GROUP,
    SCANNER_PRODUCT,
    KANBAN_CATEGORY,
    CATEGORIES = 28,
    BRAND,
    BANNER,

    ///
    /// Summary:
    ///     Invoice transaction lifecycle.
    ///
    /// Parameters:
    ///     INVOICE_OPEN_TRANSACTION:
    ///         Real-time method identifying invoice open transaction messages; publishers and subscribed clients use this exact name.
    ///
    ///     INVOICE_CLOSE_TRANSACTION:
    ///         Real-time method identifying invoice close transaction messages; publishers and subscribed clients use this exact name.
    ///
    ///     INVOICE_FINISH_TRANSACTION:
    ///         Real-time method identifying invoice finish transaction messages; publishers and subscribed clients use this exact name.
    ///
    INVOICE_OPEN_TRANSACTION = 32,
    INVOICE_CLOSE_TRANSACTION,
    INVOICE_FINISH_TRANSACTION,

    ///
    /// Summary:
    ///     Commerce operations.
    ///
    /// Parameters:
    ///     SALE_PROGRAM:
    ///         Real-time method identifying sale program messages; publishers and subscribed clients use this exact name.
    ///
    ///     PROMOTION:
    ///         Real-time method identifying promotion messages; publishers and subscribed clients use this exact name.
    ///
    ///     DISCOUNT:
    ///         Real-time method identifying discount messages; publishers and subscribed clients use this exact name.
    ///
    SALE_PROGRAM,
    PROMOTION,
    DISCOUNT,

    ///
    /// Summary:
    ///     Customer and audience updates.
    ///
    /// Parameters:
    ///     CUSTOMER:
    ///         Real-time method identifying customer messages; publishers and subscribed clients use this exact name.
    ///
    ///     MEMBERSHIP:
    ///         Real-time method identifying membership messages; publishers and subscribed clients use this exact name.
    ///
    ///     AUDIENCE_CONSENT:
    ///         Real-time method identifying audience consent messages; publishers and subscribed clients use this exact name.
    ///
    ///     AUDIENCE_CREDENTIAL:
    ///         Real-time method identifying audience credential messages; publishers and subscribed clients use this exact name.
    ///
    CUSTOMER,
    MEMBERSHIP,
    AUDIENCE_CONSENT,
    AUDIENCE_CREDENTIAL,

    ///
    /// Summary:
    ///     Marketing delivery and automation.
    ///
    /// Parameters:
    ///     MAIL_CREDENTIAL:
    ///         Real-time method identifying mail credential messages; publishers and subscribed clients use this exact name.
    ///
    ///     ZALO_CREDENTIAL:
    ///         Real-time method identifying zalo credential messages; publishers and subscribed clients use this exact name.
    ///
    ///     TELEGRAM_CREDENTIAL:
    ///         Real-time method identifying telegram credential messages; publishers and subscribed clients use this exact name.
    ///
    ///     WEB_PUSH_CREDENTIAL:
    ///         Real-time method identifying web push credential messages; publishers and subscribed clients use this exact name.
    ///
    MAIL_CREDENTIAL,
    ZALO_CREDENTIAL,
    TELEGRAM_CREDENTIAL,
    WEB_PUSH_CREDENTIAL,

    ///
    /// Summary:
    ///     Financial activity notifications.
    ///
    /// Parameters:
    ///     FINANCIAL_TIMELINE:
    ///         Real-time method identifying financial timeline messages; publishers and subscribed clients use this exact name.
    ///
    FINANCIAL_TIMELINE,

    ///
    /// Summary:
    ///     Commerce operations.
    ///
    /// Parameters:
    ///     ORDER:
    ///         Real-time method identifying order messages; publishers and subscribed clients use this exact name.
    ///
    ///     ORDER_COMPLETED:
    ///         Real-time method identifying order completed messages; publishers and subscribed clients use this exact name.
    ///
    ORDER,
    ORDER_COMPLETED,

    ///
    /// Summary:
    ///     Redirect and domain updates.
    ///
    /// Parameters:
    ///     DOMAIN:
    ///         Real-time method identifying domain messages; publishers and subscribed clients use this exact name.
    ///
    ///     REDIRECT_URL:
    ///         Real-time method identifying redirect url messages; publishers and subscribed clients use this exact name.
    ///
    ///     REDIRECT_CAMPAIGN:
    ///         Real-time method identifying redirect campaign messages; publishers and subscribed clients use this exact name.
    ///
    ///     REDIRECT_TEMPLATE:
    ///         Real-time method identifying redirect template messages; publishers and subscribed clients use this exact name.
    ///
    ///     REDIRECT_WEBHOOK:
    ///         Real-time method identifying redirect webhook messages; publishers and subscribed clients use this exact name.
    ///
    DOMAIN,
    REDIRECT_URL,
    REDIRECT_CAMPAIGN,
    REDIRECT_TEMPLATE,
    REDIRECT_WEBHOOK,

    ///
    /// Summary:
    ///     Warehouse operations.
    ///
    /// Parameters:
    ///     WAREHOUSE:
    ///         Real-time method identifying warehouse messages; publishers and subscribed clients use this exact name.
    ///
    ///     WAREHOUSE_GOODS_RECEIPT:
    ///         Real-time method identifying warehouse goods receipt messages; publishers and subscribed clients use this exact name.
    ///
    ///     WAREHOUSE_GOODS_ISSUE:
    ///         Real-time method identifying warehouse goods issue messages; publishers and subscribed clients use this exact name.
    ///
    ///     WAREHOUSE_STOCK_COUNT:
    ///         Real-time method identifying warehouse stock count messages; publishers and subscribed clients use this exact name.
    ///
    ///     WAREHOUSE_INVENTORY_ADJUSTMENT:
    ///         Real-time method identifying warehouse inventory adjustment messages; publishers and subscribed clients use this exact name.
    ///
    WAREHOUSE,
    WAREHOUSE_GOODS_RECEIPT,
    WAREHOUSE_GOODS_ISSUE,
    WAREHOUSE_STOCK_COUNT,
    WAREHOUSE_INVENTORY_ADJUSTMENT,
}
