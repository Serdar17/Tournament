using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Application.Competitions.Commands.UpdateCompetition;

public class UpdateCompetitionHandler : ICommandHandler<UpdateCompetitionCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateCompetitionHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result> Handle(UpdateCompetitionCommand request, CancellationToken cancellationToken)
    {
        var competition =
            await _dbContext.Competitions.FirstOrDefaultAsync(item =>
                item.Id == request.Id, cancellationToken);

        if (competition is null)
        {
            Log.Information("Entity \"{Name}\" {@CompetitionId} was not found",
                nameof(Competition), request.Id);
            
            return Result.NotFound($"Entity \"{nameof(Competition)}\" ({request.Id}) was not found.");
        }
        
        competition.UpdateFields(request.Title, request.Description, request.StartDateTime, 
            request.PlaceDescription, request.TableCount, request.RoundsCount);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}