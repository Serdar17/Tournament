using Tournament.Domain.Models.Competitions;

namespace Tournament.Application.Solver;

public class RatingCalculator
{
    private readonly List<Player> _players;
    private readonly List<Schedule> _schedules;

    public RatingCalculator(List<Player> players, List<Schedule> schedules)
    {
        _players = players;
        _schedules = schedules;
    }
     
    public List<Player> CalculateRating()
    {
        var matrix = GenerateMatrixByPlayers();
        var freeMembers = GenerateFreeMembersBySchedule();
        var solver = new Application.Solver.Solver();
        var result = solver.Solve(matrix, freeMembers);

        for (var i = 0; i < _players.Count; i++)
        {
            _players[i].CurrentRating = Convert.ToInt32(Math.Round(result[i]));
        }
        
        return _players;
    }

    public double[][] GenerateMatrixByPlayers()
    {
        var matrix = new double[_players.Count + 1][];

        for (var i = 0; i < _players.Count; i++)
        {
            matrix[i] = new double[_players.Count];

            var schedules = _schedules
                .Where(x => x.FirstPlayerId == _players[i].Id || x.SecondPlayerId == _players[i].Id)
                .ToList();

            var index = 0;
            for (var j = 0; j < _players.Count; j++)
            {
                if (i == j)
                {
                    matrix[i][j] = 1.0;
                }
                else
                {
                    var schedule = _schedules
                        .FirstOrDefault(x => x.FirstPlayerId == _players[i].Id && x.SecondPlayerId == _players[j].Id ||
                                             x.FirstPlayerId == _players[j].Id && x.SecondPlayerId == _players[i].Id);

                    if (schedule is null || (schedule.FirstPlayerScore == 0 && schedule.SecondPlayerScore == 0))
                    {
                        matrix[i][j] = -2.0 / (_players[i].Total + (_players.Count - schedules.Count - 1) * 2);
                    }
                    else
                    {
                        matrix[i][j] = -1.0 * (schedule.FirstPlayerScore + schedule.SecondPlayerScore) /
                                       (_players[i].Total + (_players.Count - schedules.Count - 1) * 2);
                    }
                }
            }
        } 

        matrix[_players.Count] = new double[_players.Count];

        for (var i = 0; i < _players.Count; i++)
        {
            matrix[_players.Count][i] = 1.0;
        }

        return matrix;
    }

    public double[] GenerateFreeMembersBySchedule()
    {
        var freeMembers = new double[_players.Count + 1];
        for (var i = 0; i < _players.Count; i++)
        {
            var schedulesCount = _schedules
                .Count(x => x.FirstPlayerId == _players[i].Id || x.SecondPlayerId == _players[i].Id);

            var ratio = 1000.0 * (_players[i].Scored - _players[i].Missed) /
                        (_players[i].Total + (_players.Count - schedulesCount - 1) * 2);
            freeMembers[i] = ratio;
        }

        freeMembers[_players.Count] = 2200.0 * _players.Count;
        return freeMembers;
    }

}