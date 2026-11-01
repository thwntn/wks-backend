namespace Wks.Interfaces;

public interface IGroupExpenseFinancialAnalytics
{
    Task<List<Expense>> ExecuteAsync(
        Guid profileId,
        FinancialAnalyticsDataTransformer.GroupExpenseOptions groupExpenseOptions
    );
}
