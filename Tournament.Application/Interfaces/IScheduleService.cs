using Tournament.Domain.Models.Competitions;

namespace Tournament.Application.Interfaces;

public interface IScheduleService
{
    List<(Player, Player)> GenerateSchedule(List<Player> players);
}