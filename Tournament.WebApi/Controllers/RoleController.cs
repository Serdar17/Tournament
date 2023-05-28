using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tournament.Application.Dto;
using Tournament.Application.Dto.Auth;
using Tournament.Application.Interfaces;

namespace Tournament.Controllers;

public class RoleController : ApiController
{
    private readonly ILogger<RoleController> _logger;
    private readonly IMapper _mapper;
    private readonly IAccountManager _accountManager;
    
    public RoleController(ILogger<RoleController> logger, IMapper mapper, IAccountManager accountManager)
    {
        _logger = logger;
        _mapper = mapper;
        _accountManager = accountManager;
    }
    
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.Admin)]
    [HttpPost("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterRoleModel registerRoleModel)
    {
        _logger.LogInformation("Entity from client \"{Name}\" {@RegisterRoleModel}",
            nameof(RegisterRoleModel), registerRoleModel);
        var response = await _accountManager.RegisterAdminAsync(registerRoleModel);

        if (response.IsSuccess)
        {
            return Ok(response.Value);
        }

        return StatusCode(StatusCodes.Status400BadRequest, new Response()
        {
            Status = "Error",
            Message = response.Errors.FirstOrDefault()
        });
    }
    
    [HttpPost("register-referee")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.Admin)]
    public async Task<IActionResult> RegisterManager([FromBody] RegisterRoleModel registerRoleModel)
    {
        _logger.LogInformation("Entity from client \"{Name}\" {@RegisterRoleModel}",
            nameof(RegisterRoleModel), registerRoleModel);
        
        var response = await _accountManager.RegisterRefereeAsync(registerRoleModel);
        
        if (response.IsSuccess)
        {
            return Ok(response.Value);
        }

        return StatusCode(StatusCodes.Status400BadRequest, new Response()
        {
            Status = "Error",
            Message = response.Errors.FirstOrDefault()
        });
    }
}