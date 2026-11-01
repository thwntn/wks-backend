namespace Wks.Interfaces;

///
/// Summary:
///     Interface for retrieving monthly spending chart data for home dashboard.
///
public interface IMonthlyChartHomeFinancial
{
    ///
    /// Summary:
    ///     Retrieves monthly spending data grouped by month.
    ///
    /// Parameters:
    ///     profileId:
    ///         The identifier of the profile to retrieve data for.
    ///
    /// Returns:
    ///     List of monthly spending objects with month and amount.
    ///
    Task<List<HomeFinancialObject.MonthlySpending>> ExecuteAsync(Guid profileId);
}
