namespace Wks.Controllers;

/// <summary>Generates Financial transaction descriptions for accounts with expense access.</summary>
[ApiController, Route(RouteConstant.INTELLIGENCE_TEXT), Tags(SwaggerConstant.INTELLIGENCE_TEXT)]
public class IntelligenceTextController(ITemplateGenerateIntelligence _templateGenerateIntelligence) : ControllerBase
{
    /// <summary>Generates text from the Financial template and supplied transaction name and amount.</summary>
    [Permission(nameof(PermissionConstant.FINANCIAL_EXPENSE)), HttpPost(RouteConstant.INTELLIGENCE_TEXT_TEMPLATE)]
    public async Task<IActionResult> Template([FromBody] IntelligenceTextDataTransformer.TemplateGenerate request)
    {
        string result = await _templateGenerateIntelligence.ExecuteAsync(request.Input, request.Template);
        return Ok(result);
    }
}
