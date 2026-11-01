namespace Wks.Interfaces;

public interface IChangeCoverPictureProfile
{
    Task<Account> ExecuteAsync(Guid profileId, string artifactToken);
}
