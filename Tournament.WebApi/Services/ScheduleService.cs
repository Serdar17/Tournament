using Tournament.Application.Interfaces;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Services;

public class ScheduleService : IScheduleService
{
    public async Task<List<(Player, Player)>> GenerateSchedule(List<Player> players)
    {
        // var removedPlayers = new List<Player>();
        players = players.Count % 2 != 0 
            ? RemovePlayer(players) 
            : players.OrderByDescending(p => p.CurrentRating).ToList();
            
        var pairs = GeneratePairs(players);

        return pairs;
    }
    
    public List<Player> RemovePlayer(List<Player> players)
    {
        var gameCount = players.Select(p => p.PlayedGames.Count).Max();

        var player = players.FirstOrDefault(p => p.PlayedGames.Count != gameCount);

        if (player is null)
        {
            return players
                .OrderByDescending(p => p.CurrentRating)
                .Take(players.Count - 1)
                .ToList();
        }

        players = players
            .OrderByDescending(x => x.CurrentRating)
            .ToList();

        for (var i = players.Count - 1; i >= 0; i--)
        {
            if (players[i].PlayedGames.Count() == gameCount)
            {
                players.Remove(players[i]);
                break;
            }
        }

        return players;
    }

    public List<(Player, Player)> GeneratePairs(List<Player> players)
    {
        var result = new List<(Player, Player)>();
        var set = new HashSet<Player>(players);
            
        while (set.Count > 0)
        {
            var currentPlayer = set.First();
                
            foreach (var player in set)
            {
                if (currentPlayer.Id == player.Id || currentPlayer.PlayedGames.Contains(player.Id)) 
                    continue;
                result.Add((currentPlayer, player));
                set.Remove(currentPlayer);
                set.Remove(player);
                break;
            }
        }
            
        return result;
    }
}