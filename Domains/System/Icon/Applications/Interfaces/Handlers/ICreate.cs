namespace Wks.Interfaces;

public interface ICreateIconHandler
{
    Task<Icon> ExecuteAsync(IconDataTransformer.Create create);
}
