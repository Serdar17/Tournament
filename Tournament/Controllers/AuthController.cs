using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Dto;
using Tournament.Models;
using Tournament.Services;

namespace Tournament.Controllers;

public sealed class AuthController : ApiController
{
    private readonly IMapper _mapper;
    private readonly IAccountManager _manager;
    
    public AuthController(IMapper mapper, IAccountManager manager)
    {
        _mapper = mapper;
        _manager = manager;
    }
    
    [HttpPost("register")]
    [AllowAnonymous]
    public IActionResult Register([FromBody] RegisterModel registerModel)
    {
        var participant = _mapper.Map<Participant>(registerModel);

        var result = _manager.RegistrationAsync(participant);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.ValidationErrors);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginModel loginModel)
    {
        var result = _manager.LoginAsync(loginModel);
        
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.ValidationErrors);
    }
    
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet(nameof(GetResult))]
    public IActionResult GetResult()
    {
        return Ok("API Validated");
    }
}