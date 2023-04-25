using Ardalis.Result;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competition;

namespace Tournament.Application.Competitions.Commands.CreateCompetition;

public class CreateCompetitionInfoHandler : 
    ICommandHandler<CreateCompetitionCommand>
{
    private readonly ICompetitionDbContext _dbContext;

    public CreateCompetitionInfoHandler(ICompetitionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(CreateCompetitionCommand request, CancellationToken cancellationToken)
    {
        var competitionInfo = new Competition()
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            StartDateTime = request.StartDateTime,
            CreationDateTime = DateTime.UtcNow,
            PlaceDescription = request.PlaceDescription
        };

        await _dbContext.Competitions.AddAsync(competitionInfo, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}