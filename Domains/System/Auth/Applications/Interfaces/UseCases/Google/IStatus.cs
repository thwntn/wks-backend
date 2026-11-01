namespace Wks.Interfaces;

public interface IStatusSetupGoogleAuth
{
    Task<bool> ExecuteAsync(Guid accountId);
}
