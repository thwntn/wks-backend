namespace Wks.Interfaces;

///
/// Summary:
///     Interface for retrieving spending grouped by expense category for home dashboard.
///
public interface ISpendingByExpenseHomeFinancial
{
    ///
    /// Summary:
    ///     Retrieves spending data grouped by expense category.
    ///
    /// Parameters:
    ///     profileId:
    ///         The identifier of the profile to retrieve data for.
    ///
    /// Returns:
    ///     List of spending by expense objects with name, total, and color.
    ///
    Task<List<HomeFinancialObject.SpendingByExpense>> ExecuteAsync(Guid profileId);
}
