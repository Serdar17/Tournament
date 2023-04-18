using MediatR;

namespace Tournament.Application.Competitions.Commands.DeleteCompetitionInfo;

public class DeleteCompetitionInfoCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}