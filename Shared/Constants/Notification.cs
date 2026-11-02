namespace Shared;

///
/// Summary:
///     Defines notification codes grouped by application responsibility.
///
public enum NotificationNames
{
    ///
    /// Summary:
    ///     General notifications and uploads.
    ///
    /// Parameters:
    ///     DEFAULT:
    ///         Notification code identifying default events in notification payloads.
    ///
    DEFAULT = 1,

    ///
    /// Summary:
    ///     Group membership changes.
    ///
    /// Parameters:
    ///     REMOVE_FROM_GROUP:
    ///         Notification code identifying remove from group events in notification payloads.
    ///
    ///     INVITE_TO_GROUP:
    ///         Notification code identifying invite to group events in notification payloads.
    ///
    REMOVE_FROM_GROUP = 2,
    INVITE_TO_GROUP = 3,

    ///
    /// Summary:
    ///     General notifications and uploads.
    ///
    /// Parameters:
    ///     UPLOADABLE:
    ///         Notification code identifying uploadable events in notification payloads.
    ///
    UPLOADABLE = 4,

    ///
    /// Summary:
    ///     Subscription expiry and reminders.
    ///
    /// Parameters:
    ///     EXPIRE_PLAN:
    ///         Notification code identifying expire plan events in notification payloads.
    ///
    EXPIRE_PLAN = 5,

    ///
    /// Summary:
    ///     Expense transaction activity.
    ///
    /// Parameters:
    ///     CREATED_EXPENSE_TRANSACTION:
    ///         Notification code identifying created expense transaction events in notification payloads.
    ///
    ///     EXPENSE_TRANSACTION_CREATE:
    ///         Notification code identifying expense transaction create events in notification payloads.
    ///
    CREATED_EXPENSE_TRANSACTION = 6,
    EXPENSE_TRANSACTION_CREATE = 7,

    ///
    /// Summary:
    ///     Subscription expiry and reminders.
    ///
    /// Parameters:
    ///     PLAN_REMINDER:
    ///         Notification code identifying plan reminder events in notification payloads.
    ///
    PLAN_REMINDER = 8,

    ///
    /// Summary:
    ///     Redirect campaign export results.
    ///
    /// Parameters:
    ///     REDIRECT_EXPORT_CAMPAIGN:
    ///         Notification code identifying redirect export campaign events in notification payloads.
    ///
    ///     REDIRECT_EXPORT_CAMPAIGN_FAILED:
    ///         Notification code identifying redirect export campaign failed events in notification payloads.
    ///
    REDIRECT_EXPORT_CAMPAIGN = 9,
    REDIRECT_EXPORT_CAMPAIGN_FAILED = 10,
}
