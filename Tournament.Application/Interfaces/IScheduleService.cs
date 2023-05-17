using Ardalis.Result;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Application.Interfaces;

public interface IScheduleService
{
    Task<List<(Player, Player)>> GenerateSchedule(List<Player> players);
}