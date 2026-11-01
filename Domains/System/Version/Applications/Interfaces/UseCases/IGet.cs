namespace Wks.Interfaces;

public interface IGetVersion
{
    Task<AppVersion> ExecuteAsync(Guid versionId);
}
