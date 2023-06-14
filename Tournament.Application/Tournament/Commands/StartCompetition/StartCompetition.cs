using Tournament.Application.Abstraction.Messaging;

namespace Tournament.Application.Tournament.Commands.StartCompetition;

public class StartCompetitionCommand : ICommand
{
    public Guid CompetitionId { get; set; }
}