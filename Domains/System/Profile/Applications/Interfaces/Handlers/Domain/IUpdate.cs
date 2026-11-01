namespace Wks.Interfaces;

public interface IUpdateDomainHandler
{
    Task<Domain> ExecuteAsync(
        Guid profileId,
        DomainDataTransformer.Update update
    );
}
