namespace Wks.Interfaces;

public interface IGetFinancialReport
{
    Task<ReportObject.Statement> ExecuteAsync(Guid profileId, DateTime from, DateTime to);
}
