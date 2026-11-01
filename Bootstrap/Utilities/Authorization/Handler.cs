using System.Security.Claims;

namespace Wks.Mains;

///
/// Summary:
///     Authorization handler that evaluates whether the current account
///     holds at least one of the permission codes required by the policy.
///
public sealed class PermissionHandler(ICheckPermission _checkPermission)
    : AuthorizationHandler<PermissionRequirement>
{
    ///
    /// Summary:
    ///     Evaluates the permission requirement against the authenticated account.
    ///     Marks the requirement as succeeded when the account owns a matching permission.
    ///
    /// Parameters:
    ///     context:
    ///         The authorization context carrying the current user claims principal.
    ///
    ///     requirement:
    ///         The permission requirement holding the accepted permission codes.
    ///
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement
    )
    {
        /// Logic:
        ///     Read the account identifier claim from the JWT principal.
        ///     The claim name matches the UserJsonWebToken.Identifier property.
        ///
        Claim subject = context.User.FindFirst(
            nameof(UserJsonWebToken.Identifier)
        );

        /// Logic:
        ///     Guard against a missing identifier claim.
        ///     When the token carries no account identifier, leave the requirement
        ///     unhandled so the pipeline denies the request instead of throwing.
        ///
        if (subject.Justifiable() is false)
            return;

        /// Logic:
        ///     Delegate the permission check to the domain use case.
        ///     Parse the claim value into the account GUID and pass the required
        ///     permission codes; the use case returns true when the account holds
        ///     at least one of them.
        ///     On success, mark the requirement satisfied so the pipeline authorizes
        ///     the request; otherwise leave it unhandled to deny access.
        ///
        bool validated = await _checkPermission.ExecuteAsync(
            Guid.Parse(subject.Value),
            requirement.Permissions.ToList()
        );

        if (validated)
            context.Succeed(requirement);
    }
}
