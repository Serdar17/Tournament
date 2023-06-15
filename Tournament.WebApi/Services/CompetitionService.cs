using Ardalis.Result;
using MediatR;
using Tournament.Application.Common.Exceptions;
using Tournament.Application.Interfaces;
using Tournament.Application.Tournament.Commands.GenerateSchedule;
using Tournament.Application.Tournament.Commands.RatingCalculate;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Repositories;

namespace Tournament.Services;

public sealed class CompetitionService : ICompetitionService
{
    private readonly ISender _sender;
    private readonly ICompetitionRepository _competitionRepository;
    public Guid CurrentCompetitionId { get; set; }

    public CompetitionService(ISender sender, 
        ICompetitionRepository competitionRepository)
    {
        _sender = sender;
        _competitionRepository = competitionRepository;
    }
    
    private async Task<Result> GenerateScheduleAsync()
    {
        var command = new GenerateScheduleCommand()
        {
            CompetitionId = CurrentCompetitionId
        };

        var result = await _sender.Send(command);

        return result.IsSuccess
            ? Result.Success()
            : Result.Error(result.Errors.FirstOrDefault());
    }

    private async Task RatingCalculateAsync()
    {
        var command = new RatingCalculateCommand()
        {
            CompetitionId = CurrentCompetitionId
        };

        await _sender.Send(command);
    }

    public async Task<Result> ExecuteAsync()
    {
        if (Guid.Empty.Equals(CurrentCompetitionId))
            return Result.Error("The current tournament does not exist");
        
        var competition = await _competitionRepository.GetCompetitionByIdAsync(CurrentCompetitionId);

        if (competition is null)
        {
            return Result.NotFound($"Entity \"{nameof(Competition)}\" ({CurrentCompetitionId}) was not found.");
        }

        var playedCount = competition.Schedules.Count(x => x.IsConfirmed);

        if (competition.Schedules.Count - playedCount >= competition.TableCount)
            Result.Error("The number of available pairs is more or equal than the number of tables");
            
        await RatingCalculateAsync();
        
        await GenerateScheduleAsync();

        return Result.Success();
    }
}