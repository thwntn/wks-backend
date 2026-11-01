namespace Wks.Interfaces;

public interface IGetProfile
{
    Task<Profile> ExecuteAsync(Guid profileId);
}
