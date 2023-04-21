using Ardalis.Result;
using MediatR;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Common.Exceptions;
using Tournament.Application.Interfaces;
using Tournament.Domain.Models.Competition;

namespace Tournament.Application.Competitions.Commands.DeleteCompetitionInfo;

public class DeleteCompetitionInfoHandler : ICommandHandler<DeleteCompetitionInfoCommand>
{
    private readonly ICompetitionDbContext _dbContext;

    public DeleteCompetitionInfoHandler(ICompetitionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(DeleteCompetitionInfoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.CompetitionInfos.FindAsync(new object[] {request.Id},
            cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(CompetitionInfo), request.Id);
        }

        _dbContext.CompetitionInfos.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}