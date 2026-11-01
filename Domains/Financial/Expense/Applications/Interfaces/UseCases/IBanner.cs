namespace Wks.Interfaces;

public interface IBannerExpense
{
    Task<ExpenseObject.Banner> ExecuteAsync(Guid profileId, Guid timelineId);
}
