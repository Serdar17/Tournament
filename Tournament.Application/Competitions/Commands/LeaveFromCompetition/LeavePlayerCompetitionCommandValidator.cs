using FluentValidation;

namespace Tournament.Application.Competitions.Commands.LeaveFromCompetition;

public class LeavePlayerCompetitionCommandValidator : AbstractValidator<LeavePlayerCompetitionCommand>
{
    public LeavePlayerCompetitionCommandValidator()
    {
        RuleFor(command => command.ParticipantId).NotEqual(Guid.Empty);
        
        RuleFor(command => command.CompetitionId).NotEqual(Guid.Empty);
    }
}