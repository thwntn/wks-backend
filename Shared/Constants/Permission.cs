namespace Wks.Mains;

///
/// Summary:
///     Defines permission codes used by the retained WKS authorization policies.
///     Policies and persisted role permissions use these member names as string codes.
///
public enum PermissionConstant
{
    ///
    /// Summary:
    ///     Allows access to actor account and actor role management.
    ///
    ACTOR,
    ///
    /// Summary:
    ///     Allows platform administration, including profiles, roles, settings, domains and versions.
    ///
    ADMIN_SETTING,
    ///
    /// Summary:
    ///     Allows access to the Financial application.
    ///
    APPLICATION_FINANCIAL,
    ///
    /// Summary:
    ///     Allows financial timelines, expense categories and transaction management.
    ///
    FINANCIAL_EXPENSE,
    ///
    /// Summary:
    ///     Allows financial reporting, analytics and dashboards.
    ///
    FINANCIAL_DASHBOARD,
    ///
    /// Summary:
    ///     Allows the Financial overview on the WKS home dashboard.
    ///
    HOME_OVERVIEW,
}
