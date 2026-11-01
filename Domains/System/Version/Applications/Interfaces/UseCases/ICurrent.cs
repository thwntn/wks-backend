namespace Wks.Interfaces;

public interface ICurrentVersion
{
    Task<AppVersion> ExecuteAsync();
}
