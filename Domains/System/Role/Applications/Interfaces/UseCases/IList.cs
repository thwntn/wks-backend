namespace Wks.Interfaces;

///
/// Summary:
///     Interface for the list roles use case.
///     Defines the contract for retrieving all roles belonging to an account.
///
public interface IListRoles
{
    Task<List<Role>> ExecuteAsync(Guid accountId);
}
