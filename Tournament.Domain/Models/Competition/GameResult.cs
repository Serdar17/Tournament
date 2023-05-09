using Tournament.Domain.Primitives;

namespace Tournament.Domain.Models.Competition;

public class GameResult : BaseEntity<Guid>
{
    public Guid WinnerPlayerId { get; set; }

    public Guid LoserPlayerId { get; set; }

    public int ScoreWinner { get; set; }

    public int ScoreLoser { get; set; }

    public DateTime StartGameDateTime { get; set; }

    public DateTime EndGameDateTime { get; set; }

}