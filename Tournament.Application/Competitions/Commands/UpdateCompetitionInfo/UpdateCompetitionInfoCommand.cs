using MediatR;
using Tournament.Application.Abstraction.Messaging;

namespace Tournament.Application.Competitions.Commands.UpdateCompetitionInfo;

public class UpdateCompetitionInfoCommand : ICommand
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public string Description { get; set; }

    public DateTime StartDateTime { get; set; }
    
    public string PlaceDescription { get; set; }
}