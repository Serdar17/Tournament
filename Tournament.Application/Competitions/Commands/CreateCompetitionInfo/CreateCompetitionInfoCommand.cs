using MediatR;

namespace Tournament.Application.Competitions.Commands.CreateCompetitionInfo;

public class CreateCompetitionInfoCommand : IRequest<Guid>
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public DateTime StartDateTime { get; set; }

    public string PlaceDescription { get; set; }
}