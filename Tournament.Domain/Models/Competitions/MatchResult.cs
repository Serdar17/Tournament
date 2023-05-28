using Tournament.Domain.Primitives;

namespace Tournament.Domain.Models.Competitions;

public class MatchResult : BaseEntity<Guid>
{
    public Guid FirstPlayerId { get; set; }

    public Guid SecondPlayerId { get; set; }

    public Score? FirstPlayerScore { get; set; }

    public Score? SecondPlayerScore { get; set; }
    
    public int TableNumber { get; set; }
    
    public int RoundsNumber { get; set; }

    public DateTime StartGameDateTime { get; set; }

    public DateTime EndGameDateTime { get; set; }
    
    public Guid? CompetitionId { get; set; }
    
    public Competition? Competition { get; set; }
}