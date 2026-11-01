namespace Wks.Interfaces;

public interface IGenerateTextIntelligent
{
    Task<string> ExecuteAsync(string prompt);
}
