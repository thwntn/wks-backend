namespace Wks.Interfaces;

///
/// Summary:
///     Use case interface for retrieving grouped accounts in organizational hierarchy.
///     Defines contract for fetching accounts within the same group structure.
///
public interface IGetGroupToken
{
    ///
    /// Summary:
    ///     Retrieves all accounts belonging to the authenticated user's group hierarchy.
    ///     Returns accounts based on parent-child relationship structure.
    ///
    /// Returns:
    ///     An enumerable collection of Account objects with profile information included.
    ///
    IEnumerable<Account> Execute();
}
