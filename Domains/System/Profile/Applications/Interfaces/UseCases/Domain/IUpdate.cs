namespace Wks.Interfaces;

public interface IUpdateDomain
{
    Task<Domain> ExecuteAsync(
        Guid profileId,
        DomainDataTransformer.Update update
    );
}
