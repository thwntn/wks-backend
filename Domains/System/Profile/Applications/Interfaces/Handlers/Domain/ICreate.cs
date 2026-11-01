namespace Wks.Interfaces;

public interface ICreateDomainHandler
{
    Task<Domain> ExecuteAsync(
        Guid profileId,
        DomainDataTransformer.Create create
    );
}
