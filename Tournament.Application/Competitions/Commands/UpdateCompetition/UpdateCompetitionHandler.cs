using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Common.Exceptions;
using Tournament.Application.Interfaces;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competition;

namespace Tournament.Application.Competitions.Commands.UpdateCompetition;

public class UpdateCompetitionHandler : ICommandHandler<UpdateCompetitionCommand>
{
    private readonly ICompetitionDbContext _dbContext;

    public UpdateCompetitionHandler(ICompetitionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result> Handle(UpdateCompetitionCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _dbContext.Competitions.FirstOrDefaultAsync(item =>
                item.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Competition), request.Id);
        }

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.StartDateTime = request.StartDateTime;
        entity.PlaceDescription = request.PlaceDescription;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}