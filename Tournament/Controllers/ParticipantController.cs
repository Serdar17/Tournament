using Microsoft.AspNetCore.Mvc;
using Tournament.DbContext;
using Tournament.Models;
using Tournament.Services;

namespace Tournament.Controllers;

[ApiController]
[Route("[controller]")]
public class ParticipantController : ControllerBase
{
    private readonly ILogger<ParticipantController> _logger;
    // private readonly IParticipantService _participantService;
    private readonly ApplicationDbContext _dbContext;

    public ParticipantController(ILogger<ParticipantController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        // _participantService = participantService;
        _dbContext = dbContext;
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