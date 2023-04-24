using Ardalis.Result;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Common.Exceptions;
using Tournament.Application.Interfaces;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competition;

namespace Tournament.Application.Competitions.Commands.DeleteCompetition;

public class DeleteCompetitionHandler : ICommandHandler<DeleteCompetitionCommand>
{
    private readonly ICompetitionDbContext _dbContext;

    public DeleteCompetitionHandler(ICompetitionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(DeleteCompetitionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Competitions.FindAsync(new object[] {request.Id},
            cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException(nameof(Competition), request.Id);
        }

        _dbContext.Competitions.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}