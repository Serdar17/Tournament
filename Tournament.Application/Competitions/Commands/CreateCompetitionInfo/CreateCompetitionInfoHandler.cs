using MediatR;
using Tournament.Application.Interfaces;
using Tournament.Domain.Models.Competition;

namespace Tournament.Application.Competitions.Commands.CreateCompetitionInfo;

public class CreateCompetitionInfoHandler : 
    IRequestHandler<CreateCompetitionInfoCommand, Guid>
{
    private readonly ICompetitionDbContext _dbContext;

    public CreateCompetitionInfoHandler(ICompetitionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateCompetitionInfoCommand request, CancellationToken cancellationToken)
    {
        var competitionInfo = new CompetitionInfo()
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            StartDateTime = request.StartDateTime,
            CreationDateTime = DateTime.UtcNow,
            PlaceDescription = request.PlaceDescription
        };

        await _dbContext.CompetitionInfos.AddAsync(competitionInfo, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return competitionInfo.Id;
    }
}