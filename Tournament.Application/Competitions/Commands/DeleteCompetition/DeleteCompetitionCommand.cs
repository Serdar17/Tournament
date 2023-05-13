using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto.Competitions.Create;

namespace Tournament.Application.Competitions.Commands.DeleteCompetition;

public class DeleteCompetitionCommand : ICommand<CompetitionListVm>
{
    public Guid Id { get; set; }
}