using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tournament.Application.Competitions.Commands.CreateCompetition;
using Tournament.Application.Competitions.Commands.DeleteCompetition;
using Tournament.Application.Competitions.Commands.JoinPlayerCompetition;
using Tournament.Application.Competitions.Commands.LeaveFromCompetition;
using Tournament.Application.Competitions.Commands.UpdateCompetition;
using Tournament.Application.Competitions.Commands.UpdateRefereePlayers;
using Tournament.Application.Competitions.Queries.GetCompetitionDetails;
using Tournament.Application.Competitions.Queries.GetCompetitionList;
using Tournament.Application.Competitions.Queries.GetJoinedPlayersById;
using Tournament.Application.Competitions.Queries.GetRefereePlayers;
using Tournament.Application.Competitions.Queries.GetScheduleByCompetitionId;
using Tournament.Application.Dto.Competitions;
using Tournament.Application.Dto.Competitions.Create;
using Tournament.Application.Dto.Competitions.Join;
using Tournament.Domain.Models.Participants;
using Tournament.Models.Competition;
using Tournament.Models.Tournament;

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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(CompetitionVm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetCompetitionDetailsQuery()
        {
            Id = id
        };

        var result = await _sender.Send(query);

        if (result.IsSuccess)
            return Ok(result);

        return NotFound(result.Errors.FirstOrDefault());
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(CompetitionListVm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetCompetitionInfoListQuery();

        var result = await _sender.Send(query);

        return Ok(result);
    }
    
    /// <summary>
    /// WORKING BETA
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet("{id:guid}/schedule")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSchedule([FromRoute] Guid id)
    {
        var query = new GetScheduleByCompetitionIdQuery()
        {
            CompetitionId = id
        };

        var result = await _sender.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }
    
    /// <summary>
    /// WORKING !!!
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet("{id:guid}/players")]
    [ProducesResponseType(typeof(JoinedPlayerList), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPlayers([FromRoute] Guid id)
    {
        var query = new GetJoinedPlayersByIdQuery()
        {
            CompetitionId = id
        };

        var result = await _sender.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Errors.FirstOrDefault());
    }
    
    [HttpGet("players/{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.AdminAndReferee)]
    [ProducesResponseType(typeof(RefereePlayerList), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPlayersForReferee([FromRoute] Guid id)
    {
        var query = new GetRefereePlayersListQuery() { CompetitionId = id };

        var result = await _sender.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return StatusCode(StatusCodes.Status404NotFound, result.Errors.FirstOrDefault());
    }
    
    
    [HttpPost("create")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.AdminAndReferee)]
    [ProducesResponseType(typeof(CompetitionListVm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCompetitionDto createCompetitionDto)
    {
        var command = _mapper.Map<CreateCompetitionCommand>(createCompetitionDto);

        var result = await _sender.Send(command);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        
        return BadRequest(result.Errors);
    }

    [HttpPost("join")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(UserWithCompetitions), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Join([FromBody] AddPlayerDto playerDto)
    {
        var command = _mapper.Map<JoinPlayerCompetitionCommand>(playerDto);

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Ok(result.Value);
        
        return NotFound(result.Errors.FirstOrDefault());
    }

    [HttpPost("leave")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(UserWithCompetitions), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Leave([FromBody] LeavePlayerDto leavePlayerDto)
    {
        var command = _mapper.Map<LeavePlayerCompetitionCommand>(leavePlayerDto);
        
        var result = await _sender.Send(command);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Errors.FirstOrDefault());
    }


    [HttpPut("update")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.AdminAndReferee)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdateCompetitionDto updateCompetitionDto)
    {
        var command = _mapper.Map<UpdateCompetitionCommand>(updateCompetitionDto);

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return NoContent();

        return NotFound(result.Errors.FirstOrDefault());
    }

    [HttpPut("players/{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.AdminAndReferee)]
    [ProducesResponseType(typeof(RefereePlayerList), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePLayers([FromRoute] Guid id, [FromBody] IList<RefereePlayerLookup> playerList)
    {
        var command = new UpdateRefereePlayersCommand()
        {
            CompetitionId = id,
            Players = playerList
        };

        var result = await _sender.Send(command);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Errors.FirstOrDefault());
    }

    [HttpDelete("delete/{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ParticipantRole.AdminAndReferee)]
    [ProducesResponseType(typeof(CompetitionListVm), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteCompetitionCommand()
        {
            Id = id
        };

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Ok(result.Value);

        return NotFound(result.Errors.FirstOrDefault());
    }
}