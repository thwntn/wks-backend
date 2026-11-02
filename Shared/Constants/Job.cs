namespace Shared;

///
/// Summary:
///     Defines scheduled job identifiers grouped by application responsibility.
///
public enum JobNames
{
    ///
    /// Summary:
    ///     Redirect traffic, campaign and delivery maintenance.
    ///
    /// Parameters:
    ///     RedirectDailyTraffic:
    ///         Scheduled job identifier for redirect daily traffic processing; used to register and address the recurring task.
    ///
    ///     RedirectTemplateStatistic:
    ///         Scheduled job identifier for redirect template statistic processing; used to register and address the recurring task.
    ///
    RedirectDailyTraffic,
    RedirectTemplateStatistic,

    ///
    /// Summary:
    ///     Audience membership maintenance.
    ///
    /// Parameters:
    ///     AudienceMemberUpgrade:
    ///         Scheduled job identifier for audience member upgrade processing; used to register and address the recurring task.
    ///
    AudienceMemberUpgrade,

    ///
    /// Summary:
    ///     Broadcast statistics.
    ///
    /// Parameters:
    ///     BroadcastDailyStatistic:
    ///         Scheduled job identifier for broadcast daily statistic processing; used to register and address the recurring task.
    ///
    BroadcastDailyStatistic,

    ///
    /// Summary:
    ///     Redirect traffic, campaign and delivery maintenance.
    ///
    /// Parameters:
    ///     RedirectCampaignStatus:
    ///         Scheduled job identifier for redirect campaign status processing; used to register and address the recurring task.
    ///
    RedirectCampaignStatus,

    ///
    /// Summary:
    ///     Automation cleanup.
    ///
    /// Parameters:
    ///     AutomationClearNode:
    ///         Scheduled job identifier for automation clear node processing; used to register and address the recurring task.
    ///
    AutomationClearNode,

    ///
    /// Summary:
    ///     Redirect traffic, campaign and delivery maintenance.
    ///
    /// Parameters:
    ///     RedirectDeliveryClear:
    ///         Scheduled job identifier for redirect delivery clear processing; used to register and address the recurring task.
    ///
    RedirectDeliveryClear,

    ///
    /// Summary:
    ///     Storefront banner lifecycle.
    ///
    /// Parameters:
    ///     BannerExpiredStatus:
    ///         Scheduled job identifier for banner expired status processing; used to register and address the recurring task.
    ///
    BannerExpiredStatus,

    ///
    /// Summary:
    ///     Zalo credential renewal.
    ///
    /// Parameters:
    ///     ZaloCredentialRefreshToken:
    ///         Scheduled job identifier for zalo credential refresh token processing; used to register and address the recurring task.
    ///
    ZaloCredentialRefreshToken,

    ///
    /// Summary:
    ///     Warehouse receipt completion.
    ///
    /// Parameters:
    ///     GoodsReceiptAutoComplete:
    ///         Scheduled job identifier for goods receipt auto complete processing; used to register and address the recurring task.
    ///
    GoodsReceiptAutoComplete,
}
