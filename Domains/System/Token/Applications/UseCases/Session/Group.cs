namespace Wks.UseCases;

///
/// Summary:
///     Retrieves all accounts within the same organizational group as the authenticated user.
///     Implements hierarchical account grouping based on parent-child relationships.
///
public class GetGroupToken(
    ISessionTokenInformation _getInformation,
    DatabaseContext _databaseContext
) : IGetGroupToken
{
    ///
    /// Summary:
    ///     Retrieves all accounts belonging to the authenticated user's group hierarchy.
    ///     Includes both parent and child accounts based on the parent account identifier.
    ///
    /// Returns:
    ///     An enumerable collection of Account objects with profile information included.
    ///
    public IEnumerable<Account> Execute()
    {
        /// Logic:
        ///     Extract user information from current session JWT token.
        ///
        UserJsonWebToken information = _getInformation.Execute();

        /// Logic:
        ///     Determine parent account identifier, defaulting to empty GUID if not set.
        ///
        Guid parentId =
            information.ParentId == Guid.Empty
                ? Guid.Empty
                : information.ParentId;

        /// Logic:
        ///     Query database for accounts matching group hierarchy.
        ///     Include profile information and filter by parent-child relationships.
        ///
        IEnumerable<Account> accounts = _databaseContext
            .Account.Include(account => account.Profile)
            .Where(account =>
                account.Identifier == parentId || account.Id == parentId
            )
            .AsEnumerable();

        return accounts;
    }
}
