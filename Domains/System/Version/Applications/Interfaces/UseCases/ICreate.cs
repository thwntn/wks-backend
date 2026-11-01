namespace Wks.Interfaces;

public interface ICreateVersion
{
    Task<AppVersion> ExecuteAsync(VersionObject.CreateParams createParams);
}
