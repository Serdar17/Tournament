using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto.Competitions.Create;

namespace Tournament.Application.Competitions.Commands.CreateCompetition;

public class CreateCompetitionCommand : ICommand<CompetitionListVm>
{
    public Guid Id { get; set; }

    public string Title { get; set; }
    
    public string Description { get; set; }
    
    public int TableCount { get; set; }
    
    public int RoundsCount { get; set; }
    
    public DateTime StartDateTime { get; set; }

    public string PlaceDescription { get; set; }
}