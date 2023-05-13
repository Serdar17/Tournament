using FluentValidation;
using Tournament.Application.Dto.Competitions;

namespace Tournament.Application.Competitions.Commands.UpdateRefereePlayers;

public class RefereePlayerLookupValidator : AbstractValidator<RefereePlayerLookup>
{
    public RefereePlayerLookupValidator()
    {
        RuleFor(x => x.PlayerId).NotEqual(Guid.Empty);
        
        RuleFor(x => x.FirstName).NotEmpty();
        
        RuleFor(x => x.MiddleName).NotEmpty();
        
        RuleFor(x => x.LastName).NotEmpty();

        RuleFor(x => x.IsBlocked).NotEmpty();
        
        RuleFor(x => x.IsParticipation).NotEmpty();
        
        RuleFor(x => x.CurrentRating).NotEmpty();
    }
}