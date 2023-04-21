using Tournament.Application.Abstraction.Messaging;

namespace Tournament.Application.Competitions.Commands.DeleteCompetitionInfo;

public class DeleteCompetitionInfoCommand : ICommand
{
    public Guid Id { get; set; }
}