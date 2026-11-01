namespace Wks.Controllers;

[ApiController, Route(RouteConstant.PROFILE), Tags(SwaggerConstant.PROFILES)]
public class ProfileController(
    IMeProfile _meProfile,
    IUpdateProfileHandler _updateProfileHandler,
    IChangeAvatarHandler _changeAvatarHandler,
    IChangeCoverPictureHandler _changeCoverPictureHandler
) : ControllerBase
{
    [Authorize, HttpGet(RouteConstant.PROFILE_ME)]
    public async Task<IActionResult> Me()
    {
        Guid identifier = await HttpContext.Identifier(
            personal: true
        );
        Account information = await _meProfile.ExecuteAsync(identifier);
        AuthObject.UsePasswordResponse usePasswordResponse =
            information.MapTo<AuthObject.UsePasswordResponse>();
        return Ok(usePasswordResponse);
    }

    [Authorize, HttpPatch]
    public async Task<IActionResult> Update(
        [FromBody] ProfileDataTransformer.Update update
    )
    {
        Guid identifier = await HttpContext.Identifier(
            personal: true
        );
        Account info = await _updateProfileHandler.ExecuteAsync(
            identifier,
            update
        );
        return Ok(info);
    }

    [Authorize, HttpPost(RouteConstant.PROFILE_UPDATE_AVATAR)]
    public async Task<IActionResult> ChangeAvatar(
        [FromBody] ProfileDataTransformer.UpdateAvatar updateAvatarTransformer
    )
    {
        Guid identifier = await HttpContext.Identifier(
            personal: true
        );
        Account user = await _changeAvatarHandler.ExecuteAsync(
            identifier,
            updateAvatarTransformer.ArtifactToken
        );
        return Ok(user);
    }

    [Authorize, HttpPost(RouteConstant.PROFILE_UPDATE_COVER_PICTURE)]
    public async Task<IActionResult> ChangeCoverPicture(
        [FromBody] ProfileDataTransformer.UpdateCover updateCoverTransformer
    )
    {
        Guid identifier = await HttpContext.Identifier(
            personal: true
        );
        Account user = await _changeCoverPictureHandler.ExecuteAsync(
            identifier,
            updateCoverTransformer.ArtifactToken
        );
        return Ok(user);
    }
}
