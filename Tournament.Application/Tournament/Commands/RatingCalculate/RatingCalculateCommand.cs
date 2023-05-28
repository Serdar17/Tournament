using Tournament.Application.Abstraction.Messaging;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Application.Tournament.Commands.RatingCalculate;

public class RatingCalculateCommand : ICommand
{
    public Guid CompetitionId { get; set; }
}