namespace Wks.Interfaces;

public interface IUpdateProfileHandler
{
    Task<Account> ExecuteAsync(
        Guid profileId,
        ProfileDataTransformer.Update update
    );
}
