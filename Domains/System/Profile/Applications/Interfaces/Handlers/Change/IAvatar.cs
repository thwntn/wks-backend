namespace Wks.Interfaces;

public interface IChangeAvatarHandler
{
    Task<Account> ExecuteAsync(Guid profileId, string artifactToken);
}
