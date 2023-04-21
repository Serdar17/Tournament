using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Tournament.Application.Dto;
using Tournament.Application.Interfaces;

namespace Tournament.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class AccountController: ApiController
{
    private readonly IParticipantService _service;
    private readonly ILogger<AccountController> _logger;
    private readonly IMapper _mapper;

    public AccountController(IParticipantService service, ILogger<AccountController> logger, IMapper mapper)
    {
        _service = service;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ParticipantInfoModel>), StatusCodes.Status200OK)]
    public IEnumerable<ParticipantInfoModel> GetAll()
    {
        var participaints = _service.GetAll();

        var result = participaints.Select(x => _mapper.Map<ParticipantInfoModel>(x));
        
        return result;
    }
    
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ParticipantInfoModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetParticipantByIdAsync(id);

        if (result.IsSuccess)
        {
            var model = _mapper.Map<ParticipantInfoModel>(result.Value);
            return Ok(model);
        }

        return StatusCode(StatusCodes.Status404NotFound, new Response()
        {
            Status = "Not Found",
            Message = result.Errors.FirstOrDefault()
        });
    }

    [HttpPatch("{id:guid}")]
    [ProducesResponseType(typeof(ParticipantInfoModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateParticipant(Guid id, [FromBody] 
        JsonPatchDocument<ParticipantInfoModel> patch)
    {
        if (patch is null)
        {
            _logger.LogError("Patch object sent from client is null.");
            return BadRequest("Patch object is null");
        }

        var result = await _service.PatchParticipantAsync(id, patch);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return StatusCode(StatusCodes.Status404NotFound, new Response()
        {
            Status = "Not Found",
            Message = result.Errors.FirstOrDefault()
        });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteParticipant(Guid id)
    {
        var result = await _service.DeleteParticipantByIdAsync(id);

        if (result.IsSuccess)
        {
            return StatusCode(StatusCodes.Status204NoContent);
        }
        
        return StatusCode(StatusCodes.Status404NotFound, new Response()
        {
            Status = "Not found",
            Message = result.Errors.FirstOrDefault()
        });
    }
}