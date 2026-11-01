namespace Wks.Interfaces;

public interface IUpdateProfile
{
    Account Execute(Guid profileId, ProfileDataTransformer.Update update);
}
