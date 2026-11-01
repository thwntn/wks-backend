namespace Wks.Interfaces;

///
/// Summary:
///     Interface for retrieving recent transactions for home dashboard.
///
public interface IRecentTransactionHomeFinancial
{
    ///
    /// Summary:
    ///     Retrieves the 10 most recent transactions for a specific profile.
    ///
    /// Parameters:
    ///     profileId:
    ///         The identifier of the profile to retrieve transactions for.
    ///
    /// Returns:
    ///     List of recent transaction objects ordered by date descending.
    ///
    Task<List<HomeFinancialObject.RecentTransaction>> ExecuteAsync(Guid profileId);
}
