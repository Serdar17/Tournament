using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Application.Competitions.Commands.CreateCompetitionInfo;
using Tournament.Application.Competitions.Commands.DeleteCompetitionInfo;
using Tournament.Application.Competitions.Commands.UpdateCompetitionInfo;
using Tournament.Application.Competitions.Queries.GetCompetitionInfoDetail;
using Tournament.Application.Competitions.Queries.GetCompetitionInfoDetails;
using Tournament.Application.Competitions.Queries.GetCompetitionInfoList;
using Tournament.Domain.Models.Participant;
using Tournament.Models.Competition;

namespace Tournament.Controllers;

public sealed class CompetitionController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CompetitionController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.Manager)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CompetitonInfoVm>> Get(Guid id)
    {
        var query = new GetCompetitionInfoDetailsQuery()
        {
            Id = id
        };

        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CompetitionInfoListVm>> GetAll()
    {
        var query = new GetCompetitionInfoListQuery();

        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost("create")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.Manager)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateCompetitionDto createCompetitionDto)
    {
        var command = _mapper.Map<CreateCompetitionInfoCommand>(createCompetitionDto);

        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpPut("update")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.Manager)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromBody] UpdateCompetitionDto updateCompetitionDto)
    {
        var command = _mapper.Map<UpdateCompetitionInfoCommand>(updateCompetitionDto);

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("delete/{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.Manager)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteCompetitionInfoCommand()
        {
            Id = id
        };

        await _mediator.Send(command);

        return NoContent();
    }
}