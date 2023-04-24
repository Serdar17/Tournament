using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Application.Competitions.Commands.CreateCompetition;
using Tournament.Application.Competitions.Commands.DeleteCompetition;
using Tournament.Application.Competitions.Commands.UpdateCompetition;
using Tournament.Application.Competitions.Queries.GetCompetitionDetails;
using Tournament.Application.Competitions.Queries.GetCompetitionList;
using Tournament.Domain.Models.Participants;
using Tournament.Models.Competition;

namespace Tournament.Controllers;

public sealed class CompetitionController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public CompetitionController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.Manager)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CompetitionVm>> Get(Guid id)
    {
        var query = new GetCompetitionDetailsQuery()
        {
            Id = id
        };

        var result = await _sender.Send(query);

        return Ok(result);
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CompetitionListVm>> GetAll()
    {
        var query = new GetCompetitionInfoListQuery();

        var result = await _sender.Send(query);

        return Ok(result);
    }

    [HttpPost("create")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.Manager)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateCompetitionDto createCompetitionDto)
    {
        var command = _mapper.Map<CreateCompetitionCommand>(createCompetitionDto);

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Ok();
        
        return BadRequest(result.Errors);
    }

    [HttpPut("update")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.Manager)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromBody] UpdateCompetitionDto updateCompetitionDto)
    {
        var command = _mapper.Map<UpdateCompetitionCommand>(updateCompetitionDto);

        await _sender.Send(command);

        return NoContent();
    }

    [HttpDelete("delete/{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.Manager)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteCompetitionCommand()
        {
            Id = id
        };

        await _sender.Send(command);

        return NoContent();
    }
}