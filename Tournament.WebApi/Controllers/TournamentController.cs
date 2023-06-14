using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Application.Dto.Competitions.ConfirmedMatchResult;
using Tournament.Application.Features.Players.Commands.DeletePlayer;
using Tournament.Application.Features.Players.Queries.GetCompetitionPlayers;
using Tournament.Application.Interfaces;
using Tournament.Application.Tournament.Commands.GenerateSchedule;
using Tournament.Application.Tournament.Commands.RatingCalculate;
using Tournament.Application.Tournament.Commands.SaveSchedule;
using Tournament.Application.Tournament.Commands.StartCompetition;
using Tournament.Application.Tournament.Queries.GetConfirmedMatchResults;
using Tournament.BackgroundServices;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Models.Participants;
using Tournament.Models.Tournament;

namespace Tournament.Controllers;

public class TournamentController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public TournamentController(ISender sender, 
        IMapper mapper, 
        ICompetitionService competitionService)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet("{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(PlayersVm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPlayers([FromRoute] Guid id)
    {
        var query = new GetCompetitionPlayersQuery()
        {
            CompetitionId = id
        };

        var result = await _sender.Send(query);

        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Errors.FirstOrDefault());
    }

    [HttpGet("competition/{id:guid}/match-results")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.AdminAndReferee)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetConfirmedMatchResults([FromRoute] Guid id)
    {
        var query = new GetConfirmedMatchResultQuery()
        {
            CompetitionId = id
        };

        var result = await _sender.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Errors);
    }

    [HttpPost("competition/{id:guid}/start-competition")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.AdminAndReferee)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> StartCompetition(Guid id, CancellationToken cancellationToken)
    {
        var command = new StartCompetitionCommand()
        {
            CompetitionId = id
        };

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok("Competition started successfully");
        }

        return BadRequest(result.Errors);
    }
    
    [HttpPost("competition/generate-schedule")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.AdminAndReferee)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GenerateSchedule([FromBody] CompetitionDto competitionDto)
    {
        var command = new GenerateScheduleCommand()
        {
            CompetitionId = competitionDto.CompetitionId
        };

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return StatusCode(StatusCodes.Status201Created);

        return NotFound(result.Errors.FirstOrDefault());
    }

    [HttpPost("competition/{id:guid}/save")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.AdminAndReferee)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SaveResult([FromRoute] Guid id, [FromBody] ConfirmedMatchResultLookup confirmedMatchResult)
    {
        var command = new SaveScheduleCommand()
        {
            CompetitionId = id,
            ConfirmedMatchResultLookup = confirmedMatchResult
        };

        var result = await _sender.Send(command);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Errors);
    }

    [HttpPost("competition/{id:guid}/rating-calculate")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.AdminAndReferee)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RatingCalculate([FromRoute] Guid id)
    {
        var command = new RatingCalculateCommand()
        {
            CompetitionId = id
        };

        var result = await _sender.Send(command);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        return NotFound(result.Errors);
    }


    [HttpDelete("delete")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete([FromBody] DeletePlayerDto deletePlayerDto)
    {
        var command = _mapper.Map<DeletePlayerCommand>(deletePlayerDto);

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return NoContent();

        return NotFound(result.Errors.FirstOrDefault());
    }
}