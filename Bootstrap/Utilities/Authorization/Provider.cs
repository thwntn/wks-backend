namespace Wks.Mains;

///
/// Summary:
///     Builds authorization policies dynamically from a policy name.
///     The policy name is a delimiter-joined list of permission codes produced
///     by PermissionAttribute; each resolves to a single PermissionRequirement.
///
public sealed class PermissionPolicyProvider(
    IOptions<AuthorizationOptions> options
) : IAuthorizationPolicyProvider
{
    private readonly DefaultAuthorizationPolicyProvider _fallback = new(
        options
    );

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() =>
        _fallback.GetDefaultPolicyAsync();

    public Task<AuthorizationPolicy> GetFallbackPolicyAsync() =>
        _fallback.GetFallbackPolicyAsync();

    public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        string[] permissions = policyName.Split(
            QueryOptions.PREFIX_SPLIT,
            StringSplitOptions.RemoveEmptyEntries
        );

        AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(permissions))
            .Build();

        return Task.FromResult(policy);
    }
}
