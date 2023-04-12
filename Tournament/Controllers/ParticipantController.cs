using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Services;

namespace Tournament.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("[controller]")]
public class ParticipantController : ControllerBase
{
    private readonly ILogger<ParticipantController> _logger;
    private readonly IParticipantService _participantService;

    public ParticipantController(ILogger<ParticipantController> logger, IParticipantService participantService)
    {
        _logger = logger;
        _participantService = participantService;
    }

    [HttpGet("get-all")]
    public IActionResult GetAll()
    {
        var participants = _participantService.GetAll();
        return Ok(participants);
    }
    
    
    
    // [HttpPost("create")]
    // public IActionResult Create(Participant participant)
    // {
    //
    //     return Ok();
    //
    // }

    // [HttpGet("getall")]
    // public IEnumerable<Participant> GetAll()
    // {
    //     var all = _dbContext.Participants.ToList();
    //     return all;
    // }
}