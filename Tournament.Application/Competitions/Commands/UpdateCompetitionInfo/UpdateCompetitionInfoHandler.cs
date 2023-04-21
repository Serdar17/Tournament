using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Common.Exceptions;
using Tournament.Application.Interfaces;
using Tournament.Domain.Models.Competition;

namespace Tournament.Application.Competitions.Commands.UpdateCompetitionInfo;

public class UpdateCompetitionInfoHandler : ICommandHandler<UpdateCompetitionInfoCommand>
{
    private readonly ICompetitionDbContext _dbContext;

    public UpdateCompetitionInfoHandler(ICompetitionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result> Handle(UpdateCompetitionInfoCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _dbContext.CompetitionInfos.FirstOrDefaultAsync(item =>
                item.Id == request.Id, cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(CompetitionInfo), request.Id);
        }

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.StartDateTime = request.StartDateTime;
        entity.PlaceDescription = request.PlaceDescription;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}