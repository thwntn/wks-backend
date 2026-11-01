namespace Wks.Interfaces;

public interface IChangeCoverPictureHandler
{
    Task<Account> ExecuteAsync(Guid profileId, string artifactToken);
}
