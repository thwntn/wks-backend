namespace Wks.Mains;

///
/// Summary:
///     Authorization attribute that guards an endpoint with one or more permission codes.
///     The supplied codes are delimiter-joined into a policy name so PermissionPolicyProvider
///     can rebuild them into a single PermissionRequirement at authorization time.
///     Access is granted when the account owns at least one of the given codes.
///
public sealed class PermissionAttribute : AuthorizeAttribute
{
    ///
    /// Summary:
    ///     Initializes the attribute with the accepted permission codes.
    ///
    /// Parameters:
    ///   permissions:
    ///     The permission codes required to access the endpoint; the account
    ///     needs at least one of them.
    ///
    public PermissionAttribute(params string[] permissions)
    {
        Policy = string.Join(QueryOptions.PREFIX_SPLIT, permissions);
    }
}
