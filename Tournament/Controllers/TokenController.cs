using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Tournament.Dto;
using Tournament.Services;

namespace Tournament.Controllers;

public sealed class TokenController: ApiController
{
    private readonly ITokenService _tokenService;
    // private readonly IParticipantService _participantService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
        // _participantService = participantService;
    }

    // [HttpPost("refresh")]
    // public IActionResult Refresh(TokenApiModel tokenApiModel)
    // {
    //     var principal = _tokenService.GetPrincipalFromExpiredToken(tokenApiModel.AccessToken);
    //     var claim = principal.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Email));
    //
    //     var entity = _participantService.GetParticipantByUserName(claim.Value);
    //
    //     if (entity is null)
    //     {
    //         return BadRequest("Tokens is not valid");
    //     }
    //
    //     var newAccessToken = _tokenService.GenerateAccessToken(entity);
    //     var newRefreshToken = _tokenService.GenerateRefreshToken(entity);
    //
    //     return Ok(new TokenApiModel
    //     {
    //         AccessToken = newAccessToken,
    //         RefreshToken = newRefreshToken
    //     });
    // }
}