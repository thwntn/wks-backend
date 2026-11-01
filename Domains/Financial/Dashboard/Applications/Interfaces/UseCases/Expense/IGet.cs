namespace Wks.Interfaces;

public interface IGetExpenseAnalyst
{
    Task<List<ReportObject.TimelineCounter>> ExecuteAsync(Guid profileId);
}
