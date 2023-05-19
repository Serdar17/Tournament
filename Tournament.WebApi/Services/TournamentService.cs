using Tournament.Application.Interfaces;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Services;

public class TournamentService : ITournamentService
{
    public int AvailableTableCount { get; set; }
    public int RoundNumber { get; set; }
    
    private readonly IScheduleService _scheduleService;
    private readonly Queue<(Player, Player)> _queue;

    public TournamentService(IScheduleService scheduleService, Queue<(Player, Player)> queue)
    {
        _scheduleService = scheduleService;
        _queue = queue;
    }
    
    public List<(Player, Player)> GetAvailablePairs(List<Player> players, Competition competition)
    {
        throw new NotImplementedException();
    }
}