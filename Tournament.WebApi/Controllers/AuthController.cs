using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Application.Dto;
using Tournament.Application.Interfaces;
using Tournament.Domain.Models.Participants;

namespace Tournament.Controllers;

public sealed class AuthController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IAccountManager _accountManager;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IMapper mapper, IAccountManager accountManager, ILogger<AuthController> logger)
    {
        _mapper = mapper;
        _accountManager = accountManager;
        _logger = logger;
    }
    
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
    {
        _logger.LogInformation("Entity from client \"{Name}\" {@RegisterModel}",
            nameof(RegisterModel), registerModel);
        
        var participant = _mapper.Map<Participant>(registerModel);

        var result = await _accountManager.RegistrationAsync(participant);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        
        return StatusCode(StatusCodes.Status400BadRequest,
            new Response()
            {
                Status = "Error",
                Message = result.Errors.FirstOrDefault()
            });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        _logger.LogInformation("Entity from client \"{Name}\" {@LoginModel}",
            nameof(LoginModel), loginModel);
        
        var result = await _accountManager.LoginAsync(loginModel);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return StatusCode(StatusCodes.Status400BadRequest, new Response()
        {
            Status = "Error",
            Message = result.Errors.FirstOrDefault()
        });
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.Admin)]
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

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.Admin)]
    [HttpPost("register-referee")]
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

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(TokenApiModel tokenApiModel)
    {
        _logger.LogInformation("Entity from client \"{Name}\" {@TokenApiModel}",
            nameof(TokenApiModel), tokenApiModel);
        
        var result = await _accountManager.RefreshTokenAsync(tokenApiModel);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return StatusCode(StatusCodes.Status400BadRequest, new Response()
        {
            Status = "Error",
            Message = result.Errors.FirstOrDefault()
        });
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("revoke/{id:guid}")]
    public async Task<IActionResult> RevokeToken([FromRoute] Guid id)
    {
        var result = await _accountManager.RevokeRefreshTokenByIdAsync(id);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return StatusCode(StatusCodes.Status400BadRequest, new Response()
        {
            Status = "Error",
            Message = result.Errors.FirstOrDefault()
        });
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.Admin)]
    [HttpPost("revoke-all")]
    public async Task<IActionResult> RevokeAll()
    {
        var result = await _accountManager.RevokeAllRefreshTokenAsync();

        return Ok(result.Value);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet(nameof(GetResult))]
    public IActionResult GetResult()
    {
        return Ok("API Validated");
    }
}