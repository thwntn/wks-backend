namespace Wks.Mains;

///
/// Summary:
///     Authorization requirement carrying the permission codes accepted by a policy.
///     Evaluated by PermissionHandler, which grants access when the account owns
///     at least one of the listed codes.
///
///
/// Summary:
///     Initializes the requirement with the accepted permission codes.
///
/// Parameters:
///   permissions:
///     The permission codes that satisfy this requirement.
///
public sealed class PermissionRequirement(string[] permissions)
    : IAuthorizationRequirement
{
    ///
    /// Summary:
    ///     The permission codes accepted by the policy; the account needs at least one.
    ///
    public string[] Permissions { get; } = permissions;
}
