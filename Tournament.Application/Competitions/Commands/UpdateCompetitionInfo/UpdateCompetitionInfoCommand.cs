using MediatR;

namespace Tournament.Application.Competitions.Commands.UpdateCompetitionInfo;

public class UpdateCompetitionInfoCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }

    public DateTime StartDateTime { get; set; }
    
    public string PlaceDescription { get; set; }
}