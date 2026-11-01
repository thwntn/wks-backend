namespace Wks.Interfaces;

public interface ICreateDomain
{
    Task<Domain> ExecuteAsync(
        Guid profileId,
        DomainDataTransformer.Create create
    );
}
