namespace Wks.Interfaces;

///
/// Summary:
///     Interface for retrieving financial statistics counter for home dashboard.
///
public interface ICounterHomeFinancial
{
    ///
    /// Summary:
    ///     Retrieves financial counter statistics for a specific profile.
    ///
    /// Parameters:
    ///     profileId:
    ///         The identifier of the profile to retrieve statistics for.
    ///
    /// Returns:
    ///     Counter object containing income, spending, and transaction count.
    ///
    Task<HomeFinancialObject.Counter> ExecuteAsync(Guid profileId);
}
