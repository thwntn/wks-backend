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
}
