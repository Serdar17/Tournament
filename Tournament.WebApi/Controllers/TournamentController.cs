using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Application.Features.Players.Commands.CreatePlayer;
using Tournament.Application.Features.Players.Commands.DeletePlayer;
using Tournament.Application.Features.Players.Queries.GetCompetitionPlayers;
using Tournament.Domain.Models.Participants;
using Tournament.Models.Tournament;

namespace Tournament.Controllers;

public class TournamentController : ApiController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public TournamentController(ISender sender, IMapper mapper)
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

    // [HttpPost("add")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    // [ProducesResponseType(StatusCodes.Status201Created)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<IActionResult> Add([FromBody] AddPlayerDto playerDto)
    // {
    //     var command = _mapper.Map<CreatePlayerCommand>(playerDto);
    //
    //     var result = await _sender.Send(command);
    //
    //     if (result.IsSuccess)
    //         return StatusCode(StatusCodes.Status201Created);
    //     
    //     return NotFound(result.Errors.FirstOrDefault());
    // }

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