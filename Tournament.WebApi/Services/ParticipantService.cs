using Ardalis.Result;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tournament.Application.Dto;
using Tournament.Application.Dto.Account;
using Tournament.Application.Interfaces;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Models.Participants;
using Tournament.Domain.Repositories;

namespace Tournament.Services;

public sealed class ParticipantService : IParticipantService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly ICompetitionRepository _competitionRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IMatchResultRepository _matchResultRepository;
    private readonly IScheduleRepository _scheduleRepository;
    
    public ParticipantService(UserManager<ApplicationUser> userManager, IMapper mapper,
        IMatchResultRepository matchResultRepository, 
        IPlayerRepository playerRepository, 
        ICompetitionRepository competitionRepository, IScheduleRepository scheduleRepository)
    {
        _userManager = userManager;
        _mapper = mapper;
        _matchResultRepository = matchResultRepository;
        _playerRepository = playerRepository;
        _competitionRepository = competitionRepository;
        _scheduleRepository = scheduleRepository;
    }
    
    public List<ApplicationUser> GetAll()
    {
        return _userManager.Users.ToList();
    }

    public async Task<Result<ParticipantInfoModel>> GetParticipantByIdAsync(Guid guid)
    {
        var participant = await _userManager.Users
            .Where(x => x.Id == guid.ToString())
            .Include(x => x.Player)
            .ThenInclude(p => p.Competition)
            .FirstOrDefaultAsync();

        if (participant is null)
        {
            return Result.Error($"Participant with id: {guid.ToString()} doesn't exist in the database.");
        }

        var resultModel = _mapper.Map<ParticipantInfoModel>(participant);
        
        if (participant.Player?.Competition is null)
        {
            return resultModel;
        }

        var competition = await _competitionRepository.GetCompetitionByIdAsync(participant.Player.CompetitionId);

        if (competition is null)
        {
            Log.Information("Entity \"{Name}\" {@CompetitionId} was not found",
                nameof(Competition), participant.Player.CompetitionId);
            
            return Result.NotFound($"Entity \"{nameof(Competition)}\" ({participant.Player.CompetitionId}) was not found.");
        }
        
        var schedules = competition.Schedules
            .Where(x => (x.FirstPlayerId == participant.Player.Id || x.SecondPlayerId == participant.Player.Id) &&
                        !x.IsConfirmed)
            .ToList();

        foreach (var schedule in schedules)
        {
            if (schedule.FirstPlayerScored != null && schedule.FirstPlayerId == participant.Player.Id && schedule.FirstPlayerScored.IsConfirmed)
                continue;
            
            if (schedule.SecondPlayerScored != null && schedule.SecondPlayerId == participant.Player.Id && schedule.SecondPlayerScored.IsConfirmed)
                continue;
            
            var firstPlayer = await _playerRepository.GetPlayerByIdAsync(schedule.FirstPlayerId);
            var secondPlayer = await _playerRepository.GetPlayerByIdAsync(schedule.SecondPlayerId);

            var model = new MatchResultModel()
            {
                FirstPlayerId = schedule.FirstPlayerId,
                FirstPlayerDto = _mapper.Map<PlayerModel>(firstPlayer),
                SecondPlayerId = schedule.SecondPlayerId,
                SecondPlayerDto = _mapper.Map<PlayerModel>(secondPlayer),
                Score = new Score(0, 0, false),
            };

            model.FirstPlayerDto.CurrentRating = firstPlayer.CurrentRating;
            model.SecondPlayerDto.CurrentRating = secondPlayer.CurrentRating;
            
            resultModel.MatchResultModels.Add(model);
        }
        
        return resultModel;
    }

    public async Task<Result> ConfirmMatchResult(MatchResultModel matchResult, Guid id)
    {
        var participant = await _userManager.Users
            .Where(x => x.Id == id.ToString())
            .Include(x => x.Player)
            .ThenInclude(p => p.Competition)
            .ThenInclude(c => c.Schedules)
            .FirstOrDefaultAsync();
        
        if (participant is null)
        {
            return Result.Error($"Participant with id: {id.ToString()} doesn't exist in the database.");
        }

        // var result =
        //     _matchResultRepository.GetMatchResultByPlayersId(matchResult.FirstPlayerId, matchResult.SecondPlayerId);

        var schedule = participant.Player.Competition.Schedules
            .FirstOrDefault(x => x.FirstPlayerId == matchResult.FirstPlayerId && x.SecondPlayerId == matchResult.SecondPlayerId || 
                                 x.FirstPlayerId == matchResult.SecondPlayerId && x.SecondPlayerId == matchResult.FirstPlayerId);

        if (schedule.FirstPlayerId == participant.Player.Id)
        {
            schedule.FirstPlayerScored = new Score(matchResult.Score.FirstPlayerScored,
                matchResult.Score.SecondPlayerScored, true);
            
            _scheduleRepository.Update(schedule);
        }

        if (schedule.SecondPlayerId == participant.Player.Id)
        {
            schedule.SecondPlayerScored = new Score(matchResult.Score.FirstPlayerScored,
                matchResult.Score.SecondPlayerScored, true);
            
            _scheduleRepository.Update(schedule);
        }
        
        await _scheduleRepository.SaveAsync();
        //
        // if (result is null)
        // {
        //     var newMatchResult = new MatchResult()
        //     {
        //         FirstPlayerId = participant.Player.Id,
        //         FirstPlayerScore = new Score(matchResult.Score.Scored, matchResult.Score.SecondPlayerScored, true),
        //         CompetitionId = participant.Player.CompetitionId,
        //         Competition = participant.Player.Competition
        //     };
        //
        //     await _matchResultRepository.AddAsync(newMatchResult);
        //     
        //     return Result.Success();
        // }
        //
        // result.SecondPlayerId = participant.Player.Id;
        // result.SecondPlayerScore = new Score(matchResult.Score.Scored, matchResult.Score.SecondPlayerScored, true);
        //
        // await _matchResultRepository.UpdateAsync(result);

        return Result.Success();
    }


    public async Task<Result<ParticipantInfoModel>> PatchParticipantAsync(Guid guid, 
        JsonPatchDocument<ParticipantInfoModel> patch)
    {
        var participant = await _userManager.FindByIdAsync(guid.ToString());

        if (participant is null)
        {
            return Result.Error($"Participant with id: {guid.ToString()} doesn't exist in the database.");
        }
        
        var participantPatch = _mapper.Map<ParticipantInfoModel>(participant);
        
        patch.ApplyTo(participantPatch);

        _mapper.Map(participantPatch, participant);
        
        participant.SetRating();

        var result = await _userManager.UpdateAsync(participant);

        if (!result.Succeeded)
        {
            return Result.Error($"Participant with id: {guid.ToString()} could not be updated");
        }

        return Result.Success(participantPatch);
    }

    public async Task<Result<UserDto>> UpdateParticipant(UserDto user)
    {
        var participant = await _userManager.FindByIdAsync(user.Id);

        if (participant is null)
        {
            return Result.Error($"Participant with id: {user.Id} doesn't exist in the database.");
        }
        
        _mapper.Map(user, participant);
        
        participant.SetRating();

        var result = await _userManager.UpdateAsync(participant);

        if (!result.Succeeded)
        {
            return Result.Error($"Participant with id: {user.Id} could not be updated");
        }

        return Result.Success(user);
    }

    public async Task<Result> DeleteParticipantByIdAsync(Guid guid)
    {
        var participant = await _userManager.FindByIdAsync(guid.ToString());

        if (participant is null)
        {
            return Result.Error($"Participant with id: {guid.ToString()} doesn't exist in the database.");
        }

        var result = await _userManager.DeleteAsync(participant);

        if (!result.Succeeded)
        {
            return Result.Error($"Participant with id: {guid.ToString()} could not be deleted");
        }

        return Result.Success();
    }
}