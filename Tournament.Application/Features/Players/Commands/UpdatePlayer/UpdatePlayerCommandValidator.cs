using FluentValidation;

namespace Tournament.Application.Features.Players.Commands.UpdatePlayer;

public class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
{
    public UpdatePlayerCommandValidator()
    {
        RuleFor(command => command.ParticipantId).NotEqual(Guid.Empty);
        
        RuleFor(command => command.CompetitionId).NotEqual(Guid.Empty);
        
        RuleFor(command => command.PlayerId).NotEqual(Guid.Empty);

        RuleFor(command => command.Missed).NotNull();

        RuleFor(command => command.Scored).NotNull();

        RuleFor(command => command.CurrentRating).NotNull();

        RuleFor(command => command.IsParticipation).NotNull();
    }
}