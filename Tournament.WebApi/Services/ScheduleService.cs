using Tournament.Application.Interfaces;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Services;

public class ScheduleService : IScheduleService
{
    public List<(Player, Player)> GenerateSchedule(List<Player> players)
    {
        players = players.Count % 2 != 0 
            ? RemovePlayer(players) 
            : players.OrderByDescending(p => p.CurrentRating).ToList();
            
        var pairs = GeneratePairs(players);

        return pairs;
    }
    
    private List<Player> RemovePlayer(List<Player> players)
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

    private List<(Player, Player)> GeneratePairs(List<Player> players)
    {
        // var result = new List<(Player, Player)>();
        // var set = new HashSet<Player>(players);
        //     
        // while (set.Count > 0)
        // {
        //     var currentPlayer = set.First();
        //         
        //     foreach (var player in set)
        //     {
        //         if (currentPlayer.Id == player.Id || currentPlayer.PlayedGames.Contains(player.Id)) 
        //             continue;
        //         result.Add((currentPlayer, player));
        //         set.Remove(currentPlayer);
        //         set.Remove(player);
        //         break;
        //     }
        // }
        //     
        // return result;
        
        var pairs = new List<(Player, Player)>();
            
        while (true)
        {
            pairs = GeneratePairs(players, new HashSet<Player>(players));
                
            if (pairs.Count * 2 == players.Count)
            {
                break;
            }
                
            pairs = GeneratePairsWithRandom(players, new HashSet<Player>(players), new Random());
                
            if (pairs.Count * 2 == players.Count)
            {
                break;
            }
        }
            
        return pairs;
    }

    private static List<(Player, Player)> GeneratePairs(List<Player> players, HashSet<Player> set)
    {
        var result = new List<(Player, Player)>();
            
        for (var i = 0; i < players.Count / 2; i++)
        {
            var currentPlayer = set.First();
                
            foreach (var player in set)
            {
                if (currentPlayer.Id != player.Id
                    && !currentPlayer.PlayedGames.Contains(player.Id))
                {
                    result.Add((currentPlayer, player));
                    set.Remove(currentPlayer);
                    set.Remove(player);
                    break;
                }
            }
        }
            
        return result;
    }
    
    private static List<(Player, Player)> GeneratePairsWithRandom(List<Player> players, HashSet<Player> set,
        Random random)
    {
        var result = new List<(Player, Player)>();

        for (var i = 0; i < players.Count / 2; i++)
        {
            var currentPlayer = set.First();

            var availablePlayers =
                set
                    .Where(x => x.Id != currentPlayer.Id && !currentPlayer.PlayedGames.Contains(x.Id))
                    .ToList();
                
            while (true)
            {
                var a = random.Next(availablePlayers.Count);
                if (availablePlayers.Contains(players.FirstOrDefault(x => x.Id == availablePlayers[a].Id)))
                {
                    result.Add((currentPlayer, players[a]));
                    set.Remove(currentPlayer);
                    set.Remove(players[a]);
                    break;
                }
            }
        }

        return result;
    }
}