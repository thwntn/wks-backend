namespace Wks.Interfaces;

public interface IChangeAvatarProfile
{
    Task<Account> ExecuteAsync(Guid profileId, string artifactToken);
}
