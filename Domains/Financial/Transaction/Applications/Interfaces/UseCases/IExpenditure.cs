namespace Wks.Interfaces;

public interface IExpenditureExpenseTransaction
{
    Task<IEnumerable<DashboardObject.Value>> ExecuteAsync(Guid profileId, Guid categoryId);
}
