namespace Wks.Interfaces;

public interface ICreateVersionHandler
{
    Task<AppVersion> ExecuteAsync(VersionObject.CreateParams createParams);
}
