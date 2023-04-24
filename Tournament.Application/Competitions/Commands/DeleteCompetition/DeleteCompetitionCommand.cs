using Tournament.Application.Abstraction.Messaging;

namespace Tournament.Application.Competitions.Commands.DeleteCompetition;

public class DeleteCompetitionCommand : ICommand
{
    public Guid Id { get; set; }
}