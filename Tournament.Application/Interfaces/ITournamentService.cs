using Tournament.Domain.Models.Competitions;

namespace Tournament.Application.Interfaces;

public interface ITournamentService
{
    List<Player> RatingCalculate(List<Player> players);
}